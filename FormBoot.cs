using SkyStopwatch.View;
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
        private IMainBox _LastTheme;

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
                    if (this.IsDeadExt()) return;

                    this.RunOnMain(() =>
                    {
                        this.Hide();
                        this.OnChangeTheme();
                    });
                });

                GlobalData.Default.CloseApp += Default_CloseApp;
                GlobalData.Default.ChangeTheme += Default_ChangeTheme;
                GlobalData.Default.CloseMainBox += Default_CloseMainBox;
            }
            catch (Exception ex)
            {
                this.OnError(ex);
            }
        }

        private void Default_ChangeTheme(object sender, EventArgs e)
        {
            this.RunOnMain(this.OnChangeTheme);
        }

        private void OnChangeTheme()
        {
            if (this._LastTheme != null && !_LastTheme.IsDead())
            {
                _LastTheme.CloseSource = MainBoxCloseSource.ChangeTheme;
                _LastTheme.Close();

            }

            _LastTheme = null;

            var box = new BoxGameTime(GlobalData.Default.BootingArgs);
            box.SetDefaultLocation();
            box.Show();
            _LastTheme = box;
        }


        private void Default_CloseApp(object sender, EventArgs e)
        {
            _LastTheme = null;
            this.RunOnMain(this.Close);
        }

        private void Default_CloseMainBox(object sender, CloseMainBoxEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"close main box - {e.Kind}, {e.Source}");

            if (e.Source == MainBoxCloseSource.UserXOut)
            {
                _LastTheme = null;
                this.RunOnMain(this.Close);
            }
        }




    }
}
