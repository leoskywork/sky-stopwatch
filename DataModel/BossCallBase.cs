using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallBase
    {
        public int Id { get; set; }

        public int FirstMatchValue { get; set; } = -1;
        public DateTime FirstMatchTime { get; set; }

        public DateTime FirstMatchImageCreateAt { get; set; }


        public bool IsValid { get; set; }

        //does NOT involve in busness logic, just for fast ui updating of boss call number
        public bool PreCounting { get; set; }


        //public bool IsExpired
        //{
        //    get
        //    {
        //        return this.IsTop1CallSameRoundWith(DateTime.Now);
        //    }
        //}

        public bool IsFirstCallImageSameRoundWithUTC(DateTime secondImageTime)
        {
            //return this.FirstMatchTime.AddSeconds(MainOCR.MinBossCallTimeSeconds - this.FirstMatchValue) > secondTime;
            return this.FirstMatchImageCreateAt.AddSeconds(GlobalData.MinBossCallTimeSeconds) > secondImageTime;
        }

        public bool IsFirstCallImageMatchSecondCountdownWithUTC(DateTime secondImageTime, int secondValue)
        {
            int offsetSeconds = this.FirstMatchValue - secondValue;
            int offsetMS = offsetSeconds * GlobalData.BossCountingScanTimerIntervalMS;

            //hack, sometimes, the MS seems not accrute
            //I know why this is not right, the time is wrong, should be the time img creatd, not the time img being compared
            //offsetMS = offsetMS - 200;
            //offsetSeconds = offsetSeconds + 5;

            bool greatValid = this.FirstMatchImageCreateAt.AddSeconds(offsetSeconds + 1) >= secondImageTime;
            bool lessValid = this.FirstMatchImageCreateAt.AddMilliseconds(offsetMS - 100) <= secondImageTime;

            if (!lessValid) //for debug
            {

            }

            return lessValid && greatValid;
        }
    }
}
