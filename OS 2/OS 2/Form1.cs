using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OS_2
{
    public partial class Form1 : Form
    {
        List<double> holes = new List<double>();
        List<double> delocated = new List<double>();
        List<double> startaddresses = new List<double>();
        List<double> processes = new List<double>();
        List<int> holeofprocesses = new List<int>();
        List<double> processstart = new List<double>();
        int holesnumber=0;
        int count;
        double totalsize;
        int yaxis;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            holes.Clear();
            startaddresses.Clear();
            panel1.Refresh();
            SolidBrush sb = new SolidBrush(Color.MediumTurquoise);
            SolidBrush sb2 = new SolidBrush(Color.DarkGray);
            SolidBrush sb3 = new SolidBrush(Color.WhiteSmoke);
            Pen pen = new Pen(Color.DarkGray, 2);
            Graphics g = panel1.CreateGraphics();
            Graphics b = panel1.CreateGraphics();
            Graphics c = panel1.CreateGraphics();
            FontFamily ff = new FontFamily("Century Gothic");
            System.Drawing.Font font = new System.Drawing.Font(ff, 10, FontStyle.Bold);
            count = 0;
            if (textBox4.Text == "ex: 1000")
            {
                MessageBox.Show("Please enter memory size");
                return;
            }
            if (textBox1.Text == "ex: 100 50 70")
            {
                MessageBox.Show("Please enter holes sizes");
                return;
            }
            if (textBox3.Text == "ex: 0 250 500")
            {
                MessageBox.Show("Please enter start addresses");
                return;
            }
              string fromholes = textBox1.Text;
                string[] holesarray = fromholes.Split(' ');
                //double[] holessizes = new double[holesarray.Length];
                string fromstart = textBox3.Text;
                string[] startarray = fromstart.Split(' ');
                //double[] startaddresses = new double[holesarray.Length];

                string memorysize = textBox4.Text;
                totalsize = double.Parse(memorysize);
            
            if ((holesarray.Length) != (startarray.Length))
            {
                MessageBox.Show("Please enter holes sizes equal to start addresses");
                return;
            }
            holesnumber = holesarray.Length;
            double sumOfHoles = 0;
            int height;
            
            try
            {
                for (int i = 0; i < holesarray.Length; i++)
                {
                    holes.Add(double.Parse(holesarray[i]));
                    startaddresses.Add(double.Parse(startarray[i]));
                    sumOfHoles += holes[i];
                }
            }
            catch
            {
                MessageBox.Show("Error");
                return;
            }
            if (sumOfHoles > totalsize)
            {
                MessageBox.Show("Please make sure total sum of holes sizes are smaller than memory size");
                return;
            }
            b.FillRectangle(sb2, 60, 1, 100, 440);
            b.DrawRectangle(pen, 60, 1, 100, 440);
            for (int i = 0; i < holesarray.Length; i++)
            {
                height = (int)((holes[i] * 440) / totalsize);
                yaxis = (int)((startaddresses[i] * 440) / totalsize);
                if (yaxis == 0)
                    yaxis = 1;
                b.DrawRectangle(pen, 60, yaxis, 100, height);
                b.FillRectangle(sb3, 61, (yaxis+1), 98, height);
                //b.FillRectangle(sb, 61, (yaxis+1), 98, (int)(40*410/totalsize)); //bshof bs hyb2a lshkl 3aml ezai xD
                //c.DrawString("20 KB", font, sb3, new PointF(90, (float)yaxis)); //bshof bs hyb2a lshkl 3aml ezai xD
                c.DrawString(holesarray[i]+ " KB", font, sb2, new PointF(160, (float)yaxis));
                c.DrawString(startarray[i] , font, sb2, new PointF(30, (float)yaxis));
                //yaxis += height;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SolidBrush sb = new SolidBrush(Color.MediumTurquoise);
            SolidBrush sb2 = new SolidBrush(Color.DarkGray);
            SolidBrush sb3 = new SolidBrush(Color.WhiteSmoke);
            Pen pen = new Pen(Color.DarkGray, 1);
            Graphics g = panel1.CreateGraphics();
            Graphics b = panel1.CreateGraphics();
            Graphics c = panel1.CreateGraphics();
            FontFamily ff = new FontFamily("Century Gothic");
            System.Drawing.Font font = new System.Drawing.Font(ff, 9, FontStyle.Bold);
            
            if(holesnumber==0)
                MessageBox.Show("Please set holes first");
            string processsize = textBox2.Text;
            double Processsize = double.Parse(processsize);
            processes.Add(Processsize);
            bool flag = true;
            count++;
            if (comboBox1.Text == "First Fit")
            {
                for (int i = 0; i < holes.Count; i++)
                {
                    if (holes[i] >= Processsize)
                    {
                        yaxis = (int)((startaddresses[i] * 440) / totalsize);
                        processstart.Add(startaddresses[i]);
                        holeofprocesses.Add(i);
                        if (yaxis == 0)
                            yaxis = 1;
                        b.DrawRectangle(pen, 60, yaxis, 100, (int)((Processsize * 440) / totalsize));
                        b.FillRectangle(sb, 61, (yaxis + 1), 98, (int)((Processsize * 440) / totalsize));
                        c.DrawString("P" + count + " " + processsize + " KB", font, sb3, new PointF(82, (float)yaxis));
                        c.DrawString(startaddresses[i].ToString(), font, sb, new PointF(30, (float)yaxis));
                        holes[i] -= Processsize;
                        startaddresses[i] += Processsize;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    MessageBox.Show("There is no empty space please de-allocate some processes");
                flag = true;
            }
            else if (comboBox1.Text == "Best Fit")
            {
                int perfect = 0;
                for (int i = 0; i < holes.Count; i++)
                {
                    if (holes[i] >= Processsize)
                    {
                        perfect = i;
                        flag = false;
                        break;
                    }
                }
                for (int i = 0; i < holes.Count; i++)
                {
                    if ((holes[i] < holes[perfect]) && (Processsize <= holes[i]))
                    {
                        perfect = i;
                        flag = false;
                    }
                }
                if (flag)
                    MessageBox.Show("There is no empty space please de-allocate some processes");
                else
                {
                    yaxis = (int)((startaddresses[perfect] * 440) / totalsize);
                    processstart.Add(startaddresses[perfect]);
                    holeofprocesses.Add(perfect);
                    if (yaxis == 0)
                        yaxis = 1;
                    b.DrawRectangle(pen, 60, yaxis, 100, (int)((Processsize * 440) / totalsize));
                    b.FillRectangle(sb, 61, (yaxis + 1), 98, (int)((Processsize * 440) / totalsize));
                    c.DrawString("P" + count + " " + processsize + " KB", font, sb3, new PointF(82, (float)yaxis));
                    c.DrawString(startaddresses[perfect].ToString(), font, sb, new PointF(30, (float)yaxis));
                    holes[perfect] -= Processsize;
                    startaddresses[perfect] += Processsize;
                }
                flag = true;
            }
            else if (comboBox1.Text == "Worst Fit")
            {
                int worst = 0;
                for (int i = 0; i < holes.Count; i++)
                {
                    if ((holes[i] >= holes[worst]) && (Processsize <= holes[i]))
                    {
                        worst = i;
                        flag = false;
                    }
                }
                if (flag)
                    MessageBox.Show("There is no empty space please de-allocate some processes");
                else
                {
                    yaxis = (int)((startaddresses[worst] * 440) / totalsize);
                    processstart.Add(startaddresses[worst]);
                    holeofprocesses.Add(worst);
                    if (yaxis == 0)
                        yaxis = 1;
                    b.DrawRectangle(pen, 60, yaxis, 100, (int)((Processsize * 440) / totalsize));
                    b.FillRectangle(sb, 61, (yaxis + 1), 98, (int)((Processsize * 440) / totalsize));
                    c.DrawString("P" + count + " " + processsize + " KB", font, sb3, new PointF(82, (float)yaxis));
                    c.DrawString(startaddresses[worst].ToString(), font, sb, new PointF(30, (float)yaxis));
                    holes[worst] -= Processsize;
                    startaddresses[worst] += Processsize;
                }
                flag = true;
            }
            else
            {
                MessageBox.Show("Please choose an allocation method");
                count--;
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SolidBrush sb = new SolidBrush(Color.MediumTurquoise);
            SolidBrush sb2 = new SolidBrush(Color.DarkGray);
            SolidBrush sb3 = new SolidBrush(Color.WhiteSmoke);
            Pen pen = new Pen(Color.DarkGray, 1);
            Graphics g = panel1.CreateGraphics();
            Graphics b = panel1.CreateGraphics();
            string processno = textBox5.Text;
            int Processnumber = int.Parse(processno);
            int height;
            for (int i = 0; i < delocated.Count; i++)
            {
                if (Processnumber == delocated[i])
                {
                    MessageBox.Show("This process is no longer avaliable ");
                    return;
                }
            }
            if (Processnumber > count)
            {
                MessageBox.Show("Please enter an available process");
                return;
            }
            else
            {
                bool flg = true;
                delocated.Add(Processnumber);
                for (int i = 0; i < holes.Count; i++)
                {
                    if (((processstart[Processnumber - 1] + processes[Processnumber - 1]) == startaddresses[i])&&(holes[i]!=0))
                    {
                        holes[i] += processes[Processnumber - 1];
                        startaddresses[i] -= processes[Processnumber - 1];
                        flg = false;
                        break;
                    }
                    else if (((startaddresses[i] + holes[i]) == processstart[Processnumber - 1])&&(holes[i]!=0))
                    {
                        holes[i] += processes[Processnumber - 1];
                        flg = false;
                        break;
                    }
                }
                if (flg)
                {
                    holes.Add(processes[Processnumber - 1]);
                    startaddresses.Add(processstart[Processnumber - 1]);
                    holesnumber++;
                }
                for (int i = 0; i < holes.Count; i++)
                {
                    for (int j = i+1; j < holes.Count; j++)
                    {
                        if (((holes[i] + startaddresses[i]) == startaddresses[j])&&(holes[i]!=0))
                        {
                            holes[i] += holes[j];
                            for (int k = j + 1; k < holes.Count; k++)
                            {
                                
                                holes[j] = holes[k];
                                startaddresses[j] = startaddresses[k];
                            }
                            if (j == (holes.Count - 1))
                                holes[j] = 0;
                            holesnumber--;
                        }
                    }
                }
                height = (int)((processes[Processnumber - 1] * 440) / totalsize);
                yaxis = (int)((processstart[Processnumber - 1] * 440) / totalsize);
                //g.DrawRectangle(pen, 60, yaxis, 100, height);
                g.FillRectangle(sb3, 61, (yaxis + 1), 98, height+1);
                //holesnumber++;
            }
            double temp;
            for (int i = 0; i < holes.Count; i++)
            {
                for (int j = i+1; j < holes.Count; j++)
                {
                    if (startaddresses[j] < startaddresses[i])
                    {
                        temp = startaddresses[i];
                        startaddresses[i] = startaddresses[j];
                        startaddresses[j] = temp;
                        temp = holes[i];
                        holes[i] = holes[j];
                        holes[j] = temp;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Refresh();
            textBox4.Text = "ex: 1000";
            textBox1.Text = "ex: 100 50 70";
            textBox3.Text = "ex: 0 250 500";
            comboBox1.Text = " ";
            textBox2.Text = "ex: 60";
            textBox5.Text = "ex: 1";
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 32 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only seperated with spaces");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 32 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only seperated with spaces");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers only");
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
                MessageBox.Show("Please enter numbers");
            }
        }
    }
}
