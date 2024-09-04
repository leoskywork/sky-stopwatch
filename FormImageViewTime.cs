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
    public partial class FormImageViewTime : Form
    {

        public FormImageViewTime()
        {
            InitializeComponent();

            ReadPreviewArgsFromViewModel();
            ReadPresetsFromViewModel(true, true);
        }

        private void ReadPreviewArgsFromViewModel()
        {
            this.numericUpDownX.Value = OCRGameTime.XPoint;
            this.numericUpDownY.Value = OCRGameTime.YPoint;
            this.numericUpDownWidth.Value = OCRGameTime.BlockWidth;
            this.numericUpDownHeight.Value = OCRGameTime.BlockHeight;
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
                OCRGameTime.XPoint = x; 
                OCRGameTime.YPoint = y;
                OCRGameTime.BlockWidth = width;
                OCRGameTime.BlockHeight = height;
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
            this.numericUpDownX.Value = OCRGameTime.Preset1XPoint;
            this.numericUpDownY.Value = OCRGameTime.Preset1YPoint;
            this.numericUpDownWidth.Value = OCRGameTime.Preset1BlockWidth;
            this.numericUpDownHeight.Value = OCRGameTime.Preset1BlockHeight;
            this.TryUpdateImage(nameof(buttonApplyPreset1_Click));
        }

        private void buttonApplyPreset2_Click(object sender, EventArgs e)
        {
            this.DisableButtonShortTime(this.buttonApplyPreset2);
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
                this.DisableButtonShortTime(this.buttonSetAsPreset1);

                ReadInputArgs(out int x, out int y, out int width, out int height);
                OCRGameTime.Preset1XPoint = x;
                OCRGameTime.Preset1YPoint = y;
                OCRGameTime.Preset1BlockWidth = width;
                OCRGameTime.Preset1BlockHeight = height;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save as preset 1"));
                ReadPresetsFromViewModel(true, false);
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
                this.DisableButtonShortTime(this.buttonSetAsPreset2);

                ReadInputArgs(out int x, out int y, out int width, out int height);
                OCRGameTime.Preset2XPoint = x;
                OCRGameTime.Preset2YPoint = y;
                OCRGameTime.Preset2BlockWidth = width;
                OCRGameTime.Preset2BlockHeight = height;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save as preset 2"));
                ReadPresetsFromViewModel(false, true);
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

    }
}
