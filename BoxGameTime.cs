using SkyStopwatch.DataModel;
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
    public partial class BoxGameTime : Form, IPopupBox, ITopForm
    {
        private bool _ShouldUpdatingPassedTime = false;

        /// <summary>
        /// only the first time matter, the time does not auto update on the game label
        /// </summary>
        private bool _IsAutoRefreshing = false;
        private Tesseract.TesseractEngine _DefaultOCREngine;
        private Tesseract.TesseractEngine _AuxEngine;
        private bool _HasTimeNodeWarningPopped = false;
        private bool _HasTargetProcessExit = false;


        public DateTime CreateAt => DateTime.Now;
        public string Key => GlobalData.PopupKeyGameTime;

        private bool _IsPaused;
        public bool IsPaused => _IsPaused;

        public OCRGameTime Model { get { return ViewModelFactory.Instance.GetGameTime(); } }

        public bool IsPermanent => true;

        public bool IsTimeLocked
        {
            get { return this.Model.IsTimeLocked; }
            set
            {
                this.Model.IsTimeLocked = value;
                System.Diagnostics.Debug.WriteLine($"set locked: {value}");
            }
        }

        public BoxGameTime(int args)
        {
            InitializeComponent();
            this.InitBase();

            this.Model.BootingArgs = args;

            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            this.labelTimer.Text = "unset";
            this.IsTimeLocked = false;
            this.timerMainUI.Interval = OCRGameTime.TimerDisplayUIIntervalMS;
            this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRFastIntervalMS;

            //do the following in form_loaded
            //InitGUILayoutV1();
            //InitGUILayoutV2();


            //pre warm up
            this.timerMainUI.Start();
            this.labelTimer.Text = "run";
            Task.Factory.StartNew(() =>
            {
                _DefaultOCREngine = this.Model.CreateOCREngine();
                Thread.Sleep(500);
                if (this.IsDead()) return;

                this.BeginInvoke(new Action(() =>
                {
                    //this.OnNewGameStart();
                    this.OnRunOCR(() => WarmUpEndSequence(), GlobalData.ChangeTimeSourcePreWarmUp);
                }));
            }).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    this.OnError(task.Exception);
                }
            });
        }

        private void WarmUpEndSequence()
        {
            _ShouldUpdatingPassedTime = true;
            if (!this.timerAutoRefresh.Enabled)
            {
                this.timerAutoRefresh.Start();
            }
        }

        public void SetDefaultLocation(IPopupBox parent = null)
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
                //this.Location = new Point(center.X - 470, center.Y - 398); //ahead top mini time 2 - larger space
                //this.Location = new Point(center.X + 360, center.Y - 390); //tail top mini time

                this.Location = GlobalData.Default.BoxTimeLastCloseLocation;
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
            this.buttonToolBox.Text = null;

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
            this.TopMost = GlobalData.Default.EnableTopMost;

            if ((PopupBoxTheme)this.Model.BootingArgs == PopupBoxTheme.ThinOCRTime)
            {
                this.buttonToolBox.Text = null;
            }
            else
            {
                this.buttonToolBox.Text = GlobalData.Default.EnableTopMost ? "+" : "-";//this._TopMost ? "Pin" : "-P";
            }
        }

        private void SetGameStartTime(DateTime newTime, string source, string detail)
        {
            //DateTime oldTime = this.Model.TimeAroundGameStart;

            //if (oldTime != newTime)
            {
                this.Model.TimeAroundGameStart = newTime;
                var changeSource = TimeChangeSource.Unset;

                if (source == GlobalData.ChangeTimeSourceNewGameAuto)
                {
                    changeSource = TimeChangeSource.AppAutoRestart;
                }
                else if (source == GlobalData.ChangeTimeSourceNewGameButton)
                {
                    changeSource = TimeChangeSource.UserClickNewGame;
                }
                else if (source == GlobalData.ChangeTimeSourceTimerOCR)
                {
                    bool isSecondary = this.EnableReadMiddleAsSecondary() && "true".Equals(detail, StringComparison.OrdinalIgnoreCase);
                    changeSource = isSecondary ? TimeChangeSource.AppAutoUpdateBySecondary : TimeChangeSource.AppAutoUpdate;
                }
                else if (source == GlobalData.ChangeTimeSourceOCRTimeIsNegativeOne)
                {
                    changeSource = TimeChangeSource.TargetAppExit;
                }
                else if(source == GlobalData.ChangeTimeSourceOCRTimeIsNegativeTwo)
                {
                    changeSource = TimeChangeSource.TargetAppStartup;
                }
                //else leotodo, do not need to know other cases now

                this.Model.TimeChangeSource = changeSource;

                GlobalData.Default.FireChangeGameStartTime(new ChangeGameStartTimeEventArgs(newTime, source));
                bool saveScreenShot = newTime != DateTime.MinValue;
                this.Log().SaveAsync($"set start: {newTime.ToString(GlobalData.TimeFormat6Digits)}, detail: {detail}", source, saveScreenShot);
            }
        }



        private void SetUIStopwatch(string ocrDisplayTime, int kickOffDelaySeconds, string source, string detail = null)
        {
            if (string.IsNullOrEmpty(ocrDisplayTime)) return;
            if (!_ShouldUpdatingPassedTime) return;

            if (!this.timerMainUI.Enabled)
            {
                this.timerMainUI.Start();
            }

            if (TimeSpan.TryParseExact(ocrDisplayTime, GlobalData.TimeSpanFormat, System.Globalization.CultureInfo.InvariantCulture, out TimeSpan ocrTimeSpan))
            {
                int passedSeconds = (int)ocrTimeSpan.TotalSeconds + kickOffDelaySeconds;

                if (GlobalData.Default.IsUsingScreenTopTime)
                {
                    if (ocrTimeSpan.Minutes > GlobalData.GameRoundMaxMinute)
                    {
                        passedSeconds = 0;
                    }
                }
                else
                {
                    if (source == GlobalData.ChangeTimeSourceTimerOCR)
                    {
                        int magicSecond = GlobalData.Default.TimeViewScanMiddleDelaySecond - OCRGameTime.AutoOCRDelaySeconds;
                        int magicSecondLimit = 5;

                        if (passedSeconds > magicSecondLimit)
                        {
                            passedSeconds -= magicSecond;
                        }
                    }
                    else if (source == GlobalData.ChangeTimeSourceAdjustTimeButton)
                    {
                        //do nothing if manually adjust
                    }
                    else
                    {
                        if (ocrTimeSpan.TotalMinutes > GlobalData.PreRoundGameMinutes)
                        {
                            passedSeconds = 0;
                        }
                    }
                }

                this.Model.AutoOCRTimeOfLastRead = ocrDisplayTime;
                var startTimeByOCR = DateTime.Now.AddSeconds(passedSeconds * -1);
                SetGameStartTime(startTimeByOCR, source, detail);
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
                tool.SetLocation(this);
                tool.Show();
                return tool;
            }
        }

        private FormBootSetting CreateToolBox(Bitmap bitmap)
        {
            var args = new BootSettingArgs()
            {
                Image = bitmap,
                IsTimeLocked = this.IsTimeLocked,
                LockSource = this.Model.LockSource,
                EnableLockButton = this.Model.TimeAroundGameStart != DateTime.MinValue,
            };


            FormBootSetting tool = new FormBootSetting(args,
                               (_, __) => { this.OnInitToolBox(_, __); },
                               () => { this.OnRunOCR(null, GlobalData.ChangeTimeSourceManualOCRButton); },
                               () => { this.OnNewGameStart(false); },
                               () => { this.OnSwitchTopMost(); },
                               () => { this.OnClearOCR(); },
                               (_) => { this.OnAddSeconds(_); },
                               (_) => { this.OnChangeTimeNodes(_); },
                               () => { this.OnSwitchTimeLockState(); } );

            return tool;
        }

        private void OnChangeTimeNodes(string newTimeNodes)
        {
            this.CheckTimeNodes();
        }

        private void OnInitToolBox(Button toolBoxButtonOCR, string initialTimeNodes)
        {
            toolBoxButtonOCR.Enabled = !_ShouldUpdatingPassedTime;

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

            //System.Diagnostics.Debug.WriteLine($"----- OnInitToolBox, {nameof(initialTimeNodes)}: {initialTimeNodes}");
            //System.Diagnostics.Debug.WriteLine($"old: {this._TimeNodeCheckingList}");
            //do not do this, causing bug
            //this._TimeNodeCheckingList = initialTimeNodes;
        }

        private void OnRunOCR(Action afterDone, string source)
        {
            _ShouldUpdatingPassedTime = false;
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
                string ocrDisplayTime = this.Model.Find(data, GlobalData.Default.IsUsingScreenTopTime);

                if (this.IsDead()) return;
                this.BeginInvoke((Action)(() =>
                {
                    if (!string.IsNullOrEmpty(ocrDisplayTime))
                    {
                        _ShouldUpdatingPassedTime = true;
                        SetUIStopwatch(ocrDisplayTime, OCRGameTime.ManualOCRDelaySeconds, source);
                    }
                    else
                    {
                        labelTimer.Text = "none";
                    }

                    afterDone?.Invoke();
                }));
            });
        }

        private void OnNewGameStart(bool autoRestart)
        {
            _ShouldUpdatingPassedTime = true;
            //this.buttonOCR.Enabled = false;

            //reset flags/history values
            //flag 1
            this.Model.ResetAutoOCR(autoRestart ? TimeLocKSource.AutoLockChecker : TimeLocKSource.UserClick);

            //flag 2 - this._TimeAroundGameStart, which will be updated in the following method
            var source = autoRestart ? GlobalData.ChangeTimeSourceNewGameAuto : GlobalData.ChangeTimeSourceNewGameButton;
            SetUIStopwatch(TimeSpan.Zero.ToString(GlobalData.TimeSpanFormat), OCRGameTime.NewGameDelaySeconds, source);

            if (!this.timerAutoRefresh.Enabled)
            {
                this.timerAutoRefresh.Start();
            }
        }

        private void OnAddSeconds(int seconds)
        {
            if (this.Model.TimeAroundGameStart == DateTime.MinValue) return;

            _ShouldUpdatingPassedTime = true;
            //this.buttonOCR.Enabled = false;

            TimeSpan passedTimeWithIncrease = DateTime.Now.AddSeconds(seconds) - this.Model.TimeAroundGameStart;

            if (passedTimeWithIncrease < TimeSpan.Zero)
            {
                passedTimeWithIncrease = TimeSpan.Zero;
            }

            SetUIStopwatch(passedTimeWithIncrease.ToString(GlobalData.TimeSpanFormat), OCRGameTime.NoDelay, GlobalData.ChangeTimeSourceAdjustTimeButton);
        }

        private void OnSwitchTopMost()
        {
            SyncTopMost();
        }

        private void OnSwitchTimeLockState()
        {
            this.IsTimeLocked = !this.IsTimeLocked;

            if (this.IsTimeLocked)
            {
                this.Model.LockSource = DataModel.TimeLocKSource.UserClick;
            }
            else
            {
                this.Model.ResetAutoLockArgs(TimeLocKSource.UserClick);
            }
        }

        private void timerRefreshUI_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.labelTitle.Visible)
                {
                    //this.labelTitle.Text = "23:59";
                    this.labelTitle.Text = DateTime.Now.ToString(GlobalData.TimeFormatNoSecond);
                }

                if (!this.labelTimer.Visible) return;

                if (_ShouldUpdatingPassedTime)
                {
                    if (this.Model.TimeAroundGameStart != DateTime.MinValue)
                    {
                        this.CheckTimeNodes();

                        var passed = DateTime.Now - this.Model.TimeAroundGameStart;
                        if (passed <= TimeSpan.FromSeconds(GlobalData.GameRoundMaxMinute * 60 + GlobalData.GameRoundAdjustSeconds))
                        {
                            this.labelTimer.Text = passed.ToString(GlobalData.UIElapsedTimeFormat);
                            this.labelTimer.ForeColor = this.Model.IsTimeLocked ? Color.MediumBlue : Color.Black;
                        }
                        else
                        {
                            this.OnNewGameStart(true);
                        }
                    }
                    else if(this.Model.TimeChangeSource != TimeChangeSource.TargetAppExit)
                    {
                        if (DateTime.Now.Millisecond > 700)
                        {
                            this.labelTimer.Text = DateTime.Now.Second % 2 == 0 ? null : ".";
                        }
                    }
                }

                if (DateTime.Now.Second % 10 == 0)
                {
                    object checkedSign = "-checked";
                    if (this.labelTimer.Tag == checkedSign) return;

                    this.CheckTargetAppRunning();
                    Task.Run(() => this.ScanInGameFlagIfEnabled(string.Empty, false));

                    this.labelTimer.Tag = checkedSign;
                    return;
                }

                this.labelTimer.Tag = null;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void CheckTargetAppRunning()
        {
            bool isTargetRunning = PowerTool.AnyTargetProcessRunning();
            GlobalData.Default.EnableTopMost = isTargetRunning;
            _HasTargetProcessExit = !isTargetRunning;

            if (this.TopMost != isTargetRunning)
            {
                this.TopMost = isTargetRunning;
            }

            if (_ShouldUpdatingPassedTime)
            {
                if (isTargetRunning)
                {
                    if (this.Model.TimeAroundGameStart == DateTime.MinValue && this.Model.TimeChangeSource != TimeChangeSource.TargetAppStartup)
                    {
                        this.labelTimer.Text = ".";
                        SetGameStartTime(DateTime.MinValue, GlobalData.ChangeTimeSourceOCRTimeIsNegativeTwo, "target app running");
                    }
                }
                else
                {
                    this.labelTimer.Text = "..";//"--";
                    SetGameStartTime(DateTime.MinValue, GlobalData.ChangeTimeSourceOCRTimeIsNegativeOne, "target app exit");
                }
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

            foreach (var node in timeNodes)
            {
                var fixedLengthNode = node.PadLeft(GlobalData.TImeSpanFormatNoHour.Length - 1, '0');
                var parsedNode = TimeSpan.ParseExact(fixedLengthNode, GlobalData.TImeSpanFormatNoHour, System.Globalization.CultureInfo.InvariantCulture);
                var remainingSeconds = parsedNode.TotalSeconds - elapsedSeconds;

                if (remainingSeconds > 0 && remainingSeconds < GlobalData.TimeNodeEarlyWarningSeconds)
                {
                    _HasTimeNodeWarningPopped = true;
                    var warningBox = new BoxPhaseBossWarning(() => _HasTimeNodeWarningPopped = false);
                    warningBox.ShowAside(this);
                    return;
                }
            }
        }

        private void OnClearOCR()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("clear");

                _ShouldUpdatingPassedTime = false;
                SetGameStartTime(DateTime.MinValue, GlobalData.ChangeTimeSourceClearButton, "user clear");
                _IsAutoRefreshing = false;
                this.Model.ResetAutoOCR(TimeLocKSource.UserClick);

                this.labelTimer.Text = "--";
                this.timerAutoRefresh.Stop();
                //this.timerMainUI.Stop(); //still want to update the clock

                if (_DefaultOCREngine != null)
                {
                    _DefaultOCREngine.Dispose();
                    _DefaultOCREngine = null;
                }

                if(_AuxEngine != null)
                {
                    _AuxEngine.Dispose();
                    _AuxEngine = null;
                }

                System.Diagnostics.Debug.WriteLine("clear done");
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void timerOCRProcess_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_HasTargetProcessExit) return;
                if (!labelTimer.Visible) return;
                if (_IsAutoRefreshing) return;
                if (this.IsTimeLocked && !TryAutoUnlockTime()) return;

                var intervalKind = SetTimerInterval();
                if (intervalKind == OCRTimerSpeed.InGameMiniTopTimeSlow) return;
                _IsAutoRefreshing = true;

                Task.Factory.StartNew(() =>
                {
                    if (_DefaultOCREngine == null)
                    {
                        _DefaultOCREngine = this.Model.CreateOCREngine();
                    }

                    var ocrPrimaryTime = ScanPrimaryTime();
                    var inGameFlagFound = ScanInGameFlagIfEnabled(ocrPrimaryTime, true);
                    var middleTimeResult = ScanMiddleTimeIfEnabled(ocrPrimaryTime, inGameFlagFound);

                    if (middleTimeResult != null)
                    {
                        return middleTimeResult;
                    }

                    return Tuple.Create(ocrPrimaryTime, 1);
                }).ContinueWith(t =>
                {
                    if (this.IsDead()) return;
                    _IsAutoRefreshing = false;
                    if (t.IsFaulted){ this.OnError(t.Exception); return;}

                    this.RunOnMain(() =>
                    {
                        string ocrDisplayTime = t.Result.Item1;
                        bool isSecondary = t.Result.Item2 == 2;
                        if (this.Model.IsOCRTopTimeMisread(isSecondary ? string.Empty : ocrDisplayTime)) return;

                        if (!string.IsNullOrEmpty(ocrDisplayTime))
                        {
                            if (ocrDisplayTime != this.Model.AutoOCRTimeOfLastRead)
                            {
                                int delaySeconds = GlobalData.Default.IsUsingScreenTopTime ? 1 : OCRGameTime.AutoOCRDelaySeconds;
                                SetUIStopwatch(ocrDisplayTime, delaySeconds, GlobalData.ChangeTimeSourceTimerOCR, isSecondary.ToString());
                                TryAutoLockTime();
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"same as last, ocr: {ocrDisplayTime}"); //this read is a repeat read, no need to update
                            }
                        }//else just use last result
                    });
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private string ScanPrimaryTime()
        {
            byte[] screenShotBytes = this.Model.GetImageBytes();
            if (screenShotBytes == null) return null;

            string data = OCRBase.ReadImageFromMemory(_DefaultOCREngine, screenShotBytes);
            string ocrTime = this.Model.Find(data, GlobalData.Default.IsUsingScreenTopTime);
            System.Diagnostics.Debug.WriteLine($"ocr1: [{ocrTime}] - empty #{this.Model.EmptyCount}");
            bool saveImage = false;// !string.IsNullOrWhiteSpace(ocrTime);

            if (GlobalData.Default.IsDebugging || saveImage)
            {
                System.Diagnostics.Debug.WriteLine($"OCR data: {data}, parsed: {ocrTime}");
                string tmpPath = OCRBase.SaveTmpFile($"game-time-top-{GlobalData.Default.IsUsingScreenTopTime}", screenShotBytes);
                System.Diagnostics.Debug.WriteLine($"Tmp file: {tmpPath}");
            }

            //this bug occurs when run cf in small window mode e.g: [StartUIStopwatch, ocr: 22:44:34]: set game start: 21:06:04, screen shot saved
            if (ocrTime != null && ocrTime.Length > 2 && int.TryParse(ocrTime.Substring(0, 2), out int hour) && hour != 0)
            {
                this.Log().SaveAsync($"hour not 0, ocr: [{data}], parsed: [{ocrTime}]", "timerAutoRefresh_Tick", screenShotBytes);
                ocrTime = string.Empty; //do this as a temp fix
            }

            return ocrTime;
        }

        private bool ScanInGameFlagIfEnabled(string ocrPrimaryTime, bool useDefalutEngine)
        {
            if (!string.IsNullOrEmpty(ocrPrimaryTime)) return false;

            var flagBytes = this.Model.GetImageBytesBy(TimeScanKind.InGameFlag);
            if (flagBytes == null) return false;

            if(_AuxEngine == null)
            {
                _AuxEngine = this.Model.CreateOCREngine();
            }

            var engine = useDefalutEngine ? _DefaultOCREngine : _AuxEngine;
            var flagData = OCRBase.ReadImageFromMemory(engine, flagBytes);
            var flagResult = this.Model.FindInGameFlag(flagData);
            System.Diagnostics.Debug.WriteLine($"ocr2: [{flagResult}] - #{this.Model.InGameFlagCount}");

            return flagResult;
        }

        private Tuple<string, int> ScanMiddleTimeIfEnabled(string ocrPrimaryTime, bool inGameFlagFound)
        {
            if (string.IsNullOrEmpty(ocrPrimaryTime) && EnableReadMiddleAsSecondary() && !inGameFlagFound)
            {
                var middleImageBytes = this.Model.GetImageBytesBy(TimeScanKind.MiddleTime);
                if (middleImageBytes == null) return null;

                var middleData = OCRBase.ReadImageFromMemory(_DefaultOCREngine, middleImageBytes);
                var middleTime = this.Model.Find(middleData, false);
                System.Diagnostics.Debug.WriteLine($"ocr3: [{middleTime}]");
                return Tuple.Create(middleTime, 2);
            }

            return null;
        }

        private bool EnableReadMiddleAsSecondary()
        {
            return GlobalData.Default.IsUsingScreenTopTime && GlobalData.Default.EnableTimeViewMiddleAsSecondary;
        }

        private bool TryAutoLockTime()
        {
            if (this.IsTimeLocked) return false;

            if (this.Model.ShouldAutoLock())
            {
                this.IsTimeLocked = true;
                this.Model.LockSource = DataModel.TimeLocKSource.AutoLockChecker;
                this.RunOnMainAsync(() => 
                {
                    System.Diagnostics.Debug.WriteLine($"auto lock time, boss warning pop: {_HasTimeNodeWarningPopped}");
                    BoxMessage.Show("Going to lock time", this, _HasTimeNodeWarningPopped);
                });

                this.SetTimerInterval();
                return true;
            }

            return false;
        }

        private bool TryAutoUnlockTime()
        {
            if (!this.IsTimeLocked) return false;

            if (this.Model.ShouldAutoUnlock())
            {
                this.Model.ResetAutoLockArgs(TimeLocKSource.AutoLockChecker);
                System.Diagnostics.Debug.WriteLine($"auto unlock, remain: {this.Model.GameRemainingSeconds / 60}");
                return true;
            }

            return false;
        }

        private OCRTimerSpeed SetTimerInterval()
        {
            if (GlobalData.Default.IsUsingScreenTopTime)
            {
                if (this.IsTimeLocked || this.Model.IsWithinOneGameRoundOrNap)
                {
                    System.Diagnostics.Debug.WriteLine("within one round/nap/locked, going to skip");
                    _IsAutoRefreshing = false;

                    this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRSlowIntervalMS;
                    return OCRTimerSpeed.InGameMiniTopTimeSlow;
                }
                else if (this.Model.IsEmptyReadTooMany)
                {
                    int interval = this.EnableReadMiddleAsSecondary() ? 3000 : OCRGameTime.TimerAutoOCRSlowIntervalMS;
                    this.timerAutoRefresh.Interval = interval;
                    return OCRTimerSpeed.InGameMiniTopTimeSlowByTooManyEmpty;
                }
                else
                {
                    this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRFastIntervalMS;
                    return OCRTimerSpeed.InGameMiniTopTimeFast;
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
                    return OCRTimerSpeed.PreGameTimeFast;
                }

                this.timerAutoRefresh.Interval = OCRGameTime.TimerAutoOCRPreGameSlowIntervalMS;
                return OCRTimerSpeed.PreGameTimeSlow;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalData.Default.BoxTimeLastCloseLocation = this.Location;
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

        public void SetUserFriendlyTitle()
        {
            this.SetVersion();
        }
    }
}
