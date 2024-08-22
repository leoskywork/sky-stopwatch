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
    public partial class FormImageView : Form
    {

        public FormImageView()
        {
            InitializeComponent();


            var screen = Screen.PrimaryScreen.Bounds;
            this.numericUpDownX.Value = OCRGameTime.XPoint;
            this.numericUpDownY.Value = OCRGameTime.YPoint;
            this.numericUpDownWidth.Value = OCRGameTime.BlockWidth;
            this.numericUpDownHeight.Value = OCRGameTime.BlockHeight;
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
                
                OCRBase.SafeCheckImageBlock(ref x, ref y, ref width, ref height);

                //MainOCR.XPercent = decimal.Round(x / (decimal)screenRect.Width, MainOCR.XYPercentDecimalSize);
                //MainOCR.YPercent = decimal.Round(y / (decimal)screenRect.Height, MainOCR.XYPercentDecimalSize);
                OCRGameTime.XPoint = x; 
                OCRGameTime.YPoint = y;
                OCRGameTime.BlockWidth = width;
                OCRGameTime.BlockHeight = height;
                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true));
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
    }
}
