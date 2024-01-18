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

        public ToolBox(Bitmap image, string message, Action onNewGame = null) : this()
        {

            this.pictureBoxOne.Image = image;
            this.labelMessage.Text = message;
            this._NewGameClick = onNewGame;
        }

        public ToolBox(string imagePath, string message, Action onNewGame = null) : this(new Bitmap(imagePath), message, onNewGame)
        {
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            try
            {
                _NewGameClick?.Invoke();
                _NewGameClick = null;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            _NewGameClick = null;
            this.Close();
        }
    }
}
