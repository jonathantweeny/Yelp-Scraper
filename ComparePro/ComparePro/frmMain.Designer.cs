namespace ComparePro
{
    partial class frmMain
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
            this.wbMain = new System.Windows.Forms.WebBrowser();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // wbMain
            // 
            this.wbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.wbMain.Location = new System.Drawing.Point(0, 358);
            this.wbMain.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMain.Name = "wbMain";
            this.wbMain.Size = new System.Drawing.Size(484, 20);
            this.wbMain.TabIndex = 0;
            this.wbMain.Visible = false;
            // 
            // lstLog
            // 
            this.lstLog.BackColor = System.Drawing.SystemColors.Control;
            this.lstLog.FormattingEnabled = true;
            this.lstLog.Location = new System.Drawing.Point(0, 179);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(484, 199);
            this.lstLog.TabIndex = 1;
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(311, 75);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.Transparent;
            this.btnSubmit.Location = new System.Drawing.Point(143, 115);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(81, 28);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Upload File";
            // 
            // txtFile
            // 
            this.txtFile.BackColor = System.Drawing.SystemColors.Control;
            this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFile.Location = new System.Drawing.Point(144, 77);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(161, 20);
            this.txtFile.TabIndex = 6;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(484, 378);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.wbMain);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbMain;
        private System.Windows.Forms.ListBox lstLog;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.TextBox txtFile;
    }
}

