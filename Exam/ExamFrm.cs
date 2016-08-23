using System;
using System.Data;
using System.Windows.Forms;
using Rsx;

namespace Exam
{
    public partial class ExamFrm : Form
    {
        #region FORM RELATED

        public ExamFrm()
        {
            InitializeComponent();

            DB set = this.dB;
            DB.SetTAM(ref set);

            SetMenues();

            if (!System.IO.Directory.Exists(ExasmPath))
            {
                System.IO.Directory.CreateDirectory(ExasmPath);
            }

            EventHandler callBack = this.scannerDataReceived;
            this.ucScan.MakeScanner(ref callBack);
            this.ucScan.OpenPort = true;

            SetBindings();





            /*
             string crypto = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword("hola fulvio", pass, null );
             MessageBox.Show(crypto);

             string decript = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(crypto, pass, 0);
             MessageBox.Show(decript);
              */
        }

        private void questionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != this.FigureFile.Index) return;

            MessageBox.Show("open file");
            // this.richAnsBox.SaveFile(, RichTextBoxStreamType.)
            // this.richAnsBox.LoadFile()
        }

        private void generateQuestion_Clicked(object sender, EventArgs e)
        {
            GenerateQuestions(1); //genera 1 pregunta
        }

        private void deleteExams_Clicked(object sender, EventArgs e)
        {
            DeleteExams();

            FillTables();
        }

        private void form_Load(object sender, EventArgs e)
        {
            FillTables();
        }

        private void scannerDataReceived(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                UpdateControls delegado = new UpdateControls(AssociateExamOrStudent);
                this.BeginInvoke(delegado);

                //  AssociateExamToStudent();
            }
            else AssociateExamOrStudent();
        }

        private void generateList_Click(object sender, EventArgs e)
        {
            MakeEvaluationList();
        }

        private void generateExams_Click(object sender, EventArgs e)
        {
            DoExams();
        }

        /// <summary>
        /// FILTRADO SELECCION DE DATAGRDIVIEWS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///

        private void dGV_SelectionChanged(object sender, EventArgs e)
        {
            if (working) return;

            try
            {
                DataGridView dgv = sender as DataGridView;
                BindingSource bs = dgv.DataSource as BindingSource;
                if (bs.Current == null) return;
                byte[] auxiliar = null;
                DataRowView view = (bs.Current as DataRowView);
                DataRow row = view.Row;
                if (row == null) return;
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

        #endregion FORM RELATED

        #region VALIDATION STUDENT

        private void carneBox_TextChanged(object sender, EventArgs e)
        {
            this.verBox.Text = string.Empty;
          //  this.verBox.Visible = false;
          //  this.validator.Visible = false;
        //    this.resultlbl.Visible = false;
            //this.verBox.Enabled = false;
            string valu = carneBox.Text;



         
            Tools.StripAnswer(ref valu);
            if (valu.CompareTo(carneBox.Text) != 0)
            {
                carneBox.Text = valu; //re-execute, because the string changed and this is how you find the values
                return;

            }


         //   this.stuListBS.Position = this.stuListBS.Find(this.dB.StuList.StudentIDColumn.ColumnName, valu);

          //  this.picBox.Image = null;

            CarneValidation(valu);


            if (currentStudent != null)
            {
                if (currentStudent.IsScoreNull())
                {
                    //this.verBox.Enabled = true;
                 //   this.verBox.Visible = true;
                   // this.resultlbl.Visible = true;
                    this.statuslbl.Text = Properties.Resources.EstudianteNoEvaluado;
                }
                else
                {
                    this.statuslbl.Text = Properties.Resources.EstudianteEvaluado;

                    // this.picBox.Image = Tools.CreateQRCode(currentStudent.GUID, qrSise);
                }
            }

        }

        /// <summary>
        /// VALIDACION DE LOS RESULTADOS DE UN ALUMNO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validator_Click(object sender, EventArgs e)
        {
            //       this.studentDataGridView.SuspendLayout();
            this.carneBox.Enabled = false;
            string stuID = this.carneBox.Text;

            this.verBox.Enabled = false;

            if (currentStudent != null)
            {

                currentStudent.LProvided = verBox.Text;
                bool ok = ValidateExamStudent();


                if (ok)
                {
                    this.carneBox.Text = string.Empty;
                    this.carneBox.Text = stuID;
                    this.verBox.Text = string.Empty;

                }
            }
            this.carneBox.Enabled = true;
            this.verBox.Enabled = true;



        }
       
    
        private bool CompareAnswerLenght(int lenghtprovided)
        {
         //   if (currentStudent.ExamsListRow == null) return false; //quiere decir que no ha sido escaneado

            string TrueAns = currentExam.CLAnswer;
            TrueAns = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(TrueAns, password, 0);
            Tools.StripAnswer(ref TrueAns);

            if (lenghtprovided == TrueAns.Length)
            {
                this.statuslbl.Text = Properties.Resources.Coincide;
                return true;
            }
            else this.statuslbl.Text = Properties.Resources.NoCoincide;

            return false;
        }

        #endregion VALIDATION STUDENT

        /*
        private void Ge_Click(object sender, EventArgs e)
        {
            IList<DB.ExamsRow> exams = null;
            //       DB.ExamsDataTable clone = this.dB.Exams.Copy() as DB.ExamsDataTable;
            string examen = string.Empty;

       //     W.Document doc = MakeWord(string.Empty, ref WApp); //makes the word file

            exams = this.dB.Exams.ToList();
            //       int i = 1;
            W.Range aux = doc.Paragraphs.Last.Range;

            int pregunta = 1;

            string Clave = string.Empty;

            foreach (DB.ExamsRow r in exams)
            {
                examen = pregunta.ToString() + ") " + r.QuestionsRow.Question + "\n";
                pregunta++;

                aux.Text = examen;
                aux.set_Style(W.WdBuiltinStyle.wdStyleHeading2);

                doc.Paragraphs.Add(aux); // insert onces

                string[] answers = r.AIDString.Split(sep2); ///separates de correct value from the array of answers
                int AIDcorrecto = Convert.ToInt32(answers[1]);
                answers = answers[0].Split(sep); //the array of answers

                IEnumerable<DB.AnswersRow> answ = r.QuestionsRow.GetAnswersRows();
                int option = 0;
                examen = string.Empty;

                foreach (string s in answers)
                {
                    int aid = Convert.ToInt32(s);
                    DB.AnswersRow a = answ.FirstOrDefault(o => o.AID == aid);

                    string respuesta = a.Answer;
                    string letra = alpha[option].ToString();
                    if (a.AID == AIDcorrecto) Clave += letra;

                    examen += letra;
                    examen += ")  " + respuesta + "\n";
                    option++;
                }
                examen += "\n";

                aux = doc.Paragraphs.Last.Range;

                aux.Text = examen; // now you can isert the questions
                aux.set_Style(W.WdBuiltinStyle.wdStyleHeading3);
                doc.Paragraphs.Add(aux);

                aux = doc.Paragraphs.Last.Range;
            }

            aux = doc.Paragraphs.Last.Range;
            aux.Text = "\n\n" + Clave;

            aux.set_Style(W.WdBuiltinStyle.wdStyleFooter);
            doc.Paragraphs.Add(aux);
        }

        */
    }
}