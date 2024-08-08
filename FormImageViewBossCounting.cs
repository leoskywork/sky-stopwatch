using SkyStopwatch.DataModel;
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
using System.Xml;

namespace SkyStopwatch
{
    public partial class FormImageViewBossCounting : Form
    {

        //leotodo, pentontial multi-thread issue
        private Queue<byte[]> _BossCallImageQueue = new Queue<byte[]>();
        private Queue<Tuple<byte[], byte[]>> _BossCallImagePairQueue = new Queue<Tuple<byte[], byte[]>>();
        private int _ScanCount;
        private int _CompareCount;
        private Tesseract.TesseractEngine _AutoOCREngine;
        private bool _IsComparing;

        //leotodo, multi-thread issue
        private BossCallSet _BossGroups = new BossCallSet();

        private DateTime _LastBossCallFoundTime;
        private bool _AutoShowPopupBox;

        public FormImageViewBossCounting(bool autoPopup)
        {
            InitializeComponent();


            this.numericUpDownX.Value = MainOCRBossCounting.XPoint;
            this.numericUpDownY.Value = MainOCRBossCounting.YPoint;
            this.numericUpDownWidth.Value = MainOCRBossCounting.BlockWidth;
            this.numericUpDownHeight.Value = MainOCRBossCounting.BlockHeight;
            this.checkBoxAutoSlice.Checked = MainOCRBossCounting.EnableAutoSlice;
            this.numericUpDownAutoSliceIntervalSeconds.Value = MainOCRBossCounting.AutoSliceIntervalSeconds;
            this._AutoShowPopupBox = autoPopup;

            if (this._AutoShowPopupBox)
            {
                this.Hide();
                this.Location = new Point(10000, 10000);
            }

            this.timerScan.Interval = GlobalData.Default.BossCountingScanTimerIntervalMS;
            this.timerCompare.Interval = GlobalData.Default.BossCountingCompareTimerIntervalMS;

            GlobalData.Default.ChangeGameStartTime += OnChangeGameStartTime;
            this.checkBoxOneMode.Checked = GlobalData.Default.EnableBossCountingOneMode;
        }

        private void OnChangeGameStartTime(object sender, ChangeGameStartTimeEventArgs e)
        {
            if (e == null || e.Source == null) return;

            if (!e.Source.StartsWith(GlobalData.ChangeTimeSourcePreWarmUp) && !e.Source.StartsWith(GlobalData.ChangeTimeSourceAdjustTimeButton))
            {
                _BossGroups.Reset();
            }
        }

      

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonSave.Enabled = false;

                int x = (int)this.numericUpDownX.Value;
                int y = (int)this.numericUpDownY.Value;
                int width = (int)this.numericUpDownWidth.Value;
                int height = (int)this.numericUpDownHeight.Value;

                MainOCR.SafeCheckImageBlock(ref x, ref y, ref width, ref height);

