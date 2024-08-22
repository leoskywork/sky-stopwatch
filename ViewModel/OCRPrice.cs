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
    public class OCRPrice : OCRBase
    {
        //big screen (pc-pro) ----- 1470, 240, 160, 740
        public static int XPoint = 1470;
        public static int YPoint = 240;
        public static int BlockWidth = 140;
        public static int BlockHeight = 740;

        public static int TargetPrice = 1000;
        public static int Aux1Price = 1001;
        public static int Aux2Price = 1002;


        public override Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            return OCRBase.GetOCREngine("0123456789oO"); //only look for pre-set chars for speed up
        }


        public override Rectangle GetScreenBlock()
        {
            return new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight);
        }

        public Tuple<bool, int> Find(string data, bool enableAux1, bool enableAux2)
        {
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string[] zeroAlikeArray = new[] { "o", "O" };
           
            string targetPriceString = TargetPrice.ToString();
            string aux1String = enableAux1 ? OCRPrice.Aux1Price.ToString() : "-1";
            string aux2String = enableAux2 ? OCRPrice.Aux2Price.ToString() : "-1";
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
