using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public class GlobalData
    {
        public const string ChangeTimeSourceOCRTimeIsNegativeOne = "ocrDisplayTime is -1";
        public const string ChangeTimeSourceClearButton = "OnClearOCR";
        public const string ChangeTimeSourceManualOCRButton = "ManualOCR";
        public const string ChangeTimeSourceNewGame = "NewGame";
        public const string ChangeTimeSourceAdjustTimeButton = "ManualAdjustButtons";
        public const string ChangeTimeSourceTimerOCR = "AutoTimerOCR";
        public const string ChangeTimeSourcePreWarmUp = "PreWarmUp";

        public event EventHandler ChangeTheme;
        public event EventHandler CloseApp;
        public event EventHandler<ChangeAppConfigEventArgs> ChangeAppConfig;
        public event EventHandler<ChangeGameStartTimeEventArgs> ChangeGameStartTime;



        private static GlobalData _instance;
        public static GlobalData Default
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GlobalData();
                }

                return _instance;
            }
        }

        public bool IsDebugging { get; set; }

        //leotodo, improve this?
        public List<Form> LongLivePopups { get; } = new List<Form>();

        public int BootingArgs { get; set; } = 0;
        public bool EnableBossCountingOneMode { get; set; }

        private GlobalData() { }

        public void FireChangeTheme()
        {
            ChangeTheme?.Invoke(null, null);
        }

        public void FireCloseApp()
        {
            CloseApp?.Invoke(null, null);
        }

        public void FireChangeAppConfig(ChangeAppConfigEventArgs e)
        {
            ChangeAppConfig?.Invoke(null, e);
        }

        public void FireChangeGameStartTime(ChangeGameStartTimeEventArgs e)
        {
            ChangeGameStartTime?.Invoke(null, e);
        }

        public void ClearPopups()
        {
            this.LongLivePopups.ForEach(c =>
            {
                if (!c.Disposing && !c.IsDisposed) 
                { 
                    c.Close();
                };
            });

            this.LongLivePopups.Clear();
        }
    }

    public class ChangeAppConfigEventArgs : EventArgs
    {
        public string Source { get; set; }
        public bool SaveRightNow { get; set; }

        public ChangeAppConfigEventArgs(string source, bool saveRightNow)
        {
            this.Source = source;
            this.SaveRightNow = saveRightNow;
        }
    }

    public class ChangeGameStartTimeEventArgs : EventArgs
    {
        public DateTime NewTime { get; set; }
        public string Source { get; set; }

        public ChangeGameStartTimeEventArgs(DateTime time, string source)
        {
            NewTime = time;
            Source = source;
        }
    }
}
