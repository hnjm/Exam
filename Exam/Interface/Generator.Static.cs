using Exam.Properties;
using Rsx.Dumb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using static Exam.DB;
using W = Microsoft.Office.Interop.Word;

namespace Exam
{
    public partial class Generator
    {
        private static object MakeDOC(ref ExamsListRow ls, ref IList<string[]> questionAnswer, string filepathAux, string title, bool showAnswer, string pass)
        {
            W.Document doc = openDocApp(filepathAux);

            string Intro = "Recorte el Cupón";
            // W.Document doc = ExamMain.MakeWord(ref destFile, ref w); //makes the word file
            makeExamIntro(ref doc, filepathAux + JPG_EXT, Intro, title);
            makeExamCoupon(ref doc, filepathAux + JPG_EXT, ref ls, showAnswer, pass);
            makeExamFileBody(ref questionAnswer, ref doc);

         //   Application.DoEvents();
            //save word
            doc.Save();
            return doc;
        }
        private static void MakePDF(ref object document, bool close = true, bool quit = true)
        {
            W.Document doc = document as W.Document;
            W.Application w = doc.Application;
            ///MAKE PDF
            Tools.MakePDF(ref doc);

            if (close) w.Documents.Close(ref roObj, ref nullObj, nullObj);
            // Application.DoEvents();
            if (quit) w.Quit(ref nullObj, ref nullObj, ref nullObj);

        }
        private static void FindFactor(ref PreferencesRow p,ref ExamsListRow ls)
        {
            ExamsRow[] arr = ls.GetExamsRows();
            double sumPoint = arr.Sum(o => o.QuestionsRow.Weight);
            double factor = Convert.ToDouble(p.Points);
            factor /= (sumPoint);
            p.Factor = factor;
            arr = null;
        }
        private static W.Document openDocApp(string filepathAux)
        {
            W.Application w = new W.Application();
            object dest = (filepathAux + WORD_EXT) as object;
            W.Document doc = w.Documents.Open(ref dest, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj, ref roObj, ref nullObj, ref nullObj, ref nullObj, ref nullObj);
            return doc;
        }

        private static void doOneEncriptExam(ref PreferencesRow p, ref ExamsListRow ls, out IList<string[]> questionAnswer, string password)
        {
            IEnumerable<DB.ExamsRow> join = ls.GetExamsRows();
            string[] ClaveWQID = null;

            ClaveWQID = makeQAs(ref join, ref p, out questionAnswer);
            ls.LQuestion = ClaveWQID[1]; // question weight string
            string LAnswer = ClaveWQID[0]; //clave verdadera sin encriptar
            string QIDString = ClaveWQID[2]; //secuencia de preguntas, importante guardar

            ls.CLAnswer = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(LAnswer, password, null); //encripta
            ls.CQIDString = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(QIDString, password, null); //encripta
        }

        private static string[] ExtractAnswersArray( ref ExamsRow r)
        {

            string[] answers = r.AIDString.Split(SEP2); ///separates de correct value AID, from the array of answers AIDs
          
            return answers;
        }

        private static string ExtractAnswers(string[] answers, ref ExamsRow r)
        {

            int AIDcorrecto = Convert.ToInt32(answers[1]); //toma la respuesta correcta
            answers = answers[0].Split(SEP); //the array of answers ID

            QuestionsRow q = r.QuestionsRow;

            IEnumerable<AnswersRow> answ = q.GetAnswersRows();
            int option = 0;
            string examen;
            examen = string.Empty;
            foreach (string s in answers)
            {
                char letra = Tools.Alpha[option];
                int aid = Convert.ToInt32(s);
                AnswersRow a = answ.FirstOrDefault(o => o.AID == aid); //selectivamente en el mismo orden en que fue generada
               
                string respuesta = a.Answer;
                examen += letra;
                examen += ") " + respuesta + "\n"; //respuesta
                option++;
            }

            return examen;
        }

        private static void ExtractKey(ref string Clave, ref int claveCount, string[] answers, ref ExamsRow r)
        {

            int AIDcorrecto = Convert.ToInt32(answers[1]); //toma la respuesta correcta
            answers = answers[0].Split(SEP); //the array of answers ID

            QuestionsRow q = r.QuestionsRow;

            int option2 = 0;
            IEnumerable<AnswersRow> answ2 = q.GetAnswersRows();
            foreach (string s in answers)
            {
                char letra = Tools.Alpha[option2];
                int aid = Convert.ToInt32(s);
                AnswersRow a = answ2.FirstOrDefault(o => o.AID == aid); //selectivamente en el mismo orden en que fue generada
                a.Char = letra.ToString().ToUpper().Trim();
                if (a.AID == AIDcorrecto)
                {
                    Clave += letra;
                }
                option2++;
            }

            if (claveCount == answers.Count())
            {
                Clave += SEP.ToString();
                claveCount = 0;
            }

            claveCount++;

           
        }

        private static string PUNTOS = "Puntos";

