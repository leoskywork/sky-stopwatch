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
    public partial class FormBoot : Form
    {
        private Form _LastTheme;

        public FormBoot()
        {
            InitializeComponent();
        }

        private void FormBoot_Load(object sender, EventArgs e)
        {
            try
            {
                //this.Hide(); //not working
                //this.Visible = false;

                Task.Run(() =>
                {
                    Thread.Sleep(300);

                    this.BeginInvoke(new Action(() =>
                    {
                        this.Hide();

                        OnChangeTheme();

                    }));
                });

                MainOCR.ChangeTheme += (_, __) =>
                {
                    this.OnChangeTheme();
                };

                MainOCR.CloseApp += (_, __) =>
                {
                    _LastTheme = null;
                    this.Close();
                };
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

  

        private void OnChangeTheme()
        {
            if (this._LastTheme != null && !_LastTheme.Disposing && !_LastTheme.IsDisposed)
            {
                _LastTheme.Close();
            }

            _LastTheme = new FormMain(MainOCR.BootingArgs);
            _LastTheme.Show();
        }

    }
}
