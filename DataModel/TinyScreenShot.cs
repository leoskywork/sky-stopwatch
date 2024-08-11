using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class TinyScreenShot
    {
        public int Id { get; set; }

        public DateTime CreateAt { get; set; }

        public byte[] Data { get; set; }

    }
}
