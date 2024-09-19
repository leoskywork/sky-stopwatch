using SkyStopwatch.ViewModel;
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
    public partial class FormBootSetting : Form
    {
        private Action _RunOCR;
        private Action _NewGameClick;
        private Action _TopMostClick;
        private Action _ClearClick;
        private Action<int> _AddSecondsClick;
        private Action<string> _ChangeTimeNodes;
        private Action _LockClick;

        private string _OriginalTimeNodes;
        private BootSettingArgs _Args;

        private FormBootSetting()
        {
            InitializeComponent();
        }

        public FormBootSetting(BootSettingArgs args,
            Action<Button, string> onInit,
            Action runOCR,
            Action onNewGame,
            Action topMost,
            Action clear,
            Action<int> addSeconds,
            Action<string> changeTimeNodes,
            Action lockTime
            ) : this()
        {
            this._Args = args ?? throw new ArgumentNullException("args");
            this.pictureBoxOne.Image = args.Image;
            args.Image = null;
            this.labelMessage.Text = "<hover to show config>";
            this.buttonLockTime.Text = args.IsTimeLocked ? "Unlock" : "Lock";
            this.buttonLockTime.Enabled = args.EnableLockButton;

            _RunOCR = runOCR;
            _NewGameClick = onNewGame;
            _TopMostClick = topMost;
            _ClearClick = clear;
            _AddSecondsClick = addSeconds;
            _ChangeTimeNodes = changeTimeNodes;
            _LockClick = lockTime;

            //got error when call this.close(), cross threads issue, thus use ui control timer instead
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(60 * 1000);
            //    if (this.IsDead()) return;
            //    {
            //        this.BeginInvoke(new Action(() => { this.Close(); }));
            //    }
            //});
            this.timerAutoClose.Interval = 30 * 1000;
            this.timerAutoClose.Start();

            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";

           

            this.checkBoxPopWarning.Checked = GlobalData.EnableCheckTimeNode;
            this.textBoxTimeSpanNodes.Text = GlobalData.TimeNodeCheckingList;
            this.checkBoxDebugging.Checked = GlobalData.Default.IsDebugging;


            if (GlobalData.Default.IsDebugging)
            {

                //this.textBoxTimeSpanNodes.Text = "1:00";
                //this.textBoxTimeSpanNodes.Text = "01:00";
                //this.textBoxTimeSpanNodes.Text = "10:30\r\n20:30\r\n35:00";
                //this.textBoxTimeSpanNodes.Text = "1:00\r\n2:30\r\n10:00";
            }

            SetDialogTitle();

            this._Args.ThemeArgs = GlobalData.Default.BootingArgs;
            SetMainOCRBootingArgsAndButtonText();

            this.buttonTopMost.Visible = false;
            this.checkBoxTopMost.Checked = GlobalData.EnableTopMost;

            //do this at last
            this._OriginalTimeNodes = this.textBoxTimeSpanNodes.Text;
            onInit?.Invoke(this.buttonOCR, _OriginalTimeNodes);
        }


        public void SetLocation(Control parent)
        {
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(parent.Location.X - this.Width + parent.Width + 10, parent.Location.Y + parent.Size.Height + 30);
            }
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
            _AddSecondsClick?.Invoke(OCRBase.IncrementSeconds);
            buttonClear.Enabled = true;
        }

        private void buttonReduceSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _AddSecondsClick?.Invoke(OCRBase.DecrementSeconds * -1);
            buttonClear.Enabled = true;
        }


        private void buttonOCR_Click(object sender, EventArgs e)
        {
            _RunOCR?.Invoke();
            this.Close();
        }

        private void buttonImageView_Click(object sender, EventArgs e)
        {
            var imageView = new FormImageViewTime();
            imageView.Show();
            this.Close();
        }




        private void checkBoxPopWarning_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxTimeNode.Enabled = this.checkBoxPopWarning.Checked;
            GlobalData.EnableCheckTimeNode = this.checkBoxPopWarning.Checked;

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
            System.Diagnostics.Debug.WriteLine($"old: {GlobalData.TimeNodeCheckingList}");
            System.Diagnostics.Debug.WriteLine($"new: {this.textBoxTimeSpanNodes.Text}");

            GlobalData.TimeNodeCheckingList = this.textBoxTimeSpanNodes.Text;
            _ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
        }

        private void checkBoxDebugging_CheckedChanged(object sender, EventArgs e)
        {
            GlobalData.Default.IsDebugging = this.checkBoxDebugging.Checked;
            SetDialogTitle();
        }

        private void SetDialogTitle()
        {
            if (GlobalData.ExeCreateDate == DateTime.MinValue)
            {
                string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                GlobalData.ExeCreateDate = System.IO.File.GetCreationTime(assemblyPath);
                GlobalData.ExeUpdateDate = System.IO.File.GetLastWriteTime(assemblyPath);
            }

            string prefix = GlobalData.Default.IsDebugging ? $"debugging - OCR data {GlobalData.OCRTesseractDataFolder}" : $"Auto close in {this.timerAutoClose.Interval/1000}s";
            string exeTime = GlobalData.ExeUpdateDate.ToString("yyyy.MMdd.HHmm");
            string suffix = $"Time locked: {this._Args.IsTimeLocked}_{this._Args.LockSource}";
            this.Text = $"{prefix} - V{GlobalData.Version}.{GlobalData.Subversion} - {exeTime} - {suffix}";
        }


        private void FormToolBox_Load(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        private void buttonChangeTheme_Click(object sender, EventArgs e)
        {
            this.buttonChangeTheme.Enabled = false;
            this._Args.ThemeArgs++;
            SetMainOCRBootingArgsAndButtonText();

          

            GlobalData.Default.FireChangeTheme();

            Task.Delay(300).ContinueWith((_) =>
            {
                if (this.IsDead()) return;
                this.BeginInvoke((Action)(() =>
                {
                    this.buttonChangeTheme.Enabled = true;
                    //Task.Delay(500).ContinueWith((__) => { this.Close(); });
                    this.Close();
                }));
            });
        }

        private void SetMainOCRBootingArgsAndButtonText()
        {
            int themeCount = Enum.GetNames(typeof(PopupBoxTheme)).Length - 1;
            GlobalData.Default.BootingArgs = this._Args.ThemeArgs % themeCount;

            //lazy way to do it, error when theme count >= 10
            string prefix = ""; //this.buttonChangeTheme.Text.Substring(0, this.buttonChangeTheme.Text.Length - 2);
            this.buttonChangeTheme.Text = $"{prefix}{GlobalData.Default.BootingArgs} - {(PopupBoxTheme)GlobalData.Default.BootingArgs}";
        }

        private void buttonAddMinute_Click(object sender, EventArgs e)
        {
            this.buttonAddMinute.Enabled = false;
            _AddSecondsClick?.Invoke(OCRBase.IncrementMinutes * 60);
            this.buttonAddMinute.Enabled = true;
        }

        private void buttonReduceMinute_Click(object sender, EventArgs e)
        {
            this.buttonReduceMinute.Enabled = false;

            _AddSecondsClick?.Invoke(OCRBase.DecrementMinutes * -60);
            this.buttonReduceMinute.Enabled = true;
        }

        private void buttonCloseApp_Click(object sender, EventArgs e)
        {
            this.Close();
            GlobalData.Default.FireCloseApp();
        }

        private void buttonPriceList_Click(object sender, EventArgs e)
        {
            var view = new FormImageViewPrice();
            view.Show();
            this.Close();
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"----- checkBoxTopMost_CheckedChanged, checked: {this.checkBoxTopMost.Checked}");
            _TopMostClick?.Invoke();
        }

        private void labelMessage_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = new ToolTip();

            tip.AutoPopDelay = 5000;//提示信息的可见时间
            tip.InitialDelay = 50;// 500;//事件触发多久后出现提示
            tip.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            tip.ShowAlways = true;

            StringBuilder builder = new StringBuilder();
            builder.Append($"{nameof(GlobalData.EnableLogToFile)}: {GlobalData.EnableLogToFile}");
            builder.Append(Environment.NewLine);

            builder.Append($"{nameof(GlobalData.ProcessList)}: ");
            GlobalData.ProcessList.ForEach(p => builder.Append(p + ","));
            builder.Remove(builder.Length - 1, 1); //remove the last comma (,)

            tip.SetToolTip(this.labelMessage, builder.ToString());
        }

        private void buttonCount_Click(object sender, EventArgs e)
        {
            var imageView = new FormImageViewBossCounting(false);
            imageView.Show();
            this.Close();
        }

        private void buttonCountRun_Click(object sender, EventArgs e)
        {
            var imageView = new FormImageViewBossCounting(true);
            imageView.Show();
            this.Close();
        }

        private void buttonLockTime_Click(object sender, EventArgs e)
        {
            _LockClick();
            this.Close();
        }
    }
}
