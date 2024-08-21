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

        ThinSystemTime = 4,
        ThinOCRTime = 0,

        BossCallCounting = 5
    }
}
