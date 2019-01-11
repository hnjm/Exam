using Rsx.Dumb;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Exam
{
    public class BS
    {
        private DB set;
        private DB auxiliar;



        public BindingSource RandomAnswers { get; private set; }
        public BindingSource RandomQuestions { get; private set; }

        public BindingSource Answers { get; private set; }
        public BindingSource Topics { get; private set; }

        public DB.ExamsListRow CurrentExam
        {
            get
            {
                return (ExamsList.Current as DataRowView)?.Row as DB.ExamsListRow;
            }
        }

        public DB.PreferencesRow CurrentPreference
        {
            get
            {
                return (Preferences.Current as DataRowView)?.Row as DB.PreferencesRow;
            }
        }

        public DB.PreferencesRow LastPreference
        {
            get
            {
                return (LogPref.Current as DataRowView)?.Row as DB.PreferencesRow;
            }
        }

        public DB.StudentRow CurrentStudent
        {
            get
            {
                return (Student.Current as DataRowView)?.Row as DB.StudentRow;
            }
        }
        public DB.TopicsRow CurrentTopic
        {
            get
            {
                return (Topics.Current as DataRowView)?.Row as DB.TopicsRow;
            }
        }

        public BindingSource Exam { get; private set; }

        public BindingSource ExamsList { get; private set; }

        public BindingSource LogPref { get; private set; }

  //      public BindingSource Order { get; private set; }

        public BindingSource Preferences { get; private set; }

        public BindingSource Questions { get; private set; }

        public BindingSource Student { get; private set; }
        public BindingSource Class { get; private set; }
        public BindingSource AYear { get; private set; }

        public BindingSource StudentsList { get; private set; }

        public bool Working
        {
            get;
            set;
        }
        /*
        public bool Working
        {
          
            set
            {
                if (!value)
                {
                    //    Exam.CurrentChanged += Exam_CurrentChanged;
                    ExamsList.CurrentChanged += currentChanged;
                    //     Student.CurrentChanged += Exam_CurrentChanged;
                    StudentsList.CurrentChanged += currentChanged;
                    Questions.CurrentChanged += currentChanged;
                    //    Answers.CurrentChanged += Exam_CurrentChanged;
                    //      Preferences.CurrentChanged += Exam_CurrentChanged;
                    LogPref.CurrentChanged += currentChanged;
                    //      Order.CurrentChanged += Exam_CurrentChanged;
                    //     Class.CurrentChanged += Exam_CurrentChanged;
                    //     AYear.CurrentChanged += Exam_CurrentChanged;

                    Topics.CurrentChanged += currentChanged;
                }
                else
                {
                    //    Exam.CurrentChanged += Exam_CurrentChanged;
                    ExamsList.CurrentChanged -= currentChanged;
                    //     Student.CurrentChanged += Exam_CurrentChanged;
                    StudentsList.CurrentChanged -= currentChanged;
                    Questions.CurrentChanged -= currentChanged;
                    //    Answers.CurrentChanged += Exam_CurrentChanged;
                    //      Preferences.CurrentChanged += Exam_CurrentChanged;
                    LogPref.CurrentChanged -= currentChanged;
                    //      Order.CurrentChanged += Exam_CurrentChanged;
                    //     Class.CurrentChanged += Exam_CurrentChanged;
                    //     AYear.CurrentChanged += Exam_CurrentChanged;

                    Topics.CurrentChanged -= currentChanged;
                }
            }
        }
        */
        private void currentChanged(object sender, EventArgs e)
        {

            if (Working) return;

            try
            {
                // DataGridView dgv = sender as DataGridView;
                BindingSource bs = sender as BindingSource;
                if (bs.Current == null) return;
                DataRowView view = (bs.Current as DataRowView);
                DataRow row = view.Row;
                if (row == null) return;

                //   byte[] auxiliar = null;

                if (sender.Equals(this.Topics))
                {
                    DB.TopicsRow r = row as DB.TopicsRow;
                    DB.QuestionsDataTable dt = set.Questions;
                    Questions.Filter = dt.TopicIDColumn.ColumnName + " = " + r.TopicID;
                }
                else if (sender.Equals(this.Questions) )
                {
                    DB.QuestionsRow r = row as DB.QuestionsRow;
                    string dt =set.Answers.QIDColumn.ColumnName;
                    Answers.Filter = dt + " = " + r.QID;
                }
                else if ( sender.Equals(this.RandomQuestions))
                {
                    DB.QuestionsRow r = row as DB.QuestionsRow;
                    string dt = auxiliar.Answers.QIDColumn.ColumnName;
                    RandomAnswers.Filter = dt + " = " + r.QID;
                }
                else if (sender.Equals(this.Exam))
                {
                    string dt = auxiliar.Questions.QIDColumn.ColumnName;
                    DB.ExamsRow r = row as DB.ExamsRow;
                    //        string[] aux  = r.AIDString.Split(',');
                    RandomQuestions.Position= RandomQuestions.Find(dt, r.QID);
                    Questions.Position = Questions.Find(dt, r.QID);

                 
                }
                else if (sender.Equals(this.StudentsList))
                {
                    DB.StuListRow r = row as DB.StuListRow;
                    DB.StudentDataTable dt = set.Student;
                    Student.Filter = dt.EIDColumn.ColumnName + " = " + r.SLID;
              
                    //     DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);

                }
                else if (sender.Equals(this.ExamsList))
                {
                    DB.ExamsListRow r = row as DB.ExamsListRow;
                    //     DB.ExamsDataTable dt = set.Exams;
                    Exam.Filter = auxiliar.Exams.EIDColumn.ColumnName + " = " + r.EID;
                    Working = true;
                    auxiliar.PopulateExamQuestions(ref r);
                    Working = false;
                    Exam.MoveFirst();
            

                }
                else if (sender.Equals(this.LogPref))
                {
                    DB.PreferencesRow r = row as DB.PreferencesRow;
                    DB.ExamsListDataTable dt = set.ExamsList;

                    ExamsList.Filter = dt.PIDColumn.ColumnName + " = " + r.PID;

                    DB.TAM.ExamsListTableAdapter.FillByPID(dt, r.PID);
                    /*
                    if (!r.IsELDataNull())
                    {
                       // string pid = ExasmPath + r.PID.ToString() + ".xml";
                        auxiliar = r.ELData;
                        Tables.ReadDTBytes(ExasmPath,ref auxiliar, ref dt);
                    }
                    else
                    {
                        DB.TAM.ExamsListTableAdapter.FillByPID(dt, r.PID);
                        // MakeTableBytes(ref r);
                    }
                    */
                }
            }
            catch (SystemException ex)
            {
            }
        }

      

        public void EndEdit()
        {

            foreach (var item in ls)
            {
                item.EndEdit();
            }

        }

        private string asc = " asc";
        private string desc = " desc";


        private List<BindingSource> ls;

        public BS(ref DB dataset)
        {
        

            set = dataset;
            auxiliar = dataset.Clone() as DB;

            ls = new List<BindingSource>();

         
            ExamsList = new BindingSource(set, set.ExamsList.TableName);
            Student = new BindingSource(set, set.Student.TableName);
            StudentsList = new BindingSource(set, set.StuList.TableName);
            Questions = new BindingSource(set, set.Questions.TableName);
            Answers = new BindingSource(set, set.Answers.TableName);

            Exam = new BindingSource(auxiliar, auxiliar.Exams.TableName);
            RandomQuestions = new BindingSource(auxiliar, auxiliar.Questions.TableName);
            RandomAnswers = new BindingSource(auxiliar, auxiliar.Answers.TableName);

            Preferences = new BindingSource(set, set.Preferences.TableName);
            LogPref = new BindingSource(set, set.Preferences.TableName);
   
            Class = new BindingSource(set, set.Class.TableName);
            AYear = new BindingSource(set, set.AYear.TableName);

            Topics = new BindingSource(set, set.Topics.TableName);

            //    Order = new BindingSource(set, set.Order.TableName);

            ls.Add(Exam);
            ls.Add(ExamsList);
            ls.Add(Student);
            ls.Add(StudentsList);
            ls.Add(Questions);
            ls.Add(Answers);
            ls.Add(RandomQuestions);
            ls.Add(RandomAnswers);
            ls.Add(Topics);
            ls.Add(LogPref);
            ls.Add(Preferences);
            ls.Add(Class);
            ls.Add(AYear);


            Working = false;

                Exam.CurrentChanged += currentChanged;
            ExamsList.CurrentChanged += currentChanged;
            //     Student.CurrentChanged += Exam_CurrentChanged;
            StudentsList.CurrentChanged += currentChanged;
            RandomQuestions.CurrentChanged += currentChanged;
            Questions.CurrentChanged += currentChanged;
            //    Answers.CurrentChanged += Exam_CurrentChanged;
            //      Preferences.CurrentChanged += Exam_CurrentChanged;
            LogPref.CurrentChanged += currentChanged;
            //      Order.CurrentChanged += Exam_CurrentChanged;
            //     Class.CurrentChanged += Exam_CurrentChanged;
            //     AYear.CurrentChanged += Exam_CurrentChanged;

            Topics.CurrentChanged += currentChanged;


            setFilters();



         

        }

        private void setFilters()
        {

            // preferences is done=FALSE as we want to create
            Preferences.Filter = set.Preferences.DoneColumn.ColumnName + " = false";
            // log is preferences executed (exams generated)
            this.LogPref.Filter = set.Preferences.DoneColumn.ColumnName + " = true";
            this.LogPref.Sort = set.Preferences.DateTimeColumn.ColumnName + desc;

            this.Class.Sort = set.Class.ClassColumn.ColumnName + desc;
            this.AYear.Sort = set.AYear.AYearIDColumn.ColumnName + asc;
            this.Topics.Sort = set.Topics.TopicColumn.ColumnName + asc;

            this.ExamsList.Sort = set.ExamsList.EIDColumn.ColumnName + desc;
            this.Exam.Sort = set.Exams.IDColumn.ColumnName + asc;
            this.Answers.Sort = set.Answers.CorrectColumn.ColumnName + asc;
            this.Questions.Sort = set.Questions.QIDColumn.ColumnName + asc;
            this.RandomAnswers.Sort = string.Empty;
            this.RandomQuestions.Sort = string.Empty;
            //    this.Order.Sort = set.Order.IDColumn.ColumnName + asc;
        }
    }
}