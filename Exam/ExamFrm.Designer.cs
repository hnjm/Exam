namespace Exam
{
    partial class ExamFrm
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
            this.tab = new System.Windows.Forms.TabControl();
            this.prefTab = new System.Windows.Forms.TabPage();
            this.ucGenerator1 = new Exam.ucGenerator();
            this.dbTab = new System.Windows.Forms.TabPage();
            this.ucDataBase1 = new Exam.ucDataBase();
            this.EvalTab = new System.Windows.Forms.TabPage();
            this.ucValidator1 = new Exam.ucValidator();
            this.ExamTab = new System.Windows.Forms.TabPage();
            this.ucExamGUI = new Exam.ucDataBase();
            this.TLP = new System.Windows.Forms.TableLayoutPanel();
            this.ctrl1 = new Exam.Ctrl();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tab.SuspendLayout();
            this.prefTab.SuspendLayout();
            this.dbTab.SuspendLayout();
            this.EvalTab.SuspendLayout();
            this.ExamTab.SuspendLayout();
            this.TLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.TLP.SetColumnSpan(this.tab, 2);
            this.tab.Controls.Add(this.prefTab);
            this.tab.Controls.Add(this.dbTab);
            this.tab.Controls.Add(this.EvalTab);
            this.tab.Controls.Add(this.ExamTab);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(3, 89);
            this.tab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1288, 647);
            this.tab.TabIndex = 0;
            // 
            // prefTab
            // 
            this.prefTab.BackColor = System.Drawing.Color.Gray;
            this.prefTab.Controls.Add(this.ucGenerator1);
            this.prefTab.Location = new System.Drawing.Point(4, 33);
            this.prefTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prefTab.Name = "prefTab";
            this.prefTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prefTab.Size = new System.Drawing.Size(1280, 610);
            this.prefTab.TabIndex = 2;
            this.prefTab.Text = "Generador";
            // 
            // ucGenerator1
            // 
            this.ucGenerator1.BackColor = System.Drawing.Color.Gray;
            this.ucGenerator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGenerator1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucGenerator1.Location = new System.Drawing.Point(3, 4);
            this.ucGenerator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucGenerator1.Name = "ucGenerator1";
            this.ucGenerator1.Size = new System.Drawing.Size(1274, 602);
            this.ucGenerator1.TabIndex = 0;
            // 
            // dbTab
            // 
            this.dbTab.BackColor = System.Drawing.Color.Gray;
            this.dbTab.Controls.Add(this.ucDataBase1);
            this.dbTab.Location = new System.Drawing.Point(4, 33);
            this.dbTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dbTab.Name = "dbTab";
            this.dbTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dbTab.Size = new System.Drawing.Size(1280, 610);
            this.dbTab.TabIndex = 1;
            this.dbTab.Text = "Base de Datos";
            // 
            // ucDataBase1
            // 
            this.ucDataBase1.BackColor = System.Drawing.Color.Gray;
            this.ucDataBase1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDataBase1.ExamGUI = false;
            this.ucDataBase1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucDataBase1.Location = new System.Drawing.Point(3, 4);
            this.ucDataBase1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucDataBase1.Name = "ucDataBase1";
            this.ucDataBase1.Size = new System.Drawing.Size(1274, 602);
            this.ucDataBase1.TabIndex = 0;
            // 
            // EvalTab
            // 
            this.EvalTab.Controls.Add(this.ucValidator1);
            this.EvalTab.Location = new System.Drawing.Point(4, 33);
            this.EvalTab.Name = "EvalTab";
            this.EvalTab.Size = new System.Drawing.Size(1280, 610);
            this.EvalTab.TabIndex = 3;
            this.EvalTab.Text = "Evaluador";
            this.EvalTab.UseVisualStyleBackColor = true;
            // 
            // ucValidator1
            // 
            this.ucValidator1.BackColor = System.Drawing.Color.Gray;
            this.ucValidator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucValidator1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucValidator1.Location = new System.Drawing.Point(0, 0);
            this.ucValidator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucValidator1.Name = "ucValidator1";
            this.ucValidator1.Size = new System.Drawing.Size(1280, 610);
            this.ucValidator1.TabIndex = 0;
            // 
            // ExamTab
            // 
            this.ExamTab.Controls.Add(this.ucExamGUI);
            this.ExamTab.Location = new System.Drawing.Point(4, 33);
            this.ExamTab.Name = "ExamTab";
            this.ExamTab.Padding = new System.Windows.Forms.Padding(3);
            this.ExamTab.Size = new System.Drawing.Size(1280, 610);
            this.ExamTab.TabIndex = 4;
            this.ExamTab.Text = "Examen GUI";
            this.ExamTab.UseVisualStyleBackColor = true;
            // 
            // ucExamGUI
            // 
            this.ucExamGUI.BackColor = System.Drawing.Color.Gray;
            this.ucExamGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucExamGUI.ExamGUI = true;
            this.ucExamGUI.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucExamGUI.Location = new System.Drawing.Point(3, 3);
            this.ucExamGUI.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucExamGUI.Name = "ucExamGUI";
            this.ucExamGUI.Size = new System.Drawing.Size(1274, 604);
            this.ucExamGUI.TabIndex = 1;
            // 
            // TLP
            // 
            this.TLP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TLP.ColumnCount = 2;
            this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.8779F));
            this.TLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.1221F));
            this.TLP.Controls.Add(this.tab, 0, 2);
            this.TLP.Controls.Add(this.ctrl1, 0, 0);
            this.TLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLP.Location = new System.Drawing.Point(0, 0);
            this.TLP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TLP.Name = "TLP";
            this.TLP.RowCount = 3;
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.739811F));
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.26019F));
            this.TLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLP.Size = new System.Drawing.Size(1294, 740);
            this.TLP.TabIndex = 0;
            // 
            // ctrl1
            // 
            this.ctrl1.BackColor = System.Drawing.Color.Gray;
            this.TLP.SetColumnSpan(this.ctrl1, 2);
            this.ctrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl1.Location = new System.Drawing.Point(3, 4);
            this.ctrl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ctrl1.Name = "ctrl1";
            this.TLP.SetRowSpan(this.ctrl1, 2);
            this.ctrl1.Size = new System.Drawing.Size(1288, 77);
            this.ctrl1.TabIndex = 1;
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ExamFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1294, 740);
            this.Controls.Add(this.TLP);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExamFrm";
            this.Text = "Generador de Exámenes del  Departamento de Física - Universidad Simón Bolívar    " +
    "                                            Contacto: Fulvio Farina / fulviofari" +
    "na@usb.ve";
            this.tab.ResumeLayout(false);
            this.prefTab.ResumeLayout(false);
            this.dbTab.ResumeLayout(false);
            this.EvalTab.ResumeLayout(false);
            this.ExamTab.ResumeLayout(false);
            this.TLP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TableLayoutPanel TLP;
        private System.Windows.Forms.TabPage dbTab;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private Ctrl ctrl1;
        private System.Windows.Forms.TabPage prefTab;
        private ucGenerator ucGenerator1;
        private ucDataBase ucDataBase1;
        private System.Windows.Forms.TabPage EvalTab;
        private ucValidator ucValidator1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage ExamTab;
        private ucDataBase ucExamGUI;
        //  private Exam exam1;
    }
}

