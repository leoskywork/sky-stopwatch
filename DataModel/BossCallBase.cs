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

        public DateTime FirstMatchTime { get; set; }
        public int FirstMatchValue { get; set; }



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

        public bool IsTop1CallSameRoundWith(DateTime secondTime)
        {
            //return this.FirstMatchTime.AddSeconds(MainOCR.MinBossCallTimeSeconds - this.FirstMatchValue) > secondTime;
            return this.FirstMatchTime.AddSeconds(MainOCR.MinBossCallTimeSeconds) > secondTime;
        }

        public bool IsTop1CallsMatchSecondCountdownWith(DateTime secondTime, int secondValue)
        {
            int offsetSeconds = this.FirstMatchValue - secondValue;
            int offsetMS = offsetSeconds * GlobalData.Default.BossCountingScanTimerIntervalMS;

            //hack, sometimes, the MS seems not accrute
            offsetMS = offsetMS - 200;
            offsetSeconds = offsetSeconds + 4;

            bool match = this.FirstMatchTime.AddMilliseconds(offsetMS) < secondTime && this.FirstMatchTime.AddSeconds(offsetSeconds) > secondTime;

            if (!match) //for debug
            {

            }

            return match;
        }
    }
}
