using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2
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
            image = new Bitmap(DraweLine(image, x1, y1, x2, y2, Color.Black));
            panel1.BackgroundImage = image;
        }

        private Bitmap DraweLine(Bitmap img, float x1, float y1, float x2, float y2, Color color)
        {
            // Целочисленные значения координат начала и конца отрезка,
            // округленные до ближайшего целого
            int iX1 = (int)Math.Round(x1);
            int iY1 = (int)Math.Round(y1);
            int iX2 = (int)Math.Round(x2);
            int iY2 = (int)Math.Round(y2);

            // Длина и высота линии
            int deltaX = Math.Abs(iX1 - iX2);
            int deltaY = Math.Abs(iY1 - iY2);

            // Считаем минимальное количество итераций, необходимое
            // для отрисовки отрезка. Выбирая максимум из длины и высоты
            // линии, обеспечиваем связность линии
            int length = Math.Max(deltaX, deltaY);

            // особый случай, на экране закрашивается ровно один пиксел
            if (length == 0)
            {
                img.SetPixel(iX1, iY1, color);
                return img;
            }

            // Вычисляем приращения на каждом шаге по осям абсцисс и ординат
            double dX = (x2 - x1) / length;
            double dY = (y2 - y1) / length;

            // Начальные значения
            double x = x1;
            double y = y1;

            // Основной цикл
            length++;
            while (length != 0)
            {
                length--;
                x += dX;
                y += dY;
                img.SetPixel((int)Math.Round(x), (int)Math.Round(y), color);
            }

            return img;
        }
    }
}
