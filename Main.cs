using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class Main : Form
    {
        private bool _TopMost = false;//false;
        private DateTime _TimeAroundGameStart = DateTime.MinValue;
        private bool _IsUpdating = false;
        public const string UITimeFormat = @"mm\:ss";

        public Main()
        {
            InitializeComponent();
            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            this.labelTimerPrefix.Hide();
            this.labelTimer.Text = "unset";

            this.timerMain.Interval = 500;

        }

        private void SyncTopMost()
        {
            this.TopMost = _TopMost;
            this.buttonTopMost.Text = this._TopMost ? "Pin" : "Unpin";
        }

        private void buttonOCR_Click(object sender, EventArgs e)
        {
            try
            {
                _IsUpdating = false;

                buttonOCR.Enabled = false;
                labelTimer.Text = "shot...";

                string screenShotPath = MainHelper.PrintScreenAsTempFile();
                labelTimer.Text = "ocr...";

                screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp\test-1.bmp";

                string data = MainHelper.ReadImageAsText(screenShotPath);
                DateTime time = MainHelper.FindTime(data);

                if (time != DateTime.MinValue)
                {
                    TimeSpan passedTime = time - DateTime.Today;
                    _TimeAroundGameStart = DateTime.Now.AddSeconds(passedTime.TotalSeconds * -1);
                    _IsUpdating = true;
                    StartUIStopwatch();
                }
                else
                {
                    labelTimer.Text = "xxx";
                }

                System.Diagnostics.Debug.Write(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
            finally
            {
                buttonOCR.Enabled = true;
            }
        }

        private void StartUIStopwatch()
        {
            if (!_IsUpdating) return;

            if (!this.timerMain.Enabled)
            {
                this.timerMain.Start();
            }

            this.labelTimerPrefix.Show();
            var passed = DateTime.Now - _TimeAroundGameStart;
            this.labelTimer.Text = passed.ToString(UITimeFormat);
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            try
            {
                //leotodo improve this
                buttonTopMost.Enabled = false;
                _TopMost = !_TopMost;

                SyncTopMost();

                buttonTopMost.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_IsUpdating) return;

                var passed = DateTime.Now - _TimeAroundGameStart;
                this.labelTimer.Text = passed.ToString(UITimeFormat);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
