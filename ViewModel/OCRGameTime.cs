using SkyStopwatch.DataModel;
using SkyStopwatch.ViewModel;
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
using TesseractOCR.Pix;

namespace SkyStopwatch
{
    public class OCRGameTime : OCRBase
    {
        public static int XPoint = 1084;
        public static int YPoint = 1068;
        public static int BlockWidth = 140;
        public static int BlockHeight = 30;

        public static int TopXPoint = 0;
        public static int TopYPoint = 0;
        public static int TopBlockWidth = 100;
        public static int TopBlockHeight = 100;

        public static int InGameFlagXPoint = 0;
        public static int InGameFlagYPoint = 0;
        public static int InGameFlagBlockWidth = 100;
        public static int InGameFlagBlockHeight = 100;

        public static int Preset1XPoint = 0;
        public static int Preset1YPoint = 0;
        public static int Preset1BlockWidth = 100;
        public static int Preset1BlockHeight = 100;

        public static int Preset2XPoint = 0;
        public static int Preset2YPoint = 0;
        public static int Preset2BlockWidth = 100;
        public static int Preset2BlockHeight = 100;


        public const int ManualOCRDelaySeconds = 10;
        public const int AutoOCRDelaySeconds = 2;
        public const int NewGameDelaySeconds = 1;//10;
        public const int NoDelay = 0;

        public const int TimerDisplayUIIntervalMS = 200;//100; //this need less than 1000, otherwise the app exit logic will be broken

        public const int TimerAutoOCRFastIntervalMS = 800; // this value x 3 should less than 1000 (1 second)
        public const int TimerAutoOCRSlowIntervalMS = 10 * 1000;

        public const int TimerAutoOCRPreGameFastIntervalMS = 1000;
        public const int TimerAutoOCRPreGameSlowIntervalMS = 3000;//4 * 1000;//3000;

