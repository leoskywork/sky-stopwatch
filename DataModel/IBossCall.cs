using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public interface IBossCall
    {

        bool IsValid { get; set; }

        //does NOT involve in busness logic, just for fast ui updating of boss call number
        bool PreCounting { get; set; }
    }
}
