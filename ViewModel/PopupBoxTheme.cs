using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch
{
    public enum PopupBoxTheme
    {
        [Description("default theme")]
        Default = 0,

        OCR2Line = 1,
        OCR1LineLong= 2,
        OCR1LineNoSystemTime = 3,

        ThinOCRTime = 4,
        ThinSystemTime = 5,

        BossCallCounting = 6
    }
}
