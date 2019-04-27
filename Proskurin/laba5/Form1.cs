using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int xx, yy;
        int radius=10;
        Bitmap image;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            xx = e.X;
            yy = e.Y;
            label1.Text = "x: " + xx.ToString() + " ; y: " + yy.ToString();
            //image = new Bitmap(DrawLine(image, x1, y1, x2, y2, Color.Black));
            image = new Bitmap(BresenhamCircle(image, xx, yy, Color.Black));
            panel1.BackgroundImage = image;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(panel1.Size.Width, panel1.Size.Height);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            radius = Convert.ToInt32(textBox1.Text);
        }

        private Bitmap BresenhamCircle(Bitmap img, int x0, int y0, Color color)
        {
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;
            while (x >= y)
            {
                try
                {
                    img.SetPixel(x + x0, y + y0, color);
                    img.SetPixel(y + x0, x + y0, color);
                    img.SetPixel(-x + x0, y + y0, color);
                    img.SetPixel(-y + x0, x + y0, color);
                    img.SetPixel(-x + x0, -y + y0, color);
                    img.SetPixel(-y + x0, -x + y0, color);
                    img.SetPixel(x + x0, -y + y0, color);
                    img.SetPixel(y + x0, -x + y0, color);
                }
                catch { }
                y++;
                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x + 1);
                }
            }
            return img;
        }

    }
}
