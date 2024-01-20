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
    public partial class FormToolBox : Form
    {
        private Action _RunOCR;
        private Action _NewGameClick;
        private Action _TopMostClick;
        private Action _ClearClick;
        private Action<int> _AddSecondsClick;
        private Action<string> _ChangeTimeNodes;

        private string _OriginalTimeNodes;

        private FormToolBox()
        {
            InitializeComponent();

            //got error when call this.close(), cross threads issue, thus use ui control timer instead
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(60 * 1000);
            //    if (!this.Disposing || !this.IsDisposed)
            //    {
            //        this.BeginInvoke(new Action(() => { this.Close(); }));
            //    }
            //});
            this.timerAutoClose.Interval = 60 * 1000;
            this.timerAutoClose.Start();

            this.labelSize.Text = $"box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";
        }

        public FormToolBox(Bitmap image,
            string message,
            Action<Button, string> onInit = null,
            Action runOCR = null,
            Action onNewGame = null,
            Action topMost = null,
            Action clear = null,
            Action<int> addSeconds = null,
            Action<string> changeTimeNodes = null
            ) : this()
        {

            this.pictureBoxOne.Image = image;
            this.labelMessage.Text = message;

            _RunOCR = runOCR;
            _NewGameClick = onNewGame;
            _TopMostClick = topMost;
            _ClearClick = clear;
            _AddSecondsClick = addSeconds;
            _ChangeTimeNodes = changeTimeNodes;

            //do this at last
            this._OriginalTimeNodes = this.textBoxTimeSpanNodes.Text;
            string initialTimeNodes = GetBasicCheckedTimeNodes();
            onInit?.Invoke(this.buttonOCR, initialTimeNodes);
        }


        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            _NewGameClick?.Invoke();
            this.Close();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            _TopMostClick?.Invoke();
            this.Close();
        }

        private void ToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            _RunOCR = null;
            _NewGameClick = null;
            _TopMostClick = null;
            _ClearClick = null;
            _AddSecondsClick = null;
            this.pictureBoxOne.Image = null;
            _ChangeTimeNodes = null;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _ClearClick?.Invoke();
            this.Close();
        }

        private void buttonAddSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _AddSecondsClick?.Invoke(MainOCR.IncrementSeconds);
            buttonClear.Enabled = true;
        }

        private void buttonReduceSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _AddSecondsClick?.Invoke(MainOCR.DecrementSeconds * -1);
            buttonClear.Enabled = true;
        }


        private void buttonOCR_Click(object sender, EventArgs e)
        {
            _RunOCR?.Invoke();
            this.Close();
        }

        private void buttonImageView_Click(object sender, EventArgs e)
        {
            var imageView = new FormImageView();
            imageView.Show();
            this.Close();
        }




        private void checkBoxPopWarning_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPopWarning.Checked)
            {
                this.groupBoxTimeNode.Enabled = true;
            }
            else
            {
                this.groupBoxTimeNode.Enabled = false;
            }

            string nodes = GetBasicCheckedTimeNodes();
            _ChangeTimeNodes?.Invoke(nodes);
        }

        private string GetBasicCheckedTimeNodes()
        {
            if (this.checkBoxPopWarning.Checked)
            {

                return this.textBoxTimeSpanNodes.Text;
            }
            else
            {
                return string.Empty;
            }
        }

        private void textBoxTimeSpanNodes_TextChanged(object sender, EventArgs e)
        {
            bool hasActuralChanged = false;
            string newValue = this.textBoxTimeSpanNodes.Text;

            if (newValue != this._OriginalTimeNodes)
            {
                hasActuralChanged = true;

                //leotodo, ignore changes when just new line or white spaces
                //if(newValue != null && newValue.Replace("\r\n"))
            }


            if (hasActuralChanged)
            {
                this.buttonSaveTimeNode.Enabled = true;
                this.buttonResetTimeNode.Enabled = true;
            }
            else
            {
                this.buttonSaveTimeNode.Enabled = false;
                this.buttonResetTimeNode.Enabled = false;
            }
        }

        private void buttonSaveTimeNode_Click(object sender, EventArgs e)
        {
            this.buttonSaveTimeNode.Enabled = false;

            _ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
        }

        private void buttonResetTimeNode_Click(object sender, EventArgs e)
        {
            this.buttonResetTimeNode.Enabled = false;

            this.textBoxTimeSpanNodes.Text = this._OriginalTimeNodes;

        }
    }
}
