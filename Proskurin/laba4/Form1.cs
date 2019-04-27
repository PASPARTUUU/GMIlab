using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int x1, x2, y1, y2;
        Bitmap image;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(panel1.Size.Width, panel1.Size.Height);
        }

        private Bitmap DrawLine(Bitmap img, int x1, int y1, int x2, int y2, Color color)
        {
            int x, y, s1, s2, dx, dy, e, z;

            bool change;

            x = x1; y = y1;
            dx = Math.Abs(x2 - x1);
            dy = Math.Abs(y2 - y1);

            s1 = Math.Sign(x2 - x1);
            s2 = Math.Sign(y2 - y1);

            if (dy > dx)
            {
                z = dx;
                dx = dy;
                dy = z;
                change = true;
            }
            else
            {
                change = false;
            }

            e = 2 * dy - dx;

            for (int i = 1; i <= dx; i++)
            {
                img.SetPixel(x, y, color);

                while (e >= 0)
                {
                    if (change)
                    {
                        x = x + s1;
                    }
                    else
                    {
                        y = y + s2;
                    }
                    e = e - 2 * dx;
                }

                if (change)
                {
                    y = y + s2;
                }
                else
                {
                    x = x + s1;

                }
                e = e + 2 * dy;

            }
            img.SetPixel(x, y, color);

            return img;
        }

    }
}
