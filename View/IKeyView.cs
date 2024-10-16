﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch.View
{
    public interface IKeyView
    {
        string Key { get; }
    }

    public interface ISkyView : IKeyView
    {
        bool Disposing { get; }
        bool IsDisposed { get; }

        FormStartPosition StartPosition { get; set; }
        Point Location { get; set; }


        void Show();

        void Close();
    }

    public interface IMainBox : ISkyView {

        MainBoxCloseSource CloseSource { get; set; }

        bool IsDead();
    }
}
