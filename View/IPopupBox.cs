using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch.View
{
    public interface IPopupBox : ISkyForm
    {
        DateTime CreateAt { get; }

        bool IsPaused { get; }
    }
}
