using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SlideShowMaker2
{
    static class Reference
    {
        public static readonly string AppDatapath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SlideShowMaker");
        public static readonly string OptionsFile = Path.Combine(AppDatapath, "Options.ini");
        public static readonly string DefaultOptions = Path.Combine(AppDatapath, "DefaultOptions.ini");
        public static readonly string LogFolder = Path.Combine(AppDatapath, "Logs");
        public static readonly List<string> ImageExtensions = new List<string>() { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
        public static readonly List<string> VideoExtensions = new List<string>() { ".MP4", ".WMV", ".AVI" };
        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint SetThreadExecutionState(EXECUTION_STATE esFlags);

        public static void KeepAlive()
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);
        }

        public static bool isImage(this string File)
        {
            return ImageExtensions.Contains(Path.GetExtension(File).ToUpperInvariant());
        }
        public static bool isVideo(this string File)
        {
            return VideoExtensions.Contains(Path.GetExtension(File).ToUpperInvariant());
        }
        public static bool isMedia(this string File)
        {
            return File.isVideo() || File.isImage();
        }

    }
}
