using SkyStopwatch.DataModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.ViewModel
{
    public class BootSettingArgs
    {
        public int ThemeArgs { get; set; } = 0;
        public Bitmap Image { get; set; }

        public bool IsTimeLocked { get; set; }

        public TimeLocKSource LockSource { get; set; }

        public bool EnableLockButton { get; set; }

        public bool EnableForceLockButton { get; set; }

    }
}
