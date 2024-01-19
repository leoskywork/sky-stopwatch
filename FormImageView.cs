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
        private bool _IsUsingImageView = false;

        public FormImageView()
        {
            InitializeComponent();


            this.timerAutoClose.Interval = 60 * 1000;
            this.timerAutoClose.Start();

            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
            this.labelSize.Text = $"box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            if (_IsUsingImageView) return;

            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;
                this.TrySetImage();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void TrySetImage()
        {
            try
            {
                _IsUsingImageView = true;

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

                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;
                _IsUsingImageView = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            this.TrySetImage();
        }

        private void numericUpDownY_ValueChanged(object sender, EventArgs e)
        {
            this.TrySetImage();
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            this.TrySetImage();
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            this.TrySetImage();
        }
    }
}
