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


        public static bool FindPrice(string data)
        {
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] zeroAlikeArray = new[] { "o", "O" };
            string targetPriceString = TargetPrice.ToString();

            foreach (string line in lines)
            {

                string lineAdjust = line.Trim(); //line.Replace(" ", string.Empty);

                foreach (string item in zeroAlikeArray)
                {
                    lineAdjust = lineAdjust.Replace(item, "0");
                }

                if (lineAdjust.Split(' ').Any(word => word == targetPriceString))
                {
                    System.Diagnostics.Debug.WriteLine($"-----------------------------FindPrice line {targetPriceString}");
                    System.Diagnostics.Debug.WriteLine(line);
                    System.Diagnostics.Debug.WriteLine(lineAdjust);

                    return true;
                }
            }

            return false;
        }



    }
}
