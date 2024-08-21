using System;
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

namespace SkyStopwatch
{
    public class MainOCR
    {
        public const int IncrementSeconds = 10;
        public const int DecrementSeconds = 10;
        public const int IncrementMinutes = 1;
        public const int DecrementMinutes = 1;

        public const int MinBlockWidth = 10;
        public const int MinBlockHeight = 10;

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


   

        public static string ReadImageFromMemory(Tesseract.TesseractEngine engine, byte[] imgData)
        {
            if(imgData == null) throw new ArgumentNullException(nameof(imgData));   

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

  


        public static void SafeCheckImageBlock(ref int x, ref int y, ref int width, ref int height)
        {
            var screenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            //in case the block is out of screen area
            int safeWidth = Math.Min(width, screenRect.Width - x);
            int safeHeight = Math.Min(height, screenRect.Height - y);

            if (safeWidth < MainOCR.MinBlockWidth)
            {
                safeWidth = MainOCR.MinBlockWidth;
                x = screenRect.Width - MainOCR.MinBlockWidth;
            }

            if (safeHeight < MainOCR.MinBlockHeight)
            {
                safeHeight = MainOCR.MinBlockHeight;
                y = screenRect.Height - MainOCR.MinBlockHeight;
            }

            width = safeWidth; 
            height = safeHeight;
        }



      
    }



}
