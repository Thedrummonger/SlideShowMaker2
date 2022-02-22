using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SlideShowMaker2
{
    public partial class frmDisplay : Form
    {
        private readonly string _FileDirectory;
        private readonly int _Interval;
        private readonly bool _Shuffle;
        private readonly bool _MuteVideoSound;
        private readonly List<string> FileHistory;
        private readonly Random rnd;
        private readonly SearchOption _DirectorySearchOption;
        private string _CurrentFile;
        private string _CurrentAudioFile;
        private bool PlayingPreviousFile;
        public frmDisplay(string FileDirectory, int Interval, bool Shuffle, int TimeFrame, bool MuteVideoSound, CheckBox DirectorysearchOption)
        {
            InitializeComponent();
            _FileDirectory = FileDirectory;
            _Interval = Interval * TimeFrame;
            _Shuffle = Shuffle;
            _DirectorySearchOption = DirectorysearchOption.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            _MuteVideoSound = MuteVideoSound;
            FileHistory = new List<string>();
            rnd = new Random();
        }

        private void ExitPreview()
        {
            tmrTransition.Stop();
            tmrMaintenance.Stop();
            tmrVideoManager.Stop();
            tmrAudioManager.Stop();

            WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)VideoPlayer.Ctlcontrols;
            controls.stop();
            controls = (WMPLib.IWMPControls3)SoundPlayer.Ctlcontrols;
            controls.stop();

            this.Close();
        }

        private void frmDisplay_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(_FileDirectory) || !GetFiles().Any())
            {
                MessageBox.Show($"{_FileDirectory} contained no Valid Files.");
                ExitPreview();
                return;
            }

            tmrMaintenance.Enabled = true;
            tmrMaintenance.Interval = 10000;
            tmrMaintenance.Start();
        }

        private void frmDisplay_Shown(object sender, EventArgs e)
        {
            Cursor.Hide();
            frmMain._SettingHandler.WriteLine("Form Shown");
            AlignPictureBox();
            AlignVideoPlayer();

            //File Playback
            tmrTransition.Enabled = true;
            tmrTransition.Interval = _Interval;
            tmrTransition.Start();
            PlayNextFile();

            //Audio Playback
            tmrAudioManager.Enabled = true;
            tmrAudioManager.Interval = 10;
            tmrAudioManager.Start();
            PlayNextAudioFile();
        }

        private void tmrTransition_Tick(object sender, EventArgs e)
        {
            frmMain._SettingHandler.WriteLine("Transition Timer Tick");
            PlayNextFile();
        }

        private void PlayNextFile()
        {
            frmMain._SettingHandler.WriteLine("Playing Next File");
            string PreviousFile = _CurrentFile;

            var ValidFiles = GetFiles();

            if (ValidFiles.Count < 1) 
            {
                frmMain._SettingHandler.WriteLine($"No files exist, showing last available"); 
                return; 
            }
            else if (ValidFiles.Count == 1)
            {
                frmMain._SettingHandler.WriteLine($"Single File Exists, Setting as Current File.");
                _CurrentFile = ValidFiles[0];
            }
            else if (ValidFiles.Count > 1)
            {
                frmMain._SettingHandler.WriteLine($"getting Next File in Sequence.");
                if (!PlayingPreviousFile && !string.IsNullOrWhiteSpace(_CurrentFile))
                {
                    frmMain._SettingHandler.WriteLine($"adding {_CurrentFile} to History list.");
                    FileHistory.Add(_CurrentFile);
                }
                GetNextFile(ValidFiles);
            }

            if (_CurrentFile.isImage())
            {
                if (PreviousFile != _CurrentFile) { DisplayImage(_CurrentFile); }
            }
            else if (_CurrentFile.isVideo())
            {
                PlayVideo(_CurrentFile);
            }
        }

        private void PlayVideo(string newFile)
        {
            frmMain._SettingHandler.WriteLine($"Playing Video {newFile}");
            pictureBox1.Visible = false;
            VideoPlayer.Visible = true;
            VideoPlayer.URL = newFile;
            VideoPlayer.settings.mute = _MuteVideoSound;

            tmrTransition.Stop();

            tmrVideoManager.Interval = 10;
            tmrVideoManager.Start();

            frmMain._SettingHandler.WriteLine("Scanning Video Satus");
        }

        private void tmrVideoManager_Tick(object sender, EventArgs e)
        {
            if (VideoPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                frmMain._SettingHandler.WriteLine($"Video has Stopped");
                try { VideoPlayer.fullScreen = false; }
                catch { }
                tmrTransition.Start();
                tmrVideoManager.Stop();
                PlayNextFile();
            }
            else if (VideoPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying && !VideoPlayer.fullScreen)
            {
                frmMain._SettingHandler.WriteLine($"Attempting to fullscreen video");
                try { VideoPlayer.fullScreen = true; } catch { }
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //HandleMediaPlayer();
        }

        private void DisplayImage(string newFile)
        {
            frmMain._SettingHandler.WriteLine($"Displaying Image {newFile}");
            VideoPlayer.Visible = false;
            pictureBox1.Visible = true;
            pictureBox1.Image = CopyImageFromFileStream(newFile);
        }

        private void AlignPictureBox()
        {
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Height = this.Height;
            pictureBox1.Width = this.Width;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void AlignVideoPlayer()
        {
            VideoPlayer.uiMode = "none";
            VideoPlayer.Location = new Point(0, 0);
            VideoPlayer.Height = this.Height;
            VideoPlayer.Width = this.Width;
            VideoPlayer.stretchToFit = true;
        }

        private void GetNextFile(List<string> validFiles)
        {
            if (PlayingPreviousFile)
            {
                frmMain._SettingHandler.WriteLine($"Getting Previous File");
                PlayingPreviousFile = false;
                while (FileHistory.Any() && !File.Exists(FileHistory[FileHistory.Count - 1]))
                {
                    frmMain._SettingHandler.WriteLine($"{FileHistory[FileHistory.Count - 1]} No longer Exists");
                    FileHistory.RemoveAt(FileHistory.Count - 1);
                }
                if (!FileHistory.Any())
                {
                    frmMain._SettingHandler.WriteLine($"No valid files in history record");
                    return;
                }
                frmMain._SettingHandler.WriteLine($"{FileHistory[FileHistory.Count - 1]} was last file");

                _CurrentFile = FileHistory[FileHistory.Count - 1];
                FileHistory.RemoveAt(FileHistory.Count - 1);
            }
            else if (_Shuffle)
            {
                frmMain._SettingHandler.WriteLine($"Getting Random File.");
                string NewFile = _CurrentFile;

                while (NewFile == _CurrentFile)
                {
                    int r = rnd.Next(validFiles.Count);
                    NewFile = validFiles[r];
                }
                _CurrentFile = NewFile;
            }
            else
            {
                var currentIndex = validFiles.IndexOf(_CurrentFile);
                frmMain._SettingHandler.WriteLine($"Current Index is {currentIndex}");
                currentIndex++;
                if (currentIndex > validFiles.Count - 1) { currentIndex = 0; }
                frmMain._SettingHandler.WriteLine($"Getting file at new index {currentIndex}.");
                _CurrentFile = validFiles[currentIndex];
            }
        }

        ///<summary>
        ///Valid File Types image, video, audio, media.
        ///</summary>
        private List<string> GetFiles(string type = "media")
        {
            if (!Directory.Exists(_FileDirectory)) { return new List<string>(); }

            List<string> ValidFiles = new List<string>();

            foreach (var i in Directory.GetFiles(_FileDirectory, "*.*", _DirectorySearchOption))
            {
                if ((type == "media" && i.isMedia()) || (type == "audio" && i.isAudio()) || (type == "video" && i.isVideo()) || (type == "image" && i.isImage()))
                { 
                    ValidFiles.Add(i); 
                }
            }

            return ValidFiles;
        }

        private static Image CopyImageFromFileStream(string path)
        {
            byte[] imageBytes = File.ReadAllBytes(path);

            using (var ms = new MemoryStream(imageBytes))
            {
                var image = Image.FromStream(ms);
                return image;
            }
        }

        private void axWindowsMediaPlayer1_DoubleClickEvent(object sender, AxWMPLib._WMPOCXEvents_DoubleClickEvent e)
        {
            ExitPreview();
        }

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
            ExitPreview();
        }

        private void frmDisplay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ExitPreview();
            }
            else if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Back)
            {
                PlayingPreviousFile = (e.KeyCode == Keys.Back);
                tmrTransition.Stop();
                tmrTransition.Start();
                PlayNextFile();
            }
        }

        private void axWindowsMediaPlayer1_KeyUpEvent(object sender, AxWMPLib._WMPOCXEvents_KeyUpEvent e)
        {
            if (e.nKeyCode == ((short)Keys.Escape))
            {
                WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)VideoPlayer.Ctlcontrols;
                controls.stop();
                ExitPreview();
            }
            else if (e.nKeyCode == ((short)Keys.Tab) || e.nKeyCode == ((short)Keys.Back))
            {
                if (VideoPlayer.playState != WMPLib.WMPPlayState.wmppsPlaying) { return; }
                PlayingPreviousFile = (e.nKeyCode == ((short)Keys.Back));
                WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)VideoPlayer.Ctlcontrols;
                controls.stop();
            }
        }

        private void tmrMaintenance_Tick(object sender, EventArgs e)
        {
            frmMain._SettingHandler.WriteLine("Maintenance code run");
            Reference.KeepAlive();
        }

        //================================================================================================

        #region AudioPlayer

        private void tmrAudioManager_Tick(object sender, EventArgs e)
        {
            if (VideoPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying && !_MuteVideoSound)
            {
                if (SoundPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
                {
                    frmMain._SettingHandler.WriteLine("Pausing Music for video");
                    WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)SoundPlayer.Ctlcontrols;
                    controls.pause();
                }
            }
            else if (SoundPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                frmMain._SettingHandler.WriteLine("Resuming Music");
                WMPLib.IWMPControls3 controls = (WMPLib.IWMPControls3)SoundPlayer.Ctlcontrols;
                controls.play();
            }
            else if (SoundPlayer.playState == WMPLib.WMPPlayState.wmppsStopped || SoundPlayer.playState == WMPLib.WMPPlayState.wmppsUndefined)
            {
                PlayNextAudioFile();
            }
        }

        private void PlayNextAudioFile()
        {
            var validFiles = GetFiles("audio");
            if (!validFiles.Any()) { return; }
            frmMain._SettingHandler.WriteLine("Playing Next Song");
            if (validFiles.Count == 1) { _CurrentAudioFile = validFiles[0]; }
            if (_Shuffle)
            {
                frmMain._SettingHandler.WriteLine("Playing Random Song");
                string NewFile = _CurrentAudioFile;

                while (NewFile == _CurrentAudioFile)
                {
                    int r = rnd.Next(validFiles.Count);
                    NewFile = validFiles[r];
                }
                frmMain._SettingHandler.WriteLine($"Song {NewFile} Chosen");
                _CurrentAudioFile = NewFile;
            }
            else
            {
                var currentIndex = validFiles.IndexOf(_CurrentAudioFile);
                currentIndex++;
                if (currentIndex > validFiles.Count - 1) { currentIndex = 0; }
                _CurrentAudioFile = validFiles[currentIndex];
                frmMain._SettingHandler.WriteLine($"Playing Next Song in sequence {_CurrentAudioFile}");
            }
            SoundPlayer.URL = _CurrentAudioFile;
        }
        #endregion AudioPlayer
    }
}
