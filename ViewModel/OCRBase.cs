﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using Tesseract;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Concurrent;

namespace SkyStopwatch
{
    public abstract class OCRBase
    {
        public const int Increment2Seconds = 2;
        public const int Increment10Seconds = 10;
        public const int IncrementMinute = 1;
        public const int DecrementMinute = 1;
        public const int Decrement10Seconds = 10;

        public const int MinBlockWidth = 10;
        public const int MinBlockHeight = 10;

        private string _ocrConfigFolder;

        public static void PrintScreenAsFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

            using (Bitmap bitPic = new Bitmap(width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics gra = Graphics.FromImage(bitPic))
                {
                    gra.CopyFromScreen(new Point(0, 0), new Point(0, 0), bitPic.Size);
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

            if (!Directory.Exists(subFolder))
            {
                Directory.CreateDirectory(subFolder);
            }

            if (Directory.GetFiles(subFolder).Length > GlobalData.TmpFileMaxCount)
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

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            PrintScreenAsFile(path);

            return path;
        }
        public static Tuple<byte[], byte[]> PrintScreenAsBytes(Rectangle rect, Rectangle? rectAUX = null)
        {
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            using (Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height))
            using (Graphics gra = Graphics.FromImage(bitPic))
            {
                //leotodo - improve this, CopyFromScreen(...) throws Win32Exception sometimes, not sure why? happened when press ctrl + tab ?
                //happens when start app and UAC triggered(screen gray out)
                //just ignore for now
                try
                {
                    gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                    gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);
                }
                catch (Win32Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"PrintScreenBlockAsBytes - win32 error: {ex}");
                    return null;
                }

                byte[] masterImage, auxImage = null;

                using (Bitmap cloneBitmap = bitPic.Clone(rect, bitPic.PixelFormat))
                {
                    masterImage = OCRBase.BitmapToBytes(cloneBitmap);
                }

                if (rectAUX.HasValue)
                {
                    using (Bitmap cloneBitmap = bitPic.Clone(rectAUX.Value, bitPic.PixelFormat))
                    {
                        auxImage = OCRBase.BitmapToBytes(cloneBitmap);
                    }
                }

                return Tuple.Create(masterImage, auxImage);

            }
        }



        public static string ReadImageFromMemory(TesseractEngine engine, byte[] imgData)
        {
            if (imgData == null) throw new ArgumentNullException(nameof(imgData));

            //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 1");
            using (var img = Tesseract.Pix.LoadFromMemory(imgData))
            {
                // System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 2");
                using (var page = engine.Process(img))
                {
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - ReadImageFromMemory 3");
                    return page.GetText();
                }
            }
        }
        public static string ReadImageFromFile(TesseractEngine engine, string imgPath, Rectangle rect)
        {
            //using (var img = Tesseract.Pix.LoadFromFile(imgPath))
            //{
            //    using (var page = engine.Process(img))
            //    {
            //        return page.GetText();
            //    }
            //}

            //for speed up - only read part of the file
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            using (Bitmap bitmap = new Bitmap(imgPath))
            using (Bitmap cloneBitmap = bitmap.Clone(rect, bitmap.PixelFormat))
            {
                byte[] bytes = OCRBase.BitmapToBytes(cloneBitmap);

                using (var img = Tesseract.Pix.LoadFromMemory(bytes))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
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
        public static Bitmap BytesToBitmap(byte[] imageByte)
        {
            using (MemoryStream stream = new MemoryStream(imageByte))
            {
                var bitmap = new Bitmap(new Bitmap(stream)); //need nest this, or get error when saving file to disk
                return bitmap;
            }
        }


        public static string SaveTmpFile(string fileNameSuffix, byte[] data)
        {
            using (var bitmap = BytesToBitmap(data))
            {
                string exePath = Assembly.GetExecutingAssembly().Location;
                string exeDirectory = Path.GetDirectoryName(exePath);
                string subFolder = Path.Combine(exeDirectory, "tmp-debug");

                string fileName = "ocr-bytes-" + DateTime.Now.ToString("yyyy-MMdd-HHmmss-fff") + "-" + fileNameSuffix + ".bmp";
                string path = Path.Combine(subFolder, fileName);

                if (!Directory.Exists(subFolder))
                {
                    Directory.CreateDirectory(subFolder);
                }

                int maxTmpFileCount = 200;

                if (Environment.MachineName.ToUpper() == "LEO-PC-PRO" || GlobalData.Default.IsDebugging)
                {
                    maxTmpFileCount = 1000;
                }

                if (Directory.GetFiles(subFolder).Length > maxTmpFileCount)
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

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                bitmap.Save(path);
                return path;
            }
        }
        public static void SafeCheckImageBlock(ref int x, ref int y, ref int width, ref int height)
        {
            var screenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //in case the block is out of screen area
            int safeWidth = Math.Min(width, screenRect.Width - x);
            int safeHeight = Math.Min(height, screenRect.Height - y);

            if (safeWidth < OCRBase.MinBlockWidth)
            {
                safeWidth = OCRBase.MinBlockWidth;
                x = screenRect.Width - OCRBase.MinBlockWidth;
            }

            if (safeHeight < OCRBase.MinBlockHeight)
            {
                safeHeight = OCRBase.MinBlockHeight;
                y = screenRect.Height - OCRBase.MinBlockHeight;
            }

            width = safeWidth;
            height = safeHeight;
        }


        public Tesseract.TesseractEngine CreateOCREngineWith(string allowChars)
        {
            if (string.IsNullOrEmpty(_ocrConfigFolder))
            {
                _ocrConfigFolder = GlobalData.OCRTesseractDataFolder;

                if (!Directory.Exists(_ocrConfigFolder) || Directory.GetFiles(_ocrConfigFolder, "*", SearchOption.AllDirectories).Length == 0)
                {
                    //check the exe file folder if no config file found in the preset folder

                    string exePath = Assembly.GetExecutingAssembly().Location;
                    string exeDirectory = Path.GetDirectoryName(exePath);
                    string subFolder = Path.Combine(exeDirectory, "ocr");

                    if (Directory.Exists(subFolder))
                    {
                        _ocrConfigFolder = subFolder;
                    }
                }

                System.Diagnostics.Debug.WriteLine($"--> going to use ocr config file in {_ocrConfigFolder}");
            }

            var engine = new Tesseract.TesseractEngine(_ocrConfigFolder, GlobalData.OCRLanguage, Tesseract.EngineMode.Default);

            //in case the number got blocked by other images?? so try to recognise multi digits here ??
            engine.SetVariable("tessedit_char_whitelist", allowChars); //only look for pre-set chars for speed up

            //to remove "Empty page!!" either debug_file needs to be set for null, or DefaultPageSegMode needs to be set correctly
            //_tesseractEngine.SetVariable("debug_file", "NUL");
            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            return engine;
        }


        public abstract Rectangle GetScreenBlock();

        public abstract Tesseract.TesseractEngine CreateOCREngine();
        

        public virtual byte[] GetImageBytes() 
        {
            var block = GetScreenBlock();
            var pair = PrintScreenAsBytes(block);

            if (pair == null || pair.Item1 == null)
            {
                System.Diagnostics.Debug.WriteLine("screenShotBytes is null");
            }

            return pair?.Item1;
        }

     
    }
}
