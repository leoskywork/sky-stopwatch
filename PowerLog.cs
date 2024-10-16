﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public class PowerLog
    {
        public static PowerLog One { get; } = new PowerLog();


        private string _Path;
        private string _Folder;
        private DateTime _PathDate = DateTime.MinValue;

        public PowerLog()
        {
            //_PathDate = DateTime.Today;
            //_Path = GetDefaultPath();
        }

        private static string GetDefaultPath()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string subFolder = Path.Combine(exeDirectory, "tmp-log");

            string fileName = "ocr-log-" + DateTime.Now.ToString("yyyy-MMdd") + ".log";
            string path = Path.Combine(subFolder, fileName);

            if (!Directory.Exists(subFolder))
            {
                Directory.CreateDirectory(subFolder);
            }

            if (Directory.GetFiles(subFolder).Length > GlobalData.TmpLogFileMaxCount)
            {
                try
                {
                    Directory.Delete(subFolder, true);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

                Directory.CreateDirectory(subFolder);
            }

            return path;
        }

        public void Console(string message, string source = null)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now:H:mm:ss.fff} [{source}]: {message}");
        }

        public void SaveAsync(string message, string source = null, bool saveScreen = false)
        {
            System.Diagnostics.Debug.WriteLine($"{source} - {message}");

            if (!GlobalData.EnableLogToFile)
            {
                System.Diagnostics.Debug.WriteLine($"Not going to log to file, switch is off");
                return;
            }

            CheckDayRollover();

            string detail = $"{DateTime.Now:H:mm:ss.fff} [{source}] - {message}{(saveScreen ? ", screen shot saved" : null)}";
            Bitmap screenShot = null;
            Graphics graphics = null;
            DateTime createTime = DateTime.Now;

            if (saveScreen)
            {
                screenShot = new Bitmap(width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
                graphics = Graphics.FromImage(screenShot);
                graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), screenShot.Size);
            }

            //write to disk, do it on background thread
            Task.Run(() =>
            {
                if (saveScreen)
                {
                    string imageName = $"{Path.GetFileNameWithoutExtension(_Path)}-screen-{createTime.ToString("HHmmss")}.bmp";
                    screenShot.Save(Path.Combine(_Folder, imageName));
                    graphics.Dispose();
                    screenShot.Dispose();
                }

                File.AppendAllText(_Path, detail + Environment.NewLine);
            });
        }


        private void CheckDayRollover()
        {
            if (_PathDate != DateTime.Today)
            {
                _PathDate = DateTime.Today;
                _Path = GetDefaultPath();
                _Folder = Path.GetDirectoryName(_Path);
                System.Diagnostics.Debug.WriteLine($"log file path: {_Path}");
            }
        }

        public void SaveAsync(string message, string source, byte[] imageData)
        {
            Console(message, source);

            if (!GlobalData.EnableLogToFile)
            {
                System.Diagnostics.Debug.WriteLine($"not going to log to file, switch is off");
                return;
            }

            CheckDayRollover();

            string detail = $"{DateTime.Now:H:mm:ss.fff} [{source}]: {message}{", bytes saved"}";
            DateTime createTime = DateTime.Now;

            //write to disk, do it on background thread
            Task.Run(() =>
            {
                string imageName = $"{Path.GetFileNameWithoutExtension(_Path)}-bytes-before-{createTime.ToString("HHmmss")}.bmp";
                string imagePath = Path.Combine(_Folder, imageName);

                using (Bitmap image = OCRBase.BytesToBitmap(imageData))
                {
                    image.Save(imagePath);
                }

                File.AppendAllText(_Path, detail + Environment.NewLine);
            });
        }


    }
}
