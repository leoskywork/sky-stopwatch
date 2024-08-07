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
    public class MainOCRBossCounting
    {
        //big screen (pc-pro) ----- 1470, 240, 160, 740
        public static int XPoint = 1470;
        public static int YPoint = 240;
        public static int BlockWidth = 140;
        public static int BlockHeight = 740;

        public static bool EnableAutoSlice = false;
        public static int AutoSliceIntervalSeconds = 12;



        public static byte[] GetFixedLocationImageData()
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
            var engine = new Tesseract.TesseractEngine(MainOCR.OCRTessdataFolder, MainOCR.OCRLanguage, Tesseract.EngineMode.Default);

            //in case the number got blocked by other images?? so try to recognise multi digits here ??
            engine.SetVariable("tessedit_char_whitelist", "123456789"); //only look for pre-set chars for speed up

            //to remove "Empty page!!" either debug_file needs to be set for null, or DefaultPageSegMode needs to be set correctly
            //_tesseractEngine.SetVariable("debug_file", "NUL");
            engine.DefaultPageSegMode = PageSegMode.SingleBlock;

            return engine;
        }



        public static Tuple<bool, string, int> FindBossCall(string data, params int[] candidates)
        {
            if (data == null || candidates == null) throw new ArgumentNullException("data or candidates");

            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) return Tuple.Create(false, "lines-length-0", -1);

            //sometimes, failed to recognise 5, so add one more candidate(4) here
            //still, the ocr engine is poor, lots of blank-almost pics are processed as value 5, leotodo, improve the engine or replace it
            //if (lines.Length == 1 && (lines[0] == candidate1.ToString() || lines[0] == candidate2.ToString()))
            //if (lines.Length == 1 && lines[0] == candidate1.ToString())
            //{
            //    return Tuple.Create(true, lines[0]);
            //}



            for (int i = 0; i < candidates.Length; i++)
            {
                string current = candidates[i].ToString();

                if (current == lines[0])
                {
                    return Tuple.Create(true, current, candidates[i]);
                }
            }


            return Tuple.Create(false, lines[0], -1);
        }



    }
}
