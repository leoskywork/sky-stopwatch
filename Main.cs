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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SkyStopwatch
{
    public partial class Main : Form
    {
        public const string UITimeFormat = @"mm\:ss";

        //leotodo, some fileds need thread safe?
        private bool _TopMost = true;//false;
        private bool _IsUpdatingPassedTime = false;
        private DateTime _TimeAroundGameStart = DateTime.MinValue;

        /// <summary>
        /// only the first time matter, the time does not auto update on the game label
        /// </summary>
        private bool _IsAutoRefreshing = false;
        private DateTime _AutoOCRTimeOfLastRead = DateTime.MinValue;
        private Tesseract.TesseractEngine _AutoOCREngine;

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

            this.timerMain.Interval = 900;
            this.timerAutoRefresh.Interval = 1000;

            if(_AutoOCREngine == null)
            {
                //pre warm up
                Task.Factory.StartNew(() =>
                {
                    _AutoOCREngine = MainOCR.GetDefaultOCREngine();
                });
            }
        }

        private void SyncTopMost()
        {
            this.TopMost = _TopMost;
            this.buttonTopMost.Text = this._TopMost ? "Pin" : "-P";
        }

        private void buttonOCR_Click(object sender, EventArgs e)
        {
            try
            {
                _IsUpdatingPassedTime = false;

                buttonOCR.Enabled = false;
                labelTimer.Text = "shot";


                Task.Factory.StartNew(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot");
                    string screenShotPath = MainOCR.PrintScreenAsTempFile();
                    return screenShotPath;

                }).ContinueWith(t =>
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        labelTimer.Text = "ocr";
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} xxx");
                    }));

                    string screenShotPath = t.Result;
                    //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp-test\test-1.bmp";
                    //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp-test\test-2-min-zero.bmp";

                    string data = MainOCR.ReadImageFromFile(screenShotPath);
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} ocr done");

                    this.BeginInvoke((Action)(() =>
                    {
                        labelTimer.Text = "read";
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} xxx");
                    }));

                    DateTime time = MainOCR.FindTime(data, 10);

                    this.BeginInvoke((Action)(() =>
                    {
                        if (time != DateTime.MinValue)
                        {
                            _IsUpdatingPassedTime = true;
                            StartUIStopwatch(time);
                        }
                        else
                        {
                            labelTimer.Text = "none";
                        }

                        if(!this.timerAutoRefresh.Enabled)
                        {
                            this.timerAutoRefresh.Start();
                        }

                        buttonOCR.Enabled = true;
                    }));
                });
            }
            catch (Exception ex)
            {
                OnError(ex);
                buttonOCR.Enabled = true;
            }
        }

        private void StartUIStopwatch(DateTime time)
        {
            if (!_IsUpdatingPassedTime) return;

            if (!this.timerMain.Enabled)
            {
                this.timerMain.Start();
            }

            TimeSpan passedTime = time - DateTime.Today;
            _TimeAroundGameStart = DateTime.Now.AddSeconds(passedTime.TotalSeconds * -1);

            //this.labelTimerPrefix.Show();
            var nowPassed = DateTime.Now - _TimeAroundGameStart;
            this.labelTimer.Text = nowPassed.ToString(UITimeFormat);
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            try
            {
                buttonTopMost.Enabled = false;

                // test part of the full screen - fixed pic
                //{
                //    Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
                //    int x = screenRect.Width * 30 / 100;
                //    int y = screenRect.Height * 65 / 100;

                //    //string screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp-test\test-1.bmp";
                //    string screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\bin\Debug\tmp-test\test-2-min-zero.bmp";
                //    Bitmap bitPic = new Bitmap(screenShotPath);
                //    Bitmap cloneBitmap = bitPic.Clone(new Rectangle(x, y, 600, 300), bitPic.PixelFormat);
                //    (new TestBox(cloneBitmap, "fixed pic")).Show();
                //}


                // curren screen shot
                {
                    Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
                    int x = screenRect.Width * MainOCR.XPercent / 100;
                    int y = screenRect.Height * MainOCR.YPercent / 100;

                    Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height);
                    Graphics gra = Graphics.FromImage(bitPic);
                    gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                    gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);

                    Bitmap cloneBitmap = bitPic.Clone(new Rectangle(x, y, MainOCR.BlockWidth, MainOCR.BlockHeigh), bitPic.PixelFormat);
                    (new ToolBox(cloneBitmap, "current screen", () =>
                    {
                        _IsUpdatingPassedTime = true;
                        this.buttonOCR.Enabled = false;
                        StartUIStopwatch(DateTime.Today.AddSeconds(10));

                        if (!this.timerAutoRefresh.Enabled)
                        {
                            this.timerAutoRefresh.Start();
                        }

                    }, () =>
                    {
                        _TopMost = !_TopMost;
                        SyncTopMost();
                    }
                    )).Show();
                }


                buttonTopMost.Enabled = true;
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
          
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!_IsUpdatingPassedTime) return;

                var passed = DateTime.Now - _TimeAroundGameStart;
                this.labelTimer.Text = passed.ToString(UITimeFormat);

            }
            catch (Exception ex)
            {
               OnError(ex);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} clear");

                _IsUpdatingPassedTime = false;
                _TimeAroundGameStart = DateTime.MinValue;
                _IsAutoRefreshing = false;
                _AutoOCRTimeOfLastRead = DateTime.MinValue;


                this.labelTimer.Text = "--";
                this.buttonOCR.Enabled = true;
                this.timerMain.Stop();
                this.timerAutoRefresh.Stop();

                if (_AutoOCREngine != null)
                {
                    //_AutoOCREngine.Dispose(); //get error ?
                    _AutoOCREngine = null;
                }

                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} clear done");
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
        }

        private void timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_IsAutoRefreshing) return;
                _IsAutoRefreshing = true;
                buttonOCR.Enabled = false;

                Task.Factory.StartNew(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto");

                    byte[] screenShotBytes = MainOCR.PrintScreenAsBytes(true);
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - bytes loaded");

                    if(_AutoOCREngine == null)
                    {
                        _AutoOCREngine = MainOCR.GetDefaultOCREngine();
                        System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR created");
                    }

                    string data = MainOCR.ReadImageFromMemory(_AutoOCREngine, screenShotBytes);
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR done");

                    DateTime time = MainOCR.FindTime(data, 2);
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - parser txt done");

                    return time;

                }).ContinueWith(t =>
                {
                    this.BeginInvoke((Action)(() =>
                    {
                        DateTime time = t.Result;

                        if (time != DateTime.MinValue)
                        {
                            if (_AutoOCRTimeOfLastRead != time)
                            {
                                _AutoOCRTimeOfLastRead = time;
                                StartUIStopwatch(time);
                            }
                            //else the same, the time of this read is a repeat read, the data is not fresh
                        }
                        //else auto refresh failed, just use last result

                        _IsAutoRefreshing = false;
                        //buttonOCR.Enabled = true; //makes ui blink, so disable it
                        System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - end -----");
                    }));
                });
            }
            catch (Exception ex)
            {
                OnError(ex);
                buttonOCR.Enabled = true;
            }
        }

        private void OnError(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.buttonClear_Click(sender, e);
        }
    }
}
