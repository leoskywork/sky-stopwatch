﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.ViewModel
{
    public enum TimeChangeSource
    {
        Unset,
        AppAutoRestart,
        AppAutoUpdate,
        AppAutoUpdateBySecondary,
        UserClickNewGame,
        TargetAppExit
    }
}
