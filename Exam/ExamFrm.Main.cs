using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Rsx;

namespace Exam
{
    public partial class ExamFrm
    {
     

        //  private  string path = Application.StartupPath;
        private static string examsFolder = "Exams";

        private static string ExasmPath = path + slash + examsFolder + slash;
        private void ChangeClassConnection(String clase)
        {
            string[] connection = DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString.Split(';');

            string newConnection = "Initial Catalog=" + clase + ";";
            DB.TAMQA.AnswersTableAdapter.Connection.ConnectionString = connection[0] + ";" + newConnection + connection[2];
            DB.TAMQA.QuestionsTableAdapter.Connection.ConnectionString = connection[0] + ";" + newConnection + connection[2];

            DB.TAMQA.AnswersTableAdapter.Fill(this.dB.Answers);
            DB.TAMQA.QuestionsTableAdapter.Fill(this.dB.Questions);
        }
        private void FillTables()
        {
            this.dB.Clear();

            DB.TAM.StuListTableAdapter.Fill(this.dB.StuList);
            ICollection<string> hs = Rsx.Dumb.HashFrom<string>(this.dB.StuList.StudentIDColumn);
         //   this.carneBox.AutoCompleteCustomSource.Clear();
         //   this.carneBox.AutoCompleteCustomSource.AddRange(hs.ToArray());
            Dumb.FillABox(carneBox.ComboBox, hs, true, false);

            //         this.carneBox.AutoCompleteCustomSource = hs as AutoCompleteStringCollection;
            IList<string> clases = Dumb.HashFrom<string>(this.dB.StuList.ClassColumn);
            Dumb.FillABox(this.materiaBox.ComboBox, clases, true, false);

            DB.TAM.StudentTableAdapter.Fill(this.dB.Student);

            ///  CleanOrphans();
     
            DB.TAM.PreferencesTableAdapter.Fill(this.dB.Preferences);
            
            this.preferencesBS.MoveFirst();


            ChangeClassConnection(this.materiaBox.Text); //after it acquired the last preferences Class / Materia


        }

        private void CleanOrphans()
        {
            DB.TAM.ExamsTableAdapter.Fill(this.dB.Exams);

            IEnumerable<DB.ExamsRow> rows = this.dB.Exams.OfType<DB.ExamsRow>();

            rows = rows.Where(o => o.ExamsListRow == null).ToList();

            foreach (DataRow r in rows)
            {
                r.Delete();
            }

            DB.TAM.ExamsTableAdapter.Update(this.dB.Exams);

            this.dB.Exams.Clear();
        }

        private void SetBindings()
        {
            String txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;

            Binding score = new Binding(txt, this.studentBS, this.dB.Student.ScoreColumn.ColumnName, true, mode);
            Binding correct = new Binding(txt, this.studentBS, this.dB.Student.CorrectColumn.ColumnName, true, mode);
            Binding name = new Binding(txt, this.stuListBS, this.dB.StuList.FirstNamesColumn.ColumnName, true, mode);
            Binding surname = new Binding(txt, this.stuListBS, this.dB.StuList.LastNamesColumn.ColumnName, true, mode);

            Binding title = new Binding(txt, this.preferencesBS, this.dB.Preferences.TitleColumn.ColumnName, true, mode);
            Binding Classe = new Binding(txt, this.preferencesBS, this.dB.Preferences.ClassColumn.ColumnName, true, mode);
       //     Binding StudentIDs = new Binding(txt, this.stuListBS, this.dB.StuList.StudentIDColumn.ColumnName, true, mode);
            this.scoreBox.TextBox.DataBindings.Add(score);
            this.CorrectBox.TextBox.DataBindings.Add(correct);
            this.nameBox.TextBox.DataBindings.Add(name);
            this.surnameBox.TextBox.DataBindings.Add(surname);
            this.titleBox.TextBox.DataBindings.Add(title);
            this.materiaBox.ComboBox.DataBindings.Add(Classe);



            Binding question = new Binding(txt, this.questionsBS, this.dB.Questions.QuestionMetaColumn.ColumnName, true, mode);
            Binding answer = new Binding(txt, this.answersBS, this.dB.Answers.AnswerMetaColumn.ColumnName, true, mode);

            this.richQuesBox.DataBindings.Add(question);
            this.richAnsBox.DataBindings.Add(answer);


            string asc = " asc";
            string desc = " desc";
            // preferences is done=FALSE as we want to create
            this.preferencesBS.Filter = this.dB.Preferences.DoneColumn.ColumnName + " = false";
            // log is preferences executed (exams generated)
            this.logBS.Filter = this.dB.Preferences.DoneColumn.ColumnName + " = true";
            this.logBS.Sort = this.dB.Preferences.DateTimeColumn.ColumnName + desc;

            this.examsListBS.Sort = this.dB.ExamsList.EIDColumn.ColumnName + desc;
            this.examsBS.Sort = this.dB.Exams.IDColumn.ColumnName + asc;
            this.answersBS.Sort = this.dB.Answers.CorrectColumn.ColumnName + asc;
            this.questionsBS.Sort = this.dB.Questions.QIDColumn.ColumnName + asc;
            this.orderBS.Sort = this.dB.Order.IDColumn.ColumnName + asc;
        }

