using System.Windows.Forms;

namespace Exam
{
    public partial class ucDataBase : UserControl
    {
        private static string DB = "Base de Datos";
        private static string GUI = "Examen GUI";
        private bool examGUI = false;
        private Interface Interface;

        public object[] DGVs
        {
            get
            {
                return new object[] { answersDataGridView, this.examsDataGridView, questionsDataGridView, ucTopic1.DGVs[0] };
            }
        }

        public bool ExamGUI
        {
            get
            {
                return examGUI;
            }
            set
            {
                examGUI = value;

                setforExam();

                if (Interface == null) return;

                setDGVs();
                setBindings();
            }
        }

        public void Set(ref Interface inter)
        {
            Interface = inter;

            ucTopic1.Set(ref inter, false);

            resetDGVs();

            setforExam();
            setDGVs();
            setBindings();

            Rsx.Dumb.Dumb.FD(ref dB);
        }

        private void questionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != this.FigureFile.Index) return;

            MessageBox.Show("open file");
            // this.richAnsBox.SaveFile(, RichTextBoxStreamType.) this.richAnsBox.LoadFile()
        }

        private void resetDGVs()
        {
            this.answersDataGridView.DataSource = null;
            this.questionsDataGridView.DataSource = null;

            this.answersBS.Dispose();
            this.questionsBS.Dispose();

            this.examsDataGridView.DataSource = null;
            this.examsBS.Dispose();
            this.examsDataGridView.DataSource = Interface.IBS.Exam;
        }

        private void setBindings()
        {
            string txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;

            BindingSource qbs = Interface.IBS.Questions;
            BindingSource abs = Interface.IBS.Answers;
            if (examGUI) qbs = Interface.IBS.RandomQuestions;
            if (examGUI) abs = Interface.IBS.RandomAnswers;

            this.qrtfbox.DataBindings.Clear();
            this.artfbox.DataBindings.Clear();

            Binding question = new Binding(txt, qbs, Interface.IdB.Questions.QuestionColumn.ColumnName, true, mode);
            Binding answer = new Binding(txt, abs, Interface.IdB.Answers.AnswerColumn.ColumnName, true, mode);

            this.qrtfbox.DataBindings.Add(question);
            this.artfbox.DataBindings.Add(answer);
        }

        /// <summary>
        /// OK
        /// </summary>
        private void setDGVs()
        {
            this.answersDataGridView.DataSource = Interface.IBS.Answers;
            if (examGUI) this.answersDataGridView.DataSource = Interface.IBS.RandomAnswers;
            // this.answersDataGridView.MultiSelect = false;

            this.questionsDataGridView.DataSource = Interface?.IBS.Questions;
            if (examGUI) this.questionsDataGridView.DataSource = Interface.IBS.RandomQuestions;
            // this.questionsDataGridView.MultiSelect = false;
        }

        private void setforExam()
        {
            if (examGUI) this.dbTab.Text = GUI;
            else this.dbTab.Text = DB;

            qrtfbox.ReadOnly = examGUI;

            questionsDataGridView.ReadOnly = examGUI;
            artfbox.ReadOnly = examGUI;

            answersDataGridView.ReadOnly = examGUI;

            qcontainer.Panel2Collapsed = examGUI;
            qcontainer.Panel1Collapsed = !examGUI;
            acontainer.Panel1Collapsed = examGUI;
            rcontainer.Panel2Collapsed = examGUI;
        }

        public ucDataBase()
        {
            InitializeComponent();
        }
    }
}