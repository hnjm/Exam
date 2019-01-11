namespace Exam
{
    partial class Ctrl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ctrl));
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.materiaBox = new System.Windows.Forms.ToolStripComboBox();
            this.buscarBtn = new System.Windows.Forms.ToolStripButton();
            this.TLP = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.ayearIDbox = new System.Windows.Forms.ToolStripComboBox();
            this.aYearBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.yearBox = new System.Windows.Forms.ToolStripComboBox();
            this.statusTS = new System.Windows.Forms.StatusStrip();
            this.pBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statuslbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.ucMenu = new VTools.ucMenuStrip();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip7.SuspendLayout();
            this.TLP.SuspendLayout();
            this.toolStrip8.SuspendLayout();
            this.statusTS.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip7
            // 
            this.toolStrip7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip7.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel4,
            this.materiaBox,
            this.buscarBtn});
            this.toolStrip7.Location = new System.Drawing.Point(736, 45);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip7.Size = new System.Drawing.Size(558, 35);
            this.toolStrip7.TabIndex = 7;
            this.toolStrip7.Text = "toolStrip7";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel4.ForeColor = System.Drawing.Color.GreenYellow;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(79, 32);
            this.toolStripLabel4.Text = "Materia";
          
            // 
            // materiaBox
            // 
            this.materiaBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.materiaBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.materiaBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.materiaBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.materiaBox.ForeColor = System.Drawing.Color.Azure;
            this.materiaBox.Name = "materiaBox";
            this.materiaBox.Size = new System.Drawing.Size(200, 35);
            this.materiaBox.Text = "probando";
            // 
            // buscarBtn
            // 
            this.buscarBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buscarBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.buscarBtn.ForeColor = System.Drawing.Color.Yellow;
            this.buscarBtn.Image = ((System.Drawing.Image)(resources.GetObject("buscarBtn.Image")));
            this.buscarBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buscarBtn.Name = "buscarBtn";
            this.buscarBtn.Size = new System.Drawing.Size(72, 32);
            this.buscarBtn.Text = "Buscar";
            // 
            // TLP
            // 
            this.TLP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TLP.ColumnCount = 2;
            this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.8779F));
            this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.1221F));
            this.TLP.Controls.Add(this.toolStrip8, 0, 1);
            this.TLP.Controls.Add(this.toolStrip7, 1, 1);
            this.TLP.Controls.Add(this.statusTS, 1, 0);
            this.TLP.Controls.Add(this.ucMenu, 0, 0);
            this.TLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP.Location = new System.Drawing.Point(0, 0);
            this.TLP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TLP.Name = "TLP";
            this.TLP.RowCount = 2;
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.37705F));
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.62295F));
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP.Size = new System.Drawing.Size(1294, 80);
            this.TLP.TabIndex = 0;
            // 
            // toolStrip8
            // 
            this.toolStrip8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip8.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel6,
            this.ayearIDbox,
            this.aYearBox,
            this.toolStripLabel7,
            this.yearBox});
            this.toolStrip8.Location = new System.Drawing.Point(0, 45);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip8.Size = new System.Drawing.Size(736, 35);
            this.toolStrip8.TabIndex = 8;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.BackColor = System.Drawing.Color.Gray;
            this.toolStripLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel6.ForeColor = System.Drawing.Color.GreenYellow;
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(77, 32);
            this.toolStripLabel6.Text = "Periodo";
            // 
            // ayearIDbox
            // 
            this.ayearIDbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ayearIDbox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ayearIDbox.ForeColor = System.Drawing.Color.Azure;
            this.ayearIDbox.Name = "ayearIDbox";
            this.ayearIDbox.Size = new System.Drawing.Size(75, 35);
            this.ayearIDbox.Text = "1";
            // 
            // aYearBox
            // 
            this.aYearBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.aYearBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aYearBox.ForeColor = System.Drawing.Color.Azure;
            this.aYearBox.Name = "aYearBox";
            this.aYearBox.ReadOnly = true;
            this.aYearBox.Size = new System.Drawing.Size(200, 35);
            this.aYearBox.Text = "1";
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.BackColor = System.Drawing.Color.Gray;
            this.toolStripLabel7.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel7.ForeColor = System.Drawing.Color.GreenYellow;
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(47, 32);
            this.toolStripLabel7.Text = "Año";
            // 
            // yearBox
            // 
            this.yearBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.yearBox.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearBox.ForeColor = System.Drawing.Color.Azure;
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(90, 35);
            this.yearBox.Text = "2018";
            // 
            // statusTS
            // 
            this.statusTS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.statusTS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusTS.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pBar,
            this.statuslbl});
            this.statusTS.Location = new System.Drawing.Point(736, 0);
            this.statusTS.Name = "statusTS";
            this.statusTS.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusTS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusTS.Size = new System.Drawing.Size(558, 45);
            this.statusTS.TabIndex = 2;
            // 
            // pBar
            // 
            this.pBar.ForeColor = System.Drawing.Color.DarkOrange;
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(100, 39);
            // 
            // statuslbl
            // 
            this.statuslbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.statuslbl.ForeColor = System.Drawing.Color.LemonChiffon;
            this.statuslbl.Name = "statuslbl";
            this.statuslbl.Size = new System.Drawing.Size(296, 40);
            this.statuslbl.Text = "Aquí se muestra el estado del programa";
            // 
            // ucMenu
            // 
            this.ucMenu.AutoSize = true;
            this.ucMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ucMenu.BS = null;
            this.ucMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMenu.Location = new System.Drawing.Point(3, 4);
            this.ucMenu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucMenu.Name = "ucMenu";
            this.ucMenu.Size = new System.Drawing.Size(730, 37);
            this.ucMenu.TabIndex = 3;
            this.ucMenu.TableAM = new object[] {
        null,
        null};
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(86, 56);
            this.toolStripLabel1.Text = "toolStripLabel1";
            // 
            // Ctrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.TLP);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Ctrl";
            this.Size = new System.Drawing.Size(1294, 80);
      //      this.Load += new System.EventHandler(this.form_Load);
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.TLP.ResumeLayout(false);
            this.TLP.PerformLayout();
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            this.statusTS.ResumeLayout(false);
            this.statusTS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel TLP;
     //   private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
     //   private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
      //  private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
      //  private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
       // private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    
      //  private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.StatusStrip statusTS;
        private System.Windows.Forms.ToolStripStatusLabel statuslbl;
    //    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripProgressBar pBar;
    //    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private System.Windows.Forms.ToolStrip toolStrip7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox materiaBox;
        private System.Windows.Forms.ToolStrip toolStrip8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripComboBox yearBox;
        private System.Windows.Forms.ToolStripComboBox ayearIDbox;
        private System.Windows.Forms.ToolStripTextBox aYearBox;
        private System.Windows.Forms.ToolStripButton buscarBtn;
        private VTools.ucMenuStrip ucMenu;
        //  private Exam exam1;
    }
}

