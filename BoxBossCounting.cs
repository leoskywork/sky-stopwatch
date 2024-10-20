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

    [Obsolete]
    public partial class BoxBossCounting : Form, IPopupBox
    {
        private Action _BeforeClose;
        private BossCallSet  _BossCallGroups;
        private bool _AutoSlice;


        public DateTime CreateAt { get; } = DateTime.Now;
        public string Key { get; } = GlobalData.PopupKeyBossCount;
        public bool IsPaused => throw new NotImplementedException();

        public BoxBossCounting(BossCallSet groups, bool autoSlice, Action onClosing)
        {
            InitializeComponent();

            _BossCallGroups = groups;
            _BeforeClose = onClosing;
            _AutoSlice = autoSlice;

            this.TopMost = true;

            //do this at last
            this.timerClose.Interval = GlobalData.TimerIntervalShowingBossCallMS;
            this.timerClose.Start();
        }

        private void FormNodeBossCounting_Load(object sender, EventArgs e)
         {
            this.labelKill.Cursor = Cursors.Arrow;
            this.labelMessage.Text = "-";
            this.labelTotal.Text = "-";

            if (Environment.MachineName == "LEO-PC-PRO22")
            {
                for (int i = 0; i < 999; i++)
                {
                    _BossCallGroups.Last.Calls.Add(new BossCall() { PreCounting = true });
                }
            };

            if (GlobalData.Default.EnableBossCountingOneMode)
            {
                this.tableLayoutPanelRight.Hide();
                this.Size = new Size(this.Size.Width - 44, this.Size.Height - 10);
                this.labelMessage.Font = new Font("SimSun", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //this.labelMessage.Padding = new Padding(2,0,10,0);
                //this.labelMessage.BackColor = System.Drawing.Color.Gray;

                const int closeSize = 30;
                this.buttonKill.Text = null;
                this.buttonKill.Size = new System.Drawing.Size(closeSize, closeSize);
                this.buttonKill.Location = new System.Drawing.Point(this.Size.Width - closeSize, 0);
                this.buttonKill.FlatStyle = FlatStyle.Flat;
                this.buttonKill.FlatAppearance.BorderSize = 0;
                this.buttonKill.BackColor = System.Drawing.Color.MediumVioletRed;//PaleVioletRed;
                this.buttonKill.BackgroundImage = global::SkyStopwatch.Properties.Resources.power_off_512_w;
                this.buttonKill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.buttonKill.UseVisualStyleBackColor = true;
                this.buttonKill.Margin = new System.Windows.Forms.Padding(0);
                this.buttonReset.Size = new Size(50, 30);
                this.buttonReset.Location = new Point(this.Size.Width - 20, 10);

                this.RunOnMain(() => { this.labelMessage.Focus(); }, 1);
            }
            else
            {
                this.buttonKill.Hide();
                this.buttonReset.Hide();
                this.Size = new Size(this.Size.Width - 10, this.Size.Height);
                this.tableLayoutPanelRight.Size = new Size(this.tableLayoutPanelRight.Width + 12, this.tableLayoutPanelRight.Height);

                this.labelAddGroup.ForeColor = System.Drawing.Color.Black;
                this.labelAddGroup.BackColor = System.Drawing.Color.White;
                this.labelAddGroup.Padding = new Padding(0);
                this.labelAddGroup.Text = "+P";

                this.labelAddBossCall.ForeColor = System.Drawing.Color.Black;
                this.labelAddBossCall.BackColor = System.Drawing.Color.White;
                this.labelAddBossCall.Padding = new Padding(2, 0, 2, 0);
                this.labelAddBossCall.Text = "+";

                this.labelRemoveBossCall.ForeColor = System.Drawing.Color.Black;
                this.labelRemoveBossCall.BackColor = System.Drawing.Color.White;
                this.labelRemoveBossCall.Padding = new Padding(4, 0, 2, 0);
                this.labelRemoveBossCall.Text = "-";

                this.labelKill.ForeColor = System.Drawing.Color.Black;
                this.labelKill.BackColor = System.Drawing.Color.White;
                //this.labelKill.Margin = new Padding(0, -10, 0, 6); //not working
                this.labelKill.Text = "X";
            }
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            if (_BossCallGroups == null || _BossCallGroups.Count == 0)
            {
                this.labelMessage.Text = DateTime.Now.Second % 2 == 1 ? "." : "";
                this.labelTotal.Text = "-";
                return;
            }

            this.labelMessage.Text = _BossCallGroups.Last.Calls.Where(c => c.PreCounting).Count().ToString();
            this.labelTotal.Text = $"{this._BossCallGroups.Sum(g => g.Calls.Where(c => c.IsValid).Count())}-P{this._BossCallGroups.Count}";
        }

        private void FormNodeWarning_FormClosing(object sender, FormClosingEventArgs e)
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

        private void labelKill_Click(object sender, EventArgs e)
        {
            this.CloseInternal();
        }

        private void CloseInternal()
        {
            this.Close();

            if (GlobalData.Default.BootingArgs == (int)PopupBoxTheme.BossCallCounting)
            {
                GlobalData.Default.FireCloseApp();
            }
        }

        private void labelAddGroup_Click(object sender, EventArgs e)
        {
            if(_BossCallGroups == null) return;

            _BossCallGroups.Add(new BossCallGroup());


            this.DisableLabelShortTime(this.labelAddGroup);
        }
 

        private void buttonKill_Click(object sender, EventArgs e)
        {
            this.CloseInternal();
        }

        private void labelAddBossCall_Click(object sender, EventArgs e)
        {
            if (_BossCallGroups == null) return;
            if (_BossCallGroups.Count == 0) _BossCallGroups.Add(new BossCallGroup());

            _BossCallGroups.Last.Calls.Add(new BossCall()
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

            this.DisableLabelShortTime(this.labelAddBossCall);
          
        }



        private void labelRemoveBossCall_Click(object sender, EventArgs e)
        {
            if (_BossCallGroups == null || _BossCallGroups.Count == 0 || _BossCallGroups.Last.Calls.Count == 0) return;

            _BossCallGroups.Last.Calls.RemoveAt(_BossCallGroups.Last.Calls.Count - 1);

            this.DisableLabelShortTime(this.labelRemoveBossCall);
        }


 

    }
}
