using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallGroup
    {
        public List<IBossCall> Calls { get; } = new List<IBossCall>();

        public string Name { get; set; }

        public void Add(IBossCall call)
        {
            Calls.Add(call);
        }

        public void Remove(IBossCall call)
        {
            Calls.Remove(call);
        }

        public int GetValidCount()
        {
            return Calls.Sum(c => c.IsValid ? 1 : 0);
        }
 

    }
}
