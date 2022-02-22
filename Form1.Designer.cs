
namespace SlideShowMaker2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.txtFolderPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudInterval = new System.Windows.Forms.NumericUpDown();
            this.cmbTimeFrame = new System.Windows.Forms.ComboBox();
            this.folderDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.chkSubFolders = new System.Windows.Forms.CheckBox();
            this.chkShuffle = new System.Windows.Forms.CheckBox();
            this.chkMuteSound = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(119, 105);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(67, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Slide";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Folder Path";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(105, 4);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(81, 23);
            this.btnSelectFolder.TabIndex = 2;
            this.btnSelectFolder.Text = "Select";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtFolderPath
            // 
            this.txtFolderPath.Location = new System.Drawing.Point(6, 33);
            this.txtFolderPath.Name = "txtFolderPath";
            this.txtFolderPath.Size = new System.Drawing.Size(180, 20);
            this.txtFolderPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(3, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Transition Every";
            // 
            // nudInterval
            // 
            this.nudInterval.Location = new System.Drawing.Point(95, 60);
            this.nudInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudInterval.Name = "nudInterval";
            this.nudInterval.Size = new System.Drawing.Size(37, 20);
            this.nudInterval.TabIndex = 5;
            this.nudInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbTimeFrame
            // 
            this.cmbTimeFrame.FormattingEnabled = true;
            this.cmbTimeFrame.Location = new System.Drawing.Point(138, 59);
            this.cmbTimeFrame.Name = "cmbTimeFrame";
            this.cmbTimeFrame.Size = new System.Drawing.Size(48, 21);
            this.cmbTimeFrame.TabIndex = 6;
            // 
            // chkSubFolders
            // 
            this.chkSubFolders.AutoSize = true;
            this.chkSubFolders.BackColor = System.Drawing.SystemColors.Control;
            this.chkSubFolders.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkSubFolders.Location = new System.Drawing.Point(6, 86);
            this.chkSubFolders.Name = "chkSubFolders";
            this.chkSubFolders.Size = new System.Drawing.Size(119, 17);
            this.chkSubFolders.TabIndex = 9;
            this.chkSubFolders.Text = "Search Sub Folders";
            this.chkSubFolders.UseVisualStyleBackColor = false;
            // 
            // chkShuffle
            // 
            this.chkShuffle.AutoSize = true;
            this.chkShuffle.BackColor = System.Drawing.SystemColors.Control;
            this.chkShuffle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkShuffle.Location = new System.Drawing.Point(127, 86);
            this.chkShuffle.Name = "chkShuffle";
            this.chkShuffle.Size = new System.Drawing.Size(59, 17);
            this.chkShuffle.TabIndex = 7;
            this.chkShuffle.Text = "Shuffle";
            this.chkShuffle.UseVisualStyleBackColor = false;
            // 
            // chkMuteSound
            // 
            this.chkMuteSound.AutoSize = true;
            this.chkMuteSound.BackColor = System.Drawing.SystemColors.Control;
            this.chkMuteSound.Checked = true;
            this.chkMuteSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMuteSound.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkMuteSound.Location = new System.Drawing.Point(6, 109);
            this.chkMuteSound.Name = "chkMuteSound";
            this.chkMuteSound.Size = new System.Drawing.Size(114, 17);
            this.chkMuteSound.TabIndex = 8;
            this.chkMuteSound.Text = "Mute Video Sound";
            this.chkMuteSound.UseVisualStyleBackColor = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(192, 135);
            this.Controls.Add(this.chkSubFolders);
            this.Controls.Add(this.chkMuteSound);
            this.Controls.Add(this.chkShuffle);
            this.Controls.Add(this.cmbTimeFrame);
            this.Controls.Add(this.nudInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFolderPath);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Slide Show Maker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.nudInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FolderBrowserDialog folderDlg;
        public System.Windows.Forms.TextBox txtFolderPath;
        public System.Windows.Forms.NumericUpDown nudInterval;
        public System.Windows.Forms.ComboBox cmbTimeFrame;
        public System.Windows.Forms.CheckBox chkSubFolders;
        public System.Windows.Forms.CheckBox chkShuffle;
        public System.Windows.Forms.CheckBox chkMuteSound;
    }
}

