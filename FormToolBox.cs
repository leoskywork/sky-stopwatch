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
        private int _BootingArgs = 0; //0 = default theme

        private FormToolBox()
        {
            InitializeComponent();
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

            //got error when call this.close(), cross threads issue, thus use ui control timer instead
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(60 * 1000);
            //    if (this.IsDead()) return;
            //    {
            //        this.BeginInvoke(new Action(() => { this.Close(); }));
            //    }
            //});
            this.timerAutoClose.Interval = 10 * 1000;
            this.timerAutoClose.Start();

            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";

           

            this.checkBoxDebugging.Checked = MainOCR.IsDebugging;
            this.checkBoxPopWarning.Checked = MainOCR.EnableTimeNodeChecking;
            this.textBoxTimeSpanNodes.Text = MainOCR.TimeNodeCheckingList;


            if (MainOCR.IsDebugging)
            {
                this.Text = "debugging";

                //this.textBoxTimeSpanNodes.Text = "1:00";
                //this.textBoxTimeSpanNodes.Text = "01:00";
                //this.textBoxTimeSpanNodes.Text = "10:30\r\n20:30\r\n35:00";
                //this.textBoxTimeSpanNodes.Text = "1:00\r\n2:30\r\n10:00";
            }

            this._BootingArgs = MainOCR.BootingArgs;
            this.buttonChangeTheme.Text = $"Change theme {_BootingArgs}";

            //do this at last
            this._OriginalTimeNodes = this.textBoxTimeSpanNodes.Text;
            onInit?.Invoke(this.buttonOCR, _OriginalTimeNodes);
        }

  

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            _NewGameClick?.Invoke();
            this.Close();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            if (this.checkBoxDebugging.Checked) return;

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
            this.groupBoxTimeNode.Enabled = this.checkBoxPopWarning.Checked;

            MainOCR.EnableTimeNodeChecking = this.checkBoxPopWarning.Checked;
            _ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
        }

        private void textBoxTimeSpanNodes_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void UpdateSaveButtonState()
        {
            bool hasActuralChanged = false;
            string newValue = this.textBoxTimeSpanNodes.Text;

            if (this._OriginalTimeNodes != newValue)
            {
                hasActuralChanged = true;

                //ignore changes when just new line or white spaces
                if (_OriginalTimeNodes != null && newValue != null)
                {
                    if (_OriginalTimeNodes.Replace(Environment.NewLine, string.Empty) == newValue.Replace(Environment.NewLine, string.Empty))
                    {
                        hasActuralChanged = false;
                    }
                }
            }


            this.buttonSaveTimeNode.Enabled = hasActuralChanged;
        }

        private void buttonSaveTimeNode_Click(object sender, EventArgs e)
        {
            this.buttonSaveTimeNode.Enabled = false;

            System.Diagnostics.Debug.WriteLine($"----- buttonSaveTimeNode_Click");
            System.Diagnostics.Debug.WriteLine($"old: {MainOCR.TimeNodeCheckingList}");
            System.Diagnostics.Debug.WriteLine($"new: {this.textBoxTimeSpanNodes.Text}");

            MainOCR.TimeNodeCheckingList = this.textBoxTimeSpanNodes.Text;
            _ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
        }

        private void checkBoxDebugging_CheckedChanged(object sender, EventArgs e)
        {
            MainOCR.IsDebugging = this.checkBoxDebugging.Checked;
            this.Text = MainOCR.IsDebugging ? "debugging" : "dialog will auto close";
        }



        private void FormToolBox_Load(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void buttonChangeTheme_Click(object sender, EventArgs e)
        {
            this.buttonChangeTheme.Enabled = false;
            this._BootingArgs++;
            MainOCR.BootingArgs = this._BootingArgs % 4;
            //lazy way to do it, error when theme count >= 10
            string prefix = this.buttonChangeTheme.Text.Substring(0, this.buttonChangeTheme.Text.Length - 2);
            this.buttonChangeTheme.Text = $"{prefix} {MainOCR.BootingArgs}";

            MainOCR.FireChangeTheme();

            Task.Delay(300).ContinueWith((_) =>
            {
                if (this.IsDead()) return;
                this.BeginInvoke((Action)(() =>
                {
                    this.buttonChangeTheme.Enabled = true;
                }));
            });
        }

        private void buttonAddMinute_Click(object sender, EventArgs e)
        {
            this.buttonAddMinute.Enabled = false;
            _AddSecondsClick?.Invoke(MainOCR.IncrementMinutes * 60);
            this.buttonAddMinute.Enabled = true;
        }

        private void buttonReduceMinute_Click(object sender, EventArgs e)
        {
            this.buttonReduceMinute.Enabled = false;

            _AddSecondsClick?.Invoke(MainOCR.DecrementMinutes * -60);
            this.buttonReduceMinute.Enabled = true;
        }

        private void buttonCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
            MainOCR.FireCloseApp();
        }
    }
}
