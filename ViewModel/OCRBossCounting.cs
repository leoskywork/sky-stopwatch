using SkyStopwatch.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace SkyStopwatch
{
    public class OCRBossCounting : OCRBase
    {
        //big screen (pc-pro) ----- 1470, 240, 160, 740
        public static int XPoint = 1470;
        public static int YPoint = 240;
        public static int BlockWidth = 140;
        public static int BlockHeight = 740;


        //leotdo, save to config file
        //public static int AUXXPoint = 1226;
        //public static int AUXYPoint = 512;
        //public static int AUXBlockWidth = 14;
        //public static int AUXBlockHeight = 20; //seems easy get wrong value when img too small
        public static int AUXXPoint = 1211;
        public static int AUXYPoint = 508;
        public static int AUXBlockWidth = 30;
        public static int AUXBlockHeight = 30;


        public static bool EnableAutoSlice = false;
        public static int AutoSliceIntervalSeconds = 12;


        public override Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            return OCRBase.GetOCREngine("0123456789oO"); //only look for pre-set chars for speed up
        }

        public TinyScreenShotBossCall GetFixedLocationImageDataPair(bool includeAUX)
        {
            var rect = GetScreenBlock();
            var rectAUX = GetScreenBlockAUX();
            var bytes = OCRBase.PrintScreenAsBytes(rect, rectAUX);

            return new TinyScreenShotBossCall(bytes.Item1, bytes.Item2);
        }

        public override Rectangle GetScreenBlock()
        {
            return new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight);
        }

        public Rectangle GetScreenBlockAUX()
        {
            return new Rectangle(AUXXPoint, AUXYPoint, AUXBlockWidth, AUXBlockHeight);
        }


        public OCRCompareResult<int> Find(string data, params int[] candidates)
        {
            if (data == null || candidates == null) throw new ArgumentNullException("data or candidates");

            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) return OCRCompareResult<int>.Create(false, "lines-length-0", -2);

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
                    return OCRCompareResult<int>.Create(true, current, candidates[i]);
                }
            }


            return OCRCompareResult<int>.Create(false, lines[0], -1);
        }
        public OCRCompareResult<int> FindPair(string data, int max, int min)
        {
            if (data == null) throw new ArgumentNullException("data or candidates");

            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length == 0) return OCRCompareResult<int>.Create(false, "lines-length-0", -2);


            for (int i = max; i >= min; i--)
            {
                if (i.ToString() == lines[0])
                {
                    return OCRCompareResult<int>.Create(true, lines[0], i);
                }
            }

            return OCRCompareResult<int>.Create(false, lines[0], -1);
        }
    }
}
