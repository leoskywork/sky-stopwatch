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
        //public const int XPercent = 32;
        //public const int YPercent = 68;
        //public const int BlockWidth = 300;
        //public const int BlockHeight = 100;
        //public const int XYPercentDecimalSize = 4;
        //public static decimal XPercent = 0.3922m;
        //public static decimal YPercent = 0.7093m;
        public static int XPoint = 1084;
        public static int YPoint = 1068;
        public static int BlockWidth = 140;
        public static int BlockHeight = 30;

        public const int MinBlockWidth = 40;
        public const int MinBlockHeight = 10;

        public const int ManualOCRDelaySeconds = 10;
        public const int AutoOCRDelaySeconds = 2;
        public const int NewGameDelaySeconds = 1;//10;
        public const int NoDelay = 0;
        public const int IncrementSeconds = 10;
        public const int DecrementSeconds = 10;
        public const int IncrementMinutes = 1;
        public const int DecrementMinutes = 1;
        public const int TmpFileMaxCount = 5;
        public const int TmpLogFileMaxCount = 200;
        public const int TimeNodeEarlyWarningSeconds = 15;//20;//30;
        public const int TimeNodeWarningDurationSeconds = 30;//60;//40;//90;
        public const int PreRoundGameMinutes = 30; //can not join game after 30 min
        public const int MaxGameRoundMinutes = 40;
        public const int MinBossCallTimeSeconds = 5;

        public const string TimeSpanFormat = @"hh\:mm\:ss";
        public const string TImeSpanFormatNoHour = @"mm\:ss";
        public const string TimeFormatNoSecond = @"H\:mm";
        public const string TimeFormat6Digits = @"HH\:mm\:ss";
        public const string UIElapsedTimeFormat = @"m\:ss";

        public const string OCRLanguage = "eng"; //chi_sim;
        //public const string tessdataFolder = @"C:\Dev\VS2022\SkyStopwatch\Tesseract-OCR\tessdata\";
        public const string OCRTessdataFolder = @"C:\Dev\OCR\";

        //leotodo - potential multi threads issue, but simple coding to pass values between forms by static fields
        //public static bool IsDebugging { get; set; } = false; //moved to global data
        //public static bool ShowSystemClock { get; set; } = true;
        //does not default this to Empty, since user may clear up the list
        public static string TimeNodeCheckingList { get; set; } = null;
        public static bool EnableCheckTimeNode { get; set; } = true;
        public static bool EnableTopMost { get; set; } = true;//false;
        public static bool EnableLogToFile { get; set; } = false;

        public static List<string> ProcessList { get; set; } = new List<string>();
      

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

            if (Directory.GetFiles(subFolder).Length > TmpFileMaxCount)
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

        public static byte[] PrintScreenAsBytes(bool onlyReturnPartOfImage)
        {
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            if (GlobalData.Default.IsDebugging)
            {
                System.Diagnostics.Debug.WriteLine($"screen: {screenRect}");
            }

            using (Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height))
            using (Graphics gra = Graphics.FromImage(bitPic))
            {
                //leotodo - improve this, CopyFromScreen(...) throws Win32Exception sometimes, not sure why? happened when press ctrl + tab ?
                //just ignore for now
                try
                {
                    gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                    gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);
                }
                catch(Win32Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"win32 error: {ex}");
                    return null;
                }

                if (onlyReturnPartOfImage) //for speed up
                {
                    using (Bitmap cloneBitmap = bitPic.Clone(new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight), bitPic.PixelFormat))
                    {
                        return BitmapToBytes(cloneBitmap);
                    }
                }

                return BitmapToBytes(bitPic);
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


        public static string ReadImageFromFile(string imgPath)
        {
            using (var engine = GetDefaultOCREngine())
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
                using (Bitmap cloneBitmap = bitmap.Clone(new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight), bitmap.PixelFormat))
                {

                    byte[] bytes = BitmapToBytes(cloneBitmap);

                    using (var img = Tesseract.Pix.LoadFromMemory(bytes))
                    {
                        using (var page = engine.Process(img))
                        {
                            return page.GetText();
                        }
                    }
                }
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

        public static Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            var engine = new Tesseract.TesseractEngine(OCRTessdataFolder, OCRLanguage, Tesseract.EngineMode.Default);
            engine.SetVariable("tessedit_char_whitelist", "0123456789:oO"); //only look for pre-set chars for speed up

            //to remove "Empty page!!" either debug_file needs to be set for null, or DefaultPageSegMode needs to be set correctly
            //_tesseractEngine.SetVariable("debug_file", "NUL");
            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            return engine;
        }

        public static string FindTime(string data)
        {
            //hh:mm:ss
            const string regexPattern6Digits = @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";
            const int colonCount = 2;

            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] zeroAlikeArray = new[] { "o", "O" };

            foreach (string line in lines)
            {
                if (line.IndexOf(':') >= 0)
                {
                    //case 1 - xx: 00:01:26
                    //case 2 - 00:01:26
                    string line6TimeParts = line.Count(c => c == ':') > 2 ? line.Substring(line.IndexOf(":") + 1) : line;

                    //remove empty space
                    string line6TimePartsAdjust = line6TimeParts.Replace(" ", string.Empty);

                    foreach (string item in zeroAlikeArray)
                    {
                        line6TimePartsAdjust = line6TimePartsAdjust.Replace(item, "0");
                    }

                    if (Regex.IsMatch(line6TimePartsAdjust, regexPattern6Digits))
                    {
                        //timePartAdjust = timePartAdjust.Replace("00", "12"); 
                        //got bug when parse as datetime 00:00:123 -> 12:12:23 - no need to do this since we parse as timespan now
                        //if (timePartAdjust.StartsWith("00"))
                        //{
                        //    timePartAdjust = "12" + timePartAdjust.Substring(2);
                        //}

                        System.Diagnostics.Debug.WriteLine("-----------------------------regex line");
                        System.Diagnostics.Debug.WriteLine(line);
                        System.Diagnostics.Debug.WriteLine(line6TimeParts);
                        System.Diagnostics.Debug.WriteLine(line6TimePartsAdjust);

                        return line6TimePartsAdjust;
                    }

                    //handle case "1353:1131: 00:01:26", split it then take the last 3 parts
                    //do not remove empty entry here, do not want to parse invalid line part, e.g. [4  1  :: 7: 5     :: : 5  :   :: 1 ]
                    //string[] parts = charsAfterFirstColonAdjust.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    string[] parts = line6TimePartsAdjust.Split(new[] { ':' }, StringSplitOptions.None);
                    if (parts.Length > colonCount)
                    {
                        string last3Parts = $"{parts[parts.Length - 3]}:{parts[parts.Length - 2]}:{parts[parts.Length - 1]}";
                        if (Regex.IsMatch(last3Parts, regexPattern6Digits))
                        {
                            System.Diagnostics.Debug.WriteLine($"regex - get last 3 parts [{last3Parts}] from line [{line}]");
                            return last3Parts;
                        }
                    }

                    /*
                    //leotodo - ignore the following case 

                     5:11:33 :5 5
                     11:33 :5 5
                     11:33:55
                     */
                }
            }

            return string.Empty;
        }

        public static List<string> ValidateTimeSpanLines(string data)
        {
            if (data == null) return null;

            //mm:ss
            string regexPattern = @"^([0-5]?\d:[0-5]?\d)$";
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();

            foreach (string line in lines)
            {
                if (line.IndexOf(':') > 0)
                {
                    string timePartAdjust = line.Trim().Replace(": ", ":").Replace(" :", ":");

                    if (Regex.IsMatch(timePartAdjust, regexPattern))
                    {
                        //System.Diagnostics.Debug.WriteLine("-----------------------------");
                        //System.Diagnostics.Debug.WriteLine(line);
                        //System.Diagnostics.Debug.WriteLine(timePartAdjust);

                        result.Add(timePartAdjust);
                    }
                }
            }

            return result;
        }

        public static void SafeCheckImageBlock(ref int x, ref int y, ref int width, ref int height)
        {
            var screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

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



    public static class FormLeoExt
    {
        public static void OnError(this Form form, Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.ToString());

            if (GlobalData.Default.IsDebugging)
            {
                MessageBox.Show(e.ToString());
            }
            else
            {
                MessageBox.Show(e.Message);

                form.RunOnMain(() => GlobalData.Default.FireCloseApp());
            }
        }

        public static bool IsDead(this Form form)
        {
            return form.Disposing || form.IsDisposed;
        }

        public static void RunOnMain(this Form form, Action action)
        {
            if(action == null) return;
            if(form.IsDead()) return;

            //System.Diagnostics.Debug.WriteLine($"RunOnMain - is dead: {form.IsDead()}, disp: {form.Disposing}, disped:{form.IsDisposed} - before if");
            if (form.InvokeRequired)
            {
                if (form.IsDead()) return; //not sure why, the is dead check above not working sometimes, do it again here
                if (form.Disposing || form.IsDisposed) return;
                //System.Diagnostics.Debug.WriteLine($"RunOnMain - is dead: {form.IsDead()}, disp: {form.Disposing}, disped:{form.IsDisposed}");
                form.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void RunOnMain(this Form form, Action action, int delayMS)
        {
            Task.Run(() =>
            {
                Thread.Sleep(delayMS);
                RunOnMain(form, action);
            });
        }

        public static void RunOnMainAsync(this Form form, Action action)
        {
            if (action == null) return;
            if (form.IsDead()) return;

            if (form.InvokeRequired)
            {
                form.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static void RunOnMainAsync(this Form form, Action action, int delayMS)
        {
            Task.Run(() =>
            {
                Thread.Sleep(delayMS);
                RunOnMainAsync(form, action);
            });
        }

        public static PowerLog Log(this Form form)
        {
            return PowerLog.One;
        }

        public static void DisableButtonShortTime(this Form form, Label control)
        {
            var oldForeColor = control.ForeColor;
            var oldBackColor = control.BackColor;


            control.ForeColor = System.Drawing.Color.White;
            control.BackColor = System.Drawing.Color.LightGray;
            control.Enabled = false;

            form.RunOnMain(() =>
            {
                control.ForeColor = oldForeColor;
                control.BackColor = oldBackColor;
                control.Enabled = true;
            }, 300);
        }

        public static void DisableButtonShortTime(this Form form, Button control)
        {
            var oldForeColor = control.ForeColor;
            var oldBackColor = control.BackColor;

            //leotodo, tmp fix for action bar, or else only the first click will work, and button become disabled forever
            //is this caused by the transparent background/color ?
            oldForeColor = System.Drawing.Color.Black;
            oldBackColor = System.Drawing.Color.White;


            control.ForeColor = System.Drawing.Color.White;
            control.BackColor = System.Drawing.Color.LightGray;
            control.Enabled = false;

            form.RunOnMainAsync(() =>
            {
                control.ForeColor = oldForeColor;
                control.BackColor = oldBackColor;
                control.Enabled = true;
            }, 300);
        }

    }
}