        private static string ExtractQuestionAndWeight(ref string questionWeight, ref string qidString, ref int pregunta, bool showPoints, double pointFactor, ref ExamsRow r)
        {
            string examen;
            QuestionsRow q = r.QuestionsRow;
            qidString += r.QID.ToString() + SEP.ToString();
            Decimal askValue = Convert.ToDecimal(q.Weight * pointFactor);
            string weight = Decimal.Round(askValue, 3).ToString();
            examen = pregunta.ToString() + "- " + q.Question; //pregunta

            if (showPoints)
            {
                examen += "\t( " + PUNTOS +": " + weight + " )";
            }
            examen += "\n";
            pregunta++; //contador

            questionWeight += q.Weight.ToString() + SEP.ToString(); // save a string of questionsWeights
            return examen;
        }

        /// <summary>
        /// gets the questions that satisfy a given weight, based on the ExamRow.MID tag
        /// </summary>
        /// <param name="count">     how many questions to add from a given weight</param>
        /// <param name="exams">     a raw list of questions to weight in</param>
        /// <param name="multiplier">the factor to set MID range to search for</param>
        /// <param name="weight">    the weight</param>
        /// <returns>The weighted questions</returns>
        private static IList<ExamsRow> GetByWeight(ref IList<ExamsRow> exams, int multiplier, ref PreferencesRow p)
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

                IEnumerable<DB.ExamsRow> rows = exams.Where(o => o.MID >= min && o.MID <= max).Take(howMany);

                join.AddRange(rows);
            }

            return join;
        }

        private static void makeExamCoupon(ref W.Document doc, string qrCodeJPG, ref ExamsListRow ls, bool showAnswer, string password)
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
        private static void makeExamFileBody(ref IList<string[]> questionAnswer, ref W.Document doc)
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

        private static void makeExamIntro(ref W.Document doc, string qrCodeJPG, string Intro, string Title)
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
        private static string[] makeQAs(ref IEnumerable<ExamsRow> exams, ref PreferencesRow pr, out IList<string[]> questionAnswer)
        {
            bool showPoints = pr.showPoints;
            double pointFactor = pr.Factor;

            questionAnswer = new List<string[]>();

            int pregunta = 1;
            int claveCount = 1;

            string Clave;
            string questionWeight;
            string qidString;
            Clave = string.Empty;
            questionWeight = string.Empty;
            qidString = string.Empty;

            foreach (DataRow row in exams)
            {
                string[] qAns = new string[2];
                ExamsRow r = (ExamsRow)row;
                string examen = string.Empty;

                examen = ExtractQuestionAndWeight(ref questionWeight, ref qidString, ref pregunta, showPoints, pointFactor, ref r);
                qAns[0] = examen;

                string[] answers = ExtractAnswersArray(ref r);

                examen = ExtractAnswers(answers, ref r);
                qAns[1] = examen;

                ExtractKey(ref Clave, ref claveCount, answers, ref r);

                questionAnswer.Add(qAns);
            }

            if (qidString[qidString.Length - 1].Equals(SEP))
            {
                qidString = qidString.Substring(0, qidString.Length - 1); //erase last separator
            }

            if (questionWeight[questionWeight.Length - 1].Equals(SEP))
            {
                questionWeight = questionWeight.Substring(0, questionWeight.Length - 1); //erase last separator
            }

            questionWeight += SEP2.ToString() + pointFactor.ToString(); //KEEP THE FACTOR IN THE STRING!!!

            if (Clave[Clave.Length - 1].Equals(SEP))
            {
                Clave = Clave.Substring(0, Clave.Length - 1); //erase last separator
            }

            ///IMPORTANT CUMJULATIVE INFO
            return new string[3] { Clave, questionWeight, qidString }; // LAnswer , LQuestion,
        }
        private static void MakeTableBytes<T>(ref T l, string examPath)
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
                QuestionsRow q = ex.QuestionsRow;
                // afile = ExasmPath + ex.QID.ToString(); IEnumerable<DB.QuestionsRow> shortQlist =
                // new List<DB.QuestionsRow>(); ((IList<DB.QuestionsRow>)shortQlist).Add(ex.QuestionsRow);
                QuestionsDataTable qdt = new QuestionsDataTable();
                qdt.ImportRow(q);
                byte[] qarray = Tables.MakeDTBytes(ref qdt, examPath);
                ex.QData = qarray;
                Dumb.FD(ref qdt);

                AnswersDataTable adt = new AnswersDataTable();
                IEnumerable<AnswersRow> answ = q.GetAnswersRows();
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

        private static IList<ExamsRow> weightThem(ref PreferencesRow p, ref IList<ExamsRow> exams)
        {
            exams = exams.OrderBy(o => o.QuestionsRow.Weight).ToList();

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

            exams = exams.OrderBy(o => o.MID).ToList();

            IList<ExamsRow> join = null;

            join = GetByWeight(ref exams, multiplier, ref p);

            // else join = exams.ToList();
            return join;
        }

     
    }

    public partial class Generator
    {
        private const string SLASH = "\\";
        private static string EXAMS_FOLDER = "Exams";
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
    }
}