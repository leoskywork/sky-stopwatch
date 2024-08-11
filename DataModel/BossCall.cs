using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCall : BossCallBase, IBossCall
    {
        public int OCRLastMatch { get; set; }

        public DateTime SecondMatchTime { get; set; }
        public int SecondMatchValue { get; set; }


        public BossCall()
        {
        }




        public bool IsTop2CallsMatchSecondCountdown()
        {
            throw new Exception("need change code");
           // return IsTop1CallsMatchSecondCountdownWithUTC(this.SecondMatchTime, this.SecondMatchValue);
        }

        public bool IsTop2CallsSameRound()
        {
            throw new Exception("need change code");
            //return IsTop1CallSameRoundWithUTC(this.SecondMatchTime);
        }

      
    }
}
