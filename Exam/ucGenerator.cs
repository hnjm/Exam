using System;
using System.Linq;
using System.Windows.Forms;
using VTools;

namespace Exam
{
    public partial class ucGenerator : UserControl
    {
        private Interface Interface;

     

        public void Set(ref Interface inter)
        {
        

            Interface = inter;

            ucTopic1.Set(ref inter);

            setDGVs();

            setBindings();

            Rsx.Dumb.Dumb.FD(ref dB);

        }
        public object[] DGVs
        {
            get
            {
              
                return new object[] { preferencesDGV, examsListDGV, logDGV, ucTopic1.DGVs[0] };
             
            }
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

         }

        /// <summary>
        /// OK
        /// </summary>
        private void setDGVs()
        {
         
            this.examsListDGV.DataSource = Interface.IBS.ExamsList;
        
            this.preferencesDGV.DataSource = Interface.IBS.Preferences;
            this.logDGV.DataSource = Interface.IBS.LogPref;

      
          
            this.preferencesBS.Dispose();
            this.logBS.Dispose();

      
            this.examsListBS.Dispose();
       
        }

       

        public ucGenerator()
        {
            InitializeComponent();
        }
    }
}