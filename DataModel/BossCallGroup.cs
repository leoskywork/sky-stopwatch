using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallGroup
    {
        public List<BossCall> Calls { get; set; } = new List<BossCall>();

        public DateTime FirstCallTime { get; set; }
        public DateTime LastCallTime { get; set;}
    }
}
