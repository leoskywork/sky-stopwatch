using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCall : DataItem
    {
        public BossCall()
        {
        }

        public DateTime FirstMatchTime { get; set; }
        public int FirstMatchValue { get; set; }

        public DateTime SecondMatchTime { get; set; }
        public int SecondMatchValue { get; set; }

        public bool IsValid { get; set; }


        public int OCRLastMatch { get; set; }

         
        //does NOT involve in busness logic, just for fast ui updating of boss call number
        public bool PreCounting { get; set; }


        public bool IsTop2CallsMatchSecondCountdown()
        {
            int offsetSeconds = this.FirstMatchValue - this.SecondMatchValue;
            int offsetMS = offsetSeconds * GlobalData.Default.BossCountingScanTimerIntervalMS;

            return this.FirstMatchTime.AddMilliseconds(offsetMS) < this.SecondMatchTime && this.FirstMatchTime.AddSeconds(offsetSeconds + 1) > this.SecondMatchTime;
        }

        public bool IsTop2CallsSameBossCall()
        {
            return IsTop1CallSameBossCallWith(this.SecondMatchTime);
        }

        public bool IsTop1CallSameBossCallWith(DateTime secondTime)
        {
            return this.FirstMatchTime.AddSeconds(MainOCR.MinBossCallTimeSeconds) >  secondTime;
        }
    }
}
