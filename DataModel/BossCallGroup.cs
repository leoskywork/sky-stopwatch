using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallGroup
    {
        public List<BossCall> Calls { get; } = new List<BossCall>();

        public string Name { get; set; }

        public void Add(BossCall call)
        {
            Calls.Add(call);
        }

        public void Remove(BossCall call)
        {
            Calls.Remove(call);
        }

    }
}
