using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class Main : Form
    {
        public const string UITimeFormat = @"mm\:ss";

        private bool _TopMost = true;//false;
        private bool _IsUpdating = false;
        private DateTime _TimeAroundGameStart = DateTime.MinValue;

        public Main()
        {
            InitializeComponent();
            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            //this.labelTimerPrefix.Hide();
            this.labelTimer.Text = "unset";

            this.timerMain.Interval = 1000;

        }

        private void SyncTopMost()
        {
            this.TopMost = _TopMost;
            this.buttonTopMost.Text = this._TopMost ? "P" : "-P";
        }

        private void buttonOCR_Click(object sender, EventArgs e)
        {
            try
            {
                _IsUpdating = false;

                buttonOCR.Enabled = false;
                labelTimer.Text = "shot";


                Task.Factory.StartNew(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot");
                    string screenShotPath = MainHelper.PrintScreenAsTempFile();
                    return screenShotPath;

                }).ContinueWith(t =>
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        labelTimer.Text = "ocr";
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} xxx");
                    }));

                    string screenShotPath = t.Result;
                    screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp\test-1.bmp";
                    string data = MainHelper.ReadImageAsText(screenShotPath);
                    System.Diagnostics.Debug.Write(data);

                    this.BeginInvoke((Action)(() =>
                    {
                        labelTimer.Text = "read";
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} xxx");
                    }));

                    DateTime time = MainHelper.FindTime(data);

                    this.BeginInvoke((Action)(() =>
                    {
                        if (time != DateTime.MinValue)
                        {
                            TimeSpan passedTime = time - DateTime.Today;
                            _TimeAroundGameStart = DateTime.Now.AddSeconds(passedTime.TotalSeconds * -1);
                            _IsUpdating = true;
                            StartUIStopwatch();
                        }
                        else
                        {
                            labelTimer.Text = "min";
                        }
                    }));
                });
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

            //this.labelTimerPrefix.Show();
            var passed = DateTime.Now - _TimeAroundGameStart;
            this.labelTimer.Text = passed.ToString(UITimeFormat);
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _IsUpdating = false;
            _TimeAroundGameStart = DateTime.MinValue;
            this.labelTimer.Text = "--";
        }
    }
}