        private void SetMenues()
        {

            //initial binding source to link to
            this.ucMenu.BS = this.preferencesBS;

           //table adapter
            this.ucMenu.TableAM[0] = DB.TAM;
            this.ucMenu.TableAM[1] = DB.TAMQA;

            //selection change option
            this.ucMenu.DgvSelectionChanged = this.dGV_SelectionChanged;

            this.ucMenu.SetDGVs(ref preferencesDGV);
            this.ucMenu.SetDGVs(ref examsListDGV);
            this.ucMenu.SetDGVs(ref examsDataGridView);
            this.ucMenu.SetDGVs(ref stuListDGV);
            this.ucMenu.SetDGVs(ref studentDataGridView);
            this.ucMenu.SetDGVs(ref questionsDataGridView);
            this.ucMenu.SetDGVs(ref answersDataGridView);
            this.ucMenu.SetDGVs(ref orderDataGridView);
            this.ucMenu.SetDGVs(ref logDGV);

            EventHandler h1 = this.generateExams_Click;
            // EventHandler h2 = this.generateList_Click;
            EventHandler h3 = this.deleteExams_Clicked;
            EventHandler h4 = this.generateQuestion_Clicked;
            EventHandler h2 = this.form_Load;
            this.ucMenu.SetReload(ref h2);
            //  EventHandler h3 =  this.convertPDF_Click;
            this.ucMenu.SetMenues("Generar Exámenes", ref h1);
            //   this.ucMenu.SetMenues("Generar Listas", ref h2);
            this.ucMenu.SetMenues("Limpiar Base de Datos de Exámenes", ref h3);
            this.ucMenu.SetMenues("Agregar una pregunta", ref h4);

        }

        private void SetStatusException(ref Exception ex)
        {
            this.statuslbl.Text = ex.Message + "\t\t" + ex.StackTrace;
        }

        private void MakeTableBytes<T>(ref T l)
        {
            Type tipo = l.GetType();
            byte[] arr2 = null;
            string afile = null;
            if (tipo.Equals(typeof(DB.ExamsListRow)))
            {
                DB.ExamsListRow ls = l as DB.ExamsListRow;
                DB.ExamsDataTable exdt = new DB.ExamsDataTable();

                IEnumerable<DB.ExamsRow> rows = ls.GetExamsRows();
                afile = ExasmPath + ls.EID.ToString();
                arr2 = Dumb.MakeDTBytes(ref rows, ref exdt, afile);
                ls.EData = arr2;
            }
            else if (tipo.Equals(typeof(DB.ExamsRow)))
            {
                //SAVE COPY OF TABLE
                DB.AnswersDataTable adt = new DB.AnswersDataTable();
                DB.ExamsRow ex = l as DB.ExamsRow;

                DB.QuestionsDataTable qdt = new DB.QuestionsDataTable();
                string qfile = ExasmPath + ex.QID.ToString();
                IEnumerable<DB.QuestionsRow> shortQlist = new List<DB.QuestionsRow>();
                ((IList<DB.QuestionsRow>)shortQlist).Add(ex.QuestionsRow);
                byte[] qarray = Dumb.MakeDTBytes(ref shortQlist, ref qdt, qfile);
                ex.QData = qarray;

                IEnumerable<DB.AnswersRow> answ = ex.QuestionsRow.GetAnswersRows();
                afile = ExasmPath + ex.ID.ToString();
                arr2 = Dumb.MakeDTBytes(ref answ, ref  adt, afile);
                ex.AData = arr2;
            }
            else if (tipo.Equals(typeof(DB.PreferencesRow)))
            {
                DB.PreferencesRow p = l as DB.PreferencesRow;              //SAVE A COPY OF EXAMS LISTS
                IEnumerable<DB.ExamsListRow> rows = p.GetExamsListRows();
                DB.ExamsListDataTable dt = new DB.ExamsListDataTable();
                afile = ExasmPath + p.PID.ToString() + ".xml";
                arr2 = Dumb.MakeDTBytes(ref rows, ref dt, afile);
                p.ELData = arr2;
            }
        }
    }
}