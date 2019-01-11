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
        

            Interface = inter;

            this.ctrl1.Set(ref inter);
            this.ucGenerator1.Set(ref inter);
            this.ucDataBase1.Set(ref inter);
            this.ucExamGUI.Set(ref inter);
            this.ucValidator1.Set(ref inter);

            setMenues();

         //   Rsx.Dumb.Dumb.FD(ref dB);

            this.Load += load;
        }

        private void deleteExams_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.DeleteExams();

            Interface.IGenerator.DeleteExamsFiles();
        }


        private void generateExams_Click(object sender, EventArgs e)
        {
            Interface.Save();
            Interface.IGenerator.DoExams();
        }

        private void generateList_Click(object sender, EventArgs e)
        {
            // Interface.IGenerator.MakeEvaluationList();
            DB.PreferencesRow p = Interface.IBS.CurrentPreference;
          DialogResult r =  openFileDialog1.ShowDialog();
            if (r == DialogResult.OK)
            {
                Interface.IGenerator.MakeList(openFileDialog1.FileName, p.Class, p.AYearID);
            }
        }


        private void generateQuestion_Clicked(object sender, EventArgs e)
        {
            Interface.IGenerator.GenerateQuestions(1); //genera 1 pregunta
        }

        private void load(object sender, EventArgs e)
        {
            this.ctrl1.IMenu.Load();
        }


      

        /// <summary>
        /// OK
        /// </summary>
     

        private void setMenues()
        {
            object[] arr = ucGenerator1.DGVs;
            foreach (var item in arr)
            {
                DataGridView v = item as DataGridView;
                this.IMenu.SetDGVs(ref v);
            }
            arr = ucDataBase1.DGVs;
            foreach (var item in arr)
            {
                DataGridView v = item as DataGridView;
                this.IMenu.SetDGVs(ref v);
            }


            EventHandler h1 = this.generateExams_Click;
             EventHandler h2 = this.generateList_Click;

            EventHandler h4 = this.generateQuestion_Clicked;
            EventHandler h3 = this.deleteExams_Clicked;
            this.IMenu.SetMenues("Generar Exámenes", ref h1);
             this.IMenu.SetMenues("Generar Listas", ref h2);
            this.IMenu.SetMenues("Limpiar Base de Datos de Exámenes", ref h3);
            this.IMenu.SetMenues("Agregar una pregunta", ref h4);
        }

        public ExamFrm()
        {
            InitializeComponent();
        }
    }
}