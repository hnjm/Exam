using System;
using System.Windows.Forms;
using VTools;

namespace Exam
{
    public partial class ucTopic : UserControl
    {
        private Interface Interface;

     

        public void Set(ref Interface inter, bool displayUse = true)
        {
            Interface = inter;

            setDGVs();
            this.topicDGV.RowHeadersVisible = !displayUse;
            this.UseIt.Visible = displayUse;

            Rsx.Dumb.Dumb.FD(ref dB);

        }
        public object[] DGVs
        {
            get
            {
                return new object[] {  topicDGV };

            }
        }
       
       

     

     
        /// <summary>
        /// OK
        /// </summary>
        private void setDGVs()
        {
            this.topicDGV.DataSource = Interface.IBS.Topics;
            this.topicsBS.Dispose();
       
        }

       

        public ucTopic()
        {
            InitializeComponent();
        }
    }
}