using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Exam.DBTableAdapters;
using Exam.Properties;
using Rsx;
using W = Microsoft.Office.Interop.Word;

namespace Exam
{
    public partial class ExamFrm
    {
        #region FIELDS

        private bool working = false;

        //      private  IList<W.Application> WApp = null;

        private static string WordTemplateFile = "WordDoc";

        private static string model = "Model-";

        private static string jpgExt = ".jpg";
        private static string pdfExt = ".pdf";
        private static string WordExt = ".docx";
        protected static string path = Application.StartupPath.ToString();
        private static string templateFile = path + slash + WordTemplateFile + WordExt;
        private static string idFile = path + slash + "identification" + jpgExt;
        private static string logoFile = path + slash + "logo" + jpgExt;

        //FOR WORD DOCUMENT GENERATION INTEROP
        private static object nullObj = System.Reflection.Missing.Value;

        private static object roObj = true;

        #endregion FIELDS

        /// <summary>
        /// GEnerates a virtual database
        /// </summary>
        private void GenerateQuestions(int questions)
        {
            int j = 1;

            for (int i = 0; i < questions; i++)
            {
                if (j == 6) j = 1;
                DB.QuestionsRow q = this.dB.Questions.NewQuestionsRow();
                q.Weight = j;
                this.dB.Questions.AddQuestionsRow(q);
                DB.TAM.QuestionsTableAdapter.Update(q);

                q.Question = "Pregunta número " + q.QID.ToString();
               
                int m = 1;
                while (m < 6)
                {
                    DB.AnswersRow r = this.dB.Answers.NewAnswersRow();
                    r.Answer = "Respuesta " + q.QID.ToString() + "-" + m.ToString();
                    r.QID = q.QID;
                    if (m == 5) r.Correct = true;
                    this.dB.Answers.AddAnswersRow(r);
                    DB.TAM.AnswersTableAdapter.Update(r);
                    m++;
                }
               
                j++;
            }

            DB.TAM.QuestionsTableAdapter.Update(this.dB.Questions);
            DB.TAM.AnswersTableAdapter.Update(this.dB.Answers);


        }

        private void DeleteExams()
        {
            QTA qta = new QTA();
            qta.Clean();
            qta.Dispose();
            IEnumerable<string> files = System.IO.Directory.GetFiles(ExasmPath);
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch (Exception ex)
                {
                    this.statuslbl.Text = Properties.Resources.NoBorrado + file;
                }
            }
        }

        private void DoExams()
        {
            DataRow row = (this.preferencesBS.Current as DataRowView).Row;

            DB.PreferencesRow p = row as DB.PreferencesRow;
            p.Done = false;

            Dumb.CloneARow(this.dB.Preferences, p);
           
            this.dB.Questions.Clear();
            this.dB.Answers.Clear();

           

            p.Done = true;
            p.DateTime = DateTime.Now;

            this.logBS.MoveFirst();
            Application.DoEvents();

            working = true;

            this.pBar.Minimum = 0;
            this.pBar.Value = 0;
            this.pBar.Step = 1;
            this.pBar.Maximum = p.Models*4;

            int mod = 0; //models

            for (mod = 0; mod < p.Models; mod++)
            {
                this.dB.Exams.Clear();
                this.dB.Order.Clear();

                this.pBar.PerformStep(); //1
               
            
                DB.ExamsListRow ls = DoOneExam(ref p);
              
                this.pBar.PerformStep(); //4
                Application.DoEvents();

                int sumPoint = ls.GetExamsRows().Sum(o => o.QuestionsRow.Weight);
                double factor = Convert.ToDouble(p.Points);
                factor /= Convert.ToDouble(sumPoint);
                p.Factor = factor;
             
              
                IList<string[]> questionAnswer;
                Application.DoEvents();

                DoOneEncriptExam(ref p, ref ls, out questionAnswer);
                this.pBar.PerformStep(); //2

                Application.DoEvents();

                DoOneDocExam(ref p, ref ls, ref questionAnswer);

                this.pBar.PerformStep(); //3
                Application.DoEvents();

            }

            MakeTableBytes(ref p);
            Application.DoEvents();

            DB.TAM.PreferencesTableAdapter.Update(this.dB.Preferences);

            this.dB.Order.Clear();
            this.dB.Exams.Clear();

            Application.DoEvents();

            working = false;
        }

