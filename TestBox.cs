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

        public TestBox(Bitmap image) : this()
        {

            this.pictureBoxOne.Image = image;
        }

        public TestBox(string imagePath) : this()
        {
            var image = new Bitmap(imagePath);
            this.pictureBoxOne.Image = image;
        }
    }
}
