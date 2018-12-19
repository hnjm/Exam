using Rsx.Dumb;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VTools;

namespace Exam
{
    public partial class Ctrl : UserControl
    {
        private Interface Interface { get; set; }

        public IMenu IMenu
        {
            get
            {
                return ucMenu;
            }
        }

        public void Set(ref Interface inter)
        {
            Interface = inter;
            setBindings();

            setMenueBasic();

            Interface.StatusHandler += handler;

            Interface.ProgressHandler += progressHandler;
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

        private void form_Load(object sender, EventArgs e)
        {
            Interface.IGenerator.PopulateBasic();

            fillBoxes();

            this.yearBox.Text = DateTime.Now.Year.ToString();
            this.ayearIDbox.Text = Interface.IdB.ThisAYear;
        }

        private void handler(object sender, EventArgs e)
        {
            this.statuslbl.Text = Interface.Status;
            Application.DoEvents();
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

        private void setBindings()
        {
            string txt = "Text";
            DataSourceUpdateMode mode = DataSourceUpdateMode.OnPropertyChanged;
            Binding Classe = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.ClassColumn.ColumnName, true, mode);
            Binding yearID = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.AYearIDColumn.ColumnName, true, mode);
            Binding ayear = new Binding(txt, Interface.IBS.Preferences, Interface.IdB.Preferences.AYearColumn.ColumnName, true, mode);

            this.materiaBox.ComboBox.DataBindings.Add(Classe);
            this.ayearIDbox.ComboBox.DataBindings.Add(yearID);
            this.aYearBox.TextBox.DataBindings.Add(ayear);

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
             };
        }

        private void setMenueBasic()
        {
            //initial binding source to link to
            this.ucMenu.BS = Interface.IBS.Preferences;
            //table adapter
            this.ucMenu.TableAM[0] = DB.TAM;
            this.ucMenu.TableAM[1] = DB.TAMQA;
            this.ucMenu.Refresher += this.form_Load;
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            string content = this.materiaBox.Text.Trim();
            if (string.IsNullOrEmpty(content)) return;
            Interface.IGenerator.FillClassDataBase(content);
        }

        private void txtchg(object sender, EventArgs e)
        {
            if (sender.Equals(this.materiaBox) || sender.Equals(this.buscarBtn))
            {
                Interface.IGenerator.FillClassDataBase(this.materiaBox.Text);
            }
            Interface.IGenerator.FillExams(this.materiaBox.Text);
            Interface.IGenerator.FillStudents(this.materiaBox.Text, this.yearBox.Text, this.aYearBox.Text);
        }

        public Ctrl()
        {
            InitializeComponent();
        }
    }
}