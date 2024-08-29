using SkyStopwatch.View;
using SkyStopwatch.ViewModel;
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
    public partial class BoxGameTime : Form, IPopupBox
    {
        private bool _IsUpdatingPassedTime = false;

        /// <summary>
        /// only the first time matter, the time does not auto update on the game label
        /// </summary>
        private bool _IsAutoRefreshing = false;
        private Tesseract.TesseractEngine _AutoOCREngine;
        private bool _HasTimeNodeWarningPopped = false;


        public DateTime CreateAt => DateTime.Now;
        public string Key => GlobalData.PopupKeyGameTime;

        private bool _IsPaused;
        public bool IsPaused => _IsPaused;

        public OCRGameTime Model { get { return ViewModelFactory.Instance.GetGameTime(); } }

        public BoxGameTime(int args)
        {
            InitializeComponent();

            this.Model.BootingArgs = args;

            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            this.labelTimer.Text = "unset";
            this.timerMain.Interval = OCRGameTime.TimerDisplayUIIntervalMS;
            this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRFastIntervalMS;
            this.Text = $"SSW-V{GlobalData.Version}";

            //do the following in form_loaded
            //InitGUILayoutV1();
            //InitGUILayoutV2();


            //pre warm up
            this.timerMain.Start();
            this.labelTimer.Text = "run";
            Task.Factory.StartNew(() =>
            {
                _AutoOCREngine = this.Model.GetDefaultOCREngine();

                Thread.Sleep(500);

                if (this.IsDead()) return;
                this.BeginInvoke(new Action(() =>
                {
                    //this.OnNewGameStart();
                    this.OnRunOCR(() =>
                    {
                        _IsUpdatingPassedTime = true;
                        if (!this.timerAutoRefresh.Enabled)
                        {
                            this.timerAutoRefresh.Start();
                        }
                    }, GlobalData.ChangeTimeSourcePreWarmUp);
                }));
            }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    this.OnError(task.Exception);
                }
            });
        }

        public void SetDefaultLocation(IPopupBox parent)
        {
            this.StartPosition = FormStartPosition.Manual;

            if (parent != null)
            {
                this.Location = new Point(parent.Location.X, parent.Location.Y + 40);
            }
            else
            {
                var center = new Point(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
                //this.Location = new Point(center.X - 240, center.Y - 370); //right blow top mini time
                //this.Location = new Point(center.X - 356, center.Y - 398); //ahead top mini time
                this.Location = new Point(center.X + 256, center.Y - 390); //tail top mini time
            }
        }

        private void InitGUILayoutV1()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            //this.Controls.Remove(this.buttonOCR);
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
            //this.Controls.Remove(this.buttonOCR);
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
            //this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ??
            this.Size = new System.Drawing.Size(160, 40);


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
            //this.Controls.Remove(this.buttonOCR);
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

        private void InitGUILayoutV5()
        {
            //shrink width when hide ocr button
            //this.buttonOCR.Hide();
            //this.Controls.Remove(this.buttonOCR);
            this.buttonDummyAcceptHighLight.Size = new System.Drawing.Size(1, 1);
            this.buttonDummyAcceptHighLight.Location = new Point(0, 0);
            //seems not working if width < 140 //turns out it's caused by lable.auto-resize ??
            this.Size = new System.Drawing.Size(110, 32);

            //time only
            //this.labelTitle.BackColor = System.Drawing.Color.Gray;
            this.labelTitle.Text = "time";
            this.labelTitle.Size = new System.Drawing.Size(80, 32);
            this.labelTitle.Location = new System.Drawing.Point(2, 3);
            this.labelTitle.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            //time since game start
            this.labelTimer.Visible = false;

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

        private void InitGUILayoutV6()
        {
            var toolBox = this.ShowToolBox();
            var imageView = new FormImageViewBossCounting(true);
            imageView.Show();

            this.FormClosing += (_, __) =>
            {
                imageView.Close();
                GlobalData.Default.ClearPopups();
            };

            //hide main form in this theme, not working ?
            this.Location = new Point(10000, 10000);
            this.Size = new Size(1, 1);
            this.RunOnMain(() => this.Hide(), 1);
        }

        private void SyncTopMost()
        {
            this.TopMost = GlobalData.EnableTopMost;
            this.buttonToolBox.Text = GlobalData.EnableTopMost ? "+" : "-";//this._TopMost ? "Pin" : "-P";
        }

        private void SetGameStartTime(DateTime newTime, string source)
        {
            DateTime oldTime = this.Model.TimeAroundGameStart;
            this.Model.TimeAroundGameStart = newTime;

            if (oldTime != newTime)
            {
                GlobalData.Default.FireChangeGameStartTime(new ChangeGameStartTimeEventArgs(newTime, source));
                bool saveScreen = newTime != DateTime.MinValue;
                this.Log().SaveAsync($"set game start: {newTime.ToString(GlobalData.TimeFormat6Digits)}", source, saveScreen);
            }
        }



        private void StartUIStopwatch(string ocrDisplayTime, int kickOffDelaySeconds, string source)
        {
            if (string.IsNullOrEmpty(ocrDisplayTime)) return;
            if (!_IsUpdatingPassedTime) return;

            if (!this.timerMain.Enabled)
            {
                this.timerMain.Start();
            }


            TimeSpan ocrTimeSpan;

            if (TimeSpan.TryParseExact(ocrDisplayTime, GlobalData.TimeSpanFormat, System.Globalization.CultureInfo.InvariantCulture, out ocrTimeSpan))
            {
                int passedSeconds = (int)ocrTimeSpan.TotalSeconds + kickOffDelaySeconds;

                if (GlobalData.Default.IsUsingScreenTopTime)
                {
                    if (ocrTimeSpan.TotalMinutes > GlobalData.MaxScreenTopGameMinute)
                    {
                        passedSeconds = 0;
                    }
                }
                else
                {
                    if (ocrTimeSpan.TotalMinutes > GlobalData.PreRoundGameMinutes)
                    {
                        passedSeconds = 0;
                    }
                    else if (source == GlobalData.ChangeTimeSourceTimerOCR)
                    {
                        const int magicSecond = 20 - OCRGameTime.AutoOCRDelaySeconds;
                        const int magicSecondLimit = 5;
                        if (passedSeconds > magicSecondLimit)
                        {
                            passedSeconds -= magicSecond;
                        }
                    }
                }

                this.Model.AutoOCRTimeOfLastRead = ocrDisplayTime;
                SetGameStartTime(DateTime.Now.AddSeconds(passedSeconds * -1), $"{source} - StartUIStopwatch, ocr: {ocrDisplayTime}");
                this.labelTimer.Text = TimeSpan.FromSeconds(passedSeconds).ToString(GlobalData.UIElapsedTimeFormat);
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


                ShowToolBox();
                //buttonToolBox.Enabled = true;
                //this.DisableButtonShortTime(buttonToolBox);
                this.DisableButtonWithTime(buttonToolBox, 1000);
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private Form ShowToolBox()
        {
            // curren screen shot
            Rectangle screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

            using (Bitmap bitPic = new Bitmap(screenRect.Width, screenRect.Height))
            using (Graphics gra = Graphics.FromImage(bitPic))
            {

                gra.CopyFromScreen(0, 0, 0, 0, bitPic.Size);
                gra.DrawImage(bitPic, 0, 0, screenRect, GraphicsUnit.Pixel);

                //can not use using block here, since we pass the bitmap into a form and show it
                Bitmap cloneBitmap = bitPic.Clone(this.Model.GetScreenBlock(), bitPic.PixelFormat);

                FormBootSetting tool = CreateToolBox(cloneBitmap);
                tool.StartPosition = FormStartPosition.Manual;
                tool.Location = new Point(this.Location.X - tool.Width + this.Width + 10, this.Location.Y + this.Size.Height + 30);
                tool.Show();
                return tool;
            }
        }

        private FormBootSetting CreateToolBox(Bitmap bitmap)
        {
            FormBootSetting tool = new FormBootSetting(bitmap,
                               (_, __) => { this.OnInitToolBox(_, __); },
                               () => { this.OnRunOCR(null, GlobalData.ChangeTimeSourceManualOCRButton); },
                               () => { this.OnNewGameStart(false); },
                               () => { this.OnSwitchTopMost(); },
                               () => { this.OnClearOCR(); },
                               (_) => { this.OnAddSeconds(_); },
                               (_) => { this.OnChangeTimeNodes(_); });

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

        private void OnRunOCR(Action afterDone, string source)
        {
            _IsUpdatingPassedTime = false;
            //labelTimer.Text = "ocr";

            Task.Factory.StartNew(() =>
            {
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot");
                return OCRBase.PrintScreenAsTempFile();
            }).ContinueWith(t =>
            {
                if (this.IsDead()) return;

                if (t.IsFaulted)
                {
                    this.OnError(t.Exception);
                    this.BeginInvoke(new Action(() => { labelTimer.Text = "err"; }));
                    return;
                }

                //this.BeginInvoke((Action)(() => { labelTimer.Text = "ocr"; }));
                string screenShotPath = t.Result;
                //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-1.bmp";
                //screenShotPath = @"C:\Dev\VS2022\SkyStopwatch\test-image\test-2-min-zero.bmp";

                string data = this.Model.ReadImageFromFile(screenShotPath);
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} ocr done");
                //this.BeginInvoke((Action)(() => { labelTimer.Text = "read"; }));
                string ocrDisplayTime = this.Model.Find(data);

                if (this.IsDead()) return;
                this.BeginInvoke((Action)(() =>
                {
                    if (!string.IsNullOrEmpty(ocrDisplayTime))
                    {
                        _IsUpdatingPassedTime = true;
                        StartUIStopwatch(ocrDisplayTime, OCRGameTime.ManualOCRDelaySeconds, source);
                    }
                    else
                    {
                        labelTimer.Text = "none";
                    }

                    afterDone?.Invoke();
                }));
            });
        }

        private void OnNewGameStart(bool auto)
        {
            _IsUpdatingPassedTime = true;
            //this.buttonOCR.Enabled = false;

            //reset flags/history values
            //flag 1
            this.Model.ResetAutoOCR();

            //flag 2 - this._TimeAroundGameStart, which will be updated in the following method
            var source = auto ? GlobalData.ChangeTimeSourceNewGameAuto : GlobalData.ChangeTimeSourceNewGame;
            StartUIStopwatch(TimeSpan.Zero.ToString(GlobalData.TimeSpanFormat), OCRGameTime.NewGameDelaySeconds, source);

            if (!this.timerAutoRefresh.Enabled)
            {
                this.timerAutoRefresh.Start();
            }
        }

        private void OnAddSeconds(int seconds)
        {
            if (this.Model.TimeAroundGameStart == DateTime.MinValue) return;

            _IsUpdatingPassedTime = true;
            //this.buttonOCR.Enabled = false;

            TimeSpan passedTimeWithIncrease = DateTime.Now.AddSeconds(seconds) - this.Model.TimeAroundGameStart;

            if (passedTimeWithIncrease < TimeSpan.Zero)
            {
                passedTimeWithIncrease = TimeSpan.Zero;
            }

            StartUIStopwatch(passedTimeWithIncrease.ToString(GlobalData.TimeSpanFormat), OCRGameTime.NoDelay, GlobalData.ChangeTimeSourceAdjustTimeButton);
        }

        private void OnSwitchTopMost()
        {
            GlobalData.EnableTopMost = !GlobalData.EnableTopMost;
            SyncTopMost();
        }


        private void timerMain_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.labelTitle.Visible)
                {
                    this.labelTitle.Text = DateTime.Now.ToString(GlobalData.TimeFormatNoSecond);
                    //this.labelTitle.Text = "23:59";
                }

                if (this.labelTimer.Visible)
                {
                    this.CheckTimeNodes();

                    if (_IsUpdatingPassedTime && this.Model.TimeAroundGameStart != DateTime.MinValue)
                    {
                        var passed = DateTime.Now - this.Model.TimeAroundGameStart;
                        if (passed.TotalMinutes < GlobalData.MaxGameRoundMinutes)
                        {
                            this.labelTimer.Text = passed.ToString(GlobalData.UIElapsedTimeFormat);
                        }
                        else
                        {
                            this.OnNewGameStart(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void CheckTimeNodes()
        {
            if (!GlobalData.EnableCheckTimeNode) return;
            if (string.IsNullOrWhiteSpace(GlobalData.TimeNodeCheckingList)) return;
            if (this.Model.TimeAroundGameStart == DateTime.MinValue) return;
            if (_HasTimeNodeWarningPopped) return;

            var timeNodes = PowerTool.ValidateTimeSpanLines(GlobalData.TimeNodeCheckingList);

            if (timeNodes == null || timeNodes.Count == 0) return;

            var elapsedSeconds = (DateTime.Now - this.Model.TimeAroundGameStart).TotalSeconds;

            timeNodes.ForEach(node =>
            {
                string fixedLengthNode = node.PadLeft(GlobalData.TImeSpanFormatNoHour.Length - 1, '0');
                TimeSpan parsedNode = TimeSpan.ParseExact(fixedLengthNode, GlobalData.TImeSpanFormatNoHour, System.Globalization.CultureInfo.InvariantCulture);
                double remainingSeconds = parsedNode.TotalSeconds - elapsedSeconds;

                if (remainingSeconds > 0 && remainingSeconds < GlobalData.TimeNodeEarlyWarningSeconds)
                {
                    var warningBox = new BoxPhaseBossWarning(() => _HasTimeNodeWarningPopped = false);
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
                SetGameStartTime(DateTime.MinValue, GlobalData.ChangeTimeSourceClearButton);
                _IsAutoRefreshing = false;
                this.Model.ResetAutoOCR();


                this.labelTimer.Text = "--";
                //this.buttonOCR.Enabled = true;
                //this.timerMain.Stop(); //still want to update the clock
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
                if (!labelTimer.Visible) return;
                if (_IsAutoRefreshing) return;
                _IsAutoRefreshing = true;

                var intervalKind = SetTimerInterval();
                if (intervalKind  == OCRGameTimeTimerKind.InGameMiniTopTimeSlow){
                    _IsAutoRefreshing = false;
                    return;
                }

                Task.Factory.StartNew(() =>
                {
                    if (!PowerTool.AnyAppConfigProcessRunning())
                    {
                        System.Diagnostics.Debug.WriteLine($"AnyAppConfigProcessRunning is false, process list: {string.Join(",", GlobalData.ProcessList)}");
                        return "-1";
                    }

                    byte[] screenShotBytes = this.Model.GetImageBytes();
                    if (screenShotBytes == null)
                    {
                        System.Diagnostics.Debug.WriteLine("screenShotBytes is null");
                        return null;
                    }

                    if (_AutoOCREngine == null)
                    {
                        _AutoOCREngine = this.Model.GetDefaultOCREngine();
                    }

                    string data = OCRBase.ReadImageFromMemory(_AutoOCREngine, screenShotBytes);
                    string timeString = this.Model.Find(data);
                    bool saveImage = false;// !string.IsNullOrWhiteSpace(timeString); //false;
                    if (GlobalData.Default.IsDebugging || saveImage)
                    {
                        System.Diagnostics.Debug.WriteLine($"OCR data: {data}");
                        System.Diagnostics.Debug.WriteLine($"OCR time: {timeString}");
                        string tmpPath = OCRBase.SaveTmpFile($"game-time-top-{GlobalData.Default.IsUsingScreenTopTime}", screenShotBytes);
                        System.Diagnostics.Debug.WriteLine($"Tmp file: {tmpPath}");
                    }

                    //this bug occurs when run cf in small window mode
                    //bug: 21:06:04.969 [StartUIStopwatch, ocr: 22:44:34]: set game start: 21:06:04, screen shot saved
                    if (timeString != null && timeString.Length > 2 && int.TryParse(timeString.Substring(0, 2), out int hour) && hour != 0)
                    {
                        this.Log().SaveAsync($"hour not 0, ocr: [{data}], parsed: [{timeString}]", "timerAutoRefresh_Tick", screenShotBytes);
                        timeString = string.Empty; //do this as a temp fix
                    }

                    return timeString;

                }).ContinueWith(t =>
                {
                    if (this.IsDead()) return;
                    _IsAutoRefreshing = false;

                    if (t.IsFaulted)
                    {
                        this.OnError(t.Exception);
                        return;
                    }

                    this.RunOnMain(() =>
                    {
                        string ocrDisplayTime = t.Result;
                        System.Diagnostics.Debug.WriteLine($"ocr - [{ocrDisplayTime}]");
                        if (this.Model.IsOCRTimeMisread(ocrDisplayTime)) return;

                        if (!string.IsNullOrEmpty(ocrDisplayTime))
                        {
                            if (ocrDisplayTime == "-1")
                            {
                                this.labelTimer.Text = "--";
                                SetGameStartTime(DateTime.MinValue, GlobalData.ChangeTimeSourceOCRTimeIsNegativeOne);
                            }
                            else if (ocrDisplayTime != this.Model.AutoOCRTimeOfLastRead)
                            {
                                StartUIStopwatch(ocrDisplayTime, OCRGameTime.AutoOCRDelaySeconds, GlobalData.ChangeTimeSourceTimerOCR);
                            }
                            else
                            {
                                //the same, the time of this read is a repeat read, no need to update
                                System.Diagnostics.Debug.WriteLine($"same as last: {ocrDisplayTime}");
                            }
                        }
                        else if (this.Model.TimeAroundGameStart == DateTime.MinValue)
                        {
                            int second = DateTime.Now.Second;
                            this.labelTimer.Text = second % 2 == 0 ? null : ".";
                            //this.labelTimer.Text = second % 3 == 0 ? "." : (second % 3 == 1 ? ".." : "...");
                        }
                        //else auto refresh failed, just use last result
                        //buttonOCR.Enabled = true; //makes ui blink, so disable it
                    });
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
                //buttonOCR.Enabled = true;
            }
        }

        private OCRGameTimeTimerKind SetTimerInterval()
        {
            if (GlobalData.Default.IsUsingScreenTopTime)
            {
                if (this.Model.IsWithinOneGameRoundOrNap)
                {
                    System.Diagnostics.Debug.WriteLine("auto refresh - within one round/nap, going to skip");
                    _IsAutoRefreshing = false;
                    this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRSlowIntervalMS;
                    return OCRGameTimeTimerKind.InGameMiniTopTimeSlow;
                }
                else
                {
                    this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRFastIntervalMS;
                    return OCRGameTimeTimerKind.InGameMiniTopTimeFast;
                }
            }
            else
            {
                bool fastPreGame = false;

                if (this.Model.TimeAroundGameStart == DateTime.MinValue || this.Model.GameRemainingSeconds < 60 * 4)
                {
                    fastPreGame = true;
                }
                else
                {
                    var passed = DateTime.Now - this.Model.TimeAroundGameStart;
                    if (passed.Minutes >= 10 && passed.Minutes <= 13 || passed.Minutes >= 20 && passed.Minutes <= 23)
                    {
                        fastPreGame = true;
                    }
                }

                if (fastPreGame)
                {
                    this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRPreGameFastIntervalMS;
                    return OCRGameTimeTimerKind.PreGameTimeFast;
                }

                this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRPreGameSlowIntervalMS;
                return OCRGameTimeTimerKind.PreGameTimeSlow;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnClearOCR();
        }

        private void buttonCloseOverlay_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalData.Default.FireCloseApp();
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


            PopupBoxTheme theme = (PopupBoxTheme)this.Model.BootingArgs;
            switch (theme)
            {
                case PopupBoxTheme.OCR2Line:
                    InitGUILayoutV1();
                    break;
                case PopupBoxTheme.OCR1LineLong:
                    InitGUILayoutV2();
                    break;
                case PopupBoxTheme.OCR1LineNoSystemTime:
                    InitGUILayoutV3();
                    break;
                case PopupBoxTheme.ThinOCRTime:
                    InitGUILayoutV4();
                    break;
                case PopupBoxTheme.ThinSystemTime:
                    InitGUILayoutV5();
                    break;
                case PopupBoxTheme.BossCallCounting:
                    InitGUILayoutV6();
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
