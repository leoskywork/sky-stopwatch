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
    public partial class FormImageViewPrice : Form
    {

        //leotodo, pentontial multi-thread issue
        private Queue<byte[]> _PriceImageQueue = new Queue<byte[]>();
        private int _ScanCount;
        private int _CompareCount;
        private bool _FoundTargetPrice;
        private Tesseract.TesseractEngine _AutoOCREngine;
        private bool _IsComparing;

        public FormImageViewPrice()
        {
            InitializeComponent();


            this.numericUpDownX.Value = OCRPrice.XPoint;
            this.numericUpDownY.Value = OCRPrice.YPoint;
            this.numericUpDownWidth.Value = OCRPrice.BlockWidth;
            this.numericUpDownHeight.Value = OCRPrice.BlockHeight;

            this.timerScan.Interval = 300;
            this.timerCompare.Interval = 100;

            this.numericUpDownTargetPrice.Value = OCRPrice.TargetPrice;
            this.numericUpDownAux1.Value = OCRPrice.Aux1Price;
            this.numericUpDownAux2.Value = OCRPrice.Aux2Price;
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

                OCRPrice.XPoint = x;
                OCRPrice.YPoint = y;
                OCRPrice.BlockWidth = width;
                OCRPrice.BlockHeight = height;

                GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(this.ToString(), true, "btn save"));
            }
            catch (Exception ex)
            {
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

                    OCRBase.SafeCheckImageBlock(ref x, ref y, ref width, ref height);

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

        private void FormImageViewPrice_Load(object sender, EventArgs e)
        {
            this.buttonSave.Enabled = false;
            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;

                this._PriceImageQueue.Clear();
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

                this.pictureBoxOne.Image = null;
                this.textBoxPriceList.Text = null;
                this.labelMessage.Text = "start";
                this.labelPriceMessage.Text = null;

            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                this.buttonStart.Enabled = true;
                this.buttonStop.Enabled = false;

                if(this.timerScan.Enabled)
                {
                    this.timerScan.Stop();
                }

                if (this.timerCompare.Enabled)
                {
                    this.timerCompare.Stop();
                }

                this.labelMessage.Text = "stop";
                this.labelPriceMessage.Text = null;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            try
            {
                this._ScanCount++;
                var priceData = this.GetModels().Price.GetImageBytes();
                this._PriceImageQueue.Enqueue(priceData);
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void timerCompare_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_PriceImageQueue.Count == 0) return;
                if (_IsComparing) return;

                _IsComparing = true;
                this._CompareCount++;
                bool enableAux1 = this.checkBoxAux1.Checked;
                bool enableAux2 = this.checkBoxAux2.Checked;

                Task.Factory.StartNew(() =>
                {
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto");

                    if (_AutoOCREngine == null)
                    {
                        _AutoOCREngine = this.GetModels().Price.CreateOCREngine();
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR created");
                    }

                    byte[] priceData = _PriceImageQueue.Dequeue();
                    string data = OCRBase.ReadImageFromMemory(_AutoOCREngine, priceData);
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - OCR done");

                    var found = this.GetModels().Price.Find(data, enableAux1, enableAux2);
                    //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - parser txt done");

                    if (GlobalData.Default.IsDebugging)
                    {
                        System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - debugging");
                        System.Diagnostics.Debug.WriteLine($"OCR compare: {found.Item1}");
                        System.Diagnostics.Debug.WriteLine($"OCR data: {data}");
                        string tmpPath = OCRBase.SaveTmpFile(Guid.NewGuid().ToString(), priceData);
                        System.Diagnostics.Debug.WriteLine($"tmp path: {tmpPath}");
                        //System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} saving screen shot - auto - debugging end");
                    }

                    return Tuple.Create(found.Item1, data, priceData, found.Item2);

                }).ContinueWith(t =>
                {
                    if (this.IsDeadExt()) return;

                    if (t.IsFaulted)
                    {
                        this.OnError(t.Exception);
                        return;
                    }

                    this.RunOnMain(() =>
                    {
                        _FoundTargetPrice = t.Result.Item1;
                        this.labelMessage.Text = $"scan: {_ScanCount}, compare: {_CompareCount}";


                        if (_FoundTargetPrice)
                        {
                            string aux1Message = enableAux1 ? "|" + OCRPrice.Aux1Price : string.Empty;
                            string aux2Message = enableAux2 ? "|" + OCRPrice.Aux2Price : string.Empty;
                            this.labelPriceMessage.Text = $"FOUND: {OCRPrice.TargetPrice}{aux1Message}{aux2Message}, no.{t.Result.Item4 + 1}";
                            this.labelPriceMessage.BackColor = Color.Red;
                            this.labelPriceMessage.ForeColor = Color.White;

                            // the following code not show correctly
                            // this.textBoxPriceList.Text = t.Result.Item2;
                            string[] lines = t.Result.Item2.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                            this.textBoxPriceList.Text = lines.Aggregate(string.Empty, (a, b) => a + Environment.NewLine + b);
                            
                            this.pictureBoxOne.Image?.Dispose();
                            this.pictureBoxOne.Image = OCRBase.BytesToBitmap(t.Result.Item3);
                        }
                        else
                        {
                            string aux1Message = enableAux1 ? "," + OCRPrice.Aux1Price : string.Empty;
                            string aux2Message = enableAux2 ? "," + OCRPrice.Aux2Price : string.Empty;
                            this.labelPriceMessage.Text = $"checking {OCRPrice.TargetPrice}{aux1Message}{aux2Message}...";
                            this.labelPriceMessage.BackColor = Color.Transparent;
                            this.labelPriceMessage.ForeColor = Color.Black;

                            this.textBoxPriceList.Text = null;

                            if (this.pictureBoxOne.Image != null)
                            {
                                this.pictureBoxOne.Image.Dispose();
                                this.pictureBoxOne.Image = null;
                            }
                        }

                        _IsComparing = false;

                    });
                });
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void numericUpDownTargetPrice_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int oldValue = OCRPrice.TargetPrice;
                OCRPrice.TargetPrice = (int)this.numericUpDownTargetPrice.Value;
                System.Diagnostics.Debug.WriteLine($"{DateTime.Now.ToString("h:mm:ss.fff")} change target price: {oldValue} -> {OCRPrice.TargetPrice}");
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void checkBoxAux1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
              this.numericUpDownAux1.Enabled = this.checkBoxAux1.Checked;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void checkBoxAux2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.numericUpDownAux2.Enabled = this.checkBoxAux2.Checked;
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
                OCRPrice.Aux1Price = (int) this.numericUpDownAux1.Value;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void numericUpDownAux2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                OCRPrice.Aux2Price = (int) this.numericUpDownAux2.Value;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }
    }
}
