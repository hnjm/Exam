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
        private void dGV_SelectionChanged(object sender, EventArgs e)
        {
            if (working) return;

            try
            {
                DataGridView dgv = sender as DataGridView;
                BindingSource bs = dgv.DataSource as BindingSource;
                if (bs.Current == null) return;
                DataRowView view = (bs.Current as DataRowView);
                DataRow row = view.Row;
                if (row == null) return;

                byte[] auxiliar = null;

                if (sender.Equals(this.questionsDataGridView))
                {
                    DB.QuestionsRow r = row as DB.QuestionsRow;
                    this.answersBS.Filter = this.dB.Answers.QIDColumn.ColumnName + " = " + r.QID;
                }
                else if (sender.Equals(this.examsDataGridView))
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
                else if (sender.Equals(this.examsListDGV))
                {
                    DB.ExamsListRow r = row as DB.ExamsListRow;
                    DB.ExamsDataTable dt = this.dB.Exams;

                    dt.Clear();
                    dt.AcceptChanges();

                    this.examsBS.Filter = this.dB.Exams.EIDColumn.ColumnName + " = " + r.EID;

                    //now find the examsTablewith questions and answers for the EXAM (list)
                    if (!r.IsEDataNull())
                    {
                        string eid = ExasmPath + r.EID.ToString() + ".xml";
                        auxiliar = r.EData;

                        Dumb.ReadDTBytes(eid, ref auxiliar, ref dt);
                    }
                    else
                    {
                        DB.TAM.ExamsTableAdapter.FillByEID(dt, r.EID);
                        //   MakeTableBytes(ref r);
                    }
                }
                else if (sender.Equals(this.logDGV))
                {
                    DB.PreferencesRow r = row as DB.PreferencesRow;
                    DB.ExamsListDataTable dt = this.dB.ExamsList;

                    dt.Clear();
                    dt.AcceptChanges();

                    this.examsListBS.Filter = this.dB.ExamsList.PIDColumn.ColumnName + " = " + r.PID;

                    if (!r.IsELDataNull())
                    {
                        string pid = ExasmPath + r.PID.ToString() + ".xml";
                        auxiliar = r.ELData;
                        Dumb.ReadDTBytes(pid, ref auxiliar, ref dt);
                    }
                    else
                    {
                        DB.TAM.ExamsListTableAdapter.FillByPID(dt, r.PID);
                        //  MakeTableBytes(ref r);
                    }
                }
            }
            catch (SystemException ex)
            {

            }
        }

        private void examsListDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DB.ExamsListRow r = (this.examsListBS.Current as DataRowView).Row as DB.ExamsListRow;

            string destFile = ExasmPath + model + r.GUID + pdfExt; //ok

            if (!r.IsExamFileNull())
            {
                byte[] arr = r.ExamFile;
                Dumb.WriteBytesFile(ref arr, destFile);
            }

            Rsx.Dumb.Process(new System.Diagnostics.Process(), ExasmPath, "explorer.exe", destFile, true, false, 10000);
        }

    }
}