        /// <summary>
        /// MAIN FILE FOR GENERATING AN EXAM
        /// </summary>
        /// <param name="p"></param>
        private DB.ExamsListRow DoOneExam(ref DB.PreferencesRow p)
        {
            //    Microsoft.Office.Interop.Word.Application d = new Microsoft.Office.Interop.Word.Application();

            String clase = p.Class;

            ChangeClassConnection(clase);



            this.statuslbl.Text = Resources.Creando + this.dB.Questions.Count + Resources.PregAleatorias;
            ///RANDOMNIZE RAW QUESTIONS
            IEnumerable<DB.QuestionsRow> questions = this.dB.Questions; //toma todas las preguntas

            questions = Tools.RandomizeStrings(questions); //1
            Application.DoEvents();
            questions = Tools.RandomizeStrings(questions); //2
            Application.DoEvents();
            questions = Tools.RandomizeStrings(questions); //3 times random

            IList<DB.ExamsRow> exams = new List<DB.ExamsRow>(); //todas las preguntas randomnizadas seran guardadas primero en esta lista
            foreach (DB.QuestionsRow q in questions)
            {
                DB.ExamsRow er = this.dB.Exams.NewExamsRow(); //crea rows pero no los metas en la tabla
                er.QID = q.QID; //dales el id de la pregunta
                exams.Add(er); //agregalos a la lista
            }

            this.statuslbl.Text = Resources.Filtrando;
            IEnumerable<DB.ExamsRow> join = WeightThem(ref p, ref exams); //not re-ordered // preguntas seleccionadas y filtradas
            Application.DoEvents();

            this.statuslbl.Text = Resources.OrderAleatorio; // de las preguntas del examen ya pesadas (filtradaS)
            join = Tools.RandomizeStrings(join); //1
            Application.DoEvents();
            join = Tools.RandomizeStrings(join); //2
            Application.DoEvents();
            join = Tools.RandomizeStrings(join); //2

            this.statuslbl.Text = Resources.Seleccionando + join.Count() + Resources.PregAleatorias;
            Application.DoEvents();

            foreach (DB.ExamsRow ex in join)
            {
                this.dB.Exams.AddExamsRow(ex); //agregalas a la tabla para salvarlas

                IEnumerable<DB.AnswersRow> answ = ex.QuestionsRow.GetAnswersRows();
                ///RANDOMIZE RAW ASWERS
                answ = Tools.RandomizeStrings<DB.AnswersRow>(answ); //1
                answ = Tools.RandomizeStrings<DB.AnswersRow>(answ);//2
                answ = Tools.RandomizeStrings<DB.AnswersRow>(answ); //3

                //CREATE EXAM QUESTION CODE! AIDSTRING
                string code = CreateExamQuestionCode(ex.QID, ref answ);
                ex.AIDString = code;

                DB.ExamsRow auxiliar = ex;
                MakeTableBytes(ref auxiliar);
            }
            DB.TAM.ExamsTableAdapter.Update(this.dB.Exams);
            Application.DoEvents();

            //    WApp.Add(w);
            Guid g = Guid.NewGuid();
            DB.ExamsListRow ls = this.dB.ExamsList.NewExamsListRow();
            this.dB.ExamsList.AddExamsListRow(ls);
            ls.PID = p.PID;
            ////MAKES THE DOC FILE
            ls.GUID = g.ToString().Replace("-", null);//.Split('-')[4];
            ls.Time = DateTime.Now;
            ls.Class = p.Class;
            //  Image img = Tools.CreateQRCode(ls.GUID, qrSizeDB);
            //   ls.QRCode = Tools.imageToByteArray(img);

            DB.TAM.ExamsListTableAdapter.Update(this.dB.ExamsList);
            Application.DoEvents();
            //este orden es importante

            foreach (DB.ExamsRow re in join) re.EID = ls.EID; //ahora asocia las preguntas a cada examen generado
            DB.TAM.ExamsTableAdapter.Update(this.dB.Exams);
            Application.DoEvents();

            return ls;
        }

     

        private void DoOneEncriptExam(ref DB.PreferencesRow p, ref DB.ExamsListRow ls, out      IList<string[]> questionAnswer)
        {
            ////ENCRIPTAMIENTOOOOOOO
            this.statuslbl.Text = "Procesando...";
       
            
            IEnumerable<DB.ExamsRow> join = ls.GetExamsRows();
            string[] ClaveWQID = null;

            ClaveWQID = MakeQAs(ref join, ref p, out questionAnswer);
            ls.LQuestion = ClaveWQID[1]; // question weight string
            string LAnswer = ClaveWQID[0]; //clave verdadera sin encriptar
            string QIDString = ClaveWQID[2]; //secuencia de preguntas, importante guardar

            ls.CLAnswer = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(LAnswer, password, null); //encripta
            ls.CQIDString = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(QIDString, password, null); //encripta

            //SAVE COPY OF TABLE in EXAMLIST
            MakeTableBytes(ref ls);
            DB.TAM.ExamsListTableAdapter.Update(this.dB.ExamsList);
            Application.DoEvents();
        }

