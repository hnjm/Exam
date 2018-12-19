using System;
using System.Windows.Forms;
using VTools;

namespace Exam
{
    public partial class ucGenerator : UserControl
    {
        private Interface Interface;

     

        public void Set(ref Interface inter)
        {
            Rsx.Dumb.Dumb.FD(ref dB);

            Interface = inter;


            setDGVs();

            setBindings();

          
        }

       

        private void examsListDGV_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Interface.IGenerator.OpenFile();
        }

        private void setBindings()
        {
            String txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;

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
        
            this.preferencesDGV.DataSource = Interface.IBS.Preferences;
            this.logDGV.DataSource = Interface.IBS.LogPref;

      
          
            this.preferencesBS.Dispose();
            this.logBS.Dispose();

            this.examsBS.Dispose();
            this.examsListBS.Dispose();
       
        }

       

        public ucGenerator()
        {
            InitializeComponent();
        }
    }
}