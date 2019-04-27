using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap img = new Bitmap(250, 250);

        int x1, x2, y1, y2;

        static int width = 3;

        Pen pen = new Pen(Color.Black, width);

        void Drawww()
        {
            using (var graphics = Graphics.FromImage(img))
            {
                graphics.DrawLine(pen, x1, y1, x2, y2);
            }
            pictureBox1.Image = img;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x1 = e.X;
            y1 = e.Y;
            pen.Width = width;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;

            Drawww();
            pen.Width = width;
            label1.Text = "X1 - " + x1.ToString() + ", Y1 - " + y1.ToString();
            label2.Text = "X2 - " + x2.ToString() + ", Y2 - " + y2.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            width = Convert.ToInt32(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            img = new Bitmap(250, 250);
            pictureBox1.Image = img;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
            }
        }
    }
}
