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
    public partial class BoxPhaseBossWarning : Form, ITopForm
    {
        private DateTime _CreatedTime = DateTime.Now;
        private Action _BeforeClose;
        private bool _IsTestingDualPopups;

        public bool IsPermanent => false;

        public BoxPhaseBossWarning(Action beforeClose)
        {
            InitializeComponent();
            this.InitBase();

            _BeforeClose = beforeClose;

            this.TopMost = GlobalData.Default.EnableTopMost;
            
            //do this at last
            this.timerClose.Interval = 1000;
            this.timerClose.Start();
            GlobalData.Default.ChangeGameStartTime += MainOCR_ChangeGameStartTime;
        }

        private void MainOCR_ChangeGameStartTime(object sender, ChangeGameStartTimeEventArgs e)
        {
            if (_IsTestingDualPopups == false)
            {
                this.Close();
            }
        }

        private void timerClose_Tick(object sender, EventArgs e)
        {
            //_IsTestingDualPopups = true;
            var elapsedTime = DateTime.Now - _CreatedTime;
            var threshold = _IsTestingDualPopups ? 10000 : GlobalData.TimeNodeWarningDurationSeconds;

            if (elapsedTime.TotalSeconds < threshold)
            {
                this.labelMessage.ForeColor = DateTime.Now.Second % 2 == 0 ? Color.DarkGreen : Color.White;
            }
            else
            {
                this.Close();
            }
        }

        private void FormNodeWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            _BeforeClose?.Invoke();
            _BeforeClose = null;
            GlobalData.Default.ChangeGameStartTime -= MainOCR_ChangeGameStartTime;
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

        public void ShowAside(Control parent)
        {
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(parent.Location.X, parent.Location.Y + parent.Size.Height + GlobalData.MessageBoxVerticalGap);
            this.Show();
        }
    }
}
