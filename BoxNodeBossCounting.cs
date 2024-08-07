﻿using SkyStopwatch.DataModel;
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
    public partial class BoxNodeBossCounting : Form
    {
        //private DateTime _CreatedTime = DateTime.Now;
        private Action _BeforeClose;
        private List<BossCallGroup> _BossCallGroups;
        private bool _AutoSlice;

        public BoxNodeBossCounting(List<BossCallGroup> groups, bool autoSlice, Action onClosing)
        {
            InitializeComponent();

            _BossCallGroups = groups;
            _BeforeClose = onClosing;
            _AutoSlice = autoSlice;

            this.TopMost = true;
            
            //do this at last
            this.timerClose.Interval = 100;
            this.timerClose.Start();
        }

        private void FormNodeBossCounting_Load(object sender, EventArgs e)
         {
            this.labelKill.Cursor = Cursors.Arrow;
            this.labelMessage.Text = "-";
            //this.labelGroupNumber.Text = "-";
            this.labelTotal.Text = "-";
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            if(_BossCallGroups == null ||  _BossCallGroups.Count == 0)
            {
                this.labelMessage.Text = DateTime.Now.Second % 2 == 1 ?  "." : "";
                //this.labelGroupNumber.Text = "-";
                this.labelTotal.Text = "-";
                return; 
            }

            this.labelMessage.Text = _BossCallGroups.Last().Calls.Count.ToString();


            //if (_AutoSlice)
            //{
            //    this.labelGroupNumber.Text = "n." + _BossCallGroups.Count.ToString();
            //}
            //else
            //{
            //    this.labelGroupNumber.Text = "n.1";
            //}

            this.labelTotal.Text = $"P{this._BossCallGroups.Count}-{this._BossCallGroups.Sum(g => g.Calls.Count)}";
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
            this.Close();
        }
    }
}
