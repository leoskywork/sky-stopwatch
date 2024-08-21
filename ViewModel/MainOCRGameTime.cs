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
    public class MainOCRGameTime : MainOCR
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


        public bool IsUsingScreenTopTime { get; set; } = true;//false;


        public override Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            return MainOCR.GetOCREngine("0123456789:oO");
        }

        public override Rectangle GetScreenBlock()
        {
            if (IsUsingScreenTopTime)
            {
                return new Rectangle(976, 210, 60, 60);
            }

            return new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight);
        }

        public string ReadImageFromFile(string imgPath)
        {
            using (var engine = GetDefaultOCREngine())
            {
               return MainOCR.ReadImageFromFile(engine, imgPath, GetScreenBlock());
            }
        }


        public string Find(string data)
        {
            if(string.IsNullOrWhiteSpace(data)) return string.Empty;

            //leotodo, a tmp fix, screen top time has 4 digits(not 6)
            if (this.IsUsingScreenTopTime)
            {
                data = "00:" + data;
            }


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

   
    }
}
