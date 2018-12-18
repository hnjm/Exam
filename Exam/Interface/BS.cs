using System;
using System.Data;
using System.Windows.Forms;

namespace Exam
{
    public class BS
    {
        private DB set;

        public BindingSource Answers { get; private set; }

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
        public BindingSource Exam { get; private set; }

        public BindingSource ExamsList { get; private set; }

        public BindingSource LogPref { get; private set; }

        public BindingSource Order { get; private set; }

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
        private void Exam_CurrentChanged(object sender, EventArgs e)
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

                byte[] auxiliar = null;

                if (sender.Equals(this.Questions))
                {
                    DB.QuestionsRow r = row as DB.QuestionsRow;
                    DB.AnswersDataTable dt = set.Answers;

                 //   dt.Clear();
                  //  dt.AcceptChanges();

                    Answers.Filter = dt.QIDColumn.ColumnName + " = " + r.QID;
                }
                else if (sender.Equals(this.Exam))
                {
                    /*
                    DB.ExamsRow r = row as DB.ExamsRow;

                    if (!r.IsADataNull())
                    {
                        string aid = this.ExasmPath + r.ID.ToString() + ".xml";
                        auxiliar = r.AData;

                        DB.AnswersDataTable dt = new DB.AnswersDataTable();

                        Dumb.ReadDTBytes(aid, ref auxiliar, ref dt);

                        DataSet  set = new DataSet();

                        set.Tables.Add(dt);
                        Explorer explorer = new Explorer(ref set);
                        Form f = new Form();
                        f.Controls.Add(explorer);
                        f.Show();
                    }
                    else
                    {
                        //DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);
                     //   MakeTableBytes(ref r);
                    }
                     *
                     */
                }
                else if (sender.Equals(this.ExamsList))
                {
                    DB.ExamsListRow r = row as DB.ExamsListRow;
                    DB.ExamsDataTable dt = set.Exams;

                 //   dt.Clear();
               //     dt.AcceptChanges();

                    Exam.Filter = dt.EIDColumn.ColumnName + " = " + r.EID;

                    DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);

                    /*
                    //now find the examsTablewith questions and answers for the EXAM (list)
                   if (!r.IsEDataNull())
                    {
                      // string eid = ExasmPath + r.EID.ToString() + ".xml";
                        auxiliar = r.EData;

                        Tables.ReadDTBytes(ExasmPath, ref auxiliar, ref dt);
                    }
                    else
                    {
                        DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);
                        // MakeTableBytes(ref r);
                    }
                   */
                }
                else if (sender.Equals(this.LogPref))
                {
                    DB.PreferencesRow r = row as DB.PreferencesRow;
                    DB.ExamsListDataTable dt = set.ExamsList;

                 //   dt.Clear();
                   // dt.AcceptChanges();

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

        string asc = " asc";
        string desc = " desc";

        public BS(ref DB dataset)
        {
            Working = false;

            set = dataset;

            Exam = new BindingSource(set, set.Exams.TableName);
            ExamsList = new BindingSource(set, set.ExamsList.TableName);
            Student = new BindingSource(set, set.Student.TableName);
            StudentsList = new BindingSource(set, set.StuList.TableName);
            Questions = new BindingSource(set, set.Questions.TableName);
            Answers = new BindingSource(set, set.Answers.TableName);
            Preferences = new BindingSource(set, set.Preferences.TableName);
            LogPref = new BindingSource(set, set.Preferences.TableName);
            Order = new BindingSource(set, set.Order.TableName);
            Class = new BindingSource(set, set.Class.TableName);
            AYear = new BindingSource(set, set.AYear.TableName);

            Exam.CurrentChanged += Exam_CurrentChanged;
            ExamsList.CurrentChanged += Exam_CurrentChanged;
            Student.CurrentChanged += Exam_CurrentChanged;
            StudentsList.CurrentChanged += Exam_CurrentChanged;
            Questions.CurrentChanged += Exam_CurrentChanged;
            Answers.CurrentChanged += Exam_CurrentChanged;
            Preferences.CurrentChanged += Exam_CurrentChanged;
            LogPref.CurrentChanged += Exam_CurrentChanged;
            Order.CurrentChanged += Exam_CurrentChanged;
            Class.CurrentChanged += Exam_CurrentChanged;
            AYear.CurrentChanged += Exam_CurrentChanged;



         
            // preferences is done=FALSE as we want to create
            Preferences.Filter = set.Preferences.DoneColumn.ColumnName + " = false";
            // log is preferences executed (exams generated)
            this.LogPref.Filter = set.Preferences.DoneColumn.ColumnName + " = true";
            this.LogPref.Sort = set.Preferences.DateTimeColumn.ColumnName + desc;

            this.Class.Sort = set.Class.ClassColumn.ColumnName + desc;
            this.AYear.Sort = set.AYear.AYearIDColumn.ColumnName + asc;


            this.ExamsList.Sort = set.ExamsList.EIDColumn.ColumnName + desc;
            this.Exam.Sort = set.Exams.IDColumn.ColumnName + asc;
            this.Answers.Sort = set.Answers.CorrectColumn.ColumnName + asc;
            this.Questions.Sort = set.Questions.QIDColumn.ColumnName + asc;
            this.Order.Sort = set.Order.IDColumn.ColumnName + asc;


        }
    }
}