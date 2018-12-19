using System;
using System.Windows.Forms;
using VTools;

namespace Exam
{
    public partial class ExamFrm : Form
    {
        private Interface Interface;

        public IMenu IMenu
        {
            get
            {
                return ctrl1.IMenu;
            }
        }

        public void Set(ref Interface inter)
        {
            Rsx.Dumb.Dumb.FD(ref dB);

            Interface = inter;

            this.ctrl1.Set(ref inter);

            setDGVs();

            setBindings();

            setMenues();

            this.Load += load;
        }

        private void deleteExams_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.DeleteExams();

            Interface.IGenerator.DeleteExamsFiles();
        }

        private void examsListDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Interface.IGenerator.OpenFile();
        }

        private void generateExams_Click(object sender, EventArgs e)
        {
            Interface.IGenerator.DoExams();
        }

        private void generateList_Click(object sender, EventArgs e)
        {
            Interface.IGenerator.MakeEvaluationList();
        }

        private void generateQuestion_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.GenerateQuestions(1); //genera 1 pregunta
        }

        private void load(object sender, EventArgs e)
        {
            this.ctrl1.IMenu.Load();
        }

        private void questionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != this.FigureFile.Index) return;

            MessageBox.Show("open file");
            // this.richAnsBox.SaveFile(, RichTextBoxStreamType.) this.richAnsBox.LoadFile()
        }

        private void setBindings()
        {
            String txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;

            Binding question = new Binding(txt, Interface.IBS.Questions, Interface.IdB.Questions.QuestionColumn.ColumnName, true, mode);
            Binding answer = new Binding(txt, Interface.IBS.Answers, Interface.IdB.Answers.AnswerColumn.ColumnName, true, mode);

            this.richQuesBox.DataBindings.Add(question);
            this.richAnsBox.DataBindings.Add(answer);

            Binding title = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.TitleColumn.ColumnName, true, mode);

            this.titleBox.TextBox.DataBindings.Add(title);

            Binding ayear = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.AYearColumn.ColumnName, true, mode);
        }

        /// <summary>
        /// OK
        /// </summary>
        private void setDGVs()
        {
            this.examsDataGridView.DataSource = Interface.IBS.Exam;
            this.examsListDGV.DataSource = Interface.IBS.ExamsList;
            this.answersDataGridView.DataSource = Interface.IBS.Answers;
            this.questionsDataGridView.DataSource = Interface.IBS.Questions;
            this.preferencesDGV.DataSource = Interface.IBS.Preferences;
            this.logDGV.DataSource = Interface.IBS.LogPref;

            this.orderDataGridView.DataSource = Interface.IBS.Order;

            this.orderBS.Dispose();
            this.preferencesBS.Dispose();
            this.logBS.Dispose();

            this.examsBS.Dispose();
            this.examsListBS.Dispose();
            this.answersBS.Dispose();
            this.questionsBS.Dispose();
        }

        private void setMenues()
        {
            this.IMenu.SetDGVs(ref preferencesDGV);
            this.IMenu.SetDGVs(ref examsListDGV);
            this.IMenu.SetDGVs(ref examsDataGridView);

            this.IMenu.SetDGVs(ref questionsDataGridView);
            this.IMenu.SetDGVs(ref answersDataGridView);
            this.IMenu.SetDGVs(ref orderDataGridView);
            this.IMenu.SetDGVs(ref logDGV);

            EventHandler h1 = this.generateExams_Click;
            // EventHandler h2 = this.generateList_Click;

            EventHandler h4 = this.generateQuestion_Clicked;
            EventHandler h3 = this.deleteExams_Clicked;
            this.IMenu.SetMenues("Generar Exámenes", ref h1);
            // this.ucMenu.SetMenues("Generar Listas", ref h2);
            this.IMenu.SetMenues("Limpiar Base de Datos de Exámenes", ref h3);
            this.IMenu.SetMenues("Agregar una pregunta", ref h4);
        }

        public ExamFrm()
        {
            InitializeComponent();
        }
    }
}