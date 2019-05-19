using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace genetic_algorithm
{
    public partial class Form1 : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        public Form1()
        {
            InitializeComponent();

            geneticAlgorithm = new GeneticAlgorithm();

           // label1.Text = geneticAlgorithm.TextAll;
            maskedTextBox1.Text = "10";
            maskedTextBox2.Text = "4";
            maskedTextBox3.Text = "0.3";
            maskedTextBox4.Text = "0.02";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            Chromosome chromosome = geneticAlgorithm.Evolution(int.Parse(maskedTextBox1.Text), int.Parse(maskedTextBox2.Text),
                double.Parse(maskedTextBox3.Text), double.Parse(maskedTextBox4.Text));

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                label1.Text = geneticAlgorithm.TextAll;
            }
            else
            {
                label1.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                label2.Text = "";
            }
            else
            {

            }
        }
    }
}
