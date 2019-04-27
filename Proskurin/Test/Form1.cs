using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int xo;
        int yo;

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            xo = e.X;
            yo = e.Y;
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Bitmap image = new Bitmap(panel1.BackgroundImage);
            Bitmap newImage = MyPaint(image, xo, yo, Color.Green, Color.Black);
            panel1.BackgroundImage = newImage;
        }

        private Bitmap MyPaint(Bitmap sourceImage, int xx, int yy, Color fill, Color borderColor)
        {
            Bitmap image = new Bitmap(panel1.BackgroundImage);
            Stack<Point> points = new Stack<Point>();
            points.Push(new Point(xx, yy));



            Point currentPoint;
            while (points.Count != 0)
            {
                currentPoint = points.Pop();

                int x = currentPoint.X;
                int y = currentPoint.Y;
                int xt = currentPoint.X;
                int fl = 0;

                image.SetPixel(x, y, fill);
                

                // заполняем интервал слева от затравки
                x = x - 1;
                while (image.GetPixel(x, y).ToArgb() != borderColor.ToArgb())
                {
                    image.SetPixel(x, y, fill);
                    x = x - 1;
                }

                // сохраняем крайний слева пиксел
                int xl = x + 1;
                x = xt;

                // заполняем интервал справа от затравки
                x = x + 1;
                while (image.GetPixel(x, y).ToArgb() != borderColor.ToArgb())
                {
                    image.SetPixel(x, y, fill);
                    x = x + 1;
                }

                // сохраняем крайний справа пиксел
                int xr = x - 1;
                y = y + 1;
                x = xl;

                // ищем затравку на строке выше
                while (x <= xr)
                {
                    fl = 0;
                    while ((image.GetPixel(x, y).ToArgb() != borderColor.ToArgb()) && (image.GetPixel(x, y).ToArgb() != fill.ToArgb()) && (x <= xr))
                    {
                        if (fl == 0)
                        {
                            fl = 1;
                        }
                        x = x + 1;
                    }

                    if (fl == 1)
                    {
                        if (x == xr && image.GetPixel(x, y).ToArgb() != fill.ToArgb() && image.GetPixel(x, y).ToArgb() != borderColor.ToArgb())
                        {
                            points.Push(new Point(x, y));
                        }
                        else
                        {
                            points.Push(new Point(x - 1, y));
                        }
                        fl = 0;
                    }


                    xt = x;
                    while ((image.GetPixel(x, y).ToArgb() == borderColor.ToArgb() || image.GetPixel(x, y).ToArgb() == fill.ToArgb()) && x < xr)
                    {
                        x = x + 1;
                    }

                    if (x == xt)
                    {
                        x = x + 1;
                    }
                }

                y = y - 2;
                x = xl;

                while (x <= xr)
                {
                    fl = 0;
                    while (image.GetPixel(x, y).ToArgb() != borderColor.ToArgb() && image.GetPixel(x, y).ToArgb() != fill.ToArgb() && x <= xr)
                    {
                        if (fl == 0)
                        {
                            fl = 1;
                        }
                        x = x + 1;
                    }

                    if (fl == 1)
                    {
                        if (x == xr && image.GetPixel(x, y).ToArgb() != fill.ToArgb() && image.GetPixel(x, y).ToArgb() != borderColor.ToArgb())
                        {
                            points.Push(new Point(x, y));
                        }
                        else
                        {
                            points.Push(new Point(x - 1, y));
                        }
                        fl = 0;
                    }

                    xt = x;
                    while ((image.GetPixel(x, y).ToArgb() == borderColor.ToArgb() || image.GetPixel(x, y).ToArgb() == fill.ToArgb()) && x < xr)
                    {
                        x = x + 1;
                    }
                    if (x == xt)
                    {
                        x = x + 1;
                    }
                }
            }

            return image;
        }

    }
}
