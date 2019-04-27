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
            image = DrawwLine(image, x1, y1, x2, y2, Color.Black);
            //image = new Bitmap(DrawwLine(image, x1, y1, x2,y2, Color.Black));
            panel1.BackgroundImage = image;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            image = new Bitmap(panel1.Size.Width, panel1.Size.Height);
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Bitmap image = new Bitmap(panel1.BackgroundImage);
            //Bitmap newImage = MyPaint(image, xo, yo, Color.Green, Color.Black);
            //panel1.BackgroundImage = newImage;
        }

        private Bitmap DrawwLine(Bitmap img, int x1, int y1, int x2, int y2, Color color)
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
                change= false;
            }

            e = 2 * dy - dx;

            for (int i = 1; i < dx; i++)
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


        private Bitmap BresenhamCircle(Bitmap img, int x0, int y0, int radius, Color color)
        {
            int x = radius;
            int y = 0;
            int radiusError = 1 - x;
            while (x >= y)
            {
                img.SetPixel(x + x0, y + y0, color);
                img.SetPixel(y + x0, x + y0, color);
                img.SetPixel(-x + x0, y + y0, color);
                img.SetPixel(-y + x0, x + y0, color);
                img.SetPixel(-x + x0, -y + y0, color);
                img.SetPixel(-y + x0, -x + y0, color);
                img.SetPixel(x + x0, -y + y0, color);
                img.SetPixel(y + x0, -x + y0, color);
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

        private Bitmap DrawLine(Bitmap bitmap, int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (;;)
            {
                bitmap.SetPixel(x0, y0, color);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
            return bitmap;
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
