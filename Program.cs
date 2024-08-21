using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SkyStopwatch
{
    static class Program
    {
        public const string Version = "4.2.1.240808";

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
                    System.Diagnostics.Debug.WriteLine($"saved app.config for {e.Source}");
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
            GlobalData.TimeNodeCheckingList = Properties.Settings.Default.TimeNodeCheckingList;
            GlobalData.TimeNodeCheckingList = LeotodoHackNewLine(GlobalData.TimeNodeCheckingList);
            GlobalData.EnableTopMost = Properties.Settings.Default.EnableTopMost;
            GlobalData.EnableLogToFile = Properties.Settings.Default.EnableLogToFile;
            GlobalData.EnableCheckTimeNode = Properties.Settings.Default.EnableCheckTimeNode;
            GlobalData.Default.IsDebugging = Properties.Settings.Default.EnableDebugging;
            GlobalData.Default.EnableBossCountingOneMode = Properties.Settings.Default.EnableBossCountingOneMode;


            MainOCR.XPoint = Properties.Settings.Default.TimeViewPoint.X;
            MainOCR.YPoint = Properties.Settings.Default.TimeViewPoint.Y;
            MainOCR.BlockWidth = Properties.Settings.Default.TimeViewSize.Width;
            MainOCR.BlockHeight = Properties.Settings.Default.TimeViewSize.Height;

            MainOCRPrice.XPoint = Properties.Settings.Default.PriceViewPoint.X;
            MainOCRPrice.YPoint = Properties.Settings.Default.PriceViewPoint.Y;
            MainOCRPrice.BlockWidth = Properties.Settings.Default.PriceViewSize.Width;
            MainOCRPrice.BlockHeight = Properties.Settings.Default.PriceViewSize.Height;


            MainOCRBossCounting.EnableAutoSlice = Properties.Settings.Default.EnableBossCountingAutoSlice;
            MainOCRBossCounting.AutoSliceIntervalSeconds = Properties.Settings.Default.BossCountingAutoSliceSeconds;
            MainOCRBossCounting.XPoint = Properties.Settings.Default.BossCountingViewPoint.X;
            MainOCRBossCounting.YPoint = Properties.Settings.Default.BossCountingViewPoint.Y;
            MainOCRBossCounting.BlockWidth = Properties.Settings.Default.BossCountingViewSize.Width;
            MainOCRBossCounting.BlockHeight = Properties.Settings.Default.BossCountingViewSize.Height;


            //safe check
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
            MainOCR.XPoint = Math.Min(MainOCR.XPoint, screenRect.Width - MainOCR.BlockWidth);
            MainOCR.YPoint = Math.Min(MainOCR.YPoint, screenRect.Height - MainOCR.BlockHeight);
            MainOCRPrice.XPoint = Math.Min(MainOCRPrice.XPoint, screenRect.Width - MainOCRPrice.BlockWidth);
            MainOCRPrice.YPoint = Math.Min(MainOCRPrice.YPoint, screenRect.Height - MainOCRPrice.BlockHeight);
            MainOCRBossCounting.XPoint = Math.Min(MainOCRBossCounting.XPoint, screenRect.Width - MainOCRBossCounting.BlockWidth);
            MainOCRBossCounting.YPoint = Math.Min(MainOCRBossCounting.YPoint, screenRect.Height - MainOCRBossCounting.BlockHeight);

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.ProcessListCSV))
            {
                var processes = Properties.Settings.Default.ProcessListCSV.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                GlobalData.ProcessList.AddRange(processes);
            }
        }

        private static void SaveAppConfig()
        {
            try
            {
                Properties.Settings.Default.BootingArgs = GlobalData.Default.BootingArgs;
                Properties.Settings.Default.TimeNodeCheckingList = GlobalData.TimeNodeCheckingList;
                Properties.Settings.Default.EnableTopMost = GlobalData.EnableTopMost;
                Properties.Settings.Default.EnableLogToFile = GlobalData.EnableLogToFile;
                Properties.Settings.Default.EnableCheckTimeNode = GlobalData.EnableCheckTimeNode;
                Properties.Settings.Default.EnableDebugging = GlobalData.Default.IsDebugging;
                Properties.Settings.Default.EnableBossCountingOneMode = GlobalData.Default.EnableBossCountingOneMode;

                Properties.Settings.Default.TimeViewPoint = new System.Drawing.Point(MainOCR.XPoint, MainOCR.YPoint);
                Properties.Settings.Default.TimeViewSize = new System.Drawing.Size(MainOCR.BlockWidth, MainOCR.BlockHeight);

                Properties.Settings.Default.PriceViewPoint = new System.Drawing.Point(MainOCRPrice.XPoint, MainOCRPrice.YPoint);
                Properties.Settings.Default.PriceViewSize = new System.Drawing.Size(MainOCRPrice.BlockWidth, MainOCRPrice.BlockHeight);

                Properties.Settings.Default.ProcessListCSV = GlobalData.ProcessList.Count > 0 ? string.Join(",", GlobalData.ProcessList) : string.Empty;

                Properties.Settings.Default.EnableBossCountingAutoSlice = MainOCRBossCounting.EnableAutoSlice;
                Properties.Settings.Default.BossCountingAutoSliceSeconds = MainOCRBossCounting.AutoSliceIntervalSeconds;
                Properties.Settings.Default.BossCountingViewPoint = new System.Drawing.Point(MainOCRBossCounting.XPoint, MainOCRBossCounting.YPoint);
                Properties.Settings.Default.BossCountingViewSize = new System.Drawing.Size(MainOCRBossCounting.BlockWidth, MainOCRBossCounting.BlockHeight);

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
