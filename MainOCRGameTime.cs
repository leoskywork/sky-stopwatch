using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace SkyStopwatch
{
    public class MainOCRGameTime
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

     

        public const int ManualOCRDelaySeconds = 10;
        public const int AutoOCRDelaySeconds = 2;
        public const int NewGameDelaySeconds = 1;//10;
        public const int NoDelay = 0;



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
                catch (Win32Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"win32 error: {ex}");
                    return null;
                }

                if (onlyReturnPartOfImage) //for speed up
                {
                    using (Bitmap cloneBitmap = bitPic.Clone(new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight), bitPic.PixelFormat))
                    {
                        return MainOCR.BitmapToBytes(cloneBitmap);
                    }
                }

                return MainOCR.BitmapToBytes(bitPic);
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

                    byte[] bytes =  MainOCR.BitmapToBytes(cloneBitmap);

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

        public static Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            var engine = new Tesseract.TesseractEngine(GlobalData.OCRTessdataFolder, GlobalData.OCRLanguage, Tesseract.EngineMode.Default);
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

        public static Rectangle GetRectMiddle()
        {
            return new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight);
        }
    }
}
