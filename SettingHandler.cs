using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace SlideShowMaker2
{
    public class SettingHandler
    {
        public readonly frmMain _MainForm;
        public readonly Dictionary<string, CheckBox> ToggleOptions;
        public bool autostart = false;
        public SettingHandler(frmMain Mainform)
        {
            _MainForm = Mainform;
            ToggleOptions = new Dictionary<string, CheckBox>
            {
                {"shuffle", _MainForm.chkShuffle },
                {"mute", _MainForm.chkMuteSound },
                {"subfolders", _MainForm.chkSubFolders }
            };
        }

        public void LoadSettings()
        {
            if (!Directory.Exists(Reference.AppDatapath))
            {
                Directory.CreateDirectory(Reference.AppDatapath);
            }
            if (File.Exists(Reference.OptionsFile))
            {
                ApplySettingsFromJson(File.ReadAllText(Reference.OptionsFile), Reference.OptionsFile);
            }
            if (File.Exists(Reference.DefaultOptions))
            {
                ApplySettingsFromJson(File.ReadAllText(Reference.DefaultOptions), Reference.DefaultOptions);
            }
            string[] StartingArgs = Environment.GetCommandLineArgs();
            if (StartingArgs.Count() > 1)
            {
                ApplySettingsFromJson(ConvertStartingArgsToJson(StartingArgs), "Command Line");
                if (StartingArgs.Any(x => x.ToLower().Trim() == "autostart")) { autostart = true; }
                if (StartingArgs.Any(x => x.ToLower().Trim() == "kill")) { KillProgram(); }
            }
        }

        public void ApplySettingsFromJson(string Json, string Source)
        {
            Console.WriteLine($"Applying Settings From {Source}");
            Dictionary<string, string> Settings;
            try { Settings = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(Json); }
            catch { return; }

            foreach(var i in ToggleOptions)
            {
                if (Settings.ContainsKey(i.Key) && bool.TryParse(Settings[i.Key], out bool BoolResult))
                {
                    Console.WriteLine($"Contained Key {i.Key}. Value {Settings[i.Key]}");
                    i.Value.Checked = BoolResult;
                }
            }

            if (Settings.ContainsKey("path"))
            {
                Console.WriteLine($"Contained Key Path. Value {Settings["path"] }");
                _MainForm.txtFolderPath.Text = Settings["path"];
            }
            if (Settings.ContainsKey("interval") && int.TryParse(Settings["interval"], out int IntervalResult) && IntervalResult > 1)
            {
                Console.WriteLine($"Contained Key interval. Value {Settings["interval"] }");
                _MainForm.nudInterval.Value = IntervalResult;
            }
            if (Settings.ContainsKey("timeframe"))
            {
                Console.WriteLine($"Contained Key timeframe. Value {Settings["timeframe"] }");
                switch (Settings["timeframe"])
                {
                    case "s":
                        _MainForm.cmbTimeFrame.SelectedIndex = 0;
                        break;
                    case "m":
                        _MainForm.cmbTimeFrame.SelectedIndex = 1;
                        break;
                    case "h":
                        _MainForm.cmbTimeFrame.SelectedIndex = 2;
                        break;
                }
            }
        }

        public string ConvertStartingArgsToJson(string[] StartingArgs)
        {
            Dictionary<string, string> OptionsDict = new Dictionary<string, string>();

            foreach (var i in StartingArgs)
            {
                Console.WriteLine(i);
                var ParsedArg = ParseArgument(i);
                if (ParsedArg == null) { continue; }
                OptionsDict.Add(ParsedArg[0], ParsedArg[1]);
            }

            return new JavaScriptSerializer().Serialize(OptionsDict);

        }

        public string[] ParseArgument(string Arg)
        {
            var IntervalSections = Arg.Split('=').Select(x => x.Trim()).ToList();
            if (IntervalSections.Count > 1)
            {
                return IntervalSections.ToArray();
            }
            else
            {
                if (ToggleOptions.ContainsKey(Arg))
                {
                    return new string[] { Arg, "true" };
                }
            }
            return null;
        }

        public void SaveCurrentSettingsToFile(string FilePath)
        {
            Dictionary<string, string> OptionsDict = new Dictionary<string, string>
            {
                { "shuffle", _MainForm.chkShuffle.Checked.ToString() },
                { "subfolders", _MainForm.chkSubFolders.Checked.ToString() },
                { "mute", _MainForm.chkMuteSound.Checked.ToString() },
                { "interval", _MainForm.nudInterval.Value.ToString() }
            };
            if (!string.IsNullOrWhiteSpace(_MainForm.txtFolderPath.Text))
            {
                OptionsDict.Add("path", _MainForm.txtFolderPath.Text);
            }
            switch (_MainForm.cmbTimeFrame.SelectedIndex)
            {
                case 0: OptionsDict.Add("timeframe", "s"); break;
                case 1: OptionsDict.Add("timeframe", "m"); break;
                case 2: OptionsDict.Add("timeframe", "h"); break;
            }
            File.WriteAllText(FilePath, new JavaScriptSerializer().Serialize(OptionsDict));
        }

        public static void KillProgram()
        {
            var allProcesses = Process.GetProcesses();
            foreach (var i in allProcesses)
            {
                if (i.ProcessName == Process.GetCurrentProcess().ProcessName && i.Id != Process.GetCurrentProcess().Id)
                {
                    i.Kill();
                }
            }
            Process.GetCurrentProcess().Kill();
        }
    }
}
