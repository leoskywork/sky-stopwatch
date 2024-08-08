﻿using System;
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


        public int Round2FirstMatchValue { get; set; }
        public DateTime Round2FirstMatchTime { get; set; }

        public bool IsSameRound(DateTime time, int value)
        {
            bool same = IsTop1CallSameRoundWith(time) && IsTop1CallsMatchSecondCountdownWith(time, value);

            if (!same)
            {

            }

            return same;
        }
    }
}
