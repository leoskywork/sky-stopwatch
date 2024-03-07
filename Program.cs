using System;
using System.Collections.Generic;
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
            }

            //if (MainOCR.IsDebugging)
            {
                //MainOCR.TimeNodeCheckingList = "1:00";
                //MainOCR.TimeNodeCheckingList = "1:00\r\n02:30\r\n10:00";
                //MainOCR.TimeNodeCheckingList = "10:30\r\n20:30\r\n35:00";
            }

            //Application.Run(new FormMain());
            //Application.Run(new FormBoot());
            var boot = new FormBoot();
            boot.FormClosed += (_, __) => { SaveAppConfig(); };
            Application.Run(boot);
        }

        private static void ReadAppConfig()
        {
            MainOCR.BootingArgs = Properties.Settings.Default.BootingArgs;
            MainOCR.TimeNodeCheckingList = Properties.Settings.Default.TimeNodeCheckingList;
            MainOCR.TimeNodeCheckingList = LeotodoHackNewLine(MainOCR.TimeNodeCheckingList);
            MainOCR.TopMost = Properties.Settings.Default.TopMost;

            MainOCR.XPoint = Properties.Settings.Default.TimeViewPoint.X;
            MainOCR.YPoint = Properties.Settings.Default.TimeViewPoint.Y;
            MainOCR.BlockWidth = Properties.Settings.Default.TimeViewSize.Width;
            MainOCR.BlockHeight = Properties.Settings.Default.TimeViewSize.Height;

            MainOCRPrice.XPoint = Properties.Settings.Default.PriceViewPoint.X;
            MainOCRPrice.YPoint = Properties.Settings.Default.PriceViewPoint.Y;
            MainOCRPrice.BlockWidth = Properties.Settings.Default.PriceViewSize.Width;
            MainOCRPrice.BlockHeight = Properties.Settings.Default.PriceViewSize.Height;
        }

        private static void SaveAppConfig()
        {
            try
            {
                Properties.Settings.Default.BootingArgs = MainOCR.BootingArgs;
                Properties.Settings.Default.TimeNodeCheckingList = MainOCR.TimeNodeCheckingList;
                Properties.Settings.Default.TopMost = MainOCR.TopMost;

                Properties.Settings.Default.TimeViewPoint = new System.Drawing.Point(MainOCR.XPoint, MainOCR.YPoint);
                Properties.Settings.Default.TimeViewSize = new System.Drawing.Size(MainOCR.BlockWidth, MainOCR.BlockHeight);

                Properties.Settings.Default.PriceViewPoint = new System.Drawing.Point(MainOCRPrice.XPoint, MainOCRPrice.YPoint);
                Properties.Settings.Default.PriceViewSize = new System.Drawing.Size(MainOCRPrice.BlockHeight, MainOCRPrice.BlockWidth);

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
