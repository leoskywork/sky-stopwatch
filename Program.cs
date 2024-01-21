using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SkyStopwatch
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //leotodo - read from config file here?
            if (MainOCR.IsDebugging)
            {
                //MainOCR.TimeNodeCheckingList = "1:00";
                //MainOCR.TimeNodeCheckingList = "1:00\r\n02:30\r\n10:00";
                MainOCR.TimeNodeCheckingList = "10:30\r\n20:30\r\n35:00";
            }

            //Application.Run(new FormMain());
            Application.Run(new FormBoot());
        }
    }
}