                MainOCRBossCounting.XPoint = x;
                MainOCRBossCounting.YPoint = y;
                MainOCRBossCounting.BlockWidth = width;
                MainOCRBossCounting.BlockHeight = height;
                MainOCRBossCounting.EnableAutoSlice = this.checkBoxAutoSlice.Checked;
                MainOCRBossCounting.AutoSliceIntervalSeconds = (int)this.numericUpDownAutoSliceIntervalSeconds.Value;

                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true));

                this.buttonStart.Enabled = true;
            }
            catch (Exception ex)
            {
                this.buttonSave.Enabled = true;
                this.OnError(ex);
            }
        }

        private void TryUpdateImage()
        {
            try
            {
                this.buttonSave.Enabled = true;

                var screenRect = new Rectangle(0, 0, width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height);

                using (var screenShot = new Bitmap(screenRect.Width, screenRect.Height))
                using (var gra = Graphics.FromImage(screenShot))
                {
                    gra.CopyFromScreen(0, 0, 0, 0, screenShot.Size);
                    gra.DrawImage(screenShot, 0, 0, screenRect, GraphicsUnit.Pixel);

                    int x = (int)this.numericUpDownX.Value;
                    int y = (int)this.numericUpDownY.Value;
                    int width = (int)this.numericUpDownWidth.Value;
                    int height = (int)this.numericUpDownHeight.Value;

                    MainOCR.SafeCheckImageBlock(ref x, ref y, ref width, ref height);

                    //can not use using block here, since we pass the bitmap into a view and show it
                    var bitmapBlock = screenShot.Clone(new Rectangle(x, y, width, height), screenShot.PixelFormat);

                    if(this.pictureBoxOne.Image!= null)
                    {
                        this.pictureBoxOne.Image.Dispose();
                    }

                    this.pictureBoxOne.Image = bitmapBlock;

                    labelX.Text = $"X: {(int)((decimal)x / (decimal)screenRect.Width * 10000) * 0.01}%";
                    labelY.Text = $"Y: {(int)((decimal)y / (decimal)screenRect.Height * 10000)* 0.01}%";

                }
            }
            catch (Exception ex)
            {
               this.OnError(ex);
            }
        }



        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage();
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage();
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage();
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.buttonSave.Enabled = false;
            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";
            GlobalData.Default.ClearPopups();

            if (this._AutoShowPopupBox)
            {
                this.ResetTimers();
                _BossGroups.Reset();
                this.PopupCountingBox();
                this.RunOnMain(() => this.Hide(), 1);
            };
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonStart.Enabled = false;
                //leotodo, a better way to do this, unsaved changes found
                //this.buttonSave.Enabled = false;
                if (this.buttonSave.Enabled)
                {
                    MessageBox.Show("Please click the save button to submit changes first.");
                    return;
                }

                this.pictureBoxOne.Image = null;

                ResetTimers();
                _BossGroups.Reset();
                PopupCountingBox();
                this.Hide();
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void ResetTimers()
        {
            this._BossCallImageQueue.Clear();
            this._ScanCount = 0;
            this._CompareCount = 0;

            if (!this.timerScan.Enabled)
            {
                this.timerScan.Start();
            }

            if (!this.timerCompare.Enabled)
            {
                this.timerCompare.Start();
            }
        }

        private void PopupCountingBox()
        {
            Form bossCountingBox;

            if (GlobalData.Default.EnableBossCountingOneMode)
            {
                bossCountingBox = new BoxBossCountingSuccinct(_BossGroups, checkBoxAutoSlice.Checked, () => this.Close());
            }
            else
            {
                bossCountingBox = new BoxBossCounting(_BossGroups, checkBoxAutoSlice.Checked, () => this.Close());
            }


            if (this._AutoShowPopupBox)
            {
                //leotodo, improve this, just center it now
                bossCountingBox.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                bossCountingBox.StartPosition = FormStartPosition.Manual;
                bossCountingBox.Location = new Point(this.Location.X + this.Width, this.Location.Y + 100);
            }

            bossCountingBox.Show();
            GlobalData.Default.LongLivePopups.Add(bossCountingBox);
        }
       

        private void timerScan_Tick(object sender, EventArgs e)
        {
            try
            {
                if (IsWithinOneRoundBossCall()) return;

                this._ScanCount++;

                if (this.checkBox2SpotsCompare.Checked)
                {
                    var imageData = MainOCRBossCounting.GetFixedLocationImageDataPair(false);
                    _BossCallImagePairQueue.Enqueue(imageData);
                }
                else
                {
                    var imageData = MainOCRBossCounting.GetFixedLocationImageData();
                    _BossCallImageQueue.Enqueue(imageData);
                }
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private bool IsWithinOneRoundBossCall()
        {
            if (_LastBossCallFoundTime.AddSeconds(MainOCR.MinBossCallTimeSeconds) > DateTime.Now)
            {
                System.Diagnostics.Debug.WriteLine($"boss call {DateTime.Now.ToString("h:mm:ss.fff")} scan: {_ScanCount}, compare: {_CompareCount}");
                System.Diagnostics.Debug.WriteLine($"boss call check within one round - within: {_LastBossCallFoundTime.AddSeconds(MainOCR.MinBossCallTimeSeconds) > DateTime.Now}");
            }

            return _LastBossCallFoundTime.AddSeconds(MainOCR.MinBossCallTimeSeconds) > DateTime.Now;
        }

        private void timerCompare_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_IsComparing) return;
                if (_BossCallImageQueue.Count == 0 && _BossCallImagePairQueue.Count == 0) return;
                if (IsWithinOneRoundBossCall()) return;

                _IsComparing = true;
                this._CompareCount++;
                bool enable2SpotsCompare = this.checkBox2SpotsCompare.Checked;

                Task.Factory.StartNew(() =>
                {
                    if (_AutoOCREngine == null)
                    {
                        _AutoOCREngine = MainOCRBossCounting.GetDefaultOCREngine();
                    }

                    if (enable2SpotsCompare)
                    {
                        return CompareTwoSectionsOneRound();
                    }
                    else
                    {
                        return CompareOneSectionMultipleRounds();
                    }

                }).ContinueWith(t =>
                {
                    _IsComparing = false;

                    if (this.IsDead()) return;

                    if (t.IsFaulted)
                    {
                        this.OnError(t.Exception);
                        return;
                    }

                    if (_BossGroups.Sum(g => g.Calls.Count) >= 1000)
                    {
                        _BossGroups.Reset();
                    }
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private OCRCompareResult<int> CompareTwoSectionsOneRound()
        {


            return null;
        }

        private OCRCompareResult<int> CompareOneSectionMultipleRounds()
        {
            var rawData = _BossCallImageQueue.Dequeue();
            var ocrProcessedData = MainOCR.ReadImageFromMemory(_AutoOCREngine, rawData);
            var lastBossCall = _BossGroups.LastCallOrDefault<BossCall>();
            int ocrMatchDigit = 5;

            if (lastBossCall != null && !lastBossCall.IsValid && lastBossCall.IsTop1CallSameBossCallWith(DateTime.Now))
            {
                ocrMatchDigit = lastBossCall.OCRLastMatch - 1;
            }


            //var found = MainOCRBossCounting.FindBossCall(ocrProcessedData, ocrMatchDigit); //sometimes, missing a count
            var result = MainOCRBossCounting.FindBossCall(ocrProcessedData, ocrMatchDigit, ocrMatchDigit - 1); //sometimes, adding a invalid count

            if (result.IsSuccess)
            {
                if (lastBossCall == null || lastBossCall.IsValid)
                {
                    var bossCall = new BossCall
                    {
                        FirstMatchTime = DateTime.Now,
                        FirstMatchValue = result.CompareTarget,
                        OCRLastMatch = result.CompareTarget,
                        PreCounting = true
                    };
                    _BossGroups.Last().Add(bossCall);
                }
                else
                {
                    lastBossCall.SecondMatchTime = DateTime.Now;
                    lastBossCall.SecondMatchValue = result.CompareTarget;
                    lastBossCall.OCRLastMatch = result.CompareTarget;

                    if (lastBossCall.IsTop2CallsSameBossCall() && lastBossCall.IsTop2CallsMatchSecondCountdown())
                    {
                        lastBossCall.IsValid = true;
                        lastBossCall.PreCounting = true;
                        _LastBossCallFoundTime = DateTime.Now;
                    }
                    else //not the same call, remove old and add new
                    {
                        lastBossCall.PreCounting = false;
                        _BossGroups.Last().Remove(lastBossCall);
                        var bossCall = new BossCall
                        {
                            FirstMatchTime = DateTime.Now,
                            FirstMatchValue = result.CompareTarget,
                            OCRLastMatch = result.CompareTarget,
                            PreCounting = true
                        };
                        _BossGroups.Last().Add(bossCall);
                    }
                }
            }
            else if (lastBossCall != null)
            {
                if (!lastBossCall.IsValid && result.CompareTarget >= -1 && result.Info != lastBossCall.FirstMatchValue.ToString())
                {
                    lastBossCall.PreCounting = false;
                }
            }

            if (GlobalData.Default.IsDebugging || result.IsSuccess)
            {
                string tmpPath = MainOCR.SaveTmpFile($"boss-call-{result.IsSuccess}-{result.Info}-within-1-{IsWithinOneRoundBossCall()}", rawData);
                System.Diagnostics.Debug.WriteLine($"OCR compare: {result.IsSuccess}, OCR data: {ocrProcessedData}, tmp path: {tmpPath}");
            }

            return result;
        }

        private void checkBoxAux1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.numericUpDownAutoSliceIntervalSeconds.Enabled = this.checkBoxAutoSlice.Checked;
                this.buttonSave.Enabled = true;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

     

        private void numericUpDownAux1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                MainOCRBossCounting.AutoSliceIntervalSeconds = (int) this.numericUpDownAutoSliceIntervalSeconds.Value;
                this.buttonSave.Enabled = true;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void FormImageViewCounting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.timerScan.Stop();
                this.timerCompare.Stop();
                GlobalData.Default.ChangeGameStartTime -= OnChangeGameStartTime;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void checkBoxOneMode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalData.Default.EnableBossCountingOneMode = this.checkBoxOneMode.Checked;
                this.buttonSave.Enabled= true;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void checkBox2SpotsCompare_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
