using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch
{
    public enum MainTheme
    {
        [Description("default theme")]
        Default = 0,

        OCR2Line = 1,
        OCR1LineLong= 2,
        OCR1LineNoTime = 3,

        ThinTime = 4,
        ThinOCR = 0,

        BossCallOnly = 5
    }
}
