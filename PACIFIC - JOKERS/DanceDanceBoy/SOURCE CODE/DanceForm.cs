using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DanceDanceBoy
{
    public partial class DanceForm : Form
    {
        public DanceForm()
        {
            InitializeComponent();
        }

        private void DanceForm_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();

            Rectangle screenBounds = Screen.PrimaryScreen.WorkingArea;

            int maxX = screenBounds.Width - this.Width;
            int maxY = screenBounds.Height - this.Height;

            int newX = r.Next(0, maxX);
            int newY = r.Next(0, maxY);

            this.DesktopLocation = new Point(newX, newY);
        }

    }
}
