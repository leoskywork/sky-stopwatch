using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkyStopwatch
{
    public partial class TestBox : Form
    {
        public TestBox()
        {
            InitializeComponent();
        }

        public TestBox(Bitmap image, string message) : this()
        {

            this.pictureBoxOne.Image = image;
            this.labelMessage.Text = message;
        }

        public TestBox(string imagePath, string message) : this()
        {
            var image = new Bitmap(imagePath);
            this.pictureBoxOne.Image = image;
            this.labelMessage.Text = message;
        }
    }
}
