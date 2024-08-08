using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCall2Section : BossCallBase, IBossCall
    {
        public bool IsPairOneMatch { get; set; }
        public DateTime PairOneMatchTime { get; set; }

        public bool IsSameRound(DateTime time, int value)
        {
            return IsTop1CallSameRoundWith(time) && IsTop1CallsMatchSecondCountdownWith(time, value);
        }
    }
}
