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
            return IsTop1CallsMatchSecondCountdownWith(this.SecondMatchTime, this.SecondMatchValue);
        }

        public bool IsTop2CallsSameRound()
        {
            return IsTop1CallSameRoundWith(this.SecondMatchTime);
        }

      
    }
}
