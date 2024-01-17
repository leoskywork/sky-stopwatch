using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class Main : Form
    {
        private bool _TopMost = false;//false;

        public Main()
        {
            InitializeComponent();
            InitStopwatch();
            SyncTopMost();

        }

        private void InitStopwatch()
        {
            this.labelTimerPrefix.Hide();
            this.labelTimer.Text = "unset";

        }

        private void SyncTopMost()
        {
            this.TopMost = _TopMost;
            this.buttonTopMost.Text = this._TopMost ? "Pin" : "Unpin";
        }

        private void buttonOCR_Click(object sender, EventArgs e)
        {
            try
            {
                buttonOCR.Enabled = false;
                labelTimer.Text = "shot...";

                string screenShot = MainHelper.PrintScreenAsTempFile();
                labelTimer.Text = "ocr...";

                string data = MainHelper.ReadImageAsText(screenShot);

                MessageBox.Show(data);
                labelTimer.Text = "parse...";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
            finally
            {
                buttonOCR.Enabled = true;
            }
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            try
            {
                //leotodo improve this
                buttonTopMost.Enabled = false;
                _TopMost = !_TopMost;

                SyncTopMost();

                buttonTopMost.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }

    }
}
