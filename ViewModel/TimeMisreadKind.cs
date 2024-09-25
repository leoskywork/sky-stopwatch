using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.ViewModel
{
    public enum TimeMisreadKind
    {
        None = 0,
        GreaterThanMaxMinute,
        Treat2xAs1x,
        LessThanLastRead,
        GreaterThanJoinGameMaxMinute

    }
}
