namespace Sam
{
    partial class frmSam
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
            this.lstInfo = new System.Windows.Forms.ListBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.tmrMonitor = new System.Windows.Forms.Timer(this.components);
            this.lblMonitor = new System.Windows.Forms.Label();
            this.tmrWatchDog = new System.Windows.Forms.Timer(this.components);
            this.lblWatchDog = new System.Windows.Forms.Label();
            this.txtDET20Info = new System.Windows.Forms.TextBox();
            this.txtDET20Info2 = new System.Windows.Forms.TextBox();
            this.lblEva_PREAL = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstInfo
            // 
            this.lstInfo.FormattingEnabled = true;
            this.lstInfo.Location = new System.Drawing.Point(12, 87);
            this.lstInfo.Name = "lstInfo";
            this.lstInfo.Size = new System.Drawing.Size(451, 82);
            this.lstInfo.TabIndex = 5;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 96);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 13);
            this.lblInfo.TabIndex = 6;
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.Location = new System.Drawing.Point(9, 185);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(35, 13);
            this.lblTotalTime.TabIndex = 7;
            this.lblTotalTime.Text = "label1";
            // 
            // tmrMonitor
            // 
            this.tmrMonitor.Enabled = true;
            this.tmrMonitor.Interval = 2000;
            this.tmrMonitor.Tick += new System.EventHandler(this.tmrMonitor_Tick);
            // 
            // lblMonitor
            // 
            this.lblMonitor.AutoSize = true;
            this.lblMonitor.Location = new System.Drawing.Point(12, 64);
            this.lblMonitor.Name = "lblMonitor";
            this.lblMonitor.Size = new System.Drawing.Size(35, 13);
            this.lblMonitor.TabIndex = 8;
            this.lblMonitor.Text = "label1";
            // 
            // tmrWatchDog
            // 
            this.tmrWatchDog.Enabled = true;
            this.tmrWatchDog.Interval = 200;
            this.tmrWatchDog.Tick += new System.EventHandler(this.tmrWatchDog_Tick);
            // 
            // lblWatchDog
            // 
            this.lblWatchDog.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWatchDog.Location = new System.Drawing.Point(296, 0);
            this.lblWatchDog.Name = "lblWatchDog";
            this.lblWatchDog.Size = new System.Drawing.Size(100, 23);
            this.lblWatchDog.TabIndex = 9;
            this.lblWatchDog.Text = "...";
            // 
            // txtDET20Info
            // 
            this.txtDET20Info.Location = new System.Drawing.Point(301, 35);
            this.txtDET20Info.Name = "txtDET20Info";
            this.txtDET20Info.Size = new System.Drawing.Size(100, 20);
            this.txtDET20Info.TabIndex = 10;
            // 
            // txtDET20Info2
            // 
            this.txtDET20Info2.Location = new System.Drawing.Point(301, 61);
            this.txtDET20Info2.Name = "txtDET20Info2";
            this.txtDET20Info2.Size = new System.Drawing.Size(162, 20);
            this.txtDET20Info2.TabIndex = 11;
            // 
            // lblEva_PREAL
            // 
            this.lblEva_PREAL.AutoSize = true;
            this.lblEva_PREAL.Location = new System.Drawing.Point(22, 338);
            this.lblEva_PREAL.Name = "lblEva_PREAL";
            this.lblEva_PREAL.Size = new System.Drawing.Size(35, 13);
            this.lblEva_PREAL.TabIndex = 12;
            this.lblEva_PREAL.Text = "label1";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 518);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblEva_PREAL);
            this.Controls.Add(this.txtDET20Info2);
            this.Controls.Add(this.txtDET20Info);
            this.Controls.Add(this.lblWatchDog);
            this.Controls.Add(this.lblMonitor);
            this.Controls.Add(this.lblTotalTime);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lstInfo);
            this.Name = "frmSam";
            this.Text = "Spectro Acquisition Manager";
            this.Load += new System.EventHandler(this.frmSam_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSam_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstInfo;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Timer tmrMonitor;
        private System.Windows.Forms.Label lblMonitor;
        private System.Windows.Forms.Timer tmrWatchDog;
        private System.Windows.Forms.Label lblWatchDog;
        private System.Windows.Forms.TextBox txtDET20Info;
        private System.Windows.Forms.TextBox txtDET20Info2;
        private System.Windows.Forms.Label lblEva_PREAL;
        private System.Windows.Forms.Button btnClose;
    }
}