        private void DoOneDocExam(ref DB.PreferencesRow p, ref DB.ExamsListRow ls, ref IList<string[]> questionAnswer)
        {
            ///MAKE WORD DOCUMENT
            this.statuslbl.Text = Resources.Creando + "el examen " + ls.GUID;
            string CryptoGUID = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(ls.GUID, password, null);

            string filepathAux = ExasmPath + model + ls.GUID;
            string jpgQRCodeFile = filepathAux + jpgExt;
            Image img = Tools.CreateQRCode(CryptoGUID, qrSise);
            img.Save(jpgQRCodeFile);

            string destFile = filepathAux + WordExt;
            string filePDF = filepathAux + pdfExt;
            string filejpg = filepathAux + jpgExt;
            System.IO.File.Copy(templateFile, destFile);

            W.Application w = new W.Application();
            object dest = destFile as object;

            W.Document doc = w.Documents.Open(ref dest, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref roObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj);

           // doc._CodeName = p.Title;
            string Intro = "Recorte el Cupón";
            //     W.Document doc = ExamMain.MakeWord(ref destFile, ref w); //makes the word file
            MakeExamIntro(ref doc, jpgQRCodeFile, Intro, p.Title +" ("+ p.Class +")" );
            MakeExamCoupon(ref  doc, jpgQRCodeFile, ref ls, p.showAnswer);

            MakeExamFileBody(ref questionAnswer, ref doc);

       
            this.statuslbl.Text = Resources.ExamenCreado;
            Application.DoEvents();
            //save word
            doc.Save();
            ///MAKE PDF
            Tools.MakePDF(ref doc);
            Application.DoEvents();
            w.Documents.Close(ref roObj, ref nullObj, nullObj);
            //    Application.DoEvents();
            w.Quit(ref nullObj, ref nullObj, ref nullObj);

            this.statuslbl.Text = Resources.ExamenCreadoPDF;
            System.IO.File.Delete(destFile);
            System.IO.File.Delete(filejpg);


        
            Byte[] rtf = Dumb.ReadFileBytes(filePDF);
            ls.ExamFile = rtf; //salva una copia del archivo PDF en el servidor SQL
            DB.TAM.ExamsListTableAdapter.Update(ls);
            System.IO.File.Delete(filePDF);

            this.statuslbl.Text = "Examen generado";
            Application.DoEvents();
         

        }

        #region SUBFUNCTIONS

        /// <summary>
        /// gets the questions that satisfy a given weight, based on the ExamRow.MID tag
        /// </summary>
        /// <param name="count">how many questions to add from a given weight</param>
        /// <param name="exams">a raw list of questions to weight in</param>
        /// <param name="multiplier">the factor to set MID range to search for</param>
        /// <param name="weight">the weight</param>
        /// <returns>The weighted questions</returns>
        private static IList<DB.ExamsRow> GetByWeight(ref IList<DB.ExamsRow> exams, int multiplier, ref DB.PreferencesRow p)
        {
            int min = 0;
            int max = 0;

            int howMany;
            List<DB.ExamsRow> join = new List<DB.ExamsRow>(); //the array that will contain the weighted questions;
            for (int x = 1; x <= 5; x++)
            {
                min = 0;
                min = x * multiplier;

                max = 0;
                max = (x + 1) * multiplier;

                howMany = 0; //initialize
                howMany = (int)p["D" + x]; // questions to add from preferences

                IEnumerable<DB.ExamsRow> rows = exams.Where(o => o.MID > min && o.MID <= max).Take(howMany);

                join.AddRange(rows);
            }

            return join;
        }

        private static IList<DB.ExamsRow> WeightThem(ref DB.PreferencesRow p, ref IList<DB.ExamsRow> exams)
        {
            //   if (order)
            {
                exams = exams.OrderBy(o => o.QuestionsRow.Weight).ToList();
            }

            int multiplier = 10000;

            IList<DB.ExamsRow> rows = exams.ToList();
            Func<DB.ExamsRow, bool> MID = ex =>
            {
                ex.MID = (ex.QuestionsRow.Weight * multiplier);
                ex.MID += rows.IndexOf(ex);
                return true;
            };

            exams = exams.Where(MID).ToList();

            rows = null;

            /*
            foreach (DB.ExamsRow ex in exams)
            {
                ex.MID = (ex.QuestionsRow.Weight * multiplier);
                ex.MID += exams.IndexOf(ex);
            }
             */

            ///FILTER ACCORDING TO PREFERENCES
            //     if (order)
            {
                exams = exams.OrderBy(o => o.MID).ToList();
            }
            IList<DB.ExamsRow> join = null;

            //  if (order)
            {
                join = GetByWeight(ref exams, multiplier, ref p);
            }
            //    else join = exams.ToList();
            return join;
        }

