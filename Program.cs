using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SkyStopwatch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ReadAppConfig();

            if (Environment.MachineName == "LEO-PC-PRO")
            {
                //MainOCR.BootingArgs = 5;// 0;// 2;
                //PowerTool.Test();
                //MainOCR.ProcessList.Add("devenv");
                //MainOCR.ProcessList.Remove("devenv");

                //MainOCR.TimeNodeCheckingList = "1:00";
                //MainOCR.TimeNodeCheckingList = "1:00\r\n02:30\r\n10:00";
                //MainOCR.TimeNodeCheckingList = "10:30\r\n20:30\r\n35:00";
            }

            GlobalData.Default.ChangeAppConfig += (_, e) =>
            {
                if (e.SaveRightNow)
                {
                    SaveAppConfig();
                    System.Diagnostics.Debug.WriteLine($"saved app.config for {e.Source}, detail: {e.Detail}");
                }
            };

            //Application.Run(new FormMain());
            //Application.Run(new FormBoot());
            var boot = new FormBoot();
            boot.FormClosed += (_, __) => SaveAppConfig();
            Application.Run(boot);
        }

        private static void ReadAppConfig()
        {
            GlobalData.Default.BootingArgs = Properties.Settings.Default.BootingArgs;
            GlobalData.Default.EnableTopMost = Properties.Settings.Default.EnableTopMost;
            GlobalData.TimeNodeCheckingList = Properties.Settings.Default.TimeNodeCheckingList;
            GlobalData.TimeNodeCheckingList = LeotodoHackNewLine(GlobalData.TimeNodeCheckingList);
            GlobalData.EnableLogToFile = Properties.Settings.Default.EnableLogToFile;
            GlobalData.EnableCheckTimeNode = Properties.Settings.Default.EnableCheckTimeNode;
            GlobalData.Default.IsDebugging = Properties.Settings.Default.EnableDebugging;
            GlobalData.Default.EnableBossCountingOneMode = Properties.Settings.Default.EnableBossCountingOneMode;
            GlobalData.Default.IsUsingScreenTopTime = Properties.Settings.Default.IsUsingScreenTopTime;
            GlobalData.Default.EnableScreenTopTimeAutoLock = Properties.Settings.Default.EnableScreenTopTimeAutoLock;
            GlobalData.Default.TimeViewScanMiddleDelaySecond = Properties.Settings.Default.TimeViewScanMiddleDelaySecond;
            GlobalData.Default.EnableTimeViewMiddleAsSecondary = Properties.Settings.Default.EnableTimeView2Blocks;
            GlobalData.Default.BoxTimeLastCloseLocation = Properties.Settings.Default.BoxTimeLastCloseLocation;


            OCRGameTime.XPoint = Properties.Settings.Default.TimeViewPoint.X;
            OCRGameTime.YPoint = Properties.Settings.Default.TimeViewPoint.Y;
            OCRGameTime.BlockWidth = Properties.Settings.Default.TimeViewSize.Width;
            OCRGameTime.BlockHeight = Properties.Settings.Default.TimeViewSize.Height;

            OCRGameTime.TopXPoint = Properties.Settings.Default.TimeViewTopPoint.X;
            OCRGameTime.TopYPoint = Properties.Settings.Default.TimeViewTopPoint.Y;
            OCRGameTime.TopBlockWidth = Properties.Settings.Default.TimeViewTopSize.Width;
            OCRGameTime.TopBlockHeight = Properties.Settings.Default.TimeViewTopSize.Height;

            OCRGameTime.Preset1XPoint = Properties.Settings.Default.TimeViewPreset1Point.X;
            OCRGameTime.Preset1YPoint = Properties.Settings.Default.TimeViewPreset1Point.Y;
            OCRGameTime.Preset1BlockWidth = Properties.Settings.Default.TimeViewPreset1Size.Width;
            OCRGameTime.Preset1BlockHeight = Properties.Settings.Default.TimeViewPreset1Size.Height;

            OCRGameTime.Preset2XPoint = Properties.Settings.Default.TimeViewPreset2Point.X;
            OCRGameTime.Preset2YPoint = Properties.Settings.Default.TimeViewPreset2Point.Y;
            OCRGameTime.Preset2BlockWidth = Properties.Settings.Default.TimeViewPreset2Size.Width;
            OCRGameTime.Preset2BlockHeight = Properties.Settings.Default.TimeViewPreset2Size.Height;



            OCRPrice.XPoint = Properties.Settings.Default.PriceViewPoint.X;
            OCRPrice.YPoint = Properties.Settings.Default.PriceViewPoint.Y;
            OCRPrice.BlockWidth = Properties.Settings.Default.PriceViewSize.Width;
            OCRPrice.BlockHeight = Properties.Settings.Default.PriceViewSize.Height;


            OCRBossCounting.EnableAutoSlice = Properties.Settings.Default.EnableBossCountingAutoSlice;
            OCRBossCounting.AutoSliceIntervalSeconds = Properties.Settings.Default.BossCountingAutoSliceSeconds;
            OCRBossCounting.XPoint = Properties.Settings.Default.BossCountingViewPoint.X;
            OCRBossCounting.YPoint = Properties.Settings.Default.BossCountingViewPoint.Y;
            OCRBossCounting.BlockWidth = Properties.Settings.Default.BossCountingViewSize.Width;
            OCRBossCounting.BlockHeight = Properties.Settings.Default.BossCountingViewSize.Height;


            //safe check
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            OCRGameTime.XPoint = Math.Min(OCRGameTime.XPoint, screenRect.Width - OCRGameTime.BlockWidth);
            OCRGameTime.YPoint = Math.Min(OCRGameTime.YPoint, screenRect.Height - OCRGameTime.BlockHeight);
            OCRGameTime.TopXPoint = Math.Min(OCRGameTime.TopXPoint, screenRect.Width - OCRGameTime.TopBlockWidth);
            OCRGameTime.TopYPoint = Math.Min(OCRGameTime.TopYPoint, screenRect.Height - OCRGameTime.TopBlockHeight);

            OCRGameTime.Preset1XPoint = Math.Min(OCRGameTime.Preset1XPoint, screenRect.Width - OCRGameTime.Preset1BlockWidth);
            OCRGameTime.Preset1YPoint = Math.Min(OCRGameTime.Preset1YPoint, screenRect.Height - OCRGameTime.Preset1BlockHeight);
            OCRGameTime.Preset2XPoint = Math.Min(OCRGameTime.Preset2XPoint, screenRect.Width - OCRGameTime.Preset2BlockWidth);
            OCRGameTime.Preset2YPoint = Math.Min(OCRGameTime.Preset2YPoint, screenRect.Height - OCRGameTime.Preset2BlockHeight);

            OCRPrice.XPoint = Math.Min(OCRPrice.XPoint, screenRect.Width - OCRPrice.BlockWidth);
            OCRPrice.YPoint = Math.Min(OCRPrice.YPoint, screenRect.Height - OCRPrice.BlockHeight);

            OCRBossCounting.XPoint = Math.Min(OCRBossCounting.XPoint, screenRect.Width - OCRBossCounting.BlockWidth);
            OCRBossCounting.YPoint = Math.Min(OCRBossCounting.YPoint, screenRect.Height - OCRBossCounting.BlockHeight);

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.ProcessListCSV))
            {
                var processes = Properties.Settings.Default.ProcessListCSV.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                GlobalData.ProcessCheckingList.AddRange(processes);

                if (Environment.MachineName == "LEO-PC-PRO")
                {
                    GlobalData.ProcessCheckingList.Remove("devenv");
                }
            }
        }

        private static void SaveAppConfig()
        {
            try
            {
                Properties.Settings.Default.BootingArgs = GlobalData.Default.BootingArgs;
                Properties.Settings.Default.EnableTopMost = GlobalData.Default.EnableTopMost;
                Properties.Settings.Default.TimeNodeCheckingList = GlobalData.TimeNodeCheckingList;
                Properties.Settings.Default.EnableLogToFile = GlobalData.EnableLogToFile;
                Properties.Settings.Default.EnableCheckTimeNode = GlobalData.EnableCheckTimeNode;
                Properties.Settings.Default.EnableDebugging = GlobalData.Default.IsDebugging;
                Properties.Settings.Default.EnableBossCountingOneMode = GlobalData.Default.EnableBossCountingOneMode;
                Properties.Settings.Default.IsUsingScreenTopTime = GlobalData.Default.IsUsingScreenTopTime;
                Properties.Settings.Default.EnableScreenTopTimeAutoLock = GlobalData.Default.EnableScreenTopTimeAutoLock;
                Properties.Settings.Default.TimeViewScanMiddleDelaySecond = GlobalData.Default.TimeViewScanMiddleDelaySecond;
                Properties.Settings.Default.EnableTimeView2Blocks = GlobalData.Default.EnableTimeViewMiddleAsSecondary;
                Properties.Settings.Default.BoxTimeLastCloseLocation = GlobalData.Default.BoxTimeLastCloseLocation;

                Properties.Settings.Default.TimeViewPoint = new System.Drawing.Point(OCRGameTime.XPoint, OCRGameTime.YPoint);
                Properties.Settings.Default.TimeViewSize = new System.Drawing.Size(OCRGameTime.BlockWidth, OCRGameTime.BlockHeight);
                Properties.Settings.Default.TimeViewTopPoint = new System.Drawing.Point(OCRGameTime.TopXPoint, OCRGameTime.TopYPoint);
                Properties.Settings.Default.TimeViewTopSize = new System.Drawing.Size(OCRGameTime.TopBlockWidth, OCRGameTime.TopBlockHeight);
                Properties.Settings.Default.TimeViewPreset1Point = new System.Drawing.Point(OCRGameTime.Preset1XPoint, OCRGameTime.Preset1YPoint);
                Properties.Settings.Default.TimeViewPreset1Size = new System.Drawing.Size(OCRGameTime.Preset1BlockWidth, OCRGameTime.Preset1BlockHeight);
                Properties.Settings.Default.TimeViewPreset2Point = new System.Drawing.Point(OCRGameTime.Preset2XPoint, OCRGameTime.Preset2YPoint);
                Properties.Settings.Default.TimeViewPreset2Size = new System.Drawing.Size(OCRGameTime.Preset2BlockWidth, OCRGameTime.Preset2BlockHeight);

                Properties.Settings.Default.PriceViewPoint = new System.Drawing.Point(OCRPrice.XPoint, OCRPrice.YPoint);
                Properties.Settings.Default.PriceViewSize = new System.Drawing.Size(OCRPrice.BlockWidth, OCRPrice.BlockHeight);

                Properties.Settings.Default.ProcessListCSV = GlobalData.ProcessCheckingList.Count > 0 ? string.Join(",", GlobalData.ProcessCheckingList) : string.Empty;

                Properties.Settings.Default.EnableBossCountingAutoSlice = OCRBossCounting.EnableAutoSlice;
                Properties.Settings.Default.BossCountingAutoSliceSeconds = OCRBossCounting.AutoSliceIntervalSeconds;
                Properties.Settings.Default.BossCountingViewPoint = new System.Drawing.Point(OCRBossCounting.XPoint, OCRBossCounting.YPoint);
                Properties.Settings.Default.BossCountingViewSize = new System.Drawing.Size(OCRBossCounting.BlockWidth, OCRBossCounting.BlockHeight);

                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private static string LeotodoHackNewLine(string appConfigValue)
        {
            return appConfigValue.Replace("\\r\\n", "\r\n");
        }
    }
}
