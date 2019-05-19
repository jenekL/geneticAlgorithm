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

namespace genetic_algorithm
{
    public partial class Form1 : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        private Chromosome chromosome;
        public Form1()
        {
            InitializeComponent();

            geneticAlgorithm = new GeneticAlgorithm();

           // label1.Text = geneticAlgorithm.TextAll;
            maskedTextBox1.Text = "47";
            maskedTextBox2.Text = "4";
            maskedTextBox3.Text = "0.3";
            maskedTextBox4.Text = "0.02";

            chart1.Series[0].Name = "Исходная функция";
            chart1.Series.Add(new Series("Полученное значение"));

            chart1.Series[1].Color = Color.Red;
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            chromosome = geneticAlgorithm.Evolution(int.Parse(maskedTextBox1.Text), int.Parse(maskedTextBox2.Text),
                double.Parse(maskedTextBox3.Text), double.Parse(maskedTextBox4.Text));
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chartDrawer();
            label7.Text = "x: " + Math.Round(chromosome.DecimalValue, 4) + "; y:" + Math.Round(chromosome.FunctionValue, 4);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label1.Text = geneticAlgorithm.TextAll;
                chart1.Hide();
            }
            else
            {
                label1.Text = "";
            }
        }
        private void chartDrawer()
        {
            double x = 0.0;
            do
            {
                double y = FitnessFunction.Func(x);
                chart1.Series[0].Points.AddXY(x, y);
                x = x + 0.1;
            } while (x < 20.1);

            chart1.Series[1].Points.AddXY(0, chromosome.FunctionValue);
            chart1.Series[1].Points.AddXY(chromosome.DecimalValue, chromosome.FunctionValue);
            chart1.Series[1].Points.AddXY(chromosome.DecimalValue, 0);


        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {

                chart1.Show();
                label2.Text = "";

            }
            else
            {

            }
        }
    }
}
