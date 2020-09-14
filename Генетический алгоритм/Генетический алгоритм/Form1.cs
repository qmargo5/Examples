using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Генетический_алгоритм
{
    public partial class Form1 : Form
    {
        public class individual
        {
            public int[] Genes = new int[3];
        }
        Random random = new Random();
        //количество особей в популяции
        int numberOfIndividuals;
        //размерность турнира
        int sizeTourney;
        //эталонная особь
        individual model = new individual();
        //вероятность скрещивания особей
        double p = 0.7;

        population parents = new population();
        population parentsForSelection = new population();
        population children = new population();
        List<double> rezult = new List<double>();

        public class population
        {
            public List<individual> Population = new List<individual>();
        }

        //отбор при помощи турнинрой селекции с турниром (размер турнира в переменной)
        public void choice(population parent, population selectionparent)
        {
            Random rand = new Random();
            population parentForSelection = new population();
            for (int i = 0; i < numberOfIndividuals/2; i++)
            {

                int theBest = 0;
                double theBestresult = 50000;
                for (int j = 0; j < sizeTourney; j++)
                {
                    int in1 = rand.Next(0, numberOfIndividuals);
                    if (absolutlifitnessPersent(parent.Population[in1],model) < theBestresult)
                    {
                        theBestresult = absolutlifitnessPersent(parent.Population[in1], model);
                        theBest = in1;
                    }
                }
                parentForSelection.Population.Add(parent.Population[theBest]);
                selectionparent.Population.Add(parent.Population[theBest]);
            }
            parent.Population.Clear();
            individual child1;
            individual child2;
            while (parent.Population.Count< numberOfIndividuals)
            {
                int i = rand.Next(0, numberOfIndividuals/2);
                int j = rand.Next(0, numberOfIndividuals/2);
                if (p>rand.NextDouble())
                {
                    crossingover(parentForSelection.Population[i], parentForSelection.Population[j], child1 = new individual(), child2 = new individual());
                }
                else
                {
                    child1 = new individual();
                    child2 = new individual();
                    child1.Genes= parentForSelection.Population[i].Genes;
                    child2.Genes = parentForSelection.Population[j].Genes; 
                }
                parent.Population.Add(child1);
                parent.Population.Add(child2);
            }
        }
        public double absolutlifitnessPersent(individual ind, individual model)
        {
            double rgbFitness = 0;
            for (int i = 0; i < ind.Genes.Count(); i++)
            {
                rgbFitness += Math.Pow(ind.Genes[i]-model.Genes[i],2);
            }
            rgbFitness = Math.Sqrt(rgbFitness);
            return rgbFitness;
        }
        //Генетический оператор - арифметический кроссинговер
        public void crossingover (individual p1, individual p2, individual c1, individual c2)
        {
            //лямбда-переменная для кроссинговера
            double ß = random.NextDouble();
            for (int i = 0; i < p1.Genes.Count(); i++)
            {
                c1.Genes[i] = Convert.ToInt32(ß * p1.Genes[i] + (1 - ß) * p2.Genes[i]);
                c2.Genes[i] = Convert.ToInt32(ß * p2.Genes[i] + (1 - ß) * p1.Genes[i]);
            }
        }

        //Мутация
        public void Mutation(population parent)
        {
            double Pm = 2 / (numberOfIndividuals*1.0);
            for (int i = 0; i < parent.Population.Count; i++)
            {
                for (int j = 0; j < parent.Population[i].Genes.Count(); j++)
                {
                    double p = random.NextDouble();
                    if (Pm > p)
                        parent.Population[i].Genes[j] += random.Next(-5,11);
                }
            }
        }

        
        //Новое поколение формируется из особей-потомков
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox_number_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            listView2.Items.Clear();
            listView1.Items.Clear();
            int j = listBox_number.SelectedIndex + 1;
            string value;
            for (int i = j* numberOfIndividuals - numberOfIndividuals; i < j* numberOfIndividuals; i++)
            {
                value = parents.Population[i].Genes[0].ToString() + ", "+ parents.Population[i].Genes[1].ToString() + ", "+ parents.Population[i].Genes[2].ToString();
                listView3.Items.Add(value);
                value = children.Population[i].Genes[0].ToString() + ", " + children.Population[i].Genes[1].ToString() + ", " + children.Population[i].Genes[2].ToString();
                listView1.Items.Add(value);
            }
            for (int i = j* numberOfIndividuals/2 - numberOfIndividuals/2; i<j* numberOfIndividuals/2; i++)
            {
                value = parentsForSelection.Population[i].Genes[0].ToString() + ", " + parentsForSelection.Population[i].Genes[1].ToString() + ", " + parentsForSelection.Population[i].Genes[2].ToString();
                listView2.Items.Add(value);
            }
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string[] mas = listView1.Items[i].Text.Split(',', ' ');
                listView1.Items[i].BackColor = Color.FromArgb(Convert.ToInt32(mas[0]), Convert.ToInt32(mas[2]), Convert.ToInt32(mas[4]));
                mas = listView3.Items[i].Text.Split(',', ' ');
                listView3.Items[i].BackColor = Color.FromArgb(Convert.ToInt32(mas[0]), Convert.ToInt32(mas[2]), Convert.ToInt32(mas[4]));
                if (listView1.Items[i].Text == "96, 96, 159")
                    listView1.Items[i].ForeColor = Color.Aqua;
            }
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                string[] mas = listView2.Items[i].Text.Split(',', ' ');
                listView2.Items[i].BackColor = Color.FromArgb(Convert.ToInt32(mas[0]), Convert.ToInt32(mas[2]), Convert.ToInt32(mas[4]));
            }


        }

        private void button_win_Click(object sender, EventArgs e)
        {
            //данные с формы
            numberOfIndividuals = Convert.ToInt32(PopulationNumber.Text);
            sizeTourney = Convert.ToInt32(textBox_sizeTourney.Text);

            //храним родителей, родителей для селекции и потомство
            parents = new population();
            parentsForSelection = new population();
            children = new population();

            Random rnd = new Random();
            //Эталонная особь
            model.Genes = new int[] { 96, 96, 159 };
            //Генерация начальной популяции
            population FirstPopulation = new population();
            for (int i = 0; i < numberOfIndividuals; i++)
            {
                individual curentIndividuals = new individual();
                curentIndividuals.Genes = new int[] { rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256) };
                FirstPopulation.Population.Add(curentIndividuals);
            }
            bool individualIsfind = false;
            int iter = 0;
            listBox_number.Items.Clear();
            while (!individualIsfind)
            {
                double theBestreslt = 50000;
                for (int i = 0; i < FirstPopulation.Population.Count; i++)
                {
                    double rez = absolutlifitnessPersent(FirstPopulation.Population[i], model);
                    if (rez < theBestreslt)
                        theBestreslt = rez;
                    if (absolutlifitnessPersent(FirstPopulation.Population[i], model) == 0)
                    {
                        individualIsfind = true;
                        int number = i;
                        break;
                    }
                }
                rezult.Add(theBestreslt);
                if (!individualIsfind)
                {
                    iter++;
                    foreach (var item in FirstPopulation.Population)
                    {
                        parents.Population.Add(item);
                    }
                    listBox_number.Items.Add(iter);
                    choice(FirstPopulation, parentsForSelection);
                    foreach (var item in FirstPopulation.Population)
                    {
                        children.Population.Add(item);
                    }


                    Mutation(FirstPopulation);
                }
            }
            Form2 graph = new Form2(rezult,iter);
            graph.Show();

        }

    }
}
