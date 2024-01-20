using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class FormNodeWarning : Form
    {
        private DateTime _CreatedTime = DateTime.Now;
        private Action _BeforeClose;

        public FormNodeWarning(Action onClosing)
        {
            InitializeComponent();

            _BeforeClose = onClosing;

            this.TopMost = true;
            
            //do this at last
            this.timerClose.Interval = 1000;
            this.timerClose.Start();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            var elapsedTime = DateTime.Now - _CreatedTime;

            if (elapsedTime.TotalSeconds < MainOCR.TimeNodeWarningDurationSeconds)
            {
                this.labelMessage.ForeColor = DateTime.Now.Second % 2 == 0 ? Color.DarkGreen : Color.White;
            }
            else
            {
                this.Close();
            }
        }

        private void FormNodeWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            _BeforeClose?.Invoke();
            _BeforeClose = null;
        }
    }
}
