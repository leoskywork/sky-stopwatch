using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.View
{
    public interface ITopForm
    {
        bool IsPermanent { get; }
        void SetUserFriendlyTitle();
    }
}
