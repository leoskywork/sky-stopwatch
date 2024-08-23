﻿using System;
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
    public class OCRGameTime : OCRBase
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

        public const int TimerNapSeconds = 10;
        public const int TimerDisplayUIIntervalMS = 100;

        public const int TimerAutoOCRFastIntervalMS = 800; // this value x 3 should less than 1000 (1 second)
        public const int TimerAutoOCRSlowIntervalMS = 10 * 1000;

        public const int SuccessLimit = 8;//10;//120 * 1000 / TimerAutoOCRSlowIntervalMS;//3; //success 2 minutes in a row
        public const int EmptyLimit = 30;
        public const int FailParseLimit = 3;
         
        public int BootingArgs { get; set; } = 0;
        public string AutoOCRTimeOfLastRead { get; set; }

        private DateTime _TimeAroundGameStart;
        public DateTime TimeAroundGameStart
        {
            get { return _TimeAroundGameStart; }
            set
            {
                _TimeAroundGameStart = value;
                _GameTimeLastUpdateTime = DateTime.Now;
                _GameRemainingSeconds = (int)(_TimeAroundGameStart.AddMinutes(GlobalData.MaxScreenTopGameMinute) - _GameTimeLastUpdateTime).TotalSeconds;
            }
        }
        private DateTime _GameTimeLastUpdateTime;
        private int _GameRemainingSeconds;

        private int _AutoOCRSuccessCount = 0;
        private int _AutoOCREmptyInARowCount = 0;
        private int _AutoOCRFailParseInARowCount = 0;
        public bool IsWithinOneGameRoundOrNap
        {
            get
            {
                if (_AutoOCREmptyInARowCount > EmptyLimit) //total = 20 x auto-timer-fast-interval
                {
                    return false;
                }

                var sinceLastUpdate = DateTime.Now - _GameTimeLastUpdateTime;
                //leotodo, issue here, just manual reset after game end/manual exit game
                if (sinceLastUpdate.TotalSeconds >= _GameRemainingSeconds)//600)//10)// 120)
                {
                    _AutoOCRSuccessCount = 0;
                    _AutoOCREmptyInARowCount = 0;
                    _AutoOCRFailParseInARowCount = 0;
                    return false;
                }

                return _AutoOCRSuccessCount >= SuccessLimit || sinceLastUpdate.TotalSeconds < TimerNapSeconds;
            }
        }



        public override Tesseract.TesseractEngine GetDefaultOCREngine()
        {
            return OCRBase.GetOCREngine("0123456789:oO");
        }

        public override Rectangle GetScreenBlock()
        {
            if (GlobalData.Default.IsUsingScreenTopTime)
            {
                return new Rectangle(976, 216, 60, 60);
            }

            return new Rectangle(XPoint, YPoint, BlockWidth, BlockHeight);
        }

        public string ReadImageFromFile(string imgPath)
        {
            using (var engine = GetDefaultOCREngine())
            {
               return OCRBase.ReadImageFromFile(engine, imgPath, GetScreenBlock());
            }
        }


        public string Find(string data)
        {
            if(string.IsNullOrWhiteSpace(data)) return string.Empty;

            //leotodo, a tmp fix, screen top time has 4 digits(not 6)
            if (GlobalData.Default.IsUsingScreenTopTime)
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

                        //System.Diagnostics.Debug.WriteLine("regex line");
                        System.Diagnostics.Debug.WriteLine("-------------find------------");
                        System.Diagnostics.Debug.WriteLine(line);
                        System.Diagnostics.Debug.WriteLine(line6TimeParts);
                        System.Diagnostics.Debug.WriteLine(line6TimePartsAdjust);
                        System.Diagnostics.Debug.WriteLine("-----------------------------");

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


        public bool IsOCRTimeMisread(string ocrDisplayTime)
        {
            if (!GlobalData.Default.IsUsingScreenTopTime) return false;

            if (string.IsNullOrWhiteSpace(ocrDisplayTime))
            {
                this._AutoOCREmptyInARowCount++;
                this._AutoOCRFailParseInARowCount = 0;
                if (_AutoOCREmptyInARowCount > EmptyLimit) 
                {
                    System.Diagnostics.Debug.WriteLine($"reset counts, too many empty: {_AutoOCREmptyInARowCount}");
                    _AutoOCRSuccessCount = 0;
                    _AutoOCREmptyInARowCount = 0;
                }
                return false;
            }

            bool wasFoundEmptyOCR = _AutoOCREmptyInARowCount > 0;
            this._AutoOCREmptyInARowCount = 0;
             

            if (TimeSpan.TryParseExact(ocrDisplayTime, GlobalData.TimeSpanFormat, System.Globalization.CultureInfo.InvariantCulture, out TimeSpan ocrTimeSpan))
            {
                if (ocrTimeSpan.Minutes >= GlobalData.MaxGameRoundMinutes)
                {
                    return true;
                }

                var now = DateTime.Now;
                var sinceGameStart = now - this._TimeAroundGameStart;
                var sinceLastUpdate = now - this._GameTimeLastUpdateTime;

                //coner case: sometimes, treat '2x' as '1x'(e.g. 23 as 13), ignore it
                if (ocrTimeSpan.Minutes >= 10 && ocrTimeSpan.Minutes <= 19)
                {
                    if (sinceGameStart.Minutes >= 20 && wasFoundEmptyOCR)
                    {
                        return true;
                    }
                }

                //int napSecondsAdjust = TimerNapSeconds + 20;
                var ocrTimeSpanAdjust = TimeSpan.FromSeconds(ocrTimeSpan.TotalSeconds + AutoOCRDelaySeconds + 3);
                if (ocrTimeSpanAdjust < sinceGameStart && (_AutoOCRSuccessCount >= SuccessLimit || sinceLastUpdate.TotalSeconds < _GameRemainingSeconds))// sinceLastUpdate.TotalSeconds < napSecondsAdjust))
                {
                    System.Diagnostics.Debug.WriteLine($"--> misread ocr time: {ocrDisplayTime}, should NOT less than {this.AutoOCRTimeOfLastRead} + {(int)sinceLastUpdate.TotalSeconds}");
                    System.Diagnostics.Debug.WriteLine($"--> since game start: {sinceGameStart}, since last update: {sinceLastUpdate}");
                    return true;
                }
                else
                {
                    _AutoOCRSuccessCount++;
                    System.Diagnostics.Debug.WriteLine($"success count: {_AutoOCRSuccessCount}, ocr time: {ocrDisplayTime}");
                }

                _AutoOCRFailParseInARowCount = 0;
            }
            else
            {
                _AutoOCRFailParseInARowCount++;
                if (_AutoOCRFailParseInARowCount >= FailParseLimit)
                {
                    _AutoOCRSuccessCount = 0;
                    System.Diagnostics.Debug.WriteLine("reset success count, failed to parse：" + ocrDisplayTime);
                }
            }

            return false;
        }

        public void ResetAutoOCR()
        {
            this.AutoOCRTimeOfLastRead = null;
            _TimeAroundGameStart = DateTime.MinValue;
            _GameTimeLastUpdateTime = DateTime.MinValue;
            _GameRemainingSeconds = 0;

            _AutoOCRSuccessCount = 0;
            _AutoOCREmptyInARowCount = 0;
            _AutoOCRFailParseInARowCount = 0;
        }

    }
}