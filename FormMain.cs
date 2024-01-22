using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class FormMain : Form
    {

        //leotodo, some fileds need thread safe?
        private bool _IsTopMost = true;//false;
        private bool _IsUpdatingPassedTime = false;
        private DateTime _TimeAroundGameStart = DateTime.MinValue;

        /// <summary>
        /// only the first time matter, the time does not auto update on the game label
        /// </summary>
        private bool _IsAutoRefreshing = false;
        private string _AutoOCRTimeOfLastRead;
        private Tesseract.TesseractEngine _AutoOCREngine;

        private bool _HasTimeNodeWarningPopped = false;

        private int _BootingArgs = 0;

        public FormMain(int args)
        {
            InitializeComponent();

            this._BootingArgs = args;

            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            this.labelTimer.Text = "unset";
            this.timerMain.Interval = 900;
            this.timerAutoRefresh.Interval = 1000;

            //do the following in form_loaded
            //InitGUILayoutV1();
            //InitGUILayoutV2();


            //pre warm up
            this.timerMain.Start();
            Task.Factory.StartNew(() =>
            {
                _AutoOCREngine = MainOCR.GetDefaultOCREngine();

                Thread.Sleep(300);
                if (this.IsDead()) return;
                this.BeginInvoke(new Action(() =>
                {
                    this.labelTimer.Text = "run";
                }));
                Thread.Sleep(500);

                if (this.IsDead()) return;
                this.BeginInvoke(new Action(() =>
                {
                    this.OnNewGameStart();
                }));
            });
        }

        private void InitGUILayoutV1()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ??
            this.Size = new System.Drawing.Size(140, 50);

            //display time now
            //this.labelTitle.BackColor = System.Drawing.Color.LightGray;
            this.labelTitle.Size = new System.Drawing.Size(60, 16);
            this.labelTitle.Location = new System.Drawing.Point(8, 4);
            //the x out button
            const int closeSize = 22;
            this.buttonCloseOverlay.Text = "x";
            this.buttonCloseOverlay.Size = new System.Drawing.Size(closeSize, closeSize);
            this.buttonCloseOverlay.Location = new System.Drawing.Point(this.Size.Width - closeSize, 0);
            this.buttonCloseOverlay.FlatStyle = FlatStyle.Flat;
            this.buttonCloseOverlay.FlatAppearance.BorderSize = 0;
            //time since game start
            //this.labelTimer.BackColor = Color.LightGray;
            this.labelTimer.Size = new System.Drawing.Size(80, 30);
            this.labelTimer.Location = new System.Drawing.Point(6, 20);
            //button tool box
            this.buttonToolBox.Size = new System.Drawing.Size(20, 20);
            this.buttonToolBox.Location = new System.Drawing.Point(100, 24);
        }
        private void InitGUILayoutV2()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ?? //should do this in load() not the ctor
            this.Size = new System.Drawing.Size(170, 40);

            //time since game start
            //this.labelTimer.BackColor = Color.LightGray;
            this.labelTimer.Size = new System.Drawing.Size(80, 34);
            this.labelTimer.Location = new System.Drawing.Point(2, 8);

            //button tool box
            this.buttonToolBox.Size = new System.Drawing.Size(18, 18);
            this.buttonToolBox.Location = new System.Drawing.Point(108, 2);

            //display time now
            //this.labelTitle.BackColor = System.Drawing.Color.LightGray;
            //this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Size = new System.Drawing.Size(50, 16);
            this.labelTitle.Location = new System.Drawing.Point(this.buttonToolBox.Location.X - 30, 20);
            this.labelTitle.TextAlign = ContentAlignment.MiddleRight;



            //the x out button
            const int closeSize = 40;
            this.buttonCloseOverlay.Text = null;
            this.buttonCloseOverlay.Size = new System.Drawing.Size(closeSize, closeSize);
            this.buttonCloseOverlay.Location = new System.Drawing.Point(this.Size.Width - closeSize, 0);
            this.buttonCloseOverlay.FlatStyle = FlatStyle.Flat;
            this.buttonCloseOverlay.FlatAppearance.BorderSize = 0;
            this.buttonCloseOverlay.BackColor = System.Drawing.Color.MediumVioletRed;//PaleVioletRed;
        }

        private void InitGUILayoutV3()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ??
            this.Size = new System.Drawing.Size(160, 39);

            
            this.labelTitle.Visible = false;

            //time since game start
            //this.labelTimer.BackColor = Color.LightGray;
            this.labelTimer.Size = new System.Drawing.Size(80, 34);
            this.labelTimer.Location = new System.Drawing.Point(2, 6);

            //button tool box
            this.buttonToolBox.Size = new System.Drawing.Size(26, 26);
            this.buttonToolBox.Location = new System.Drawing.Point(86, 6);

            //the x out button
            const int closeSize = 40;
            this.buttonCloseOverlay.Text = null;
            this.buttonCloseOverlay.Size = new System.Drawing.Size(closeSize, closeSize);
            this.buttonCloseOverlay.Location = new System.Drawing.Point(this.Size.Width - closeSize, 0);
            this.buttonCloseOverlay.FlatStyle = FlatStyle.Flat;
            this.buttonCloseOverlay.FlatAppearance.BorderSize = 0;
            this.buttonCloseOverlay.BackColor = System.Drawing.Color.MediumVioletRed;//PaleVioletRed;
        }


        private void InitGUILayoutV4()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ??
            this.Size = new System.Drawing.Size(110, 32);


            this.labelTitle.Visible = false;

            //time since game start
            //this.labelTimer.BackColor = Color.LightGray;
            this.labelTimer.Size = new System.Drawing.Size(80, 32);
            this.labelTimer.Location = new System.Drawing.Point(2, 3);

            //button tool box
            this.buttonToolBox.Text = null;
            this.buttonToolBox.Size = new System.Drawing.Size(24, 24);
            this.buttonToolBox.Location = new System.Drawing.Point(80, 4);
            this.buttonToolBox.FlatStyle = FlatStyle.Flat;
            this.buttonToolBox.FlatAppearance.BorderSize = 0;
            this.buttonToolBox.BackgroundImage = global::SkyStopwatch.Properties.Resources.more_arrow_128_small_b;
            this.buttonToolBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;

            //the x out button
            this.buttonCloseOverlay.Visible = false;
        }

        private void SyncTopMost()
        {
            this.TopMost = _IsTopMost;
            this.buttonToolBox.Text = this._IsTopMost ? "+" : "-";//this._TopMost ? "Pin" : "-P";
        }

        private void buttonOCR_Click(object sender, EventArgs e)
        {
            try
            {
                buttonOCR.Enabled = false;

                OnRunOCR(() =>
                {
                    buttonOCR.Enabled = true;
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
                buttonOCR.Enabled = true;
            }
        }

        private void StartUIStopwatch(string ocrDisplayTime, int kickOffDelaySeconds)
        {
            if (string.IsNullOrEmpty(ocrDisplayTime)) return;
            if (!_IsUpdatingPassedTime) return;

            if (!this.timerMain.Enabled)
            {
                this.timerMain.Start();
            }


            TimeSpan ocrTimeSpan;

            if (TimeSpan.TryParseExact(ocrDisplayTime, MainOCR.TimeSpanFormat, System.Globalization.CultureInfo.InvariantCulture, out ocrTimeSpan))
            {
                int passedSeconds = (int)ocrTimeSpan.TotalSeconds + kickOffDelaySeconds;
                _TimeAroundGameStart = DateTime.Now.AddSeconds(passedSeconds * -1);
                this.labelTimer.Text = TimeSpan.FromSeconds(passedSeconds).ToString(MainOCR.UIElapsedTimeFormat);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("failed to parse time：" + ocrDisplayTime);
            }
        }

        private void buttonToolBox_Click(object sender, EventArgs e)
        {
            try
            {
                buttonToolBox.Enabled = false;

                // test part of the full screen - fixed pic
                //{
                //    Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
                //    int x = screenRect.Width * 30 / 100;
                //    int y = screenRect.Height * 65 / 100;

                //    //string screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-1.bmp";
                //    string screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-2-min-zero.bmp";
                //    Bitmap bitPic = new Bitmap(screenShotPath);
                //    Bitmap cloneBitmap = bitPic.Clone(new Rectangle(x, y, 600, 300), bitPic.PixelFormat);
                //    (new TestBox(cloneBitmap, "fixed pic")).Show();
                //}


                // curren screen shot
                {
                    Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);
                    int x = screenRect.Width * MainOCR.XPercent / 100;
                    int y = screenRect.Height * MainOCR.YPercent / 100;

                    using (Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height))
                    using (Graphics gra = Graphics.FromImage(bitPic))
                    {

                        gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                        gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);

                        //can not use using block here, since we pass the bitmap into a form and show it
                        Bitmap cloneBitmap = bitPic.Clone(new Rectangle(x, y, MainOCR.BlockWidth, MainOCR.BlockHeight), bitPic.PixelFormat);
                        {
                            FormToolBox tool = CreateToolBox(cloneBitmap, "current screen");
                            tool.StartPosition = FormStartPosition.Manual;
                            tool.Location = new Point(this.Location.X - tool.Width + this.Width + 10, this.Location.Y + this.Size.Height + 24);
                            tool.Show();
                        }
                    }
                }

                buttonToolBox.Enabled = true;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private FormToolBox CreateToolBox(Bitmap bitmap, string source)
        {
            FormToolBox tool = new FormToolBox(
                               bitmap,
                                source,
                               (_, __) => { this.OnInitToolBox(_, __); },
                               () => { this.OnRunOCR(); },
                               () => { this.OnNewGameStart(); },
                               () => { this.OnSwitchTopMost(); },
                               () => { this.OnClearOCR(); },
                               (_) => { this.OnAddSeconds(_); },
                               (_) => { this.OnChangeTimeNodes(_); }
                               );



            return tool;
        }

        private void OnChangeTimeNodes(string newTimeNodes)
        {
            this.CheckTimeNodes();
        }

        private void OnInitToolBox(Button toolBoxButtonOCR, string initialTimeNodes)
        {
            toolBoxButtonOCR.Enabled = !_IsUpdatingPassedTime;

            if (toolBoxButtonOCR.Enabled)
            {
                toolBoxButtonOCR.BackColor = Color.SlateBlue;
                toolBoxButtonOCR.ForeColor = Color.White;
                toolBoxButtonOCR.Cursor = Cursors.Default;
            }
            else
            {
                //leotodo not working?
                toolBoxButtonOCR.BackColor = Color.LightGray;
                toolBoxButtonOCR.ForeColor = Color.Gray;
                toolBoxButtonOCR.Cursor = Cursors.No;
            }

            System.Diagnostics.Debug.WriteLine($"----- OnInitToolBox, {nameof(initialTimeNodes)}: {initialTimeNodes}");
            //System.Diagnostics.Debug.WriteLine($"old: {this._TimeNodeCheckingList}");
            //do not do this, causing bug
            //this._TimeNodeCheckingList = initialTimeNodes;
        }

        private void OnRunOCR(Action afterDone = null)
        {
            _IsUpdatingPassedTime = false;
            labelTimer.Text = "shot";

            Task.Factory.StartNew(() =>
            {
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot");
                return MainOCR.PrintScreenAsTempFile();
            }).ContinueWith(t =>
            {
                if (this.IsDead()) return;

                this.BeginInvoke((Action)(() => { labelTimer.Text = "ocr"; }));
                string screenShotPath = t.Result;
                //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-1.bmp";
                //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-2-min-zero.bmp";

                string data = MainOCR.ReadImageFromFile(screenShotPath);
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} ocr done");
                this.BeginInvoke((Action)(() => { labelTimer.Text = "read"; }));
                string ocrDisplayTime = MainOCR.FindTime(data);

                this.BeginInvoke((Action)(() =>
                {
                    if (!string.IsNullOrEmpty(ocrDisplayTime))
                    {
                        _IsUpdatingPassedTime = true;
                        StartUIStopwatch(ocrDisplayTime, MainOCR.ManualOCRDelaySeconds);
                    }
                    else
                    {
                        labelTimer.Text = "none";
                    }

                    if (!this.timerAutoRefresh.Enabled)
                    {
                        this.timerAutoRefresh.Start();
                    }

                    afterDone?.Invoke();
                }));
            });
        }

        private void OnNewGameStart()
        {
            _IsUpdatingPassedTime = true;
            this.buttonOCR.Enabled = false;

            //reset flags/history values
            //flag 1
            this._AutoOCRTimeOfLastRead = null;

            //flag 2 - this._TimeAroundGameStart, which will be updated in the following method
            StartUIStopwatch(TimeSpan.Zero.ToString(MainOCR.TimeSpanFormat), MainOCR.NewGameDelaySeconds);

            if (!this.timerAutoRefresh.Enabled)
            {
                this.timerAutoRefresh.Start();
            }
        }

        private void OnAddSeconds(int seconds)
        {
            if (_TimeAroundGameStart == DateTime.MinValue) return;

            _IsUpdatingPassedTime = true;
            this.buttonOCR.Enabled = false;

            TimeSpan passedTimeWithIncrease = DateTime.Now.AddSeconds(seconds) - _TimeAroundGameStart;

            if (passedTimeWithIncrease < TimeSpan.Zero)
            {
                passedTimeWithIncrease = TimeSpan.Zero;
            }

            StartUIStopwatch(passedTimeWithIncrease.ToString(MainOCR.TimeSpanFormat), MainOCR.NoDelay);
        }

        private void OnSwitchTopMost()
        {
            _IsTopMost = !_IsTopMost;
            SyncTopMost();
        }


        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.labelTitle.Visible)
                {
                    this.labelTitle.Text = DateTime.Now.ToString(MainOCR.TimeFormatNoSecond);
                    //this.labelTitle.Text = "23:59";
                }

                this.CheckTimeNodes();

                if (!_IsUpdatingPassedTime) return;

                var passed = DateTime.Now - _TimeAroundGameStart;
                this.labelTimer.Text = passed.ToString(MainOCR.UIElapsedTimeFormat);
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void CheckTimeNodes()
        {
            if (!MainOCR.EnableTimeNodeChecking) return;
            if (string.IsNullOrWhiteSpace(MainOCR.TimeNodeCheckingList)) return;
            if (_TimeAroundGameStart == DateTime.MinValue) return;
            if (_HasTimeNodeWarningPopped) return;

            var timeNodes = MainOCR.ValidateTimeSpanLines(MainOCR.TimeNodeCheckingList);

            if (timeNodes == null || timeNodes.Count == 0) return;

            var elapsedSeconds = (DateTime.Now - _TimeAroundGameStart).TotalSeconds;

            timeNodes.ForEach(node =>
            {
                string fixedLengthNode = node.PadLeft(MainOCR.TImeSpanFormatNoHour.Length - 1, '0');
                TimeSpan parsedNode = TimeSpan.ParseExact(fixedLengthNode, MainOCR.TImeSpanFormatNoHour, System.Globalization.CultureInfo.InvariantCulture);
                double remainingSeconds = parsedNode.TotalSeconds - elapsedSeconds;

                if (remainingSeconds > 0 && remainingSeconds < MainOCR.TimeNodeEarlyWarningSeconds)
                {
                    var warningBox = new FormNodeWarning(() => _HasTimeNodeWarningPopped = false);
                    _HasTimeNodeWarningPopped = true;
                    warningBox.StartPosition = FormStartPosition.Manual;
                    warningBox.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height + 10);
                    warningBox.Show();
                    return;
                }
            });
        }

        private void OnClearOCR()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} clear");

                _IsUpdatingPassedTime = false;
                _TimeAroundGameStart = DateTime.MinValue;
                _IsAutoRefreshing = false;
                _AutoOCRTimeOfLastRead = string.Empty;


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
                this.OnError(ex);
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
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto");

                    byte[] screenShotBytes = MainOCR.PrintScreenAsBytes(true);
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - bytes loaded");

                    if (_AutoOCREngine == null)
                    {
                        _AutoOCREngine = MainOCR.GetDefaultOCREngine();
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR created");
                    }

                    string data = MainOCR.ReadImageFromMemory(_AutoOCREngine, screenShotBytes);
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR done");

                    string timeString = MainOCR.FindTime(data);
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - parser txt done");

                    if (MainOCR.IsDebugging)
                    {
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - debugging");
                        //System.Diagnostics.Debug.WriteLine($"OCR time: {timeString}");
                        //System.Diagnostics.Debug.WriteLine($"OCR data: {data}");

                        //string tmpPath = MainOCR.SaveTmpFile(Guid.NewGuid().ToString(), screenShotBytes);

                        //System.Diagnostics.Debug.WriteLine($"temp file path: {tmpPath}");
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - debugging end");
                    }

                    return timeString;

                }).ContinueWith(t =>
                {
                    if (this.IsDead()) return;

                    this.BeginInvoke((Action)(() =>
                    {
                        string ocrDisplayTime = t.Result;

                        if (!string.IsNullOrEmpty(ocrDisplayTime))
                        {
                            if (_AutoOCRTimeOfLastRead != ocrDisplayTime)
                            {
                                _AutoOCRTimeOfLastRead = ocrDisplayTime;
                                StartUIStopwatch(ocrDisplayTime, MainOCR.AutoOCRDelaySeconds);
                            }
                            //else the same, the time of this read is a repeat read, the data is not fresh
                        }
                        //else auto refresh failed, just use last result

                        _IsAutoRefreshing = false;
                        //buttonOCR.Enabled = true; //makes ui blink, so disable it
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - end -----");
                    }));
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
                buttonOCR.Enabled = true;
            }
        }

     

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnClearOCR();
        }



        private void buttonCloseOverlay_Click(object sender, EventArgs e)
        {
            this.Close();
            MainOCR.FireCloseApp();
        }







        //do this in another way
        ////圆角
        //[DllImport("kernel32.dll")]
        //public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        //[DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //private static extern IntPtr CreateRoundRectRgn
        // (int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
        ////阴影
        //private const int CS_DropSHADOW = 0x20000;
        //private const int GCL_STYLE = (-26);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        private void Main_Load(object sender, EventArgs e)
        {

            //const int roundRadius = 2;
            //圆角
            //  Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, roundRadius, roundRadius));
            //阴影
            // SetClassLong(this.Handle, GCL_STYLE, GetClassLong(this.Handle, GCL_STYLE) | CS_DropSHADOW);


            switch (this._BootingArgs)
            {
                case 1:
                    InitGUILayoutV1();
                    break;
                case 2:
                    InitGUILayoutV2();
                    break;
                case 3:
                    InitGUILayoutV3();
                    break;
                default:
                    InitGUILayoutV4();
                    break;
            }
        }

        //窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        //窗体移动
        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void labelTimer_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void labelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        //圆角
        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角   
            path.AddArc(arcRect, 180, 90);
            //   右上角   
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角   
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角   
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(System.EventArgs e)
        {
            const int roundRadius = 8;

            //SetWindowRegion
            //this.Left-10, this.Top-10, this.Width-10, this.Height-10);
            //Rectangle rect = new Rectangle(0, 22, this.Width, this.Height - 22); 
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            System.Drawing.Drawing2D.GraphicsPath formPath = GetRoundedRectPath(rect, roundRadius);
            this.Region = new Region(formPath);
        }
    }
}
