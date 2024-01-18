﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using Tesseract;

namespace SkyStopwatch
{
    class MainOCR
    {
        public static void PrintScreenAsFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

            using (Bitmap bitPic = new Bitmap(width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics gra = Graphics.FromImage(bitPic))
                {
                    gra.CopyFromScreen(new Point(0, 0), new Point(0, 0), bitPic.Size);
                    //bitPic.Save("D:\\screen.bmp");
                    bitPic.Save(path);
                }
            }
        }

        public static string PrintScreenAsTempFile()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string subFolder = Path.Combine(exeDirectory, "tmp");

            string fileName = "ocr-screen-shot-" + DateTime.Now.ToString("yyyy-MMdd-HHmmss-fff") + ".bmp";
            string path = Path.Combine(subFolder, fileName);
            
            if(!Directory.Exists(subFolder))
            {
                Directory.CreateDirectory(subFolder);
            }

            if (Directory.GetFiles(subFolder).Length > 100)
            {
                Directory.Delete(subFolder, true);
                Directory.CreateDirectory(subFolder);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            PrintScreenAsFile(path);

            return path;
        }

        public static byte[] PrintScreenAsBytes(bool onlyReturnPartOfImage)
        {
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            using (Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height))
            {
                using (Graphics gra = Graphics.FromImage(bitPic))
                {
                    gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                    gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);

                    if (onlyReturnPartOfImage) //for speed up
                    {
                        int x = screenRect.Width * 25 / 100;
                        int y = screenRect.Height * 50 / 100;

                        Bitmap cloneBitmap = bitPic.Clone(new Rectangle(x, y, 600, 300), bitPic.PixelFormat);
                        return BitmapToBytes(cloneBitmap);
                    }

                    return BitmapToBytes(bitPic);
                }
            }
        }

        public static byte[] BitmapToBytes(System.Drawing.Bitmap bitmap)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Dispose();

            return bytes;
        }

        public Bitmap BytesToBitmap(byte[] imageByte)
        {
            Bitmap bitmap = null; 
            using (MemoryStream stream = new MemoryStream(imageByte))
            {
                bitmap = new Bitmap((Image)new Bitmap(stream));
            }
            return bitmap;
        }


        public static string ReadImageFromFile(string imgPath)
        {
            using (var engine = GetDefaultOCREngine())
            {
                using (var img = Tesseract.Pix.LoadFromFile(imgPath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }

        public static string ReadImageFromMemory(Tesseract.TesseractEngine engine, byte[] imgData)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 1");
            using (var img = Tesseract.Pix.LoadFromMemory(imgData))
            {
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 2");
                using (var page = engine.Process(img))
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 3");
                    return page.GetText();
                }
            }
        }

        public static Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            const string language = "eng"; //chi_sim;
            const string tessdataFolder = @"C:\Dev\VS2022\SkyStopwatch\Tesseract-OCR\tessdata\";

            var engine = new Tesseract.TesseractEngine(tessdataFolder, language, Tesseract.EngineMode.Default);
            engine.SetVariable("tessedit_char_whitelist", "0123456789:oO"); //only look for pre-set chars for speed up

            return engine;
        }

        public static DateTime FindTime(string data, int kickOffDelaySeconds)
        {
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //hh:mm:ss
            string regexPattern = @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";

            string[] zeroAlikeArray = new [] {"o", "O" };

            foreach (string line in lines)
            {
                if(line.IndexOf(':') > 0)
                {
                    string timePart = line.Substring(line.IndexOf(":") + 1).Trim();
                    string timePartAdjust = timePart;

                    foreach (string item in zeroAlikeArray)
                    {
                        timePartAdjust = timePartAdjust.Replace(item, "0");
                    }

                    if (Regex.IsMatch(timePartAdjust, regexPattern))
                    {
                        //timePartAdjust = timePartAdjust.Replace("00", "12"); //got bug 00:00:123 -> 12:12:23
                        if (timePartAdjust.StartsWith("00"))
                        {
                            timePartAdjust = "12" + timePartAdjust.Substring(2);
                        }

                        System.Diagnostics.Debug.WriteLine("-----------------------------");
                        System.Diagnostics.Debug.WriteLine(line);
                        System.Diagnostics.Debug.WriteLine(timePart);
                        System.Diagnostics.Debug.WriteLine(timePartAdjust);

                        DateTime textTime = DateTime.ParseExact(timePartAdjust, "hh:mm:ss", CultureInfo.InvariantCulture);

                        return textTime.AddSeconds(kickOffDelaySeconds);
                    }

                }
            }

            return DateTime.MinValue;
        }

    }
}
