using SkyStopwatch.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class BoxMessage : Form, ITopForm
    {
        private DateTime _CreatedTime = DateTime.Now;
        private Action _AfterConfirm;

        public bool IsPermanent => false;

        public BoxMessage(string message, bool showConfirmButton, Action confirmAction)
        {
            InitializeComponent();
            this.InitBase();

            this.TopMost = true;
            this.labelMessage.Text = message;

            if (showConfirmButton)
            {
                this._AfterConfirm = confirmAction;
                this.buttonClose.Visible = true;
                this.buttonConfirm.Visible = true;
            }
            else
            {
                this.buttonClose.Visible = false;
                this.buttonConfirm.Visible = false;
            }
            
            //do this at last
            this.timerClose.Interval = 1000;
            this.timerClose.Start();
        }

        private void MainOCR_ChangeGameStartTime(object sender, ChangeGameStartTimeEventArgs e)
        {
            this.Close();
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            var elapsedTime = DateTime.Now - _CreatedTime;

            if (elapsedTime.TotalSeconds < GlobalData.MessageBoxDisplaySeconds)
            {
                //this.labelMessage.ForeColor = DateTime.Now.Second % 2 == 0 ? Color.DarkGreen : Color.White;
            }
            else
            {
                this.Close();
            }
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

        public void SetUserFriendlyTitle()
        {
            throw new NotImplementedException();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            this._AfterConfirm?.Invoke();
            this.Close();
        }

        public void ShowAside(Control parent, bool avoidOverlay)
        {
            if (parent != null)
            {
                this.StartPosition = FormStartPosition.Manual;
                //msg tailing parent
                //this.Location = new Point(parent.Location.X + parent.Width + 10, parent.Location.Y - 2);

                //msg below parnet
                const int magicOverlayOffset = 60; //should greater than boss warnning height
                int y = parent.Location.Y + parent.Height + GlobalData.MessageBoxVerticalGap;
                this.Location = new Point(parent.Location.X - 2, avoidOverlay ? y + magicOverlayOffset : y);
            }

            this.Show();
        }

        public static void Show(string message, Control parent, bool avoidOverlay)
        {
            var box = new BoxMessage(message, false, null);
            box.ShowAside(parent, avoidOverlay);
        }
    }
}
