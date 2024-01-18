using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class ToolBox : Form
    {
        private Action _NewGameClick;
        private Action _TopMostClick;
        private Action _ClearClick;
        private Action<int> _AddSecondsClick;


        public ToolBox()
        {
            InitializeComponent();

            //got error when call this.close(), cross threads issue, thus use ui control timer instead
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(60 * 1000);
            //    if (!this.Disposing || !this.IsDisposed)
            //    {
            //        this.BeginInvoke(new Action(() => { this.Close(); }));
            //    }
            //});
            this.timerAutoClose.Interval = 60 * 1000;
            this.timerAutoClose.Start();
        }

        public ToolBox(Bitmap image, string message,
            Action onNewGame = null,
            Action topMost = null,
            Action clear = null,
            Action<int> addSeconds = null
            ) : this()
        {

            this.pictureBoxOne.Image = image;
            this.labelMessage.Text = message;
            this._NewGameClick = onNewGame;
            _TopMostClick = topMost;
            _ClearClick = clear;
            _AddSecondsClick = addSeconds;
        }

        public ToolBox(string imagePath, string message, 
            Action onNewGame = null) : this(new Bitmap(imagePath), message, onNewGame)
        {
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            _NewGameClick?.Invoke();
            this.Close();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            _TopMostClick?.Invoke();
            this.Close();
        }

        private void ToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            _NewGameClick = null;
            _TopMostClick = null;
            _ClearClick = null;
            _AddSecondsClick = null;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _ClearClick?.Invoke();
            this.Close();
        }

        private void buttonAddSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _AddSecondsClick?.Invoke(MainOCR.IncrementSeconds);
            buttonClear.Enabled = true;
        }
    }
}
