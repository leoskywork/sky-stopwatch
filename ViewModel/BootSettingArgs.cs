using SkyStopwatch.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch.ViewModel
{
    public class BootSettingArgs
    {
        public int ThemeArgs { get; set; } = 0;
        public Bitmap Image { get; set; }

        public bool IsTimeLocked { get; set; }

        public TimeLocKSource LockSource { get; set; }

        public bool EnableUnlockButton { get; set; }

        public bool EnableForceLockButton { get; set; }

        public bool EnableDarkMode { get; set; }

    }

    public class BootSettingActonList
    {
        public Action<Button, string> OnInit { get; set; }
        public Action RunOCR { get; set; }
        public Action OnNewGame { get; set; }
        public Action TopMost { get; set; }
        public Action Clear { get; set; }
        public Action<int> AddSeconds { get; set; }
        public Action<string> ChangeTimeNodes { get; set; }
        public Action<bool> LockTime { get; set; }
        public Action<bool> SwitchDarkMode { get; set; }
    }
}