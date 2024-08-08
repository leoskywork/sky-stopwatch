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


        public DateTime SecondMatchTime { get; set; }
        public int SecondMatchValue { get; set; }


        public bool IsValid { get; set; }

        //does NOT involve in busness logic, just for fast ui updating of boss call number
        public bool PreCounting { get; set; }
    }
}
