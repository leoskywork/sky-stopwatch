using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCallSet
    {
        public List<BossCallGroup> Groups { get; } = new List<BossCallGroup>();


        public int Count { get { return this.Groups.Count; } }

        public BossCallSet()
        {
            
        }




        public void Reset()
        {
            this.Groups.Clear();
            this.Groups.Add(new BossCallGroup() { Name = "player-default"});
        }

        public BossCallGroup Last()
        {
            return this.Groups.Last();
        }

        public T LastCallOrDefault<T>() where T: class
        {
            return this.Last().Calls.LastOrDefault() as T;
        }

        public void Add(BossCallGroup group)
        {
            this.Groups.Add(group);
        }

        public int Sum(Func<BossCallGroup, int> selector)
        {
            return Groups.Sum(selector);
        }
    }
}
