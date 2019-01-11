namespace Exam
{
    partial class ucDataBase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tab = new System.Windows.Forms.TabControl();
            this.dbTab = new System.Windows.Forms.TabPage();
            this.dbTLP = new System.Windows.Forms.TableLayoutPanel();
            this.acontainer = new System.Windows.Forms.SplitContainer();
            this.ucTopic1 = new Exam.ucTopic();
            this.answersDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answersBS = new System.Windows.Forms.BindingSource(this.components);
            this.dB = new Exam.DB();
            this.qcontainer = new System.Windows.Forms.SplitContainer();
            this.examsDataGridView = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.qIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aIDStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.examsBS = new System.Windows.Forms.BindingSource(this.components);
            this.questionsDataGridView = new System.Windows.Forms.DataGridView();
            this.questionsBS = new System.Windows.Forms.BindingSource(this.components);
            this.rcontainer = new System.Windows.Forms.SplitContainer();
            this.qrtfbox = new System.Windows.Forms.RichTextBox();
            this.artfbox = new System.Windows.Forms.RichTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FigureFile = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tab.SuspendLayout();
            this.dbTab.SuspendLayout();
            this.dbTLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.acontainer)).BeginInit();
            this.acontainer.Panel1.SuspendLayout();
            this.acontainer.Panel2.SuspendLayout();
            this.acontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.answersDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.answersBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qcontainer)).BeginInit();
            this.qcontainer.Panel1.SuspendLayout();
            this.qcontainer.Panel2.SuspendLayout();
            this.qcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.examsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.examsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcontainer)).BeginInit();
            this.rcontainer.Panel1.SuspendLayout();
            this.rcontainer.Panel2.SuspendLayout();
            this.rcontainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab
            // 
            this.tab.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tab.Controls.Add(this.dbTab);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tab.Multiline = true;
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(1294, 740);
            this.tab.TabIndex = 0;
            // 
            // dbTab
            // 
            this.dbTab.BackColor = System.Drawing.Color.Gray;
            this.dbTab.Controls.Add(this.dbTLP);
            this.dbTab.Location = new System.Drawing.Point(4, 33);
            this.dbTab.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dbTab.Name = "dbTab";
            this.dbTab.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dbTab.Size = new System.Drawing.Size(1286, 703);
            this.dbTab.TabIndex = 1;
            this.dbTab.Text = "Base de Datos";
            // 
            // dbTLP
            // 
            this.dbTLP.AutoScroll = true;
            this.dbTLP.ColumnCount = 3;
            this.dbTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dbTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dbTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dbTLP.Controls.Add(this.acontainer, 2, 0);
            this.dbTLP.Controls.Add(this.qcontainer, 0, 0);
            this.dbTLP.Controls.Add(this.rcontainer, 1, 0);
            this.dbTLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dbTLP.Location = new System.Drawing.Point(3, 4);
            this.dbTLP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dbTLP.Name = "dbTLP";
            this.dbTLP.RowCount = 2;
            this.dbTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dbTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 93F));
            this.dbTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dbTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dbTLP.Size = new System.Drawing.Size(1280, 695);
            this.dbTLP.TabIndex = 0;
            // 
            // acontainer
            // 
            this.acontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.acontainer.Location = new System.Drawing.Point(659, 3);
            this.acontainer.Name = "acontainer";
            this.acontainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // acontainer.Panel1
            // 
            this.acontainer.Panel1.Controls.Add(this.ucTopic1);
            this.acontainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // acontainer.Panel2
            // 
            this.acontainer.Panel2.Controls.Add(this.answersDataGridView);
            this.acontainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.acontainer.Size = new System.Drawing.Size(618, 596);
            this.acontainer.SplitterDistance = 167;
            this.acontainer.TabIndex = 5;
            // 
            // ucTopic1
            // 
            this.ucTopic1.BackColor = System.Drawing.Color.Gray;
            this.ucTopic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTopic1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ucTopic1.Location = new System.Drawing.Point(0, 0);
            this.ucTopic1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ucTopic1.Name = "ucTopic1";
            this.ucTopic1.Size = new System.Drawing.Size(618, 167);
            this.ucTopic1.TabIndex = 3;
            // 
            // answersDataGridView
            // 
            this.answersDataGridView.AllowUserToAddRows = false;
            this.answersDataGridView.AllowUserToDeleteRows = false;
            this.answersDataGridView.AutoGenerateColumns = false;
            this.answersDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.answersDataGridView.BackgroundColor = System.Drawing.Color.Wheat;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.answersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.answersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.answersDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn4,
            this.AID});
            this.answersDataGridView.DataSource = this.answersBS;
            this.answersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.answersDataGridView.EnableHeadersVisualStyles = false;
            this.answersDataGridView.Location = new System.Drawing.Point(0, 0);
            this.answersDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.answersDataGridView.MultiSelect = false;
            this.answersDataGridView.Name = "answersDataGridView";
            this.answersDataGridView.Size = new System.Drawing.Size(618, 425);
            this.answersDataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Answer";
            this.dataGridViewTextBoxColumn3.HeaderText = "Respuesta";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Correct";
            this.dataGridViewCheckBoxColumn1.HeaderText = "Correcta";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "QID";
            this.dataGridViewTextBoxColumn2.HeaderText = "QID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AnswerMeta";
            this.dataGridViewTextBoxColumn4.HeaderText = "Meta";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // AID
            // 
            this.AID.DataPropertyName = "AID";
            this.AID.HeaderText = "AID";
            this.AID.Name = "AID";
            this.AID.ReadOnly = true;
            this.AID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // answersBS
            // 
            this.answersBS.DataMember = "Answers";
            this.answersBS.DataSource = this.dB;
            this.answersBS.Sort = "";
            // 
            // dB
            // 
            this.dB.DataSetName = "DB";
            this.dB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qcontainer
            // 
            this.qcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qcontainer.Location = new System.Drawing.Point(3, 3);
            this.qcontainer.Name = "qcontainer";
            // 
            // qcontainer.Panel1
            // 
            this.qcontainer.Panel1.Controls.Add(this.examsDataGridView);
            this.qcontainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // qcontainer.Panel2
            // 
            this.qcontainer.Panel2.Controls.Add(this.questionsDataGridView);
            this.qcontainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qcontainer.Size = new System.Drawing.Size(322, 596);
            this.qcontainer.SplitterDistance = 90;
            this.qcontainer.TabIndex = 4;
            // 
            // examsDataGridView
            // 
            this.examsDataGridView.AllowUserToAddRows = false;
            this.examsDataGridView.AllowUserToDeleteRows = false;
            this.examsDataGridView.AllowUserToOrderColumns = true;
            this.examsDataGridView.AutoGenerateColumns = false;
            this.examsDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.examsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.examsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.examsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.examsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.eIDDataGridViewTextBoxColumn,
            this.mIDDataGridViewTextBoxColumn,
            this.qIDDataGridViewTextBoxColumn,
            this.aIDStringDataGridViewTextBoxColumn});
            this.examsDataGridView.DataSource = this.examsBS;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.examsDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.examsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.examsDataGridView.EnableHeadersVisualStyles = false;
            this.examsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.examsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.examsDataGridView.MultiSelect = false;
            this.examsDataGridView.Name = "examsDataGridView";
            this.examsDataGridView.ReadOnly = true;
            this.examsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.examsDataGridView.Size = new System.Drawing.Size(90, 596);
            this.examsDataGridView.TabIndex = 2;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // eIDDataGridViewTextBoxColumn
            // 
            this.eIDDataGridViewTextBoxColumn.DataPropertyName = "EID";
            this.eIDDataGridViewTextBoxColumn.HeaderText = "EID";
            this.eIDDataGridViewTextBoxColumn.Name = "eIDDataGridViewTextBoxColumn";
            this.eIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.eIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.eIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // mIDDataGridViewTextBoxColumn
            // 
            this.mIDDataGridViewTextBoxColumn.DataPropertyName = "MID";
            this.mIDDataGridViewTextBoxColumn.HeaderText = "MID";
            this.mIDDataGridViewTextBoxColumn.Name = "mIDDataGridViewTextBoxColumn";
            this.mIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.mIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.mIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // qIDDataGridViewTextBoxColumn
            // 
            this.qIDDataGridViewTextBoxColumn.DataPropertyName = "QID";
            this.qIDDataGridViewTextBoxColumn.HeaderText = "QID";
            this.qIDDataGridViewTextBoxColumn.Name = "qIDDataGridViewTextBoxColumn";
            this.qIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.qIDDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.qIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // aIDStringDataGridViewTextBoxColumn
            // 
            this.aIDStringDataGridViewTextBoxColumn.DataPropertyName = "AIDString";
            this.aIDStringDataGridViewTextBoxColumn.HeaderText = "AIDString";
            this.aIDStringDataGridViewTextBoxColumn.Name = "aIDStringDataGridViewTextBoxColumn";
            this.aIDStringDataGridViewTextBoxColumn.ReadOnly = true;
            this.aIDStringDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // examsBS
            // 
            this.examsBS.DataMember = "Exams";
            this.examsBS.DataSource = this.dB;
            // 
            // questionsDataGridView
            // 
            this.questionsDataGridView.AllowUserToAddRows = false;
            this.questionsDataGridView.AllowUserToDeleteRows = false;
            this.questionsDataGridView.AutoGenerateColumns = false;
            this.questionsDataGridView.BackgroundColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.PaleGreen;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.questionsDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.questionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.questionsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn8,
            this.FigureFile,
            this.dataGridViewTextBoxColumn7});
            this.questionsDataGridView.DataSource = this.questionsBS;
            this.questionsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.questionsDataGridView.EnableHeadersVisualStyles = false;
            this.questionsDataGridView.Location = new System.Drawing.Point(0, 0);
            this.questionsDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.questionsDataGridView.MultiSelect = false;
            this.questionsDataGridView.Name = "questionsDataGridView";
            this.questionsDataGridView.Size = new System.Drawing.Size(228, 596);
            this.questionsDataGridView.TabIndex = 1;
            this.questionsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.questionsDataGridView_CellContentClick);
            // 
            // questionsBS
            // 
            this.questionsBS.DataMember = "Questions";
            this.questionsBS.DataSource = this.dB;
            // 
            // rcontainer
            // 
            this.rcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rcontainer.Location = new System.Drawing.Point(331, 3);
            this.rcontainer.Name = "rcontainer";
            this.rcontainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rcontainer.Panel1
            // 
            this.rcontainer.Panel1.Controls.Add(this.qrtfbox);
            this.rcontainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // rcontainer.Panel2
            // 
            this.rcontainer.Panel2.Controls.Add(this.artfbox);
            this.rcontainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rcontainer.Size = new System.Drawing.Size(322, 596);
            this.rcontainer.SplitterDistance = 168;
            this.rcontainer.TabIndex = 2;
            // 
            // qrtfbox
            // 
            this.qrtfbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qrtfbox.Location = new System.Drawing.Point(0, 0);
            this.qrtfbox.Name = "qrtfbox";
            this.qrtfbox.Size = new System.Drawing.Size(322, 168);
            this.qrtfbox.TabIndex = 0;
            this.qrtfbox.Text = "";
            // 
            // artfbox
            // 
            this.artfbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artfbox.Location = new System.Drawing.Point(0, 0);
            this.artfbox.Name = "artfbox";
            this.artfbox.Size = new System.Drawing.Size(322, 424);
            this.artfbox.TabIndex = 0;
            this.artfbox.Text = "";
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
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "QID";
            this.dataGridViewTextBoxColumn5.HeaderText = "QID";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 63;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Question";
            this.dataGridViewTextBoxColumn6.HeaderText = "Question";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 101;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Weight";
            this.dataGridViewTextBoxColumn8.HeaderText = "W";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 50;
            // 
            // FigureFile
            // 
            this.FigureFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FigureFile.DataPropertyName = "FigureFile";
            this.FigureFile.HeaderText = "Figura";
            this.FigureFile.Name = "FigureFile";
            this.FigureFile.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FigureFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FigureFile.Width = 80;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "QuestionMeta";
            this.dataGridViewTextBoxColumn7.HeaderText = "Meta";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // ucDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.tab);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucDataBase";
            this.Size = new System.Drawing.Size(1294, 740);
            this.tab.ResumeLayout(false);
            this.dbTab.ResumeLayout(false);
            this.dbTLP.ResumeLayout(false);
            this.acontainer.Panel1.ResumeLayout(false);
            this.acontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.acontainer)).EndInit();
            this.acontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.answersDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.answersBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).EndInit();
            this.qcontainer.Panel1.ResumeLayout(false);
            this.qcontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qcontainer)).EndInit();
            this.qcontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.examsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.examsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.questionsBS)).EndInit();
            this.rcontainer.Panel1.ResumeLayout(false);
            this.rcontainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rcontainer)).EndInit();
            this.rcontainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage dbTab;
        private System.Windows.Forms.TableLayoutPanel dbTLP;
        private DB dB;
        private System.Windows.Forms.BindingSource answersBS;
        private System.Windows.Forms.BindingSource questionsBS;
        private System.Windows.Forms.DataGridView questionsDataGridView;
        private System.Windows.Forms.DataGridView answersDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.SplitContainer rcontainer;
        private System.Windows.Forms.RichTextBox qrtfbox;
        private System.Windows.Forms.RichTextBox artfbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        private ucTopic ucTopic1;
        private System.Windows.Forms.SplitContainer acontainer;
        private System.Windows.Forms.SplitContainer qcontainer;
        private System.Windows.Forms.DataGridView examsDataGridView;
        private System.Windows.Forms.BindingSource examsBS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn AID;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn mIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn qIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aIDStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewButtonColumn FigureFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        //  private Exam exam1;
    }
}

