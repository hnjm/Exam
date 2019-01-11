using Rsx;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam
{
    public partial class DB
    {
        partial class AnswersDataTable
        {
        }

        public partial class ExamsListRow
        {
            public bool NeedsExams
            {

                get
                {
                    return !IsEDataNull() && GetExamsRows().Count() == 0;

                }
            }

        }

        /// <summary>
        /// Must be initialized first, stores Table Adapters list
        /// </summary>
        public static System.Collections.Hashtable hsTA;

        /// <summary>
        /// Must be initialized first
        /// </summary>
        public static DBTableAdapters.TableAdapterManager TAMQA;

        public static DBTableAdapters.TableAdapterManager TAM;

        public IList<string> StudentsIDList
        {
            get
            {
                return Rsx.Dumb.Hash.HashFrom<string>(StuList.StudentIDColumn);
            }
        }
        public string ThisAYear
        {
            get
            {
                DateTime ahora = DateTime.Now;

                int count = this.AYear.Count;
                float monthsPerPeriods = 12 / count;

                //  float monthNow = ahora.Month/12;

                int item = (int)Math.Ceiling(ahora.Month / monthsPerPeriods);

                //     string ayear = this.AYear[item - 1]?.AYear;

                return item.ToString();
            }
        }
        public IList<string> ClassList
        {
            get
            {
                return Rsx.Dumb.Hash.HashFrom<string>(Class.ClassColumn);
            }
        }
        /*
        public IList<string> AYearList
        {
            get
            {
                return Hash.HashFrom<string>(AYear.AYearColumn);
            }
        }
        */
        public IList<string> AYearIDList
        {
            get
            {
                return Rsx.Dumb.Hash.HashFrom<string>(AYear.AYearIDColumn);
            }
        }
        public int? FindAYearID(string ayear)
        {

            return AYear.FirstOrDefault(o => o.AYear.CompareTo(ayear) == 0)?.AYearID;

        }

        /// <summary>
        /// Initializer of TAs, for the given DataSet
        /// </summary>
        /// <param name="dB"></param>
        public static void SetTAM(ref DB dB)
        {
            TAM = new DBTableAdapters.TableAdapterManager();

            TAM.BackupDataSetBeforeUpdate = false;
            TAM.UpdateOrder = DBTableAdapters.TableAdapterManager.UpdateOrderOption.UpdateInsertDelete;

            TAM.ExamsListTableAdapter = new DBTableAdapters.ExamsListTableAdapter();
            TAM.ExamsTableAdapter = new DBTableAdapters.ExamsTableAdapter();
            //    TAM.OrderTableAdapter = new DBTableAdapters.OrderTableAdapter();
            TAM.ClassTableAdapter = new DBTableAdapters.ClassTableAdapter();
            TAM.AYearTableAdapter = new DBTableAdapters.AYearTableAdapter();
            TAM.PreferencesTableAdapter = new DBTableAdapters.PreferencesTableAdapter();
            TAM.StudentTableAdapter = new DBTableAdapters.StudentTableAdapter();
            TAM.StuListTableAdapter = new DBTableAdapters.StuListTableAdapter();



            TAMQA = new DBTableAdapters.TableAdapterManager();


            TAMQA.BackupDataSetBeforeUpdate = false;
            TAMQA.UpdateOrder = DBTableAdapters.TableAdapterManager.UpdateOrderOption.UpdateInsertDelete;

            TAMQA.QuestionsTableAdapter = new DBTableAdapters.QuestionsTableAdapter();
            TAMQA.AnswersTableAdapter = new DBTableAdapters.AnswersTableAdapter();
            TAMQA.TopicsTableAdapter = new DBTableAdapters.TopicsTableAdapter();


            hsTA = new System.Collections.Hashtable();
            hsTA.Add(dB.Questions.TableName, TAMQA.QuestionsTableAdapter);
            hsTA.Add(dB.Answers.TableName, TAMQA.AnswersTableAdapter);
            hsTA.Add(dB.Topics.TableName, TAMQA.TopicsTableAdapter);

            //     hsTA.Add(dB.Exams.TableName, TAM.ExamsTableAdapter);
            hsTA.Add(dB.ExamsList.TableName, TAM.ExamsListTableAdapter);
            hsTA.Add(dB.Preferences.TableName, TAM.PreferencesTableAdapter);
            hsTA.Add(dB.Student.TableName, TAM.StudentTableAdapter);
            hsTA.Add(dB.StuList.TableName, TAM.StuListTableAdapter);
            hsTA.Add(dB.Class.TableName, TAM.ClassTableAdapter);
            hsTA.Add(dB.AYear.TableName, TAM.AYearTableAdapter);



            foreach (dynamic i in hsTA.Values) //use as dynamic because I cannot find the Class
            {
                i.ClearBeforeFill = true;
            }
        }

        internal void PopulateExamQuestions(ref ExamsListRow r)
        {
            //    DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);
            //now find the examsTablewith questions and answers for the EXAM (list)
            //   if (!r.NeedsExams) return;
            // string eid = ExasmPath + r.EID.ToString() + ".xml";


            this.Preferences.Clear();
            this.Preferences.ImportRow(r.PreferencesRow);

            this.Class.Clear();
            this.Class.ImportRow(r.PreferencesRow.ClassRow);

            this.AYear.Clear();
            this.AYear.ImportRow(r.PreferencesRow.AYearRow);


            Environment.SpecialFolder dir = Environment.SpecialFolder.InternetCache;
            string cache = Environment.GetFolderPath(dir) + "\\";
            byte[] auxiliar;
            auxiliar = r.EData;
            ExamsDataTable dt2 = new ExamsDataTable();
            Rsx.Dumb.Tables.ReadDTBytes(cache, ref auxiliar, ref dt2);
            this.Exams.Clear();
            this.Exams.Merge(dt2);
            Rsx.Dumb.Dumb.FD(ref dt2);

            this.ExamsList.Clear();
            this.ExamsList.ImportRow(r);

            r = this.ExamsList.First();
            //         if (r.NeedsExams) return;

            this.Questions.Clear();

            IEnumerable<ExamsRow> rows = r.GetExamsRows();
            foreach (var item in rows)
            {
                auxiliar = item.QData;
                QuestionsDataTable qdt = new QuestionsDataTable();
                Rsx.Dumb.Tables.ReadDTBytes(cache, ref auxiliar, ref qdt);
                this.Questions.Merge(qdt);
                Rsx.Dumb.Dumb.FD(ref qdt);

            }

            this.Answers.Clear();
            //     IEnumerable<ExamsRow> rows = r.GetExamsRows();
            foreach (var item in rows)
            {
                auxiliar = item.AData;
                AnswersDataTable adt = new AnswersDataTable();
                Rsx.Dumb.Tables.ReadDTBytes(cache, ref auxiliar, ref adt);
                this.Answers.Merge(adt);
                Rsx.Dumb.Dumb.FD(ref adt);
            }

            this.AcceptChanges();

        }

        public ExamsListRow AddExam(ref PreferencesRow p)
        {
            ExamsListRow ls = ExamsList.NewExamsListRow();
            ExamsList.AddExamsListRow(ls);
            Guid g = Guid.NewGuid();
            ls.PID = p.PID;
            ////MAKES THE DOC FILE
            ls.GUID = g.ToString().Replace("-", null);//.Split('-')[4];
            ls.Time = DateTime.Now;
            ls.Class = p.Class;
            return ls;
        }
    }
}