        /// <summary>
        /// the code string for 1 exam question (already randonmized)
        /// </summary>
        /// <param name="qID"></param>
        /// <param name="answ"></param>
        /// <returns></returns>
        private string CreateExamQuestionCode(int qID, ref IEnumerable<DB.AnswersRow> answ)
        {
            string aid = string.Empty;
            ///MAKE ORDERS, which contain the string of answersID in the order of the exam, for each questionID selected
            string code = string.Empty;
            //the second separator sep2 gives the AnswrID (AID) of the right answer

            foreach (DB.AnswersRow a in answ)
            {
                DB.OrderRow or = this.dB.Order.NewOrderRow();
                this.dB.Order.AddOrderRow(or);
                or.QID = qID; //questionID
                or.AID = a.AID; //answerID
                if (!a.IsCorrectNull() && a.Correct == true) aid = a.AID.ToString();
                code += a.AID.ToString() + sep.ToString();
            }
            code = code.Substring(0, code.Length - 1); //without comma at the end
            code += sep2.ToString() + aid;
            return code;
        }

        private static void MakeExamIntro(ref W.Document doc, string qrCodeJPG, string Intro, string Title)
        {
            W.Range aux = null;

            aux = doc.Range(0, Type.Missing);
            aux.Text = Title; ///TITULO
            aux.set_Style(W.WdBuiltinStyle.wdStyleHeading1);
           
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);


            aux = doc.Paragraphs.Last.Range;
            object lastRange = aux;
       
            aux.InlineShapes.AddPicture(qrCodeJPG, ref roObj, ref roObj, ref lastRange);
            aux.InlineShapes.AddPicture(idFile, ref roObj, ref roObj, ref lastRange);

