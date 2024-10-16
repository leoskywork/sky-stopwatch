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
        private string _OriginalTimeNodes;
        private BootSettingArgs _Args;
        private BootSettingActonList _Hooks;
        private bool _IsLoaded;
        private const int DefaultLiveSeconds = 60;
        private DateTime _ShowAt;

        public int RemainingSeconds
        {
            get { return DefaultLiveSeconds - (int)(DateTime.Now - _ShowAt).TotalSeconds; }
        }

        private FormBootSetting()
        {
            InitializeComponent();
        }

        public FormBootSetting(BootSettingArgs args, BootSettingActonList hooks) : this()
        {
            this._Hooks = hooks;
            this._Args = args ?? throw new ArgumentNullException("args");
            this.pictureBoxOne.Image = args.Image;
            args.Image = null;
            this.labelMessage.Text = "<hover to show config>";
            this.labelSize.Text = $"out box: {this.pictureBoxOne.Size.Width} x {this.pictureBoxOne.Size.Height}";
            this.buttonUnlockTime.Enabled = args.EnableUnlockButton;
            this.buttonForceLock.Enabled = args.EnableForceLockButton;
            this.checkBoxDarkMode.Checked = args.EnableDarkMode;


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
            //_DefalutValueOfCheckBoxTopMost = this.checkBoxTopMost.Checked;
            //_FirstAssignValueOfTopMost = GlobalData.Default.EnableTopMost;
            this.checkBoxTopMost.Checked = GlobalData.Default.EnableTopMost;

            //do this at last
            this._OriginalTimeNodes = this.textBoxTimeSpanNodes.Text;
            hooks.OnInit?.Invoke(this.buttonOCR, _OriginalTimeNodes);
        }


        public void SetLocation(Control parent)
        {
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                //align right
                //this.Location = new Point(parent.Location.X - this.Width + parent.Width + 10, parent.Location.Y + parent.Size.Height + 30);

                //align left
                //this.Location = new Point(parent.Location.X - 8, parent.Location.Y + parent.Size.Height + 30);

                //align center
                this.Location = new Point(parent.Location.X - this.Size.Width / 2 + parent.Size.Width / 2, parent.Location.Y + parent.Size.Height + 30);
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
        }

        public void ShowAside(Control parent)
        {
            this.SetLocation(parent);
            _ShowAt = DateTime.Now;
            this.Show();
            this.Focus();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            _Hooks.OnNewGame?.Invoke();
            this.Close();
        }

        private void timerAutoClose_Tick(object sender, EventArgs e)
        {
            SetDialogTitle();

            if (this.checkBoxDebugging.Checked) return;

            if (this.RemainingSeconds < 0)
            {
                this.Close();
            }
        }

        private void buttonTopMost_Click(object sender, EventArgs e)
        {
            _Hooks.TopMost?.Invoke();
            this.Close();
        }

        private void FormToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Hooks = null;
            this.pictureBoxOne.Image = null;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _Hooks.Clear?.Invoke();
            this.Close();
        }

        private void buttonAddFewSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _Hooks.AddSeconds?.Invoke(OCRBase.Increment2Seconds);
            buttonClear.Enabled = true;
        }

        private void buttonAddSeconds_Click(object sender, EventArgs e)
        {
            buttonClear.Enabled = false;
            _Hooks.AddSeconds?.Invoke(OCRBase.Increment10Seconds);
            buttonClear.Enabled = true;
        }


        private void buttonOCR_Click(object sender, EventArgs e)
        {
            _Hooks.RunOCR?.Invoke();
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
            GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(nameof(FormBootSetting), true, "enable boss warning"));

            _Hooks.ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
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
            GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(nameof(FormBootSetting), true, "save time nodes"));
            _Hooks.ChangeTimeNodes?.Invoke(this.textBoxTimeSpanNodes.Text);
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

            int remaining = _ShowAt == DateTime.MinValue ? DefaultLiveSeconds : this.RemainingSeconds;
            string prefix = GlobalData.Default.IsDebugging ? $"debugging - OCR data {GlobalData.OCRTesseractDataFolder}" : $"Auto close in {remaining}s";
            string exeTime = GlobalData.ExeUpdateDate.ToString("yyyy.MMdd.HHmm");
            string suffix = $"Time locked: {this._Args.IsTimeLocked}_{this._Args.LockSource}";
            this.Text = $"{prefix} - V{GlobalData.Version}.{GlobalData.Subversion} - {exeTime} - {suffix}";
        }


        private void FormToolBox_Load(object sender, EventArgs e)
        {
            _IsLoaded = true;
            UpdateSaveButtonState();
        }

        private void buttonChangeTheme_Click(object sender, EventArgs e)
        {
            this.buttonChangeTheme.Enabled = false;
            this._Args.ThemeArgs++;

            SetMainOCRBootingArgsAndButtonText();

            GlobalData.Default.FireChangeTheme();
            GlobalData.Default.FireChangeAppConfig(new ChangeAppConfigEventArgs(nameof(FormBootSetting), true, "change theme"));

            Task.Delay(300).ContinueWith((_) =>
            {
                if (this.IsDeadExt()) return;
                this.BeginInvoke((Action)(() =>
                {
                    //this.buttonChangeTheme.Enabled = true;
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
            _Hooks.AddSeconds?.Invoke(OCRBase.IncrementMinute * 60);
            this.buttonAddMinute.Enabled = true;
        }

        private void buttonReduceMinute_Click(object sender, EventArgs e)
        {
            this.buttonReduceMinute.Enabled = false;

            _Hooks.AddSeconds?.Invoke(OCRBase.DecrementMinute * -60);
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
            //this will be triggered when the default value(true) is different with the assigned value(false, in this case) in ctor
            //in the case we want to skip the first trigger, for the other case(they 2 have same value), should not skip the first trigger
            //if (_IsFirstAssign && _DefalutValueOfCheckBoxTopMost != _FirstAssignValueOfTopMost)
            //{
            //    System.Diagnostics.Debug.WriteLine($"--> top most, checked: {this.checkBoxTopMost.Checked}, first assign");
            //    _IsFirstAssign = false;
            //    return;
            //}

            System.Diagnostics.Debug.WriteLine($"--> top most, checked: {this.checkBoxTopMost.Checked}, loaded： {_IsLoaded}");

            if (_IsLoaded)
            {
                _Hooks.TopMost?.Invoke();
                this.Close();
            }
        }

        private void labelMessage_MouseHover(object sender, EventArgs e)
        {
            ToolTip tip = CreateDefaultToolTip();

            StringBuilder builder = new StringBuilder();
            builder.Append($"{nameof(GlobalData.EnableLogToFile)}: {GlobalData.EnableLogToFile}");
            builder.Append(Environment.NewLine);
            builder.Append($"{nameof(GlobalData.ProcessCheckingList)}: ");
            GlobalData.ProcessCheckingList.ForEach(p => builder.Append(p + ","));
            builder.Remove(builder.Length - 1, 1); //remove the last comma (,)

            tip.SetToolTip(this.labelMessage, builder.ToString());
        }

        private ToolTip CreateDefaultToolTip()
        {
            ToolTip tip = new ToolTip();

            tip.AutoPopDelay = 5000;//提示信息的可见时间
            tip.InitialDelay = 50;// 500;//事件触发多久后出现提示
            tip.ReshowDelay = 500;//指针从一个控件移向另一个控件时，经过多久才会显示下一个提示框
            tip.ShowAlways = true;

            return tip;
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
            _Hooks.LockTime?.Invoke(false);
            this.Close();
        }

        private void buttonReduceSeconds_Click(object sender, EventArgs e)
        {
            this.DisableButtonWithTime(this.buttonReduceSeconds, 10);
            _Hooks.AddSeconds?.Invoke(OCRBase.Decrement10Seconds * -1);
        }

        private void buttonForceLock_Click(object sender, EventArgs e)
        {
            _Hooks.LockTime?.Invoke(true);
            this.Close();
        }

        private void buttonForceLock_MouseHover(object sender, EventArgs e)
        {
            var toolTip = CreateDefaultToolTip();
            toolTip.SetToolTip(this.buttonForceLock, $"Force manual lock, this overrides the auto lock by timer");
        }

        private void buttonLockTime_MouseHover(object sender, EventArgs e)
        {
            var toolTip = CreateDefaultToolTip();
            toolTip.SetToolTip(this.buttonUnlockTime, "Unlock time and resume OCR reading");// $"L = Lock, U = Unlock game time");
        }

        private void checkBoxDarkMode_CheckedChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"----------------- dark mode: {checkBoxDarkMode.Checked}, loaded: {_IsLoaded}");


            //[done] by adding loaded check - leotodo, skip first set when assign value diff with default value
            //this.Close();
            if (_IsLoaded)
            {
                _Hooks.SwitchDarkMode?.Invoke(checkBoxDarkMode.Checked);
                this.Close();
            }
        }
    }
}
