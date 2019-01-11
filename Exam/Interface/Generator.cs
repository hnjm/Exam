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
        private string examsPath;
        private Interface inter;

        private string logoFile;

        private string templateFile;

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
        public string CreateExamQuestionCode(ref IEnumerable<AnswersRow> answ)
        {
            string aid = string.Empty;
            ///MAKE ORDERS, which contain the string of answersID in the order of the exam, for each questionID selected
            string code = string.Empty;
            //the second separator sep2 gives the AnswrID (AID) of the right answer

            foreach (AnswersRow a in answ)
            {
                // OrderRow or = inter.IdB.Order.NewOrderRow(); inter.IdB.Order.AddOrderRow(or);
                // or.QID = qID; //questionID or.AID = a.AID; //answerID
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
            IEnumerable<string> files = System.IO.Directory.GetFiles(examsPath);
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

        public void DoExams()
        {



            PreferencesRow p = inter.IBS.CurrentPreference;

            inter.IBS.Working = true;

            updatePreferenceAndClone(ref p);

            inter.IBS.Working = false;

            inter.IBS.LogPref.Position = inter.IBS.LogPref.Find(inter.IdB.Preferences.PIDColumn.ColumnName, p.PID);

            inter.IBS.Working = true;


            inter.Status = "Empezando...";

            FillClassDataBase(p.Class);

            // inter.IBS.LogPref.MoveFirst(); //select item to clone Application.DoEvents();

            inter.ProgressHandler?.Invoke(p.Models, EventArgs.Empty);

            int mod = 0; //models

            for (mod = 0; mod < p.Models; mod++)
            {
                inter.IdB.Exams.Clear();

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

                ExamsListRow ls = doOneExam(ref p);
                if (ls == null)
                {

                    continue;
                }

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

                Generator.FindFactor(ref p, ref ls);


                ////ENCRIPTAMIENTOOOOOOO
                inter.Status = "Procesando examen...";

                IList<string[]> questionAnswer;
                doOneEncriptExam(ref p, ref ls, out questionAnswer, inter.Password);
                //SAVE COPY OF TABLE in EXAMLIST

                Generator.MakeTableBytes(ref ls, examsPath);

                DB.TAM.ExamsListTableAdapter.Update(inter.IdB.ExamsList);

                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);
                inter.Status = Resources.Creando + "el examen " + ls.GUID;

                doOneDocExam(ref p, ref ls, ref questionAnswer);

                inter.Status = "Examen generado";
                inter.ProgressHandler?.Invoke(0, EventArgs.Empty);

            }

            Generator.MakeTableBytes(ref p, examsPath);

            int count = p.GetExamsListRows().Count();
            if (count != 0) inter.Status = count + " Modelos generados";

            DB.TAM.PreferencesTableAdapter.Update(p);


            inter.IdB.Exams.Clear();

            inter.IBS.Working = false;
        }

        private static void updatePreferenceAndClone(ref PreferencesRow p)
        {
            // p.EndEdit();
            // p.Class = materiaBox.Text;
            p.DateTime = DateTime.Now;
            // p.AYearID = ayearID;
            p.Year = p.DateTime.Year;

            DB.TAM.PreferencesTableAdapter.Update(p);
            PreferencesRow clone = Tables.CloneARow(p.Table, p) as DB.PreferencesRow;
            p = clone;
            p.Done = true;
            DB.TAM.PreferencesTableAdapter.Update(p);
        
        }


        public void FillClassDataBase(string clase)
        {
            clearQA();

            if (clase.CompareTo(string.Empty) == 0) return;

            ClassRow c = inter.IdB.Class.FirstOrDefault(o => o.Class.CompareTo(clase) == 0);

            if (c == null) return;

            string[] connection = DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString.Split(';');

            string newDB = "Initial Catalog=" + c.DB + ";";
            string newsrv = "Data Source=" + c.Server + ";";
            string total = newsrv + newDB + connection[2];

            DB.TAMQA.Connection.ConnectionString = total;
            DB.TAMQA.QuestionsTableAdapter.Connection.ConnectionString = total;
            DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString = total;
            DB.TAMQA.TopicsTableAdapter.Connection.ConnectionString = total;

            fillQA();
        }

        public void FillClassDataBaseOld(string clase)
        {
            clearQA();

            if (clase.CompareTo(string.Empty) == 0) return;

            ClassRow[] clases = inter.IdB.Class.Where(o => o.Class.CompareTo(clase) == 0).ToArray();

            if (clases.Count() == 0) return;

            foreach (ClassRow c in clases)
            {
                string[] connection = DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString.Split(';');

                string newDB = "Initial Catalog=" + c.DB + ";";
                string newsrv = "Data Source=" + c.Server + ";";
                string total = newsrv + newDB + connection[2];
                // DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString = total;
                // DB.TAMQA.QuestionsTableAdapter.Connection.ConnectionString = total;
                // DB.TAMQA.TopicsTableAdapter.Connection.ConnectionString = total;
                DB.TAMQA.Connection.ConnectionString = total;
                fillQA();
            }
        }

        public void FillExams(string classe)
        {
            DB.TAM.ExamsListTableAdapter.FillByClass(this.inter.IdB.ExamsList, classe);
        }

        public void FillStudents(string clase, string year, string Ayear)
        {
            // this.inter.IdB.StuList.Clear();
            int nyear;
            int.TryParse(year, out nyear);

            int? ayearID = this.inter.IdB.FindAYearID(Ayear);

            DB.TAM.StuListTableAdapter.FillByClassAYear(this.inter.IdB.StuList, clase, (int)ayearID, nyear);

            // this.inter.IdB.Student.Clear();

            DB.TAM.StudentTableAdapter.FillByClass(this.inter.IdB.Student, clase);
        }

        /// <summary>
        /// GEnerates a virtual database
        /// </summary>
        public void GenerateQuestions(int questions)
        {
            int j = 1;

            int? topicID = inter.IBS.CurrentTopic?.TopicID;

            for (int i = 0; i < questions; i++)
            {
                if (j == 6) j = 1;
                QuestionsRow q = inter.IdB.Questions.NewQuestionsRow();
                q.Weight = j;
                inter.IdB.Questions.AddQuestionsRow(q);
                if (topicID != null) q.TopicID = (int)topicID;

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

        public void MakeList(string pathway, string classe, int ayearID)
        {
            string[] lines = System.IO.File.ReadAllLines(pathway);

            foreach (var item in lines)
            {
                StuListRow s = inter.IdB.StuList.NewStuListRow();
                string[] members = item.Split(',');
                s.StudentID = members[0];
                s.LastNames = members[1];
                s.FirstNames = members[3];
                s.Class = classe;
                s.Evaluated = 0;
                s.Done = false;

                s.Date = DateTime.Now;
                s.Year = s.Date.Year;
                s.AYearID = ayearID;
                inter.IdB.StuList.AddStuListRow(s);
            }
            TAM.StuListTableAdapter.Update(inter.IdB.StuList);
        }
        public void OpenFile()
        {
            try
            {
                DB.ExamsListRow erow = inter.IBS.CurrentExam;

                string destFile = model + erow.GUID + PDF_EXT; //ok

                if (erow.IsExamFileNull()) return;

                byte[] arr = erow.ExamFile;

                IO.OpenBytesFile(ref arr, destFile,examsPath);

           

            }
            catch (Exception ex)
            {
                inter.Status = "El Examen ya está abierto";
            }
        }

        public void PopulateBasic()
        {
            this.inter.IBS.Working = true;

            DB.TAM.ClassTableAdapter.Fill(this.inter.IdB.Class);

            // this.inter.IdB.StuList.Clear();

            DB.TAM.AYearTableAdapter.Fill(this.inter.IdB.AYear);

            // this.inter.IdB.Student.Clear();

            // DB.TAM.StudentTableAdapter.Fill(this.inter.IdB.Student);

            // this.inter.IdB.Preferences.Clear();
            DB.TAM.PreferencesTableAdapter.Fill(this.inter.IdB.Preferences);
            this.inter.IdB.Preferences.AcceptChanges();

            this.inter.IBS.Working = false;
        }

        private void clearQA()
        {
            inter.IdB.Answers.Clear();
            inter.IdB.Questions.Clear();
            inter.IdB.Topics.Clear();
        }

        private void doOneDocExam(ref PreferencesRow p, ref ExamsListRow ls, ref IList<string[]> questionAnswer)
        {
  
            string filepathAux = examsPath + model + ls.GUID;

            string CryptoGUID = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword(ls.GUID, inter.Password, null);
     
            //     string jpgQRCodeFile = filepathAux + JPG_EXT;

            Image img = Tools.CreateQRCode(CryptoGUID, qrSise);
            img.Save(filepathAux + JPG_EXT);
            img.Dispose();

            //    string destFile = filepathAux + WORD_EXT;

            ///MAKE WORD DOCUMENT
            ///
            File.Copy(templateFile, filepathAux + WORD_EXT);

            string title = p.Title + " (" + p.Class + ")";
            bool showAnswer = p.showAnswer;
            string pass = inter.Password;

            object doc = Generator.MakeDOC(ref ls, ref questionAnswer, filepathAux, title, showAnswer, pass);

            inter.Status = Resources.ExamenCreado;

            bool close = true;
            bool quit = true;
            Generator.MakePDF(ref doc,close,quit);

            Byte[] rtf = IO.ReadFileBytes(filepathAux + PDF_EXT);
            ls.ExamFile = rtf; //salva una copia del archivo PDF en el servidor SQL
            DB.TAM.ExamsListTableAdapter.Update(ls);

            inter.Status = Resources.ExamenCreadoPDF;


            File.Delete(filepathAux + PDF_EXT);

            File.Delete(filepathAux + WORD_EXT);

            File.Delete(filepathAux + JPG_EXT);


        }

      





        /// <summary>
        /// MAIN FILE FOR GENERATING AN EXAM
        /// </summary>
        /// <param name="p"></param>
        private ExamsListRow doOneExam(ref PreferencesRow p)
        {
            inter.Status = Resources.Creando + inter.IdB.Questions.Count + Resources.PregAleatorias;

            Func<DB.QuestionsRow, bool> selector = x =>
            {
                bool? ok = x.TopicsRow?.UseIt;
                if (ok == null) ok = false;
                return (bool)ok;
            };
            ///RANDOMNIZE RAW QUESTIONS
            IEnumerable<DB.QuestionsRow> questions = inter.IdB.Questions.Where(selector).ToArray(); //toma todas las preguntas

            if (questions.Count() == 0)
            {
                inter.Status = "No hay preguntas seleccionadas";
                return null;
            }

            int times = 3;
            questions = Tools.RandomnizeStringsTimes(questions, times);

            IList<ExamsRow> exams = new List<ExamsRow>(); //todas las preguntas randomnizadas seran guardadas primero en esta lista
            foreach (QuestionsRow q in questions)
            {
                ExamsRow er = inter.IdB.Exams.NewExamsRow(); //crea rows pero no los metas en la tabla
                er.QID = q.QID; //dales el id de la pregunta
                exams.Add(er); //agregalos a la lista
            }

            inter.Status = Resources.Filtrando;

            IEnumerable<ExamsRow> join = weightThem(ref p, ref exams); //not re-ordered // preguntas seleccionadas y filtradas
            Application.DoEvents();

            inter.Status = Resources.OrderAleatorio; // de las preguntas del examen ya pesadas (filtradaS)

            join = Tools.RandomnizeStringsTimes(join, times);

            inter.Status = Resources.Seleccionando + join.Count() + Resources.PregAleatorias;
            Application.DoEvents();

            foreach (ExamsRow ex in join)
            {
                inter.IdB.Exams.AddExamsRow(ex); //agregalas a la tabla para salvarlas

                DB.QuestionsRow q = ex.QuestionsRow;
                IEnumerable<AnswersRow> answ = q.GetAnswersRows();
                ///RANDOMIZE RAW ASWERS
                answ = Tools.RandomnizeStringsTimes(answ, times);
                //CREATE EXAM QUESTION CODE! AIDSTRING
                string code = CreateExamQuestionCode(ref answ);
                ex.AIDString = code;
                ExamsRow auxiliar = ex;
                MakeTableBytes(ref auxiliar, examsPath);
            }

            DB.TAM.ExamsTableAdapter.Update(inter.IdB.Exams);
            Application.DoEvents();

            ExamsListRow ls = inter.IdB.AddExam(ref p);
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
            DB.TAMQA.TopicsTableAdapter.Fill(inter.IdB.Topics);
            DB.TAMQA.AnswersTableAdapter.Fill(inter.IdB.Answers);
            DB.TAMQA.QuestionsTableAdapter.Fill(inter.IdB.Questions);
        }

        private void fillQAOld()
        {
            DB clone;
            bool cloned = false;

            if (inter.IdB.Topics.Count == 0) clone = inter.IdB;
            else
            {
                clone = new DB();
                cloned = true;
            }
            DB.TAMQA.AnswersTableAdapter.Fill(clone.Answers);
            DB.TAMQA.QuestionsTableAdapter.Fill(clone.Questions);
            DB.TAMQA.TopicsTableAdapter.Fill(clone.Topics);

            if (cloned)
            {
                foreach (var item in clone.Topics)
                {
                    inter.IdB.Topics.ImportRow(item);
                }
                DB.TAMQA.TopicsTableAdapter.Update(inter.IdB.Topics);
                foreach (var item in clone.Questions)
                {
                    inter.IdB.Questions.ImportRow(item);

                    DB.TAMQA.QuestionsTableAdapter.Update(inter.IdB.Questions);
                    DB.QuestionsRow last = inter.IdB.Questions.Last();
                    AnswersRow[] answers = item.GetAnswersRows();
                    foreach (var a in answers)
                    {
                        inter.IdB.Answers.ImportRow(a);
                        AnswersRow la = inter.IdB.Answers.Last();
                        la.QID = last.QID;
                    }

                    DB.TAMQA.AnswersTableAdapter.Update(inter.IdB.Answers);
                }
            }
            // DB.TAMQA.AnswersTableAdapter.Fill(inter.IdB.Answers);
            // DB.TAMQA.QuestionsTableAdapter.Fill(inter.IdB.Questions); DB.TAMQA.TopicsTableAdapter.Fill(inter.IdB.Topics);
        }

        private void updateQA()
        {
            DB.TAMQA.TopicsTableAdapter.Update(inter.IdB.Topics);
            DB.TAMQA.QuestionsTableAdapter.Update(inter.IdB.Questions);
            DB.TAMQA.AnswersTableAdapter.Update(inter.IdB.Answers);
        }

        public Generator(ref Interface interf)
        {
            inter = interf;
            examsPath = inter.Path + SLASH + EXAMS_FOLDER + SLASH;
            ID_FILE = inter.Path + SLASH + "identification" + JPG_EXT;

            logoFile = inter.Path + SLASH + "logo" + JPG_EXT;
            templateFile = inter.Path + SLASH + WORD_TEMPLATE + WORD_EXT;

            if (!Directory.Exists(examsPath))
            {
                Directory.CreateDirectory(examsPath);
            }
        }
    }
}