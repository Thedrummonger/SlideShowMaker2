using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SlideShowMaker2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            _SettingHandler = new SettingHandler(this);
        }

        public static SettingHandler _SettingHandler;

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cmbTimeFrame.SelectedIndex == -1) { cmbTimeFrame.SelectedIndex = 0; }
            frmDisplay Display = new frmDisplay(
                txtFolderPath.Text, 
                (int)nudInterval.Value, 
                chkShuffle.Checked, 
                ((KeyValuePair<string, int>)cmbTimeFrame.SelectedItem).Value, 
                chkMuteSound.Checked, 
                chkSubFolders
            );
            Display.ShowDialog();
            Console.WriteLine("Preiveiew ended");
            Cursor.Show();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (folderDlg.ShowDialog() == DialogResult.OK)
            {
                txtFolderPath.Text = folderDlg.SelectedPath;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> TimeValues = new Dictionary<string, int>
            {
                {"Sec", 1000},
                {"Min", 60000},
                {"Hour", 3600000}
            };
            cmbTimeFrame.DataSource = new BindingSource(TimeValues, null);
            cmbTimeFrame.DisplayMember = "Key";

            cmbTimeFrame.SelectedIndex = 0;

            _SettingHandler.LoadSettings();

            if (CheckForOpenProgram())
            {
                MessageBox.Show("An instance of this program is already running.");
                this.Close();
                return;
            }

            if (_SettingHandler.autostart) { btnStart_Click(sender, e); }
        }

        private static bool CheckForOpenProgram()
        {
            return Process.GetProcesses().Any(i => i.ProcessName == Process.GetCurrentProcess().ProcessName && i.Id != Process.GetCurrentProcess().Id);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_SettingHandler.TempSettings)
            {
                _SettingHandler.SaveCurrentSettingsToFile(Reference.OptionsFile);
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.F4)
            {
                if (File.Exists(Reference.DefaultOptions))
                {
                    File.Delete(Reference.DefaultOptions);
                    MessageBox.Show("Default Settings have been removed");
                }
                else
                {
                    _SettingHandler.SaveCurrentSettingsToFile(Reference.DefaultOptions);
                    MessageBox.Show("Default Settings have been saved");
                }
            }
        }
    }
}
