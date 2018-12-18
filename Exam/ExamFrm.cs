using Rsx.Dumb;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Exam
{
    public partial class ExamFrm : Form
    {
        public static Interface Interface { get; set; }

        public ExamFrm()
        {
            InitializeComponent();

            this.dB.Dispose();
            this.dB = null;
            this.dB = Interface.IdB;

            setDGVs();

            SetBindings();

            SetMenues();

            EventHandler callBack = this.scannerDataReceived;
            this.ucScan.Listen(ref callBack);

      

            Interface.StatusHandler += handler;

            Interface.ProgressHandler += progressHandler;




            /*
             string crypto = Rsx.Encryption.AESThenHMAC.SimpleEncryptWithPassword("hola fulvio", pass, null );
             MessageBox.Show(crypto);

             string decript = Rsx.Encryption.AESThenHMAC.SimpleDecryptWithPassword(crypto, pass, 0);
             MessageBox.Show(decript);
              */
        }

        private void progressHandler(object sender, EventArgs e)
        {
            if (sender.Equals(0))
            {
                this.pBar.PerformStep();
            }
            else
            {
                this.pBar.Minimum = 0;
                this.pBar.Value = 0;
                this.pBar.Step = 1;
                this.pBar.Maximum = ((int)sender) * 4;
                this.pBar.PerformStep(); //1
            }
        }

        private void handler(object sender, EventArgs e)
        {
            this.statuslbl.Text = Interface.Status;
            Application.DoEvents();
        }

        private void questionsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != this.FigureFile.Index) return;

            MessageBox.Show("open file");
            // this.richAnsBox.SaveFile(, RichTextBoxStreamType.) this.richAnsBox.LoadFile()
        }

        private void generateQuestion_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.GenerateQuestions(1); //genera 1 pregunta
        }

        private void deleteExams_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.DeleteExams();

            Interface.IGenerator.DeleteExamsFiles();
           
        }

        private void form_Load(object sender, EventArgs e)
        {


            Interface.IGenerator.PopulateBasic();
          
            fillBoxes();

          
            this.yearBox.Text = DateTime.Now.Year.ToString();
            this.ayearIDbox.Text = Interface.IdB.ThisAYear;


        }

        private void fillBoxes()
        {
            UIControl.FillABox(this.ayearIDbox.ComboBox, Interface.IdB.AYearIDList, true, false);
            UIControl.FillABox(this.materiaBox.ComboBox, Interface.IdB.ClassList, true, false);

            IList<string> hs = new List<string>();
            int year = DateTime.Now.Year;
            hs.Add(year.ToString());
            hs.Add((year - 1).ToString());
            hs.Add((year - 2).ToString());

            UIControl.FillABox(this.yearBox.ComboBox, hs, true, false);
        }

        private delegate void UpdateControls();

        private void scannerDataReceived(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                UpdateControls delegado = new UpdateControls(AssociateExamOrStudent);
                this.BeginInvoke(delegado);

                // AssociateExamToStudent();
            }
            else AssociateExamOrStudent();
        }

        private void generateList_Click(object sender, EventArgs e)
        {
            Interface.IGenerator.MakeEvaluationList();
        }

        private void generateExams_Click(object sender, EventArgs e)
        {
            int ayearID = Convert.ToInt32(this.ayearIDbox.Text);
       
            Interface.IGenerator.DoExams(ayearID);
        }

        #region VALIDATION STUDENT

        private void carneBox_TextChanged(object sender, EventArgs e)
        {
            this.verBox.Text = string.Empty;

            string old = carneBox.Text;

       

          this.carneBox.Text =  Interface.IValidator.AssignStudent(old);

        }

        /// <summary>
        /// VALIDACION DE LOS RESULTADOS DE UN ALUMNO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">     </param>
        private void validator_Click(object sender, EventArgs e)
        {
            // this.studentDataGridView.SuspendLayout();
            this.carneBox.Enabled = false;
            string stuID = this.carneBox.Text;
            this.verBox.Enabled = false;

            bool ok = Interface.IValidator.AssignStudentAnswer(verBox.Text);
            ok = ok && Interface.IValidator.ValidateExamStudent();
            if (ok)
            {
                this.carneBox.Text = string.Empty;
                this.carneBox.Text = stuID;
                this.verBox.Text = string.Empty;
            }

            this.carneBox.Enabled = true;
            this.verBox.Enabled = true;
        }

        private void AssociateExamOrStudent() //tiene que ser toda una funcion
        {
            // try {
            string res = this.ucScan.Result; //sumale por si manda el codigo QR en 2 partes
            res.Trim();
            if (string.IsNullOrEmpty(res))
            {
                Interface.Status = "Texto escaneado vacío";
                return;
            }

            Interface.Status = ucScan.Status; //no se si funcionaria

            bool isStuAnsw = Interface.IValidator.IsStudentScanned(res);

            if (isStuAnsw) //but if it was scanned as a StuID and Answer...
            {
                string answerRAW = string.Empty; //answer already in the box (typed manually)
                string stuID = string.Empty; //use as default the carnebox Text

                Interface.IValidator.GetScannedData(res, ref stuID, ref answerRAW);
                res = string.Empty; //IMPORTANTISIMO RESETEA LA VARIABLE GLOBAL!!! UNA VEZ ACEPTADO EL CARNET*RESPUESTA

                refreshStuIDAndAnswer(answerRAW, stuID);

                Interface.IValidator.AssignStudentAnswer(answerRAW); //assign answer to student
                
                // FindByStudentID(stuID); //finds the student.
            }
            else
            {
                //this.picBox.Image = null;

                bool valid = Interface.IValidator.AssignExamModel(res);
                if (valid) res = string.Empty; //IMPORTANTISIMO RESETEA LA VARIABLE GLOBAL!!! UNA VEZ ENCONTRADO EL EXAMEN
                else return; //el codigo QR está siendo mandando por partes!!!
            }

            this.picBox.Image = Interface.IValidator.Image; //it can be null

     
            bool evaluated = false;
            this.carneBox.Enabled = false;
            string studeID = this.carneBox.Text;

            evaluated = Interface.IValidator.ValidateExamStudent(); ///then validate the exam!!!

            if (evaluated)
            {
                refreshStuIDAndAnswer(string.Empty, studeID);
            }

            this.carneBox.Enabled = true;
        }

        private void refreshStuIDAndAnswer(string answerRAW, string stuID)
        {
            this.carneBox.Text = string.Empty;
            this.carneBox.Text = stuID;
            this.verBox.Text = answerRAW;
        }

     

        private void SetMenues()
        {
            //initial binding source to link to
            this.ucMenu.BS = Interface.IBS.Preferences;

            //table adapter
            this.ucMenu.TableAM[0] = DB.TAM;
            this.ucMenu.TableAM[1] = DB.TAMQA;

            //selection change option
            //    this.ucMenu.DgvSelectionChanged = this.dGV_SelectionChanged;

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
          
            EventHandler h4 = this.generateQuestion_Clicked;
            EventHandler h2 = this.form_Load;

            EventHandler h3 = this.deleteExams_Clicked;
            h3 += h2;

            this.ucMenu.SetReload(ref h2);
            // EventHandler h3 = this.convertPDF_Click;
            this.ucMenu.SetMenues("Generar Exámenes", ref h1);
            // this.ucMenu.SetMenues("Generar Listas", ref h2);
            this.ucMenu.SetMenues("Limpiar Base de Datos de Exámenes", ref h3);
            this.ucMenu.SetMenues("Agregar una pregunta", ref h4);
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
            this.studentDataGridView.DataSource = Interface.IBS.Student;
            this.stuListDGV.DataSource = Interface.IBS.StudentsList;
            this.orderDataGridView.DataSource = Interface.IBS.Order;

            this.orderBS.Dispose();
            this.preferencesBS.Dispose();
            this.logBS.Dispose();
            this.studentBS.Dispose();
            this.stuListBS.Dispose();
            this.examsBS.Dispose();
            this.examsListBS.Dispose();
            this.answersBS.Dispose();
            this.questionsBS.Dispose();
        }

        private void SetBindings()
        {




            String txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;



            Binding question = new Binding(txt, Interface.IBS.Questions, Interface.IdB.Questions.QuestionColumn.ColumnName, true, mode);
            Binding answer = new Binding(txt, Interface.IBS.Answers, Interface.IdB.Answers.AnswerColumn.ColumnName, true, mode);

            this.richQuesBox.DataBindings.Add(question);
            this.richAnsBox.DataBindings.Add(answer);


            Binding score = new Binding(txt, Interface.IBS.Student, Interface.IdB.Student.ScoreColumn.ColumnName, true, mode);
            Binding correct = new Binding(txt, Interface.IBS.Student, Interface.IdB.Student.CorrectColumn.ColumnName, true, mode);
            this.scoreBox.TextBox.DataBindings.Add(score);
            this.CorrectBox.TextBox.DataBindings.Add(correct);

            Binding name = new Binding(txt, Interface.IBS.StudentsList, Interface.IdB.StuList.FirstNamesColumn.ColumnName, true, mode);
            Binding surname = new Binding(txt, Interface.IBS.StudentsList, Interface.IdB.StuList.LastNamesColumn.ColumnName, true, mode);
            this.nameBox.TextBox.DataBindings.Add(name);
            this.surnameBox.TextBox.DataBindings.Add(surname);

            Binding title = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.TitleColumn.ColumnName, true, mode);
            Binding Classe = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.ClassColumn.ColumnName, true, mode);
           

            // Binding StudentIDs = new Binding(txt, this.stuListBS,
            // this.dB.StuList.StudentIDColumn.ColumnName, true, mode);

            this.titleBox.TextBox.DataBindings.Add(title);
            this.materiaBox.ComboBox.DataBindings.Add(Classe);

         

          

            Binding yearID = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.AYearIDColumn.ColumnName, true, mode);

            this.ayearIDbox.ComboBox.DataBindings.Add(yearID);


         //   mode = DataSourceUpdateMode.Never;
            Binding ayear = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.AYearColumn.ColumnName, true, mode);

            this.aYearBox.TextBox.DataBindings.Add(ayear);

            //       ayear.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;

            this.studTab.Enter += txtchg;


            this.aYearBox.TextChanged += txtchg;
            this.yearBox.TextChanged += txtchg;
            this.materiaBox.TextChanged += txtchg;

            //NO ENTIENDO
            this.ayearIDbox.TextChanged += delegate
             {
                 try
                 {
                     Interface.IBS.Preferences.EndEdit();
                 }
                 catch (Exception)
                 {

                     //throw;
                 }
               
             //  bool ok =  Interface.IBS.CurrentPreference.HasVersion(System.Data.DataRowVersion.Proposed);
             //     if (ok) Interface.IBS.CurrentPreference.EndEdit();
             };

            //    Binding ayear = new Binding(txt, Interface.IBS.AYear, Interface.IdB.AYear.AYearColumn.ColumnName, true, mode);
            //     this.aYearBox.ComboBox.DataBindings.Add(ayear);

            //   this.aYearBox.ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            //   this.aYearBox.ComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //  this.aYearBox.ComboBox.DataSource=  Interface.IBS.AYear;
            //     this.aYearBox.ComboBox.DisplayMember = Interface.IBS.AYear;

            //   Binding ayear = new Binding(txt, Interface.IBS.AYear, Interface.IdB.AYear.AYearColumn.ColumnName, true, mode);



        }

        private void txtchg(object sender, EventArgs e)
        {
            if (sender.Equals(this.materiaBox)|| sender.Equals(this.buscarBtn) )
            { 
                Interface.IGenerator.FillClassDataBase(this.materiaBox.Text);
            }
            Interface.IGenerator.FillExams(this.materiaBox.Text);
            Interface.IGenerator.FillStudents(this.materiaBox.Text, this.yearBox.Text, this.aYearBox.Text);
            // this.carneBox.AutoCompleteCustomSource.Clear(); this.carneBox.AutoCompleteCustomSource.AddRange(hs.ToArray());
            UIControl.FillABox(this.carneBox.ComboBox, Interface.IdB.StudentsIDList, true, false);

        }

        private void examsListDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Interface.IGenerator.OpenFile();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            string content = this.materiaBox.Text.Trim();
            if (string.IsNullOrEmpty(content)) return;
            Interface.IGenerator.FillClassDataBase(content);
        }

        #endregion VALIDATION STUDENT

        /*
        private void Ge_Click(object sender, EventArgs e)
        {
            IList<DB.ExamsRow> exams = null;
            // DB.ExamsDataTable clone = this.dB.Exams.Copy() as DB.ExamsDataTable;
            string examen = string.Empty;

       // W.Document doc = MakeWord(string.Empty, ref WApp); //makes the word file

            exams = this.dB.Exams.ToList();
            // int i = 1;
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