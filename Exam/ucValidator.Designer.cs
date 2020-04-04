namespace Exam
{
    partial class ucValidator
    {
        private System.Windows.Forms.ToolStripComboBox carneBox;

        private System.Windows.Forms.ToolStripLabel carnelbl;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridViewTextBoxColumn Correct;

        private System.Windows.Forms.ToolStripTextBox CorrectBox;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

        private DB dB;

        private System.Windows.Forms.DataGridViewTextBoxColumn Error;

        private System.Windows.Forms.ToolStripTextBox nameBox;

        private System.Windows.Forms.ToolStripLabel namelbl;

        private System.Windows.Forms.PictureBox picBox;

        private System.Windows.Forms.DataGridViewImageColumn QRCode;

        private System.Windows.Forms.ToolStripLabel resultlbl;

        private System.Windows.Forms.ToolStripTextBox scoreBox;

        private System.Windows.Forms.BindingSource studentBS;

        private System.Windows.Forms.DataGridView studentDataGridView;

        private System.Windows.Forms.DataGridViewTextBoxColumn StudentID;

        private System.Windows.Forms.BindingSource stuListBS;

        private System.Windows.Forms.DataGridView stuListDGV;

        private System.Windows.Forms.ToolStripTextBox surnameBox;

        private System.Windows.Forms.ToolStripLabel surnamelbl;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;

        private System.Windows.Forms.ToolStrip toolStrip1;

        private System.Windows.Forms.ToolStrip toolStrip2;

        private System.Windows.Forms.ToolStrip toolStrip3;

        private System.Windows.Forms.ToolStrip toolStrip4;

        private System.Windows.Forms.ToolStrip toolStrip5;

        private System.Windows.Forms.ToolStripLabel toolStripLabel1;

        private System.Windows.Forms.ToolStripLabel toolStripLabel2;

        private System.Windows.Forms.ToolStripLabel toolStripLabel5;

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

        private VTools.ucScanner ucScan;

        private System.Windows.Forms.ToolStripButton validator;

        private System.Windows.Forms.ToolStripTextBox verBox;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucValidator));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.scoreBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.CorrectBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.resultlbl = new System.Windows.Forms.ToolStripLabel();
            this.verBox = new System.Windows.Forms.ToolStripTextBox();
            this.validator = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.carnelbl = new System.Windows.Forms.ToolStripLabel();
            this.carneBox = new System.Windows.Forms.ToolStripComboBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.stuListDGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn37 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn36 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn38 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn39 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn32 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stuListBS = new System.Windows.Forms.BindingSource(this.components);
            this.dB = new Exam.DB();
            this.studentDataGridView = new System.Windows.Forms.DataGridView();
            this.QRCode = new System.Windows.Forms.DataGridViewImageColumn();
            this.StudentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Correct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn40 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studentBS = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.namelbl = new System.Windows.Forms.ToolStripLabel();
            this.nameBox = new System.Windows.Forms.ToolStripTextBox();
            this.surnamelbl = new System.Windows.Forms.ToolStripLabel();
            this.surnameBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ucScan = new VTools.ucScanner();
            this.tableLayoutPanel4.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.toolStrip4.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stuListDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stuListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBS)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.40502F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.59498F));
            this.tableLayoutPanel4.Controls.Add(this.toolStrip5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.toolStrip4, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.toolStrip3, 1, 2);
            this.tableLayoutPanel4.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.picBox, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.stuListDGV, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.studentDataGridView, 1, 3);
            this.tableLayoutPanel4.Controls.Add(this.toolStrip1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.ucScan, 1, 6);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 7;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.302325F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.807309F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.970099F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.60133F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.803987F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.04319F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.305648F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1294, 740);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // toolStrip5
            // 
            this.toolStrip5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.scoreBox});
            this.toolStrip5.Location = new System.Drawing.Point(0, 68);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(212, 57);
            this.toolStrip5.TabIndex = 7;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel2.ForeColor = System.Drawing.Color.Cyan;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(52, 54);
            this.toolStripLabel2.Text = "NOTA";
            // 
            // scoreBox
            // 
            this.scoreBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.scoreBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.scoreBox.Margin = new System.Windows.Forms.Padding(47, 0, 1, 0);
            this.scoreBox.Name = "scoreBox";
            this.scoreBox.ReadOnly = true;
            this.scoreBox.Size = new System.Drawing.Size(70, 57);
            this.scoreBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStrip4
            // 
            this.toolStrip4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.CorrectBox});
            this.toolStrip4.Location = new System.Drawing.Point(0, 125);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(212, 66);
            this.toolStrip4.TabIndex = 6;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel5.ForeColor = System.Drawing.Color.DarkOrange;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(98, 63);
            this.toolStripLabel5.Text = "CORRECTAS";
            // 
            // CorrectBox
            // 
            this.CorrectBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CorrectBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorrectBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.CorrectBox.Name = "CorrectBox";
            this.CorrectBox.ReadOnly = true;
            this.CorrectBox.Size = new System.Drawing.Size(70, 66);
            this.CorrectBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStrip3
            // 
            this.toolStrip3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resultlbl,
            this.verBox,
            this.validator});
            this.toolStrip3.Location = new System.Drawing.Point(212, 125);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1082, 66);
            this.toolStrip3.TabIndex = 4;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // resultlbl
            // 
            this.resultlbl.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultlbl.ForeColor = System.Drawing.Color.DarkOrange;
            this.resultlbl.Name = "resultlbl";
            this.resultlbl.Size = new System.Drawing.Size(93, 63);
            this.resultlbl.Text = "RESPUESTA";
            // 
            // verBox
            // 
            this.verBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.verBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.verBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.verBox.Name = "verBox";
            this.verBox.Size = new System.Drawing.Size(800, 66);
            // 
            // validator
            // 
            this.validator.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.validator.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validator.ForeColor = System.Drawing.Color.Gold;
            this.validator.Image = ((System.Drawing.Image)(resources.GetObject("validator.Image")));
            this.validator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.validator.Name = "validator";
            this.validator.Size = new System.Drawing.Size(79, 63);
            this.validator.Text = "VALIDAR";
            this.validator.Click += new System.EventHandler(this.validator_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.carnelbl,
            this.carneBox});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(212, 68);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // carnelbl
            // 
            this.carnelbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.carnelbl.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carnelbl.ForeColor = System.Drawing.Color.GreenYellow;
            this.carnelbl.Name = "carnelbl";
            this.carnelbl.Size = new System.Drawing.Size(61, 65);
            this.carnelbl.Text = "CARNÉ";
            // 
            // carneBox
            // 
            this.carneBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.carneBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.carneBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.carneBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.carneBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.carneBox.Name = "carneBox";
            this.carneBox.Size = new System.Drawing.Size(130, 68);
            this.carneBox.TextChanged += new System.EventHandler(this.carneBox_TextChanged);
            // 
            // picBox
            // 
            this.picBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBox.Location = new System.Drawing.Point(3, 195);
            this.picBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picBox.Name = "picBox";
            this.tableLayoutPanel4.SetRowSpan(this.picBox, 2);
            this.picBox.Size = new System.Drawing.Size(206, 202);
            this.picBox.TabIndex = 2;
            this.picBox.TabStop = false;
            // 
            // stuListDGV
            // 
            this.stuListDGV.AutoGenerateColumns = false;
            this.stuListDGV.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.stuListDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.stuListDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stuListDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn37,
            this.dataGridViewTextBoxColumn36,
            this.dataGridViewTextBoxColumn38,
            this.dataGridViewTextBoxColumn39,
            this.dataGridViewTextBoxColumn32});
            this.stuListDGV.DataSource = this.stuListBS;
            this.stuListDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stuListDGV.EnableHeadersVisualStyles = false;
            this.stuListDGV.Location = new System.Drawing.Point(3, 405);
            this.stuListDGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stuListDGV.Name = "stuListDGV";
            this.stuListDGV.RowHeadersVisible = false;
            this.tableLayoutPanel4.SetRowSpan(this.stuListDGV, 2);
            this.stuListDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.stuListDGV.Size = new System.Drawing.Size(206, 331);
            this.stuListDGV.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn37
            // 
            this.dataGridViewTextBoxColumn37.DataPropertyName = "StudentID";
            this.dataGridViewTextBoxColumn37.HeaderText = "ID";
            this.dataGridViewTextBoxColumn37.Name = "dataGridViewTextBoxColumn37";
            // 
            // dataGridViewTextBoxColumn36
            // 
            this.dataGridViewTextBoxColumn36.DataPropertyName = "Section";
            this.dataGridViewTextBoxColumn36.HeaderText = "S";
            this.dataGridViewTextBoxColumn36.Name = "dataGridViewTextBoxColumn36";
            // 
            // dataGridViewTextBoxColumn38
            // 
            this.dataGridViewTextBoxColumn38.DataPropertyName = "LastNames";
            this.dataGridViewTextBoxColumn38.HeaderText = "Apellidos";
            this.dataGridViewTextBoxColumn38.Name = "dataGridViewTextBoxColumn38";
            // 
            // dataGridViewTextBoxColumn39
            // 
            this.dataGridViewTextBoxColumn39.DataPropertyName = "FirstNames";
            this.dataGridViewTextBoxColumn39.HeaderText = "Nombres";
            this.dataGridViewTextBoxColumn39.Name = "dataGridViewTextBoxColumn39";
            // 
            // dataGridViewTextBoxColumn32
            // 
            this.dataGridViewTextBoxColumn32.DataPropertyName = "Class";
            this.dataGridViewTextBoxColumn32.HeaderText = "Class";
            this.dataGridViewTextBoxColumn32.Name = "dataGridViewTextBoxColumn32";
            this.dataGridViewTextBoxColumn32.Visible = false;
            // 
            // stuListBS
            // 
            this.stuListBS.DataMember = "StuList";
            this.stuListBS.DataSource = this.dB;
            // 
            // dB
            // 
            this.dB.DataSetName = "DB";
            this.dB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // studentDataGridView
            // 
            this.studentDataGridView.AllowUserToAddRows = false;
            this.studentDataGridView.AllowUserToDeleteRows = false;
            this.studentDataGridView.AutoGenerateColumns = false;
            this.studentDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.studentDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.studentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.studentDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QRCode,
            this.StudentID,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.Correct,
            this.Error,
            this.dataGridViewTextBoxColumn20,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn40,
            this.dataGridViewTextBoxColumn9});
            this.studentDataGridView.DataSource = this.studentBS;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.studentDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            this.studentDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.studentDataGridView.EnableHeadersVisualStyles = false;
            this.studentDataGridView.Location = new System.Drawing.Point(215, 195);
            this.studentDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.studentDataGridView.MultiSelect = false;
            this.studentDataGridView.Name = "studentDataGridView";
            this.studentDataGridView.ReadOnly = true;
            this.tableLayoutPanel4.SetRowSpan(this.studentDataGridView, 3);
            this.studentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.studentDataGridView.Size = new System.Drawing.Size(1076, 476);
            this.studentDataGridView.TabIndex = 0;
            // 
            // QRCode
            // 
            this.QRCode.DataPropertyName = "QRCode";
            this.QRCode.HeaderText = "QRCode";
            this.QRCode.Name = "QRCode";
            this.QRCode.ReadOnly = true;
            // 
            // StudentID
            // 
            this.StudentID.DataPropertyName = "StudentID";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.StudentID.DefaultCellStyle = dataGridViewCellStyle3;
            this.StudentID.HeaderText = "Carné";
            this.StudentID.Name = "StudentID";
            this.StudentID.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Name";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn13.HeaderText = "Nombres";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Width = 88;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Surname";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn14.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn14.HeaderText = "Apellidos";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 87;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Score";
            this.dataGridViewTextBoxColumn15.HeaderText = "Nota";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // Correct
            // 
            this.Correct.DataPropertyName = "Correct";
            this.Correct.HeaderText = "Correctas";
            this.Correct.Name = "Correct";
            this.Correct.ReadOnly = true;
            // 
            // Error
            // 
            this.Error.DataPropertyName = "Error";
            this.Error.HeaderText = "Error en";
            this.Error.Name = "Error";
            this.Error.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn20
            // 
            this.dataGridViewTextBoxColumn20.DataPropertyName = "LProvided";
            this.dataGridViewTextBoxColumn20.HeaderText = "Respuesta";
            this.dataGridViewTextBoxColumn20.Name = "dataGridViewTextBoxColumn20";
            this.dataGridViewTextBoxColumn20.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Obs";
            this.dataGridViewTextBoxColumn17.HeaderText = "Obs";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn40
            // 
            this.dataGridViewTextBoxColumn40.DataPropertyName = "GUID";
            this.dataGridViewTextBoxColumn40.HeaderText = "GUID";
            this.dataGridViewTextBoxColumn40.Name = "dataGridViewTextBoxColumn40";
            this.dataGridViewTextBoxColumn40.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "EID";
            this.dataGridViewTextBoxColumn9.HeaderText = "EID";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // studentBS
            // 
            this.studentBS.DataMember = "Student";
            this.studentBS.DataSource = this.dB;
            this.studentBS.Filter = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.namelbl,
            this.nameBox,
            this.surnamelbl,
            this.surnameBox});
            this.toolStrip1.Location = new System.Drawing.Point(212, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1082, 68);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // namelbl
            // 
            this.namelbl.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namelbl.ForeColor = System.Drawing.Color.GreenYellow;
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(86, 65);
            this.namelbl.Text = "NOMBRES";
            // 
            // nameBox
            // 
            this.nameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.nameBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.nameBox.Name = "nameBox";
            this.nameBox.ReadOnly = true;
            this.nameBox.Size = new System.Drawing.Size(400, 68);
            this.nameBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // surnamelbl
            // 
            this.surnamelbl.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surnamelbl.ForeColor = System.Drawing.Color.GreenYellow;
            this.surnamelbl.Name = "surnamelbl";
            this.surnamelbl.Size = new System.Drawing.Size(91, 65);
            this.surnamelbl.Text = "APELLIDOS";
            // 
            // surnameBox
            // 
            this.surnameBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.surnameBox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.surnameBox.ForeColor = System.Drawing.Color.LemonChiffon;
            this.surnameBox.Name = "surnameBox";
            this.surnameBox.ReadOnly = true;
            this.surnameBox.Size = new System.Drawing.Size(400, 68);
            this.surnameBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // ucScan
            // 
            this.ucScan.BaudRate = 9600;
            this.ucScan.ComPort = "8";
            this.ucScan.DataBits = 8;
            this.ucScan.DelayTimeFile = 500;
            this.ucScan.DelayTimeMsg = 50;
            this.ucScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucScan.Location = new System.Drawing.Point(216, 680);
            this.ucScan.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ucScan.MaxLength = 8;
            this.ucScan.Name = "ucScan";
            this.ucScan.Result = "";
            this.ucScan.SendContent = null;
            this.ucScan.Size = new System.Drawing.Size(1074, 55);
            this.ucScan.Status = "Connection OPENED";
            this.ucScan.TabIndex = 5;
            // 
            // ucValidator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.tableLayoutPanel4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucValidator";
            this.Size = new System.Drawing.Size(1294, 740);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stuListDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stuListBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentBS)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        //  private Exam exam1;
    }
}

