
namespace SlideShowMaker2
{
    partial class frmDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisplay));
            this.tmrTransition = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VideoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.tmrMaintenance = new System.Windows.Forms.Timer(this.components);
            this.tmrVideoManager = new System.Windows.Forms.Timer(this.components);
            this.SoundPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.tmrAudioManager = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrTransition
            // 
            this.tmrTransition.Tick += new System.EventHandler(this.tmrTransition_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(27, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(224, 174);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.PictureBox1_DoubleClick);
            // 
            // VideoPlayer
            // 
            this.VideoPlayer.Enabled = true;
            this.VideoPlayer.Location = new System.Drawing.Point(358, 192);
            this.VideoPlayer.Name = "VideoPlayer";
            this.VideoPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("VideoPlayer.OcxState")));
            this.VideoPlayer.Size = new System.Drawing.Size(275, 137);
            this.VideoPlayer.TabIndex = 3;
            this.VideoPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            this.VideoPlayer.DoubleClickEvent += new AxWMPLib._WMPOCXEvents_DoubleClickEventHandler(this.axWindowsMediaPlayer1_DoubleClickEvent);
            this.VideoPlayer.KeyUpEvent += new AxWMPLib._WMPOCXEvents_KeyUpEventHandler(this.axWindowsMediaPlayer1_KeyUpEvent);
            // 
            // tmrMaintenance
            // 
            this.tmrMaintenance.Tick += new System.EventHandler(this.tmrMaintenance_Tick);
            // 
            // tmrVideoManager
            // 
            this.tmrVideoManager.Tick += new System.EventHandler(this.tmrVideoManager_Tick);
            // 
            // SoundPlayer
            // 
            this.SoundPlayer.Enabled = true;
            this.SoundPlayer.Location = new System.Drawing.Point(56, 260);
            this.SoundPlayer.Name = "SoundPlayer";
            this.SoundPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SoundPlayer.OcxState")));
            this.SoundPlayer.Size = new System.Drawing.Size(275, 137);
            this.SoundPlayer.TabIndex = 4;
            this.SoundPlayer.Visible = false;
            // 
            // tmrAudioManager
            // 
            this.tmrAudioManager.Tick += new System.EventHandler(this.tmrAudioManager_Tick);
            // 
            // frmDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SoundPlayer);
            this.Controls.Add(this.VideoPlayer);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDisplay";
            this.Text = "frmDisplay";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDisplay_Load);
            this.Shown += new System.EventHandler(this.frmDisplay_Shown);
            this.DoubleClick += new System.EventHandler(this.PictureBox1_DoubleClick);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDisplay_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VideoPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SoundPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrTransition;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxWMPLib.AxWindowsMediaPlayer VideoPlayer;
        private System.Windows.Forms.Timer tmrMaintenance;
        private System.Windows.Forms.Timer tmrVideoManager;
        private AxWMPLib.AxWindowsMediaPlayer SoundPlayer;
        private System.Windows.Forms.Timer tmrAudioManager;
    }
}