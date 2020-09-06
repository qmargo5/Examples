using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Queue<int> q = new Queue<int>();

        public Form1()
        {
            InitializeComponent();
          
        }

        private void AddQ_Click(object sender, EventArgs e)
        {
            int number = 0;
            if (DataQ.Text != null && int.TryParse(DataQ.Text,out number))
            {
                q.Enqueue(number);
                DataQ.Clear();
                textBox1.Clear();
                foreach (int i in q)
                {
                    textBox1.Text = textBox1.Text + Environment.NewLine + i + " ";
                }
            }
            else
            {
                DataQ.Text = "Введите число";
            }
        }

        private void DelQ_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            try
            {
                DataFirst.Text = Convert.ToString(q.Dequeue());
                foreach (int i in q)
                {
                    textBox1.Text = textBox1.Text + Environment.NewLine + i + " ";
                }
            }
            catch (Exception ex)
            {
                DataFirst.Clear();
                textBox1.Text = "Очередь пуста!!!";
            }
                
        }

    }
}
