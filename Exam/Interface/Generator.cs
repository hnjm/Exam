using Exam.DBTableAdapters;
using Exam.Properties;
using Rsx.Dumb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Exam.DB;
using W = Microsoft.Office.Interop.Word;

namespace Exam
{
    public partial class Generator
    {
        public static void MakeExamCoupon(ref W.Document doc, string qrCodeJPG, ref ExamsListRow ls, bool showAnswer, string password)
        {
            string text = string.Empty;

            W.Range aux = null;

            aux = doc.Paragraphs.Last.Range;
            text = "\n\n\n";
            text += Resources.CortaCupon;
            // text += "\n\n\n";

            aux.Text = text;
            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter);
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);

            aux = doc.Paragraphs.Last.Range;
            object lastRange = aux;
            aux.InlineShapes.AddPicture(qrCodeJPG, ref roObj, ref roObj, ref lastRange);
            aux.InlineShapes.AddPicture(ID_FILE, ref roObj, ref roObj, ref lastRange);
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
                // text += "\n\n"; text += "Encriptado como:\t"; text += ;
            }

            aux.Text = text;
            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter); /// ESTA ES LA CLAVE!!!
            aux.InsertParagraphAfter();
            doc.Paragraphs.Add(aux);
        }

        public void PopulateBasic()
        {
            this.inter.IBS.Working = true;

            FillClasses();

            FillAYear();

            FillPreferences();

            this.inter.IBS.Working = false;


           
        }



        /// <summary>
        /// gets the questions that satisfy a given weight, based on the ExamRow.MID tag
        /// </summary>
        /// <param name="count">     how many questions to add from a given weight</param>
        /// <param name="exams">     a raw list of questions to weight in</param>
        /// <param name="multiplier">the factor to set MID range to search for</param>
        /// <param name="weight">    the weight</param>
        /// <returns>The weighted questions</returns>
        public static IList<ExamsRow> GetByWeight(ref IList<ExamsRow> exams, int multiplier, ref PreferencesRow p)
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

        public static void MakeExamFileBody(ref IList<string[]> questionAnswer, ref W.Document doc)
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

        public static void MakeExamIntro(ref W.Document doc, string qrCodeJPG, string Intro, string Title)
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
            aux.InlineShapes.AddPicture(ID_FILE, ref roObj, ref roObj, ref lastRange);

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

            // aux = doc.Paragraphs.Last.Range;
        }

        /// <summary>
        /// PERHAPS THE MOST IKPORTAN, WHERE QAS ARE GENERATED AND PASSWORDS TOO
        /// </summary>
        /// <param name="exams">         </param>
        /// <param name="pr">            </param>
        /// <param name="questionAnswer"></param>
        /// <returns></returns>
        public static string[] MakeQAs(ref IEnumerable<ExamsRow> exams, ref PreferencesRow pr, out IList<string[]> questionAnswer)
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

            foreach (ExamsRow r in exams)
            {
                string[] qAns = new string[2];
                questionAnswer.Add(qAns);

                QuestionsRow q = r.QuestionsRow;
                qidString += r.QID.ToString() + SEP.ToString();

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

                string[] answers = r.AIDString.Split(SEP2); ///separates de correct value AID, from the array of answers AIDs
                int AIDcorrecto = Convert.ToInt32(answers[1]); //toma la respuesta correcta
                answers = answers[0].Split(SEP); //the array of answers ID

                IEnumerable<AnswersRow> answ = r.QuestionsRow.GetAnswersRows();
                int option = 0;
                examen = string.Empty;

                foreach (string s in answers)
                {
                    int aid = Convert.ToInt32(s);
                    AnswersRow a = answ.FirstOrDefault(o => o.AID == aid); //selectivamente en el mismo orden en que fue generada

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

                questionWeight += q.Weight.ToString() + SEP.ToString(); // save a string of questionsWeights
                if (claveCount == 5)
                {
                    Clave += SEP.ToString();
                    claveCount = 0;
                }

                claveCount++;
            }

            if (qidString[qidString.Length - 1].Equals(SEP))
            {
                qidString = qidString.Substring(0, qidString.Length - 1); //erase last separator
            }

            if (questionWeight[questionWeight.Length - 1].Equals(SEP))
            {
                questionWeight = questionWeight.Substring(0, questionWeight.Length - 1); //erase last separator
            }

            questionWeight += SEP2.ToString() + pr.Factor.ToString(); //KEEP THE FACTOR IN THE STRING!!!

            if (Clave[Clave.Length - 1].Equals(SEP))
            {
                Clave = Clave.Substring(0, Clave.Length - 1); //erase last separator
            }

            ///IMPORTANT CUMJULATIVE INFO
            return new string[3] { Clave, questionWeight, qidString }; // LAnswer , LQuestion,
        }

        public static void MakeTableBytes<T>(ref T l, string examPath)
        {
            Type tipo = l.GetType();
            byte[] arr2 = null;
            // string afile = string.Empty;

            if (tipo.Equals(typeof(ExamsListRow)))
            {
                ExamsListRow ls = l as ExamsListRow;

                IEnumerable<ExamsRow> rows = ls.GetExamsRows();

                ExamsDataTable exdt = new ExamsDataTable();
                foreach (var item in rows) exdt.ImportRow(item);

                // afile = ExasmPath + ls.EID.ToString();

                arr2 = Tables.MakeDTBytes(ref exdt, examPath);
                ls.EData = arr2;
            }
            else if (tipo.Equals(typeof(ExamsRow)))
            {
                //SAVE COPY OF TABLE

                ExamsRow ex = l as ExamsRow;

                // afile = ExasmPath + ex.QID.ToString(); IEnumerable<DB.QuestionsRow> shortQlist =
                // new List<DB.QuestionsRow>(); ((IList<DB.QuestionsRow>)shortQlist).Add(ex.QuestionsRow);
                QuestionsDataTable qdt = new QuestionsDataTable();
                qdt.ImportRow(ex.QuestionsRow);
                byte[] qarray = Tables.MakeDTBytes(ref qdt, examPath);
                ex.QData = qarray;
                Dumb.FD(ref qdt);

                AnswersDataTable adt = new AnswersDataTable();
                IEnumerable<AnswersRow> answ = ex.QuestionsRow.GetAnswersRows();
                foreach (var item in answ) adt.ImportRow(item);
                // afile = ExasmPath + ex.QueToString() + ".xml";
                arr2 = Tables.MakeDTBytes(ref adt, examPath);
                ex.AData = arr2;
                Dumb.FD(ref adt);
            }
            else if (tipo.Equals(typeof(PreferencesRow)))
            {
                PreferencesRow p = l as PreferencesRow;              //SAVE A COPY OF EXAMS LISTS
                IEnumerable<DB.ExamsListRow> rows = p.GetExamsListRows();

                ExamsListDataTable dt = new ExamsListDataTable();
                foreach (var item in rows) dt.ImportRow(item);

                // afile = ExasmPath + p.PID.ToString() + ".xml";
                arr2 = Tables.MakeDTBytes(ref dt, examPath);
                p.ELData = arr2;

                Dumb.FD(ref dt);
            }
        }

        public static IList<ExamsRow> WeightThem(ref PreferencesRow p, ref IList<ExamsRow> exams)
        {
            // if (order)
            {
                exams = exams.OrderBy(o => o.QuestionsRow.Weight).ToList();
            }

            int multiplier = 10000;

            IList<ExamsRow> rows = exams.ToList();
            Func<ExamsRow, bool> MID = ex =>
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
            // if (order)
            {
                exams = exams.OrderBy(o => o.MID).ToList();
            }
            IList<ExamsRow> join = null;

            // if (order)
            {
                join = GetByWeight(ref exams, multiplier, ref p);
            }
            // else join = exams.ToList();
            return join;
        }

        public void FillExams(string classe)
        {
            DB.TAM.ExamsListTableAdapter.FillByClass(this.inter.IdB.ExamsList, classe);
        }

        private static string ID_FILE;
        private static string JPG_EXT = ".jpg";
        private static string model = "Model-";
        //FOR WORD DOCUMENT GENERATION INTEROP
        private static object nullObj = System.Reflection.Missing.Value;

        private static string PDF_EXT = ".pdf";
        private static int qrSise = 2;
        private static object roObj = true;
        // private const string slash = "\\";
        private static char SEP = '-';

        private static char SEP2 = ',';
        private static string WORD_EXT = ".docx";
        private static string WORD_TEMPLATE = "WordDoc";
        private static string EXAMS_FOLDER = "Exams";

        private const string SLASH = "\\";
    }

    public partial class Generator
    {
        public void FillAYear()
        {
            //  this.inter.IdB.StuList.Clear();

            DB.TAM.AYearTableAdapter.Fill(this.inter.IdB.AYear);

         
            // this.inter.IdB.Student.Clear();

            // DB.TAM.StudentTableAdapter.Fill(this.inter.IdB.Student);
        }

        private string ExasmPath;
        private Interface inter;

        private string logoFile;

   
        private string templateFile;

     
        public void MakeList(string pathway, string classe, int ayearID)
        {
          string[] lines = System.IO.File.ReadAllLines(pathway);

            foreach (var item in lines)
            {
            StuListRow s =    inter.IdB.StuList.NewStuListRow();
                string[] members = item.Split(',');
                s.StudentID = members[0];
                s.LastNames = members[1];
                s.FirstNames = members[2];
                s.Class = classe;
             
                s.Date = DateTime.Now;
                s.Year = s.Date.Year;
                s.AYearID = ayearID;
                inter.IdB.StuList.AddStuListRow(s);


            }
            TAM.StuListTableAdapter.Update(inter.IdB.StuList);

        }

        public void CleanOrphans()
        {
            DB.TAM.ExamsTableAdapter.Fill(inter.IdB.Exams);

            IEnumerable<DB.ExamsRow> rows = inter.IdB.Exams.OfType<DB.ExamsRow>();

            rows = rows.Where(o => o.ExamsListRow == null).ToList();

            foreach (DataRow r in rows)
            {
                r.Delete();
            }

            DB.TAM.ExamsTableAdapter.Update(inter.IdB.Exams);

            inter.IdB.Exams.Clear();
        }

        /// <summary>
        /// the code string for 1 exam question (already randonmized)
        /// </summary>
        /// <param name="qID"> </param>
        /// <param name="answ"></param>
        /// <returns></returns>
        public string CreateExamQuestionCode(int qID, ref IEnumerable<AnswersRow> answ)
        {
            string aid = string.Empty;
            ///MAKE ORDERS, which contain the string of answersID in the order of the exam, for each questionID selected
            string code = string.Empty;
            //the second separator sep2 gives the AnswrID (AID) of the right answer

            foreach (AnswersRow a in answ)
            {
                OrderRow or = inter.IdB.Order.NewOrderRow();
                inter.IdB.Order.AddOrderRow(or);
                or.QID = qID; //questionID
                or.AID = a.AID; //answerID
                if (!a.IsCorrectNull() && a.Correct == true) aid = a.AID.ToString();
                code += a.AID.ToString() + SEP.ToString();
            }
            code = code.Substring(0, code.Length - 1); //without comma at the end
            code += SEP2.ToString() + aid;
            return code;
        }

        public void DeleteExams()
        {
            QTA qta = new QTA();
            qta.Clean();
            qta.Dispose();
        }

        public void DeleteExamsFiles()
        {
            IEnumerable<string> files = System.IO.Directory.GetFiles(ExasmPath);
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                    // Status = Properties.Resources.b + file;
                }
                catch (Exception ex)
                {
                    inter.Status = Resources.NoBorrado + file;
                    // StatusHandler?.Invoke(ex, EventArgs.Empty);
                }
            }
        }

        public void DoExams(int ayearID)
        {
            //  DataRow row = (inter.IBS.Preferences.Current as DataRowView).Row;


         //   DB.TAM.PreferencesTableAdapter.Update(inter.IdB.Preferences);

         

            PreferencesRow p = inter.IBS.CurrentPreference;
            p.EndEdit();

            // p.Class = materiaBox.Text;
            p.DateTime = DateTime.Now;
            p.AYearID = ayearID;
            p.Year = p.DateTime.Year;

            DB.TAM.PreferencesTableAdapter.Update(p);

            inter.IBS.Working = true;

            PreferencesRow clone = Tables.CloneARow(inter.IdB.Preferences, p) as DB.PreferencesRow;
            p = clone;
            p.Done = true;

            DB.TAM.PreferencesTableAdapter.Update(p);


            inter.IBS.Working = false;

            inter.IBS.LogPref.MoveFirst();

            //    inter.

            inter.IBS.Working = true;


            inter.Status = "Empezando...";

            FillClassDataBase(p.Class);

       //     inter.IBS.LogPref.MoveFirst(); //select item to clone
        //    Application.DoEvents();

         

            inter.ProgressHandler?.Invoke(p.Models, EventArgs.Empty);

            int mod = 0; //models

            for (mod = 0; mod < p.Models; mod++)
            {
                inter.IdB.Exams.Clear();
                inter.IdB.Order.Clear();

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

                ExamsListRow ls = doOneExam(ref p);

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

             //   Application.DoEvents();

                ExamsRow[] arr = ls.GetExamsRows();
                double sumPoint = arr.Sum(o => o.QuestionsRow.Weight);
                double factor = Convert.ToDouble(p.Points);
                factor /=(sumPoint);
                p.Factor = factor;
                arr = null;

             //   Application.DoEvents();

                IList<string[]> questionAnswer;
           
                ////ENCRIPTAMIENTOOOOOOO
                inter.Status = "Procesando...";

                doOneEncriptExam(ref p, ref ls, out questionAnswer);

            

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

             
                inter.Status = Resources.Creando + "el examen " + ls.GUID;

                doOneDocExam(ref p, ref ls, ref questionAnswer);

                inter.Status = "Examen generado";

             //   Application.DoEvents();
                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

               // Application.DoEvents();
            }

            MakeTableBytes(ref p, ExasmPath);

            inter.Status = p.Models + " Modelos generados";
            //     Application.DoEvents();

        
        //    pClone.Done = false;

            DB.TAM.PreferencesTableAdapter.Update(p);

            inter.IdB.Order.Clear();
            inter.IdB.Exams.Clear();

       

            inter.IBS.Working = false;

     

        }

        public void FillClassDataBase(string clase)
        {
            clearQA();

            if (clase.CompareTo(string.Empty) == 0) return;
            string[] connection = DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString.Split(';');

            string newConnection = "Initial Catalog=" + clase + ";";

            string total = connection[0] + ";" + newConnection + connection[2];
            DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString = total;
            DB.TAMQA.QuestionsTableAdapter.Connection.ConnectionString = total;

            fillQA();
        }


        public void FillStudents(string clase, string year, string Ayear)
        {
       
            //  this.inter.IdB.StuList.Clear();
            int nyear;
            int.TryParse(year, out nyear);

            int? ayearID = this.inter.IdB.FindAYearID(Ayear);

            DB.TAM.StuListTableAdapter.FillByClassAYear(this.inter.IdB.StuList,clase, (int)ayearID,nyear);

          //  this.inter.IdB.Student.Clear();

            DB.TAM.StudentTableAdapter.FillByClass(this.inter.IdB.Student,clase);
  
        }
        public void FillClasses()
        {
            DB.TAM.ClassTableAdapter.Fill(this.inter.IdB.Class);
        }
        public void FillPreferences()
        {
         //   this.inter.IdB.Preferences.Clear();
            DB.TAM.PreferencesTableAdapter.Fill(this.inter.IdB.Preferences);
            this.inter.IdB.Preferences.AcceptChanges();
        }

        /// <summary>
        /// GEnerates a virtual database
        /// </summary>
        public void GenerateQuestions(int questions)
        {
            int j = 1;

            for (int i = 0; i < questions; i++)
            {
                if (j == 6) j = 1;
                QuestionsRow q = inter.IdB.Questions.NewQuestionsRow();
                q.Weight = j;
                inter.IdB.Questions.AddQuestionsRow(q);
                DB.TAMQA.QuestionsTableAdapter.Update(q);

                q.Question = "Pregunta número " + q.QID.ToString();

                int m = 1;
                while (m < 6)
                {
                    AnswersRow r = inter.IdB.Answers.NewAnswersRow();
                    r.Answer = "Respuesta " + q.QID.ToString() + "-" + m.ToString();
                    r.QID = q.QID;
                    if (m == 5) r.Correct = true;
                    inter.IdB.Answers.AddAnswersRow(r);
                    DB.TAMQA.AnswersTableAdapter.Update(r);
                    m++;
                }

                j++;
            }

            updateQA();
        }

        public void MakeEvaluationList()
        {
            IEnumerable<DB.StuListRow> list = this.inter.IdB.StuList;
            int students = list.Count();
            // IEnumerable<DB.ExamsListRow> exams = this.dB.ExamsList; int examenes = exams.Count();
            // int stuperex = students / examenes;

            foreach (DB.StuListRow r in list)
            {
                DB.StudentRow stu = this.inter.IdB.Student.NewStudentRow();

                stu.StudentID = r.StudentID;
                stu.Name = r.FirstNames.ToUpper();
                stu.Surname = r.LastNames.ToUpper();
                this.inter.IdB.Student.AddStudentRow(stu);
            }

            DB.TAM.StudentTableAdapter.Update(this.inter.IdB.Student);

            /*

            IEnumerable<DB.StudentRow> stus = this.dB.Student;

            int contador = 0;

            foreach (DB.ExamsListRow exa in exams)
            {
                IEnumerable<DB.StudentRow> sublist = null;

         // IEnumerable<DB.StudentRow> stuExam = this.dB.Student;

                sublist = stus.Where(o => o.IsGUIDNull()).Take(stuperex);

                foreach(DB.StudentRow s in sublist)
                {
                    s.GUID = exa.GUID;

                    s.EID = exa.EID;

                    contador++;
                }

            */

            // stu.EID = ls.EID;
        }
        public void OpenFile()
        {
            try
            {

          
            string destFile = ExasmPath + model + inter.IBS.CurrentExam.GUID + PDF_EXT; //ok

            if (!inter.IBS.CurrentExam.IsExamFileNull())
            {
                byte[] arr = inter.IBS.CurrentExam.ExamFile;
                IO.WriteFileBytes(ref arr, destFile);
            }

            IO.Process(new System.Diagnostics.Process(), ExasmPath, "explorer.exe", destFile, true, false, 10000);
            }
            catch (Exception ex)
            {
                inter.Status = "El Examen ya está abierto";
                // StatusHandler?.Invoke(ex, EventArgs.Empty);
           
        }

        }

        private void clearQA()
        {
            inter.IdB.Questions.Clear();
            inter.IdB.Answers.Clear();
        }

        private void doOneDocExam(ref PreferencesRow p, ref ExamsListRow ls, ref IList<string[]> questionAnswer)
        {
            ///MAKE WORD DOCUMENT

            string CryptoGUID = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(ls.GUID, inter.Password, null);

            string filepathAux = ExasmPath + model + ls.GUID;
            string jpgQRCodeFile = filepathAux + JPG_EXT;
            Image img = Tools.CreateQRCode(CryptoGUID, qrSise);
            img.Save(jpgQRCodeFile);

            string destFile = filepathAux + WORD_EXT;
      
            File.Copy(templateFile, destFile);

            W.Application w = new W.Application();
            object dest = destFile as object;

            W.Document doc = w.Documents.Open(ref dest, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref roObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj);

            // doc._CodeName = p.Title;
            string Intro = "Recorte el Cupón";
            // W.Document doc = ExamMain.MakeWord(ref destFile, ref w); //makes the word file
            MakeExamIntro(ref doc, jpgQRCodeFile, Intro, p.Title + " (" + p.Class + ")");
            MakeExamCoupon(ref doc, jpgQRCodeFile, ref ls, p.showAnswer, inter.Password);

            MakeExamFileBody(ref questionAnswer, ref doc);


            Application.DoEvents();
            //save word
            doc.Save();

            //    string filepathAux = ExasmPath + model + ls.GUID;
            string filePDF = filepathAux + PDF_EXT;
            string filejpg = filepathAux + JPG_EXT;

            inter.Status = Resources.ExamenCreado;

       
            ///MAKE PDF
            Tools.MakePDF(ref doc);

            Application.DoEvents();

            w.Documents.Close(ref roObj, ref nullObj, nullObj);
            // Application.DoEvents();
            w.Quit(ref nullObj, ref nullObj, ref nullObj);

            inter.Status = Resources.ExamenCreadoPDF;

            File.Delete(destFile);
            File.Delete(filejpg);

            Byte[] rtf = IO.ReadFileBytes(filePDF);
            ls.ExamFile = rtf; //salva una copia del archivo PDF en el servidor SQL
            DB.TAM.ExamsListTableAdapter.Update(ls);
            File.Delete(filePDF);

       
        }

        private void doOneEncriptExam(ref PreferencesRow p, ref ExamsListRow ls, out IList<string[]> questionAnswer)
        {
           
            IEnumerable<DB.ExamsRow> join = ls.GetExamsRows();
            string[] ClaveWQID = null;

            ClaveWQID = MakeQAs(ref join, ref p, out questionAnswer);
            ls.LQuestion = ClaveWQID[1]; // question weight string
            string LAnswer = ClaveWQID[0]; //clave verdadera sin encriptar
            string QIDString = ClaveWQID[2]; //secuencia de preguntas, importante guardar

            ls.CLAnswer = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(LAnswer, inter.Password, null); //encripta
            ls.CQIDString = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(QIDString, inter.Password, null); //encripta

            //SAVE COPY OF TABLE in EXAMLIST
            MakeTableBytes(ref ls, ExasmPath);
            DB.TAM.ExamsListTableAdapter.Update(inter.IdB.ExamsList);
          
        }

        /// <summary>
        /// MAIN FILE FOR GENERATING AN EXAM
        /// </summary>
        /// <param name="p"></param>
        private ExamsListRow doOneExam(ref PreferencesRow p)
        {
            inter.Status = Resources.Creando + inter.IdB.Questions.Count + Resources.PregAleatorias;

            ///RANDOMNIZE RAW QUESTIONS
            IEnumerable<DB.QuestionsRow> questions = inter.IdB.Questions; //toma todas las preguntas

            questions = Tools.RandomizeStrings(questions); //1
            Application.DoEvents();
            questions = Tools.RandomizeStrings(questions); //2
            Application.DoEvents();
            questions = Tools.RandomizeStrings(questions); //3 times random

            IList<ExamsRow> exams = new List<ExamsRow>(); //todas las preguntas randomnizadas seran guardadas primero en esta lista
            foreach (QuestionsRow q in questions)
            {
                ExamsRow er = inter.IdB.Exams.NewExamsRow(); //crea rows pero no los metas en la tabla
                er.QID = q.QID; //dales el id de la pregunta
                exams.Add(er); //agregalos a la lista
            }

            inter.Status = Resources.Filtrando;
            IEnumerable<ExamsRow> join = WeightThem(ref p, ref exams); //not re-ordered // preguntas seleccionadas y filtradas
            Application.DoEvents();

            inter.Status = Resources.OrderAleatorio; // de las preguntas del examen ya pesadas (filtradaS)
            join = Tools.RandomizeStrings(join); //1
            Application.DoEvents();
            join = Tools.RandomizeStrings(join); //2
            Application.DoEvents();
            join = Tools.RandomizeStrings(join); //2

            inter.Status = Resources.Seleccionando + join.Count() + Resources.PregAleatorias;
            Application.DoEvents();

            foreach (ExamsRow ex in join)
            {
                inter.IdB.Exams.AddExamsRow(ex); //agregalas a la tabla para salvarlas

                IEnumerable<AnswersRow> answ = ex.QuestionsRow.GetAnswersRows();
                ///RANDOMIZE RAW ASWERS
                answ = Tools.RandomizeStrings<AnswersRow>(answ); //1
                answ = Tools.RandomizeStrings<AnswersRow>(answ);//2
                answ = Tools.RandomizeStrings<AnswersRow>(answ); //3

                //CREATE EXAM QUESTION CODE! AIDSTRING
                string code = CreateExamQuestionCode(ex.QID, ref answ);
                ex.AIDString = code;

                ExamsRow auxiliar = ex;
                MakeTableBytes(ref auxiliar, ExasmPath);
            }
            DB.TAM.ExamsTableAdapter.Update(inter.IdB.Exams);
            Application.DoEvents();

            // WApp.Add(w);
            Guid g = Guid.NewGuid();
            ExamsListRow ls = inter.IdB.ExamsList.NewExamsListRow();
            inter.IdB.ExamsList.AddExamsListRow(ls);
            ls.PID = p.PID;
            ////MAKES THE DOC FILE
            ls.GUID = g.ToString().Replace("-", null);//.Split('-')[4];
            ls.Time = DateTime.Now;
            ls.Class = p.Class;
        
            // Image img = Tools.CreateQRCode(ls.GUID, qrSizeDB); ls.QRCode = Tools.imageToByteArray(img);

            DB.TAM.ExamsListTableAdapter.Update(inter.IdB.ExamsList);
            Application.DoEvents();
            //este orden es importante

            foreach (DB.ExamsRow re in join) re.EID = ls.EID; //ahora asocia las preguntas a cada examen generado
            DB.TAM.ExamsTableAdapter.Update(inter.IdB.Exams);
            Application.DoEvents();

            return ls;
        }

        private void fillQA()
        {
            DB.TAMQA.AnswersTableAdapter.Fill(inter.IdB.Answers);
            DB.TAMQA.QuestionsTableAdapter.Fill(inter.IdB.Questions);
        }
    
        private void updateQA()
        {
            DB.TAMQA.QuestionsTableAdapter.Update(inter.IdB.Questions);
            DB.TAMQA.AnswersTableAdapter.Update(inter.IdB.Answers);
        }

        public Generator(ref Interface interf)
        {
            inter = interf;
            ExasmPath = inter.Path + SLASH + EXAMS_FOLDER + SLASH;
            ID_FILE = inter.Path + SLASH + "identification" + JPG_EXT;

            logoFile = inter.Path + SLASH + "logo" + JPG_EXT;
            templateFile = inter.Path + SLASH + WORD_TEMPLATE + WORD_EXT;

            if (!Directory.Exists(ExasmPath))
            {
                Directory.CreateDirectory(ExasmPath);
            }
        }
    }
}