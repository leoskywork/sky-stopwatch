﻿using SkyStopwatch.DataModel;
using SkyStopwatch.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class BoxBossCountingSuccinct : Form, IPopupBox, ITopForm
    {
        private Action _BeforeClose;
        private BossCallSet _BossCallGroups;
        private bool _AutoSlice;
        private bool _IsShowingActionBar;
        private DateTime? _BoxLoadTime;
        private bool _IsPaused;

        public DateTime CreateAt => DateTime.Now;
        public string Key => GlobalData.PopupKeyBossCountSuccinct;

        public bool IsPaused => _IsPaused;

        public bool IsPermanent => true;

        public BoxBossCountingSuccinct(BossCallSet groups, bool autoSlice, Action onClosing)
        {
            InitializeComponent();
            this.InitBase();

            _BossCallGroups = groups;
            _BeforeClose = onClosing;
            _AutoSlice = autoSlice;

            this.TopMost = true;
           


            //do this at last
            this.timerRefresh.Interval = GlobalData.TimerIntervalShowingBossCallMS;
            this.timerRefresh.Start();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.labelMessage.Text = "-";

            if (Environment.MachineName == "LEO-PC-PRO")
            {
                for (int i = 0; i < 999; i++)
                {
                    //_BossCallGroups.Last().Calls.Add(new BossCall() { PreCounting = true, IsValid = true });
                }
            };

            this.tableLayoutPanelRight.Location = new Point(100, 2);
            this.tableLayoutPanelRight.Hide();

            const int closeSize = 30;
            this.Size = new Size(320, closeSize);

            this.buttonKill.Text = null;
            this.buttonKill.Size = new System.Drawing.Size(closeSize, closeSize);
            this.buttonKill.Location = new System.Drawing.Point(this.labelMessage.Width - 10, 0);
            this.buttonKill.FlatStyle = FlatStyle.Flat;
            this.buttonKill.FlatAppearance.BorderSize = 0;
            this.buttonKill.BackColor = System.Drawing.Color.MediumVioletRed;//PaleVioletRed;
            this.buttonKill.BackgroundImage = global::SkyStopwatch.Properties.Resources.power_off_512_w;
            this.buttonKill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonKill.UseVisualStyleBackColor = true;
            this.buttonKill.Margin = new System.Windows.Forms.Padding(0);

            this.RunOnMain(() => { this.labelMessage.Focus(); }, 1);
            _BoxLoadTime = DateTime.Now;
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            if(_IsPaused)
            {
                return;
            }

            if(_BossCallGroups == null ||  _BossCallGroups.Count == 0)
            {
                this.labelMessage.Text = DateTime.Now.Second % 2 == 1 ?  "." : "";
                return; 
            }

            if (_BoxLoadTime != null && _BoxLoadTime.Value.AddSeconds(1) > DateTime.Now)
            {
                this.labelMessage.Text = "BC#";
            }
            else
            {
                _BoxLoadTime = null; //just for fast compare
                //this.labelMessage.Text = _BossCallGroups.Last().Calls.Where(c => c.IsValid).Count().ToString();
                this.labelMessage.Text = _BossCallGroups.LastPreCount().ToString();
            }
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _BeforeClose?.Invoke();
            _BeforeClose = null;
            _BossCallGroups = null;


        }




        //窗体移动
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        //窗体移动
        private void labelMessage_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);

            System.Diagnostics.Debug.WriteLine("fire labelMessage_MouseDown");
        }

        private void labelGroupNumber_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void labelTotal_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void tableLayoutPanelRight_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }


        //圆角
        private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();
            //   左上角   
            path.AddArc(arcRect, 180, 90);
            //   右上角   
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            //   右下角   
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            //   左下角   
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(System.EventArgs e)
        {
            const int roundRadius = 8;

            //SetWindowRegion
            //this.Left-10, this.Top-10, this.Width-10, this.Height-10);
            //Rectangle rect = new Rectangle(0, 22, this.Width, this.Height - 22); 
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            System.Drawing.Drawing2D.GraphicsPath formPath = GetRoundedRectPath(rect, roundRadius);
            this.Region = new Region(formPath);
        }

       

        private void buttonKill_Click(object sender, EventArgs e)
        {
            this.Close();

            if (GlobalData.Default.BootingArgs == (int)PopupBoxTheme.BossCallCounting)
            {
                GlobalData.Default.FireCloseApp();
            }
        }
     

        private void buttonReset_Click(object sender, EventArgs e)
        {
            _BossCallGroups.Reset();
            this.DisableButtonShortTime(this.buttonReset);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_BossCallGroups.Count == 0) _BossCallGroups.Add(new BossCallGroup());

            _BossCallGroups.Last.Add(new BossCall()
            {
                Id = -1,
                IsValid = true,
                FirstMatchTime = DateTime.Now,
                FirstMatchValue = -1,
                SecondMatchTime = DateTime.Now,
                SecondMatchValue = -1,
                OCRLastMatch = -1,
                PreCounting = true
            });

            this.DisableButtonShortTime(this.buttonAdd);
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (_BossCallGroups == null || _BossCallGroups.Count == 0 || _BossCallGroups.Last.Calls.Count == 0) return;

            _BossCallGroups.Last.Calls.RemoveAt(_BossCallGroups.Last.Calls.Count - 1);

            this.DisableButtonShortTime(this.buttonRemove);
        }


        private void labelMessage_MouseHover(object sender, EventArgs e)
        {
            if (_IsShowingActionBar) { return; }

            _IsShowingActionBar = true;

            this.tableLayoutPanelRight.Show();


            this.RunOnMainAsync(() =>
            {
                this.tableLayoutPanelRight.Hide();
                _IsShowingActionBar = false;
            }, 10 * 1000);

        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            this._IsPaused = !this._IsPaused;

            if (this._IsPaused)
            {
                this.timerRefresh.Enabled = false;
                this.buttonPause.Text = "-><-";
                this.labelMessage.Text = ">";
            }
            else
            {
                this.timerRefresh.Enabled = true;
                this.buttonPause.Text = "-=-";
                this.labelMessage.Text = _BossCallGroups.LastPreCount().ToString();
            }


            this.DisableButtonShortTime(this.buttonPause);
        }

        public void SetUserFriendlyTitle()
        {
            this.SetVersion();
        }
    }
}