        public const int TimerNapSeconds = 2;//10; //ensure nap x successlimit > 1 minute
        public const int SuccessLimit = 20;//8;//10;//120 * 1000 / TimerAutoOCRSlowIntervalMS;//3; //success 2 minutes in a row
        public const int EmptyLimit = 15;//30;
        public const int FailParseLimit = 3;
        public const int MisreadLimit = 70;// 50;

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
            }
        }

        private DateTime _GameTimeLastUpdateTime;

        private int _GameRemainingSeconds;
        public int GameRemainingSeconds
        {
            get
            {
                const int magicSeconds = GlobalData.GameRoundAdjustSeconds;
                DateTime gameEndTime = _TimeAroundGameStart.AddMinutes(GlobalData.GameRoundMaxMinute).AddSeconds(magicSeconds);

                _GameRemainingSeconds = (int)(gameEndTime - DateTime.Now).TotalSeconds;

                return _GameRemainingSeconds;
            }
        }

        //leotodo, a better way to do this is using enum instead of 4 countsl
        private int _AutoOCRSuccessCount = 0;
        private int _AutoOCREmptyInARowCount = 0;
        private int _AutoOCRFailParseInARowCount = 0;
        private int _AutoOCRMisreadInARowCount = 0;
        private int _AutoOCRInGameFlagInARowCount = 0;

        private bool _HackMisread2xAs1xAndPauseReading = false;
        public bool IsWithinOneGameRoundOrNap
        {
            get
            {
                if (_AutoOCREmptyInARowCount > EmptyLimit) //total = 20 x auto-timer-fast-interval
                {
                    return false;
                }

                bool withinOneNap = false;

                if (_TimeAroundGameStart != DateTime.MinValue)
                {
                    var sinceLastUpdate = DateTime.Now - _GameTimeLastUpdateTime;
                    //leotodo, issue here, just manual reset after game end/manual exit game
                    if (sinceLastUpdate.TotalSeconds >= this.GameRemainingSeconds)//600)//10)// 120)
                    {
                        _AutoOCRSuccessCount = 0;
                        _AutoOCREmptyInARowCount = 0;
                        _AutoOCRFailParseInARowCount = 0;
                        return false;
                    }

                    withinOneNap = sinceLastUpdate.TotalSeconds < TimerNapSeconds;
                }

                return _HackMisread2xAs1xAndPauseReading || _AutoOCRSuccessCount >= SuccessLimit || withinOneNap;
            }
        }

        public bool IsEmptyReadTooMany { get { return _AutoOCREmptyInARowCount > EmptyLimit / 3; } }

        public int EmptyCount { get { return _AutoOCREmptyInARowCount; } }

        public int InGameFlagCount { get { return _AutoOCRInGameFlagInARowCount; } }

        public bool IsTimeLocked { get; set; }

        public TimeLocKSource LockSource { get; set; } = TimeLocKSource.Unset;

        public TimeChangeSource TimeChangeSource { get; set; } = TimeChangeSource.Unset;

        public OCRGameTime()
        {

        }

        public override Tesseract.TesseractEngine CreateOCREngine()
        {
            return CreateOCREngineWith("0123456789:oO");
        }

        public override Rectangle GetScreenBlock()
        {
            var kind = GlobalData.Default.IsUsingScreenTopTime ? TimeScanKind.TopMiniTime : TimeScanKind.MiddleTime;

            return GetImagePreviewArgs(kind);
        }

        public static Rectangle GetDefaultTimeBlock(TimeScanKind scanKind)
        {
            if (scanKind == TimeScanKind.TopMiniTime)
            {
                return new Rectangle(976, 216, 60, 60);
            }

            if (scanKind == TimeScanKind.InGameFlag)
            {
                return new Rectangle(1522, 250, 24, 24);
            }

            return new Rectangle(1080, 980, 140, 30);
        }

        public string ReadImageFromFile(string imgPath)
        {
            using (var engine = CreateOCREngine())
            {
                return OCRBase.ReadImageFromFile(engine, imgPath, GetScreenBlock());
            }
        }


        public string Find(string data, bool isTopTime)
        {
            if (string.IsNullOrWhiteSpace(data)) return string.Empty;

            //screen top time has 4 digits(not 6)
            if (isTopTime)
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

                        if (GlobalData.Default.IsDebugging)
                        {
                            System.Diagnostics.Debug.WriteLine("-------------find------------");
                            System.Diagnostics.Debug.WriteLine(line);
                            System.Diagnostics.Debug.WriteLine(line6TimeParts);
                            System.Diagnostics.Debug.WriteLine(line6TimePartsAdjust);
                            System.Diagnostics.Debug.WriteLine("-----------------------------");
                        }

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


        public bool IsOCRTopTimeMisread(string ocrDisplayTime)
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

            this._AutoOCREmptyInARowCount = 0;

            var misreadKind = CheckTopTimeInternal(ocrDisplayTime);
            if (misreadKind == TimeMisreadKind.Treat2xAs1x && !IsNearPhaseBossesTime())
            {
                _HackMisread2xAs1xAndPauseReading = true;
            }
            else
            {
                _HackMisread2xAs1xAndPauseReading = false;
            }

            return misreadKind != TimeMisreadKind.None;
        }

        private TimeMisreadKind CheckTopTimeInternal(string ocrDisplayTime)
        {
            if (TimeSpan.TryParseExact(ocrDisplayTime, GlobalData.TimeSpanFormat, System.Globalization.CultureInfo.InvariantCulture, out TimeSpan ocrTimeSpan))
            {
                if (ocrTimeSpan.Minutes > GlobalData.GameRoundMaxMinute)
                {
                    return TimeMisreadKind.GreaterThanMaxMinute;
                }

                if (ocrTimeSpan.TotalSeconds < 60)
                {
                    System.Diagnostics.Debug.WriteLine($"--> not a misread, game just start");
                    return TimeMisreadKind.None;
                }

                if (ocrTimeSpan.Minutes < 10 && IsNearPhaseBossesTime())
                {
                    System.Diagnostics.Debug.WriteLine($"--> not a misread, last read near exit point");
                    return TimeMisreadKind.None;
                }

                if (this._TimeAroundGameStart == DateTime.MinValue)
                {
                    System.Diagnostics.Debug.WriteLine($"--> first read: {ocrDisplayTime}");
                    return TimeMisreadKind.None;
                }

                var now = DateTime.Now;
                var sinceGameStart = now - this._TimeAroundGameStart;
                var sinceLastUpdate = now - this._GameTimeLastUpdateTime;

                if (this.TimeChangeSource == TimeChangeSource.AppAutoUpdateBySecondary)
                {
                    if ((sinceGameStart - ocrTimeSpan).TotalSeconds < 60 * 2)
                    {
                        System.Diagnostics.Debug.WriteLine($"--> first top read, replacing middle");
                        return TimeMisreadKind.None;
                    }

                    if (ocrTimeSpan.Minutes > 30)
                    {
                        return TimeMisreadKind.GreaterThanJoinGameMaxMinute;
                    }
                }

                //treat 0x/1x as 3x
                if (ocrTimeSpan.Minutes >= 30 && sinceGameStart.Minutes <= 19)
                {
                    return TimeMisreadKind.Treat0xOr1xAs3x;
                }

                //coner case: sometimes, treat '2x' as '1x'(e.g. 23 as 13), ignore it
                if (ocrTimeSpan.Minutes >= 10 && ocrTimeSpan.Minutes <= 19)
                {
                    //if (sinceGameStart.Minutes >= 20 && wasFoundEmptyOCR)
                    if (sinceGameStart.Minutes >= 20 && sinceGameStart.Minutes <= 29)
                    {
                        System.Diagnostics.Debug.WriteLine($"--> misread 2x as 1x: {ocrDisplayTime}");
                        return TimeMisreadKind.Treat2xAs1x;
                    }
                }

                //less than last read
                var ocrTimeSpanAdjust = TimeSpan.FromSeconds(ocrTimeSpan.TotalSeconds + AutoOCRDelaySeconds + 3);
                if (ocrTimeSpanAdjust < sinceGameStart && (_AutoOCRSuccessCount >= SuccessLimit || sinceLastUpdate.TotalSeconds < this.GameRemainingSeconds))// sinceLastUpdate.TotalSeconds < napSecondsAdjust))
                {
                    if (this.TimeChangeSource == TimeChangeSource.AppAutoRestart)
                    {
                        System.Diagnostics.Debug.WriteLine($"--> not a misread, src: {TimeChangeSource}");
                        return TimeMisreadKind.None;
                    }

                    _AutoOCRMisreadInARowCount++;
                    System.Diagnostics.Debug.WriteLine($"--> misread #{_AutoOCRMisreadInARowCount}: {ocrDisplayTime}, should NOT less than {this.AutoOCRTimeOfLastRead} + {(int)sinceLastUpdate.TotalSeconds}");
                    System.Diagnostics.Debug.WriteLine($"--> since game start: {sinceGameStart}, since last update: {sinceLastUpdate}");

                    if (_AutoOCRMisreadInARowCount < MisreadLimit)
                    {
                        return TimeMisreadKind.LessThanLastRead;
                    }
                    else //so manny misread, possiblily a correct read, and the last success read is a misread
                    {
                        _AutoOCRMisreadInARowCount = 0;
                        _AutoOCRSuccessCount = 0;
                        _GameRemainingSeconds = 0;
                    }
                }
                else
                {
                    _AutoOCRSuccessCount++;
                    _AutoOCRMisreadInARowCount = 0;
                    System.Diagnostics.Debug.WriteLine($"success #{_AutoOCRSuccessCount} ocr: {ocrDisplayTime}");
                }

                _AutoOCRFailParseInARowCount = 0;
            }
            else
            {
                _AutoOCRFailParseInARowCount++;
                if (_AutoOCRFailParseInARowCount >= FailParseLimit)
                {
                    _AutoOCRSuccessCount = 0;
                    System.Diagnostics.Debug.WriteLine("reset success #, failed to parse：" + ocrDisplayTime);
                }
            }

            return TimeMisreadKind.None;
        }

        public void ResetAutoOCR(TimeLocKSource source)
        {
            this.AutoOCRTimeOfLastRead = null;
            _TimeAroundGameStart = DateTime.MinValue;
            _GameTimeLastUpdateTime = DateTime.MinValue;
            _GameRemainingSeconds = 0;

            _AutoOCRSuccessCount = 0;
            _AutoOCREmptyInARowCount = 0;
            _AutoOCRFailParseInARowCount = 0;
            _AutoOCRMisreadInARowCount = 0;
            _AutoOCRInGameFlagInARowCount = 0;

            this.IsTimeLocked = false;
            this.LockSource = source;
            _HackMisread2xAs1xAndPauseReading = false;
        }

        public void ResetAutoLockArgs(TimeLocKSource source)
        {
            _AutoOCRSuccessCount = 0;
            this.IsTimeLocked = false;
            this.LockSource = source;
            _HackMisread2xAs1xAndPauseReading = false;
        }

        public bool ShouldAutoLock()
        {
            //do not lock if not in game
            if (_AutoOCRInGameFlagInARowCount <= 0)
            {
                return false;
            }

            if (this.ShouldAutoUnlock())
            {
                return false;
            }

            if (this.TimeChangeSource == TimeChangeSource.AppAutoUpdateBySecondary)
            {
                return false;
            }


            //do not lock when game just start
            if (this.GameRemainingSeconds > 37 * 60)
            {
                _AutoOCRSuccessCount = 0; //also not count as one success
                return false;
            }

            //early lock when game start less than 10 min
            if (_AutoOCRSuccessCount > 1 && this.GameRemainingSeconds > 28 * 60)
            {
                return true;
            }

            if (_AutoOCRSuccessCount > 3 && this.GameRemainingSeconds > 60)
            {
                return true;
            }

            return false;
        }

        public bool ShouldAutoUnlock()
        {
            if (this.IsTimeLocked && this.LockSource == TimeLocKSource.UserClick) { return false; }

            int remaining = this.GameRemainingSeconds;
            System.Diagnostics.Debug.WriteLine($"remaining：{remaining / 60}:{remaining % 60}");

            if (_AutoOCRInGameFlagInARowCount < 0) return true; //do not unlock if the first time not find in-game-flag (count is 0)

            //game end
            if (remaining <= 0)
            {
                return true;
            }

            if (IsNearPhaseBossesTime())
            {
                if (_AutoOCRInGameFlagInARowCount > 0) //do not unlock if still in game
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        private bool IsNearPhaseBossesTime()
        {
            int remaining = this.GameRemainingSeconds;

            //1st phase boss
            int phase1Low = GlobalData.GameRoundAdjustSeconds + 27 * 60;
            if (remaining > phase1Low && remaining < phase1Low + 60)
            {
                return true;
            }

            //2nd phase boss
            int phase2Low = GlobalData.GameRoundAdjustSeconds + 16 * 60;
            if (remaining > phase2Low && remaining < phase2Low + 120)
            {
                return true;
            }

            return false;
        }

        public byte[] GetImageBytesBy(TimeScanKind scanKind)
        {
            var block = GetImagePreviewArgs(scanKind);
            var pair = PrintScreenAsBytes(block);

            if (pair == null || pair.Item1 == null)
            {
                System.Diagnostics.Debug.WriteLine("screenShotBytes is null");
            }

            return pair?.Item1;
        }

        public static Rectangle GetImagePreviewArgs(TimeScanKind scanKind)
        {
            if (scanKind == TimeScanKind.TopMiniTime)
            {
                return new Rectangle(OCRGameTime.TopXPoint, OCRGameTime.TopYPoint, OCRGameTime.TopBlockWidth, OCRGameTime.TopBlockHeight);
            }

            if (scanKind == TimeScanKind.InGameFlag)
            {
                return new Rectangle(OCRGameTime.InGameFlagXPoint, OCRGameTime.InGameFlagYPoint, OCRGameTime.InGameFlagBlockWidth, OCRGameTime.InGameFlagBlockHeight);
            }

            if (scanKind == TimeScanKind.MiddleTime)
            {
                return new Rectangle(OCRGameTime.XPoint, OCRGameTime.YPoint, OCRGameTime.BlockWidth, OCRGameTime.BlockHeight);
            }

            throw new NotImplementedException();
        }

        public bool FindInGameFlag(string flagData)
        {
            bool found = false;

            if (!string.IsNullOrEmpty(flagData))
            {
                string[] lines = flagData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                found = lines[0].Trim() == "35";
            }

            if(found)
            {
                if(_AutoOCRInGameFlagInARowCount > 0)
                {
                    _AutoOCRInGameFlagInARowCount++;
                }
                else
                {
                    _AutoOCRInGameFlagInARowCount = 1;
                }
            }
            else
            {
                if(_AutoOCRInGameFlagInARowCount > 0)
                {
                    _AutoOCRInGameFlagInARowCount = 0;
                }
                else
                {
                    _AutoOCRInGameFlagInARowCount--;
                }
            }

            if (_AutoOCRInGameFlagInARowCount <= 0)
            {
                _HackMisread2xAs1xAndPauseReading = false;
            }

            return found;
        }
    }


}
