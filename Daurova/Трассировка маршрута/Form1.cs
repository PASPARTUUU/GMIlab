using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace трассировка_маршрута
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        bool flag = false;

        int setButonState = 0;

        int n;
        int m;

        Button[,] buton;



        private void Form1_Load(object sender, EventArgs e)
        {
            n = Convert.ToInt32(textBox1.Text);
            m = Convert.ToInt32(textBox2.Text);

            GenerateTable(n, m);
        }

        void GenerateTable(int n, int m)
        {
            buton = new Button[n, m];

            int BtnWidth = 30;
            int BtnHeight = 30;
            int IndentLeft = 0;
            int IndentTop = 0;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {

                    buton[i, j] = new Button();
                    buton[i, j].Parent = panel1;
                    buton[i, j].Left = (BtnWidth * j) + (IndentLeft * j);
                    buton[i, j].Top = (BtnHeight * i) + (IndentTop * i);
                    buton[i, j].Width = BtnWidth;
                    buton[i, j].Height = BtnHeight;

                    buton[i, j].BackColor = Color.MediumTurquoise;
                    buton[i, j].FlatStyle = FlatStyle.Flat;

                    buton[i, j].MouseClick += SetFlag;
                    buton[i, j].MouseMove += Zakraska;


                }
        }

        void DisposeTable(int n, int m)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    buton[i, j].Dispose();
                }
        }

        void SetFlag(object sender, EventArgs e)
        {
            if (flag) { flag = false; }
            else { flag = true; }

            if (setButonState == 2)
            {
                FoundSingleButonColor(Color.MediumSlateBlue);
                (sender as Button).BackColor = Color.MediumSlateBlue;
            }

            if (setButonState == 3)
            {
                FoundSingleButonColor(Color.DarkViolet);
                (sender as Button).BackColor = Color.DarkViolet;
            }

        }

        void Zakraska(object sender, EventArgs e)
        {

            if (setButonState == 0 && flag)
            {
                (sender as Button).BackColor = Color.MediumTurquoise;
            }
            if (setButonState == 1 && flag)
            {
                (sender as Button).BackColor = Color.IndianRed;
            }
        }

        void FoundSingleButonColor(Color c)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (buton[i, j].BackColor == c)
                    {
                        buton[i, j].BackColor = Color.MediumTurquoise;
                        break;
                    }
                }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DisposeTable(n, m);

                n = Convert.ToInt32(textBox1.Text);
                m = Convert.ToInt32(textBox2.Text);

                GenerateTable(n, m);
            }
            catch
            { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DisposeTable(n, m);

                n = Convert.ToInt32(textBox1.Text);
                m = Convert.ToInt32(textBox2.Text);

                GenerateTable(n, m);
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flag = false;
            setButonState = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = false;
            setButonState = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setButonState = 2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setButonState = 3;
        }


        //========================================
        // Расчетная логика




        int[,] table;
        int[,] tableWay;

        private void button5_Click(object sender, EventArgs e)
        {
            // очистка поля от значений
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    buton[i, j].Text = "";
                    if (buton[i, j].BackColor == Color.LimeGreen)
                    { buton[i, j].BackColor = Color.MediumTurquoise; }
                }

            //
            table = new int[n + 2, m + 2];
            tableWay = new int[n, m];
            for (int i = 0; i < n + 2; i++)
                for (int j = 0; j < m + 2; j++)
                { table[i, j] = 1; }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    tableWay[i, j] = 0;

                    if (buton[i, j].BackColor == Color.MediumTurquoise || buton[i, j].BackColor == Color.LimeGreen)
                    { table[i + 1, j + 1] = 0; }
                    if (buton[i, j].BackColor == Color.IndianRed)
                    { table[i + 1, j + 1] = 1; }
                    if (buton[i, j].BackColor == Color.MediumSlateBlue)
                    {
                        table[i + 1, j + 1] = 2;
                        tableWay[i, j] = 1;
                    }
                    if (buton[i, j].BackColor == Color.DarkViolet)
                    {
                        table[i + 1, j + 1] = 3;
                    }
                }

            Calculate();
        }



        void Calculate()
        {
            int wayWeight = 1;

            // если 0 то не все поле пройденно
            // если 1 то найденна конечная точка
            // если 2 то конечная точка не достижима
            int stop = 0;

            while (stop == 0)
            //for (int k = 0; k < 5; k++)
            {
                bool vhodVUslovie = false;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (tableWay[i, j] == wayWeight)
                        {
                            // проход по свободным точкам
                            if (table[i + 1 + 1, j + 1] != 1 && tableWay[i + 1, j] == 0)
                            { tableWay[i + 1, j] = wayWeight + 1; }
                            if (table[i + 1, j + 1 + 1] != 1 && tableWay[i, j + 1] == 0)
                            { tableWay[i, j + 1] = wayWeight + 1; }
                            if (table[i + 1 - 1, j + 1] != 1 && tableWay[i - 1, j] == 0)
                            { tableWay[i - 1, j] = wayWeight + 1; }
                            if (table[i + 1, j + 1 - 1] != 1 && tableWay[i, j - 1] == 0)
                            { tableWay[i, j - 1] = wayWeight + 1; }

                            // регисрация конечной точки
                            if (table[i + 1 + 1, j + 1] == 3)
                            { stop = 1; }
                            if (table[i + 1, j + 1 + 1] == 3)
                            { stop = 1; }
                            if (table[i + 1 - 1, j + 1] == 3)
                            { stop = 1; }
                            if (table[i + 1, j + 1 - 1] == 3)
                            { stop = 1; }

                            vhodVUslovie = true;
                        }
                    }
                }
                if (!vhodVUslovie)
                {
                    stop = 2;
                }
                wayWeight++;
            }


            OutTableWayOnButons(tableWay);

            if (stop == 1)
            {
                int setX = 1;
                int setY = 1;
                int setWeight = 0;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        if (table[i + 1, j + 1] == 3)
                        {
                            setX = i;
                            setY = j;
                            setWeight = tableWay[i, j];
                        }
                    }

                for (int k = setWeight; k > 1 + 1; k--)
                {
                    if (table[setX + 1 + 1, setY + 1] != 1 && tableWay[setX + 1, setY] == k - 1)
                    { buton[setX + 1, setY].BackColor = Color.LimeGreen; setX = setX + 1; }
                    if (table[setX + 1, setY + 1 + 1] != 1 && tableWay[setX, setY + 1] == k - 1)
                    { buton[setX, setY + 1].BackColor = Color.LimeGreen; setY = setY + 1; }
                    if (table[setX + 1 - 1, setY + 1] != 1 && tableWay[setX - 1, setY] == k - 1)
                    { buton[setX - 1, setY].BackColor = Color.LimeGreen; setX = setX - 1; }
                    if (table[setX + 1, setY + 1 - 1] != 1 && tableWay[setX, setY - 1] == k - 1)
                    { buton[setX, setY - 1].BackColor = Color.LimeGreen; setY = setY - 1; }
                }
            }

        }

        void OutTableWayOnButons(int[,] tbl)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    if (tbl[i, j] != 0)
                    {
                        buton[i, j].Text = tbl[i, j].ToString();
                    }
                }
        }
    }
}