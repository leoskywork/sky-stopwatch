using SkyStopwatch.View;
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
        public const string Version = "4.5";//"4.2.1.240808";
        public const string Subversion = "1.240824";
        public const string ChangeTimeSourceOCRTimeIsNegativeOne = "ocrDisplayTime is -1";
        public const string ChangeTimeSourceClearButton = "OnClearOCR";
        public const string ChangeTimeSourceManualOCRButton = "ManualOCR";
        public const string ChangeTimeSourceNewGame = "NewGame";
        public const string ChangeTimeSourceNewGameAuto = "NewGame-AutoReset";
        public const string ChangeTimeSourceAdjustTimeButton = "ManualAdjustButtons";
        public const string ChangeTimeSourceTimerOCR = "AutoTimerOCR";
        public const string ChangeTimeSourcePreWarmUp = "PreWarmUp";

        public const string TimeSpanFormat = @"hh\:mm\:ss";
        public const string TImeSpanFormatNoHour = @"mm\:ss";
        public const string TimeFormatNoSecond = @"H\:mm";
        public const string TimeFormat6Digits = @"HH\:mm\:ss";
        public const string UIElapsedTimeFormat = @"m\:ss";

        public const string OCRLanguage = "eng"; //chi_sim;
        //public const string tessdataFolder = @"C:\Dev\VS2022\SkyStopwatch\Tesseract-OCR\tessdata\";
        public const string OCRTessdataFolder = @"C:\Dev\OCR\";

        public const string PopupKeyBossCount = "pop-up-boss-call-no";
        public const string PopupKeyBossCountSuccinct = "pop-up-boss-call-one-mode";
        public const string PopupKeyGameTime = "pop-up-game-time";

        public event EventHandler ChangeTheme;
        public event EventHandler CloseApp;
        public event EventHandler<ChangeAppConfigEventArgs> ChangeAppConfig;
        public event EventHandler<ChangeGameStartTimeEventArgs> ChangeGameStartTime;


        public const int TimerIntervalShowingBossCallMS = 50; //10;
        public const int BossCountingScanTimerIntervalMS = 300;//500;
        public const int BossCountingCompareTimerIntervalMS = 50;//100;

        public const int TmpFileMaxCount = 5;
        public const int TmpLogFileMaxCount = 200;
        public const int TimeNodeEarlyWarningSeconds = 15;//20;//30;
        public const int TimeNodeWarningDurationSeconds = 30;//60;//40;//90;
        public const int PreRoundGameMinutes = 39;//38;//(join game first, then open this app)30; //can not join game after 30 min
        public const int MaxGameRoundMinutes = 40;
        public const int MaxScreenTopGameMinute = 38;
        public const int MinBossCallTimeSeconds = 5 + 1;//5 + 2;

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
        public List<IPopupBox> LongLivePopups { get; } = new List<IPopupBox>();

        public int BootingArgs { get; set; } = 0;
        public bool EnableBossCountingOneMode { get; set; }

        public bool EnableBossCountingGameTime { get; set; } = true;// false;

        public int ScreenShotSeedBossCall { get; set; } = 1;

        public bool IsUsingScreenTopTime { get; set; } = true;


        //leotodo - potential multi threads issue, but simple coding to pass values between forms by static fields
        //public static bool IsDebugging { get; set; } = false; //moved to global data
        //public static bool ShowSystemClock { get; set; } = true;
        //does not default this to Empty, since user may clear up the list
        public static string TimeNodeCheckingList { get; set; } = null;
        public static bool EnableCheckTimeNode { get; set; } = true;
        public static bool EnableTopMost { get; set; } = true;//false;
        public static bool EnableLogToFile { get; set; } = false;

        public static List<string> ProcessList { get;} = new List<string>();


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
                System.Diagnostics.Debug.WriteLine($"GD - clear popup {c.Key}, life {DateTime.Now - c.CreateAt}");

                if (!c.Disposing && !c.IsDisposed) 
                { 
                    c.Close();
                };
            });

            this.LongLivePopups.Clear();
        }

        public void AddLongLivePopup(IPopupBox popup)
        {
            if (popup == null) throw new ArgumentNullException(nameof(popup));

            this.LongLivePopups.Add(popup);
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
