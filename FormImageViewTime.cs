using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class FormImageViewTime : Form
    {
        private bool _EnableScreenTopTime;
        private bool _EnableAutoLock;
        private int _ScanMiddleDelaySecond;


        public FormImageViewTime()
        {
            InitializeComponent();

            ReadPreviewArgsFromViewModel();
            ReadPresetsFromViewModel(true, true);

            if (GlobalData.Default.IsUsingScreenTopTime)
            {
                //this.groupBoxPresetLocation.Enabled = false;
                //this.labelMessage.Text = "Some settings are disabled when scan mini top time";
                //this.labelMessage.ForeColor = Color.Red;
                this.buttonSetAsPreset1.Visible = false;
                this.labelPreset1.Visible = false;
                this.labelPreset1Title.Visible = false;
                this.buttonApplyPreset1.Visible = false;
                this.buttonResetMiddleTimeLocation.Visible = false;
            }
            else
            {
                this.buttonSetAsPreset2.Visible = false;
                this.labelPreset2.Visible = false;  
                this.labelPreset2Title.Visible = false;
                this.buttonApplyPreset2.Visible = false;
                this.buttonResetTopTimeLocation.Visible = false;    
            }
        }

        private void ReadPreviewArgsFromViewModel()
        {
            var defaultArgs = GetPreviewArgs();
            this.numericUpDownX.Value = defaultArgs.X;
            this.numericUpDownY.Value = defaultArgs.Y;
            this.numericUpDownWidth.Value = defaultArgs.Width;
            this.numericUpDownHeight.Value = defaultArgs.Height;

            this._EnableScreenTopTime = GlobalData.Default.IsUsingScreenTopTime;
            this._EnableAutoLock = GlobalData.Default.EnableScreenTopTimeAutoLock;
            this._ScanMiddleDelaySecond = GlobalData.Default.TimeViewScanMiddleDelaySecond;

            this.checkBoxReadTopTime.Checked = _EnableScreenTopTime;
            this.checkBoxAutoLock.Checked = _EnableAutoLock;
            this.checkBoxAutoLock.Enabled = _EnableScreenTopTime;
            this.buttonResetTopTimeLocation.Enabled = _EnableScreenTopTime;
            this.buttonResetMiddleTimeLocation.Enabled = !_EnableScreenTopTime;
            this.numericUpDownDelaySecond.Value = _ScanMiddleDelaySecond;
        }

        private Rectangle GetPreviewArgs()
        {
            if (GlobalData.Default.IsUsingScreenTopTime)
            {
                return new Rectangle(OCRGameTime.TopXPoint, OCRGameTime.TopYPoint, OCRGameTime.TopBlockWidth, OCRGameTime.TopBlockHeight);
            }

            return new Rectangle(OCRGameTime.XPoint, OCRGameTime.YPoint, OCRGameTime.BlockWidth, OCRGameTime.BlockHeight);
        }

        private void ReadPresetsFromViewModel(bool preset1, bool preset2)
        {
            if (preset1)
            {
                bool unset = OCRGameTime.Preset1XPoint == 0 && OCRGameTime.Preset1YPoint == 0 && OCRGameTime.Preset1BlockWidth == 0 && OCRGameTime.Preset1BlockHeight == 0;
                this.buttonApplyPreset1.Enabled = !unset;
                this.labelPreset1.Text = unset ? "--" : $"({OCRGameTime.Preset1XPoint},{OCRGameTime.Preset1YPoint}) {OCRGameTime.Preset1BlockWidth}*{OCRGameTime.Preset1BlockHeight}";
            }

            if (preset2)
            {
                bool unset = OCRGameTime.Preset2XPoint == 0 && OCRGameTime.Preset2YPoint == 0 && OCRGameTime.Preset2BlockWidth == 0 && OCRGameTime.Preset2BlockHeight == 0;
                this.buttonApplyPreset2.Enabled = !unset;
                this.labelPreset2.Text = unset ? "--" : $"({OCRGameTime.Preset2XPoint},{OCRGameTime.Preset2YPoint}) {OCRGameTime.Preset2BlockWidth}*{OCRGameTime.Preset2BlockHeight}";
            }
        }


        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonSave.Enabled = false;

                ReadInputArgs(out int x, out int y, out int width, out int height);
                //MainOCR.XPercent = decimal.Round(x / (decimal)screenRect.Width, MainOCR.XYPercentDecimalSize);
                //MainOCR.YPercent = decimal.Round(y / (decimal)screenRect.Height, MainOCR.XYPercentDecimalSize);

                if (GlobalData.Default.IsUsingScreenTopTime)
                {
                    OCRGameTime.TopXPoint = x;
                    OCRGameTime.TopYPoint = y;
                    OCRGameTime.TopBlockWidth = width;
                    OCRGameTime.TopBlockHeight = height;
                }
                else
                {
                    OCRGameTime.XPoint = x;
                    OCRGameTime.YPoint = y;
                    OCRGameTime.BlockWidth = width;
                    OCRGameTime.BlockHeight = height;
                }

                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save"));
                this.Close();
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void TryUpdateImage(string source)
        {
            try
            {
                this.buttonSave.Enabled = true;
                //this.buttonSetAsPreset1.Enabled= true;
                //this.buttonSetAsPreset2.Enabled= true;

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

                    System.Diagnostics.Debug.WriteLine($"screen: {screenRect}, thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    System.Diagnostics.Debug.WriteLine($"image block: {x},{y}  {width},{height}");
                    OCRBase.SafeCheckImageBlock(ref x, ref y, ref width, ref height);
                    System.Diagnostics.Debug.WriteLine($"image block: {x},{y}  {width},{height} - after safe check");

                    //can not use using block here, since we pass the bitmap into a view and show it
                    var bitmapBlock = screenShot.Clone(new Rectangle(x, y, width, height), screenShot.PixelFormat);

                    if (this.pictureBoxOne.Image != null)
                    {
                        this.pictureBoxOne.Image.Dispose();
                    }

                    this.pictureBoxOne.Image = bitmapBlock;

                    //labelX.Text = $"X: {decimal.Round(x / (decimal)screenRect.Width, MainOCR.XYPercentDecimalSize)}";
                    //labelY.Text = $"Y: {decimal.Round(y / (decimal)screenRect.Height, MainOCR.XYPercentDecimalSize)}";

                    labelX.Text = $"X: {(int)((decimal)x / (decimal)screenRect.Width * 10000) * 0.01}%";
                    labelY.Text = $"Y: {(int)((decimal)y / (decimal)screenRect.Height * 10000) * 0.01}%";
                    labelMessage.Text = $"change by {source}";
                }
            }
            catch (Exception ex)
            {
               this.OnError(ex);
            }
        }



        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage(nameof(numericUpDownX_ValueChanged));
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage(nameof(numericUpDownY_ValueChanged));
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage(nameof(numericUpDownWidth_ValueChanged));
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            this.TryUpdateImage(nameof(numericUpDownHeight_ValueChanged));
        }

        private void FormImageView_Load(object sender, EventArgs e)
        {
            this.buttonSave.Enabled = false;
            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";

        }

        private void buttonApplyPreset1_Click(object sender, EventArgs e)
        {
            this.DisableButtonShortTime(this.buttonApplyPreset1);
            this.buttonSetAsPreset1.Enabled = false;
            this.numericUpDownX.Value = OCRGameTime.Preset1XPoint;
            this.numericUpDownY.Value = OCRGameTime.Preset1YPoint;
            this.numericUpDownWidth.Value = OCRGameTime.Preset1BlockWidth;
            this.numericUpDownHeight.Value = OCRGameTime.Preset1BlockHeight;
            this.TryUpdateImage(nameof(buttonApplyPreset1_Click));
        }

        private void buttonApplyPreset2_Click(object sender, EventArgs e)
        {
            this.DisableButtonShortTime(this.buttonApplyPreset2);
            this.buttonSetAsPreset2.Enabled = false;
            this.numericUpDownX.Value = OCRGameTime.Preset2XPoint;
            this.numericUpDownY.Value = OCRGameTime.Preset2YPoint;
            this.numericUpDownWidth.Value = OCRGameTime.Preset2BlockWidth;
            this.numericUpDownHeight.Value = OCRGameTime.Preset2BlockHeight;
            this.TryUpdateImage(nameof(buttonApplyPreset2_Click));
        }

        private void buttonSetAsPreset1_Click(object sender, EventArgs e)
        {
            try
            {
                this.labelMessage.Text = null;
                this.DisableLabelShortTime(this.labelMessage);
                this.DisableButtonShortTime(this.buttonSetAsPreset1);

                ReadInputArgs(out int x, out int y, out int width, out int height);
                OCRGameTime.Preset1XPoint = x;
                OCRGameTime.Preset1YPoint = y;
                OCRGameTime.Preset1BlockWidth = width;
                OCRGameTime.Preset1BlockHeight = height;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save as preset 1"));
                ReadPresetsFromViewModel(true, false);
                this.RunOnMain(() => this.labelMessage.Text = "set as P1 done", 500);
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void buttonSetAsPreset2_Click(object sender, EventArgs e)
        {
            try
            {
                this.labelMessage.Text = null;
                this.DisableLabelShortTime(this.labelMessage);
                this.DisableButtonShortTime(this.buttonSetAsPreset2);

                ReadInputArgs(out int x, out int y, out int width, out int height);
                OCRGameTime.Preset2XPoint = x;
                OCRGameTime.Preset2YPoint = y;
                OCRGameTime.Preset2BlockWidth = width;
                OCRGameTime.Preset2BlockHeight = height;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save as preset 2"));
                ReadPresetsFromViewModel(false, true);
                this.RunOnMain(() => this.labelMessage.Text = "set as P2 done", 500);
                
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }


        private void ReadInputArgs(out int x, out int y, out int width, out int height)
        {
            x = (int)this.numericUpDownX.Value;
            y = (int)this.numericUpDownY.Value;
            width = (int)this.numericUpDownWidth.Value;
            height = (int)this.numericUpDownHeight.Value;

            OCRBase.SafeCheckImageBlock(ref x, ref y, ref width, ref height);
        }

        private void checkBoxReadTopTime_CheckedChanged(object sender, EventArgs e)
        {
            _EnableScreenTopTime = this.checkBoxReadTopTime.Checked;
            checkBoxAutoLock.Enabled = _EnableScreenTopTime;
            buttonResetTopTimeLocation.Enabled = _EnableScreenTopTime;
            SetButtonSaveSettingState();
        }

        private void numericUpDownDelaySecond_ValueChanged(object sender, EventArgs e)
        {
            _ScanMiddleDelaySecond = (int)this.numericUpDownDelaySecond.Value;
            SetButtonSaveSettingState();
        }

        private void SetButtonSaveSettingState()
        {
            bool diffTopTimeState = GlobalData.Default.IsUsingScreenTopTime != _EnableScreenTopTime;
            bool diffAutoLock = GlobalData.Default.EnableScreenTopTimeAutoLock != _EnableAutoLock;
            bool diffDelay = GlobalData.Default.TimeViewScanMiddleDelaySecond != _ScanMiddleDelaySecond;

            this.buttonSaveSetting.Enabled = diffTopTimeState || diffAutoLock || diffDelay;
        }

        private void buttonSaveSetting_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonSaveSetting.Enabled = false;

                GlobalData.Default.IsUsingScreenTopTime = _EnableScreenTopTime;
                GlobalData.Default.EnableScreenTopTimeAutoLock = _EnableAutoLock;
                GlobalData.Default.TimeViewScanMiddleDelaySecond = _ScanMiddleDelaySecond;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save setting"));
                this.Close();
            }
            catch (Exception ex)
            {
                this.buttonSaveSetting.Enabled = true;
                this.OnError(ex);
            }
        }

        private void checkBoxAutoLock_CheckedChanged(object sender, EventArgs e)
        {
            _EnableAutoLock = checkBoxAutoLock.Checked;
            SetButtonSaveSettingState();
        }

        private void buttonResetTopTimeLocation_Click(object sender, EventArgs e)
        {
            this.DisableButtonShortTime(this.buttonResetTopTimeLocation);

            var defaultArgs = OCRGameTime.GetDefaultTimeBlock(true);         
            this.numericUpDownX.Value = defaultArgs.X;
            this.numericUpDownY.Value = defaultArgs.Y;
            this.numericUpDownWidth.Value = defaultArgs.Width;
            this.numericUpDownHeight.Value = defaultArgs.Height;

            TryUpdateImage(nameof(buttonResetTopTimeLocation_Click));
        }

        private void buttonResetMiddleTimeLocation_Click(object sender, EventArgs e)
        {
            this.DisableButtonShortTime(this.buttonResetMiddleTimeLocation);

            var defaultArgs = OCRGameTime.GetDefaultTimeBlock(false);
            this.numericUpDownX.Value = defaultArgs.X;
            this.numericUpDownY.Value = defaultArgs.Y;
            this.numericUpDownWidth.Value = defaultArgs.Width;
            this.numericUpDownHeight.Value = defaultArgs.Height;

            TryUpdateImage(nameof(buttonResetMiddleTimeLocation_Click));
        }

        private void checkBoxScanBothLocations_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("not support yet");
            this.checkBoxScanBothLocations.Enabled = false;
        }
    }
}
