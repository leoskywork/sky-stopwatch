using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallDualSection : BossCallBase, IBossCall
    {
        public bool IsPairOneMatch { get; set; }
        public DateTime PairOneMatchTime { get; set; }


        public int PairTwoMatchValue { get; set; }
        public DateTime PairTwoLastMatchTime { get; set; }

        public DateTime PairTwoImageCreateAt { get; set; }



        //leotodo, not a good way, but do this for now
        public BossCallDualSection Previous { get; set; }   






        public bool IsImageTimeSameRoundUTC(DateTime time, int value)
        {
            bool same = IsFirstCallImageSameRoundWithUTC(time);
            bool match = IsFirstCallImageMatchSecondCountdownWithUTC(time, value);

            if (!same)
            {

            }

            return same && match;
        }
    }
}
