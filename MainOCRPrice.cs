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
    public class MainOCRPrice
    {
        //leotodo, a better way to do this, config file ？
        //big screen (pc-pro) ----- 1470, 240, 160, 740
        public static int XPoint = 1470;
        public static int YPoint = 240;
        public static int BlockWidth = 160;
        public static int BlockHeight = 740;

        public static int TargetPrice = 1000;
        public static int Aux1Price = 1001;
        public static int Aux2Price = 1002;


        public static byte[] GetPriceImageData()
        {
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            if (MainOCR.IsDebugging)
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

                //if (onlyReturnPartOfImage) //for speed up
                {
                    using (Bitmap cloneBitmap = bitPic.Clone(new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight), bitPic.PixelFormat))
                    {
                        return MainOCR.BitmapToBytes(cloneBitmap);
                    }
                }

            }
        }


        public static Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            const string language = "eng"; //chi_sim;
            const string tessdataFolder = @"C:\Dev\VS2022\SkyStopwatch\Tesseract-OCR\tessdata\";

            var engine = new Tesseract.TesseractEngine(tessdataFolder, language, Tesseract.EngineMode.Default);
            engine.SetVariable("tessedit_char_whitelist", "0123456789oO"); //only look for pre-set chars for speed up

            //to remove "Empty page!!" either debug_file needs to be set for null, or DefaultPageSegMode needs to be set correctly
            //_tesseractEngine.SetVariable("debug_file", "NUL");
            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            return engine;
        }


        public static Tuple<bool, int> FindPrice(string data, bool enableAux1, bool enableAux2)
        {
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] zeroAlikeArray = new[] { "o", "O" };
           
            string targetPriceString = TargetPrice.ToString();
            string aux1String = enableAux1 ? MainOCRPrice.Aux1Price.ToString() : "-1";
            string aux2String = enableAux2 ? MainOCRPrice.Aux2Price.ToString() : "-1";
            //the coin icon at the last position, sometimes treat as 9, sometimes 5, 1, 0
            //just remove the last char
            //bool allEndWithZero = true;
            //bool allEndWithNine = true;
            List<string> linesWithoutLastChars = new List<string>();
            int lineIndex = -1;

            foreach (string line in lines)
            {
                lineIndex++;
                string lineAdjust = line.Trim();

                foreach (string item in zeroAlikeArray)
                {
                    lineAdjust = lineAdjust.Replace(item, "0");
                }

                string[] parts = lineAdjust.Split(' ');

                if (parts.Any(p => p == targetPriceString || p == aux1String || p == aux2String))
                {
                    System.Diagnostics.Debug.WriteLine($"-----------------------------FindPrice line {targetPriceString}");
                    System.Diagnostics.Debug.WriteLine(line);
                    System.Diagnostics.Debug.WriteLine(lineAdjust);

                    return Tuple.Create( true, lineIndex);
                }

                //case: 1000
                //      1154 0
                //sometimes, there is a space, so entire line, not first part
                string lineToAdd = lineAdjust.Replace(" ", string.Empty);
                //if (!lineToAdd.EndsWith("0"))
                //{
                //    allEndWithZero = false;
                //}
                //if (!lineToAdd.EndsWith("9"))
                //{
                //    allEndWithNine = false;
                //}

                linesWithoutLastChars.Add(lineToAdd.Substring(0, lineToAdd.Length - 1));
            }

            //if (allEndWithZero || allEndWithNine)
            {
                bool found = linesWithoutLastChars.Any(p => p == targetPriceString || p == aux1String || p == aux2String);
                lineIndex = -100;
                return Tuple.Create(found, lineIndex);
            }

            //return false;
        }


    }
}
