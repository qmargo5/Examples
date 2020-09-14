using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Генетический_алгоритм
{
    public partial class Form2 : Form
    {
        public Form2(List<double>result, int iter)
        {
            InitializeComponent();
            this.chart1.Series.Clear();
            var series = new Series("Индекс");
            double[] values = result.ToArray();
            int[] points = new int[iter+1];
            series.ChartType = SeriesChartType.Line;
            for (int i = 0; i < points.Count(); i++)
            {
                points[i] = i + 1;
                series.Points.AddXY(points[i], values[i]);
            }
            chart1.Series.Add(series);


        }
    }
}
