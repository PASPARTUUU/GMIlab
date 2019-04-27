using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int x1, x2, y1, y2;
        Bitmap image;

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(panel1.Size.Width, panel1.Size.Height);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;  
            image = new Bitmap(DrawLine(image, x1, y1, x2, y2, Color.Black));
            panel1.BackgroundImage = image;
        }

        private Bitmap DrawLine(Bitmap img, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (;;)
            {
                img.SetPixel(x0, y0, color);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
            return img;
        }

    }
}
