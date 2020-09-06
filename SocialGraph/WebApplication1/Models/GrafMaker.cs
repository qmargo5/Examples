using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GrafMaker
    {
        public List<List<int>> list;

        public int SizeOfStartGraf { get; set; }
        
        public double Probability { get; set; }
        
        public int NumberOfNodes { get; set; }
        static Random rand = new Random();

        //Добавление вершины
        public void AddVertex()
        {
            list.Add(new List<int>());

            double edgeOut = 0; // Переменная для подсчета числа ребер исходящих из вершины



            double edgeAll = 0;                      //Переменная для подсчета всех ребер

            for (int i = 0; i < list.Count - 1; i++) //Подсчет всех ребер в матрице
            {
                for (int j = 0; j < list.Count - 1; j++)
                {
                    if (list[i][j] == 1)
                        edgeAll += 1;
                }
            }

            double temp;

            //Цикл для генерации ребер входящих в новую вершину
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - 1; j++) //Подсчет ребер исходящих из вершины
                {
                    if (list[i][j] == 1)
                        edgeOut += 1;
                }

                temp = rand.NextDouble();

                // Вывод подсчитанных ребер и сгенерированного числа
                /*Console.Write(temp + " " + edgeOut + " " + edgeAll + " ");
                Console.Write(edgeOut / edgeAll);
                Console.WriteLine();*/

                //Добавление входящего ребра
                if (temp <= edgeOut / edgeAll)            //edgeOut / edgeAll - вероятность по Барабаши-Альберт
                    list[i].Add(1);
                else list[i].Add(0);

                edgeOut = 0;
            }

            //Цикл для добавления ребер исходящих из новой вершины
            for (int i = 0; i < list.Count; i++)
            {
                temp = rand.NextDouble();
                if (temp <= Probability)                   //Каждое ребро добавляется с вероятность 0.01
                    list[list.Count - 1].Add(1);
                else list[list.Count - 1].Add(0);
            }
            list[list.Count - 1][list.Count - 1] = 0;

        }

        public int[] outDegree()
        {
            int min = 10000;
            int max = 0;
            int[] outD = new int[list.Count];
            for (int i = 0; i < list.Count; i++)  //Подсчет степени каждой вершины
            {
                for (int j = 0; j < list.Count; j++)
                {
                    outD[i] += list[i][j];
                }
            }

            for (int i = 0; i < list.Count; i++)  //Поиск максимума и минимума в массиве
            {
                if (outD[i] < min)
                    min = outD[i];
                if (outD[i] > max)
                    max = outD[i];
            }

            int length = max - min;

            int[] numV = new int[length + 1];    // Массив с количеством вершин по степеням. index - степень

            for (int i = 0; i <= length; i++)  // подсчета числа вершин с определенной степенью
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (outD[j] == min)
                        numV[i]++;
                }
                min++;
            }

            return numV;
        }  // возвращает массив outDegree

        public int[] inDegree()
        {
            int min = 10000;
            int max = 0;
            int[] inD = new int[list.Count];
            for (int i = 0; i < list.Count; i++)  //Подсчет степени каждой вершины
            {
                for (int j = 0; j < list.Count; j++)
                {
                    inD[i] += list[j][i];
                }

            }



            for (int i = 0; i < list.Count; i++)  //Поиск максимума и минимума в массиве
            {
                if (inD[i] < min)
                    min = inD[i];
                if (inD[i] > max)
                    max = inD[i];
            }

            int length = max - min;

            int[] numV = new int[length + 1];    // Массив с количеством вершин по степеням. index - степень

            for (int i = 0; i <= length; i++)  // подсчета числа вершин с определенной степенью
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (inD[j] == min)
                        numV[i]++;
                }
                min++;
            }

            return numV;
        }   // возвращает массив inDegree

        //Создание полного графа-затравки с числом вершин num
        public void makeGraph()
        {
            list = new List<List<int>>();
            for (int i = 0; i < SizeOfStartGraf; i++)
            {
                list.Add(new List<int>());

                for (int j = 0; j < SizeOfStartGraf; j++)
                {
                    list[i].Add(1);
                }
                list[i][i] = 0;
            }

        }
        public string getString()
        {
            string l = "";
            bool flag = false;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[i][j] == 1)
                    {
                        flag = true;
                        l += i.ToString() + ',' + j.ToString() + ';';
                    }
                }
                if (flag == false)
                {
                    l += i.ToString() + ',' + "zero" + ';';
                }

                flag = false;
            }
            return l;
        }
        public string ConvertToGraphML()
        {


            string graphML = "<?xml version = \"1.0\" encoding = \"UTF-8\" standalone = \"no\"?>\n" +
                             "<graphml xmlns = \"http://graphml.graphdrawing.org/xmlns\">\n" +

                             "<key attr.name = \"name\" attr.type = \"string\" for= \"node\" id = \"name\" />\n" +
                              "<key attr.name = \"label\" attr.type = \"string\" for= \"node\" id = \"label\" />\n" +
                              "<key attr.name = \"name\" attr.type = \"string\" for= \"edge\" id = \"name\" />\n" +
                              " <key attr.name = \"weight\" attr.type = \"double\"   for= \"edge\" id = \"weight\" />\n" +
                              "<key attr.name = \"name\" attr.type = \"string\" for= \"graph\" id = \"name\" />\n" +
                              "<graph edgedefault = \"directed\" id = \"Network\">\n";


            int n = list.Count();


            string nodes = "";
            string edges = "";
            for (int i = 0; i < n; i++)
            {
                nodes += "<node id=\"" + i.ToString() + "\">\n" +
                            "<data key=\"name\">" + i.ToString() + "</data>\n" +
                             "<data key=\"label\">" + i.ToString() + "</data>\n" +
                             "</node>\n";

                for (int j = 0; j < n; j++)
                {

                    if (list[i][j] != 0)
                        edges += "<edge source=\"" + i.ToString() + "\" target = \"" + j.ToString() + "\">\n" +
                          "<data key = \"weight\">" + "1" + "</data>\n</edge>\n";


                }
            }


            graphML += nodes;
            graphML += edges;

            return graphML += "</graph>\n</graphml> ";


           

        }

        public int[] RadiusDiametr()
        {
            List<List<int>> g = new List<List<int>>();

            for (int k = 0; k < list.Count; k++)
            {
                g.Add(new List<int>());
                for (int i = 0; i < list.Count; i++)
                {
                    g[k].Add(list[k][i]);
                    if (g[k][i] == 0) g[k][i] = 35000;
                }
            }
            for (int k = 0; k < g.Count; k++)
                for (int i = 0; i < g.Count; i++)
                    for (int j = 0; j < g.Count; j++)
                        if (g[i][k] + g[k][j] < g[i][j])
                            g[i][j] = g[i][k] + g[k][j];
            int max = 0;
            int[] rd = new int[2];
            rd[0] = 35000; rd[1] = 0;
            for (int n = 0; n < g.Count; n++)
            {
                max = 1;
                for (int m = 0; m < g.Count; m++)
                {
                    if (g[n][m] > max && g[n][m] != 35000) max = g[n][m];
                    if (max > rd[1]) rd[1] = max;
                    if (max < rd[0]) rd[0] = max;
                }
            }
            return rd;
        }
        public double clustering()
        {
            double Global = 0;
            double sum = 0;
            double[] local = new double[list.Count];
            List<int> neibghor = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[i][j] == 1 || list[j][i] == 1)
                    {
                        neibghor.Add(j);
                    }
                }
                int all = neibghor.Count * (neibghor.Count - 1);
                double existing = 0;
                for (int j = 0; j < neibghor.Count; j++)
                {
                    for (int k = 0; k < neibghor.Count; k++)
                    {
                        if (list[neibghor[j]][neibghor[k]] == 1)
                        {
                            existing += 1;
                        }
                    }
                }
                neibghor.Clear();
                if (all != 0)
                    local[i] = existing / all;
                else
                    local[i] = 0;
            }
            for (int i = 0; i < local.Length; i++)
            {
                sum += local[i];
            }
            Global = sum / (local.Length);
            return Global;
        }
    }
}