
namespace Exam
{
    partial class ucTopic
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
            this.prefTLP = new System.Windows.Forms.TableLayoutPanel();
            this.topicDGV = new System.Windows.Forms.DataGridView();
            this.topicsBS = new System.Windows.Forms.BindingSource(this.components);
            this.dB = new Exam.DB();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.UseIt = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.topicIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.topicDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastEval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prefTLP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.topicDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.topicsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).BeginInit();
            this.SuspendLayout();
            // 
            // prefTLP
            // 
            this.prefTLP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.prefTLP.ColumnCount = 1;
            this.prefTLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.62637F));
            this.prefTLP.Controls.Add(this.topicDGV, 0, 0);
            this.prefTLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prefTLP.Location = new System.Drawing.Point(0, 0);
            this.prefTLP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.prefTLP.Name = "prefTLP";
            this.prefTLP.RowCount = 1;
            this.prefTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.prefTLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.prefTLP.Size = new System.Drawing.Size(452, 438);
            this.prefTLP.TabIndex = 0;
            // 
            // topicDGV
            // 
            this.topicDGV.AllowUserToAddRows = false;
            this.topicDGV.AllowUserToDeleteRows = false;
            this.topicDGV.AllowUserToOrderColumns = true;
            this.topicDGV.AutoGenerateColumns = false;
            this.topicDGV.BackgroundColor = System.Drawing.Color.MintCream;
            this.topicDGV.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.topicDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.topicDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.topicDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UseIt,
            this.topicIDDataGridViewTextBoxColumn,
            this.topicDataGridViewTextBoxColumn,
            this.LastEval});
            this.topicDGV.DataSource = this.topicsBS;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.topicDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.topicDGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topicDGV.EnableHeadersVisualStyles = false;
            this.topicDGV.Location = new System.Drawing.Point(3, 4);
            this.topicDGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.topicDGV.MultiSelect = false;
            this.topicDGV.Name = "topicDGV";
            this.topicDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.topicDGV.Size = new System.Drawing.Size(446, 430);
            this.topicDGV.TabIndex = 1;
            // 
            // topicsBS
            // 
            this.topicsBS.DataMember = "Topics";
            this.topicsBS.DataSource = this.dB;
            // 
            // dB
            // 
            this.dB.DataSetName = "DB";
            this.dB.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // UseIt
            // 
            this.UseIt.DataPropertyName = "UseIt";
            this.UseIt.HeaderText = "UseIt";
            this.UseIt.Name = "UseIt";
            // 
            // topicIDDataGridViewTextBoxColumn
            // 
            this.topicIDDataGridViewTextBoxColumn.DataPropertyName = "TopicID";
            this.topicIDDataGridViewTextBoxColumn.HeaderText = "TopicID";
            this.topicIDDataGridViewTextBoxColumn.Name = "topicIDDataGridViewTextBoxColumn";
            this.topicIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // topicDataGridViewTextBoxColumn
            // 
            this.topicDataGridViewTextBoxColumn.DataPropertyName = "Topic";
            this.topicDataGridViewTextBoxColumn.HeaderText = "Topic";
            this.topicDataGridViewTextBoxColumn.Name = "topicDataGridViewTextBoxColumn";
            // 
            // LastEval
            // 
            this.LastEval.DataPropertyName = "LastEval";
            this.LastEval.HeaderText = "LastEval";
            this.LastEval.Name = "LastEval";
            // 
            // ucTopic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.Controls.Add(this.prefTLP);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ucTopic";
            this.Size = new System.Drawing.Size(452, 438);
            this.prefTLP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.topicDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.topicsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DB dB;
        private System.Windows.Forms.BindingSource topicsBS;
        private System.Windows.Forms.DataGridView topicDGV;
    
        private System.Windows.Forms.TableLayoutPanel prefTLP;
     
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UseIt;
        private System.Windows.Forms.DataGridViewTextBoxColumn topicIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn topicDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastEval;
    }
}