            aux.set_Style(W.WdBuiltinStyle.wdStyleCaption); //title style for image???
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux); //esto no estaba

            /////TITULOOOOOOOOOOOO
            if (string.IsNullOrEmpty(Intro)) return;

            aux = doc.Paragraphs.Last.Range;
            aux.Text = Intro;
            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter);
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);

            //  aux = doc.Paragraphs.Last.Range;
        }

        private void MakeExamCoupon(ref W.Document doc, string qrCodeJPG, ref DB.ExamsListRow ls, bool showAnswer)
        {
            string text = string.Empty;

            W.Range aux = null;

            aux = doc.Paragraphs.Last.Range;
            text = "\n\n\n";
            text += Resources.CortaCupon;
            //   text += "\n\n\n";

            aux.Text = text;
            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter);
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);

            aux = doc.Paragraphs.Last.Range;
            object lastRange = aux;
            aux.InlineShapes.AddPicture(qrCodeJPG, ref roObj, ref roObj, ref lastRange);
            aux.InlineShapes.AddPicture(idFile, ref roObj, ref roObj, ref lastRange);
            aux.set_Style(W.WdBuiltinStyle.wdStyleTitle);
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);

            aux = doc.Paragraphs.Last.Range;

            int cuadros = ls.GetExamsRows().Count();
            text = string.Empty;
            text += Resources.ExamenRespuesta;
            text += "     ";

            string ahorcado = string.Empty;
            int j = 0;
            for (int i = 0; i < cuadros; i++)
            {
                ahorcado += "__ ";
                j++;
                if (j == 5)
                {
                    ahorcado += "-";
                    j = 0;
                }
            }

            text += ahorcado;

            text += "\n\n";
            text += Resources.ExamenRespuesta;
            text += "     ";

            string ahorcadoNr = string.Empty;
            j = 0;
            for (int i = 0; i < cuadros; i++)
            {
                int cuenta = i + 1;
                ahorcadoNr += cuenta.ToString();
                ahorcadoNr += " ";
                if (cuenta < 10) ahorcadoNr += "  ";

                j++;
                if (j == 5)
                {
                    ahorcadoNr += "-";
                    j = 0;
                }
            }

            text += ahorcadoNr;

            if (showAnswer)
            {
                text += "\n\n";
                text += Resources.ExamenRespuestaCorrecta;
                text += "\t";
                text += Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(ls.CLAnswer, password, 0);
                text += "\n\n";
                text += "Encriptada como:\t";
                text += ls.CLAnswer;
                text += "\n\n";
                text += "Examen modelo:\t";
                text += ls.GUID;
                //    text += "\n\n";
                //    text += "Encriptado como:\t";
                //  text += ;
            }

            aux.Text = text;
            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter); /// ESTA ES LA CLAVE!!!
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);
        }

        /// <summary>
        /// PERHAPS THE MOST IKPORTAN, WHERE QAS ARE GENERATED AND PASSWORDS TOO
        /// </summary>
        /// <param name="exams"></param>
        /// <param name="pr"></param>
        /// <param name="questionAnswer"></param>
        /// <returns></returns>
        private static string[] MakeQAs(ref IEnumerable<DB.ExamsRow> exams, ref DB.PreferencesRow pr, out  IList<string[]> questionAnswer)
        {
            string Clave;
            string questionWeight;
            string qidString;

            int pregunta = 1;
            string examen = string.Empty;
            Clave = string.Empty;
            questionWeight = string.Empty;
            qidString = string.Empty;
            int claveCount = 1;

            questionAnswer = new List<string[]>();

            foreach (DB.ExamsRow r in exams)
            {
                string[] qAns = new string[2];
                questionAnswer.Add(qAns);

                DB.QuestionsRow q = r.QuestionsRow;
                qidString += r.QID.ToString() + sep.ToString();

                Decimal askValue = Convert.ToDecimal(q.Weight * pr.Factor);

                string weight = Decimal.Round(askValue, 3).ToString();

                examen = pregunta.ToString() + "- " + q.Question; //pregunta
                if (pr.showPoints)
                {
                    examen += "\t(Puntos: " + weight + " )";
                }
                examen += "\n";

                pregunta++; //contador

                qAns[0] = examen;

                string[] answers = r.AIDString.Split(sep2); ///separates de correct value AID, from the array of answers AIDs
                int AIDcorrecto = Convert.ToInt32(answers[1]); //toma la respuesta correcta
                answers = answers[0].Split(sep); //the array of answers ID

                IEnumerable<DB.AnswersRow> answ = r.QuestionsRow.GetAnswersRows();
                int option = 0;
                examen = string.Empty;

                foreach (string s in answers)
                {
                    int aid = Convert.ToInt32(s);
                    DB.AnswersRow a = answ.FirstOrDefault(o => o.AID == aid); //selectivamente en el mismo orden en que fue generada

                    string respuesta = a.Answer;
                    char letra = Tools.Alpha[option];
                    if (a.AID == AIDcorrecto)
                    {
                        Clave += letra;
                    }

                    examen += letra;
                    examen += ") " + respuesta + "\n"; //respuesta
                    option++;
                }
                //examen += "\n";

                qAns[1] = examen;

                questionWeight += q.Weight.ToString() + sep.ToString(); // save a string of questionsWeights
                if (claveCount == 5)
                {
                    Clave += sep.ToString();
                    claveCount = 0;
                }

                claveCount++;
            }

            if (qidString[qidString.Length - 1].Equals(sep))
            {
                qidString = qidString.Substring(0, qidString.Length - 1); //erase last separator
            }
            if (questionWeight[questionWeight.Length - 1].Equals(sep))
            {
                questionWeight = questionWeight.Substring(0, questionWeight.Length - 1); //erase last separator
            }

            questionWeight += sep2.ToString() + pr.Factor.ToString(); //KEEP THE FACTOR IN THE STRING!!!

            if (Clave[Clave.Length - 1].Equals(sep))
            {
                Clave = Clave.Substring(0, Clave.Length - 1); //erase last separator
            }

            ///IMPORTANT CUMJULATIVE INFO
            return new string[3] { Clave, questionWeight, qidString }; // LAnswer , LQuestion,
        }

        private static void MakeExamFileBody(ref  IList<string[]> questionAnswer, ref  W.Document doc)
        {
            W.Range aux = null;

            foreach (string[] qAns in questionAnswer)
            {
                aux = doc.Paragraphs.Last.Range;
                aux.Text = qAns[0];
                aux.set_Style(W.WdBuiltinStyle.wdStyleHeading2);
                doc.Paragraphs.Add(aux); // insert onces

                aux = doc.Paragraphs.Last.Range;
                aux.Text = qAns[1]; // now you can isert the questions
                aux.set_Style(W.WdBuiltinStyle.wdStyleHeading3);
                doc.Paragraphs.Add(aux);
            }
        }

        #endregion SUBFUNCTIONS
    }
}