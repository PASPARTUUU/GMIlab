using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPoligon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        Bitmap img;

        int x1, x2, y1, y2;

        static int width = 3;


        Pen pen = new Pen(Color.Black, width);

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            img.SetPixel(e.X, e.Y, Color.Red);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            img = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);

        }




        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            

        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = img;
        }


    }
}
