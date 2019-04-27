using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba6
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

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(panel1.BackgroundImage);
            Bitmap newImage = PaintZone(image, xo, yo, Color.Green, Color.Black);
            panel1.BackgroundImage = newImage;
        }

        private Bitmap PaintZone(Bitmap sourceImage, int x, int y, Color color, Color borderColor)
        {
            Bitmap image = (Bitmap)sourceImage.Clone();
            Stack<Point> points = new Stack<Point>();
            points.Push(new Point(x, y));

            Point currentPoint;
            while (points.Count != 0)
            {
                currentPoint = points.Pop();
                image.SetPixel(currentPoint.X, currentPoint.Y, color);

                Color topPixel = image.GetPixel(currentPoint.X, currentPoint.Y + 1);
                if (topPixel.ToArgb() != borderColor.ToArgb() && topPixel.ToArgb() != color.ToArgb())
                {
                    points.Push(new Point(currentPoint.X, currentPoint.Y + 1));
                }

                Color rightPixel = image.GetPixel(currentPoint.X + 1, currentPoint.Y);
                if (rightPixel.ToArgb() != borderColor.ToArgb() && rightPixel.ToArgb() != color.ToArgb())
                {
                    points.Push(new Point(currentPoint.X + 1, currentPoint.Y));
                }

                Color bottomPixel = image.GetPixel(currentPoint.X, currentPoint.Y - 1);
                if (bottomPixel.ToArgb() != borderColor.ToArgb() && bottomPixel.ToArgb() != color.ToArgb())
                {
                    points.Push(new Point(currentPoint.X, currentPoint.Y - 1));
                }

                Color leftPixel = image.GetPixel(currentPoint.X - 1, currentPoint.Y);
                if (leftPixel.ToArgb() != borderColor.ToArgb() && leftPixel.ToArgb() != color.ToArgb())
                {
                    points.Push(new Point(currentPoint.X - 1, currentPoint.Y));
                }
            }

            return image;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
