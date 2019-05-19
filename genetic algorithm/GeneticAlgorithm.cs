using genetic_algorithm.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace genetic_algorithm
{
    class GeneticAlgorithm
    {
        private static readonly int INTERVALS_COUNT = 500;
        private Random random = new Random();
        private Label text;
        private StringBuilder textAll = new StringBuilder();

        public string TextAll { get => textAll.ToString(); }

        public GeneticAlgorithm(Label text)
        {
            this.text = text;
        }

        public GeneticAlgorithm()
        {
        }

        public void printGenome(List<Chromosome> genome)
        {
            int i = 0;
            foreach (Chromosome c in genome)
            {
                textAll.Append("["+ i++ + "]" +c.BinaryValue + " " + c.DecimalValue + "\n");
                System.Console.WriteLine(c.BinaryValue + " " + c.DecimalValue);
            }
          
        }

        public Chromosome Evolution(int chromosomesCount, int iterationsCount, double crossingoverChance, double mutationChance)
        {
            textAll.Clear();

            double minInterval = FitnessFunction.minInterval;
            double maxInterval = FitnessFunction.maxInterval;
                                     

            int chromosomesSize = ValuesUtil.getChromosomeSize(INTERVALS_COUNT * (int)(maxInterval - minInterval));
            int currentCount = 0; //количество итераций

            List<Chromosome> genome = CreateGenome(chromosomesSize, chromosomesCount);
            foreach (Chromosome c in genome)
            {
                c.DecimalValue = ValuesUtil.getChromosomeValue(c.BinaryValue, minInterval, maxInterval);
            }

            textAll.Append("Исходная популяция\n");
            System.Console.WriteLine("Исходная популяция");
            printGenome(genome);
          

            while (currentCount < iterationsCount)
            {
                textAll.Append("Итерация " + (currentCount + 1) + "\n");
                System.Console.WriteLine("Итерация " + (currentCount + 1));

                Reproduction(genome, minInterval, maxInterval, chromosomesCount);
                printGenome(genome);

                foreach (Chromosome chromosome in genome)
                {
                    if(random.NextDouble() <= crossingoverChance)
                    {
                        textAll.Append("Crossingover with chance " + crossingoverChance + "\n");
                        System.Console.WriteLine("Crossingover with chance " + crossingoverChance);
                        Crossingover(genome, chromosome, minInterval, maxInterval);
                    }

                    string binary = chromosome.BinaryValue;
                    if (random.NextDouble() <= mutationChance)
                    {
                        textAll.Append("Mutation with chance " + mutationChance + "\n");
                        System.Console.WriteLine("Mutation with chance " + mutationChance);
                        chromosome.BinaryValue = Mutation(binary);
                        chromosome.DecimalValue = ValuesUtil.getChromosomeValue(chromosome.BinaryValue, minInterval, maxInterval);
                    }

                    double value = ValuesUtil.getChromosomeValue(binary, minInterval, maxInterval);
                    double functionValue = FitnessFunction.Func(value);

                    chromosome.DecimalValue = value;
                    chromosome.FunctionValue = functionValue;
                }


                // ?????????????????
                genome.AddRange(CreateGenome(chromosomesSize, chromosomesCount - genome.Count));

                foreach (Chromosome c in genome)
                { 
                    c.DecimalValue = ValuesUtil.getChromosomeValue(c.BinaryValue, minInterval, maxInterval);
                    c.FunctionValue = FitnessFunction.Func(c.DecimalValue);
                }

                currentCount++;
            }

            textAll.Append("Финальный геном" + "\n");
            System.Console.WriteLine("Финальный геном");
            printGenome(genome);

            Chromosome best = null;
            double bestFunc = -100000;


            foreach (Chromosome c in genome)
            {
                if (c.FunctionValue > bestFunc)
                { 
                    bestFunc = c.FunctionValue;
                    best = c;
                }
            }

            textAll.Append("Лучшая: " + best.BinaryValue + " | " + best.DecimalValue + " | " + best.FunctionValue + "\n");
            System.Console.WriteLine("Лучшая: " + best.BinaryValue + " | " + best.DecimalValue + " | " + best.FunctionValue);
            //System.Console.WriteLine("Ее числовое значение: " + best.DecimalValue);
            //System.Console.WriteLine("Значение фитнес-функции: " + best.FunctionValue);

            return best;
        }

        private string Mutation(string chromosome)
        {
            StringBuilder newChromosome = new StringBuilder(chromosome);
            int i = random.Next(chromosome.Count());
            char c = newChromosome[i];

            if (c == '0')
            {
                newChromosome[i] = '1';
            }
            else
            {
                newChromosome[i] = '0';
            }
            return newChromosome.ToString();
        }
        private void Reproduction(List<Chromosome> genome, double minInterval, double maxInterval, int chromosomesCount)
        {
            double fValuesSum = 0;
            foreach (Chromosome c in genome)
            {
                double functionValue = FitnessFunction.Func(ValuesUtil.getChromosomeValue(c.BinaryValue, minInterval, maxInterval));
                c.FunctionValue = functionValue;
                fValuesSum += functionValue;
            }

            List<Chromosome> newGenome = new List<Chromosome>();

            textAll.Append("Reproduction:\n");
            foreach (Chromosome c in genome)
            {
                long count = (long)Math.Round((c.FunctionValue / fValuesSum) * chromosomesCount);
                textAll.Append("C repeats " + count + ", " + c.BinaryValue + " " + c.DecimalValue + "\n");
                for (int i = 0; i < count; i++)
                {
                    newGenome.Add(c); 
                }
            }

            genome.Clear();
            genome.AddRange(newGenome); 
        }
        private void Crossingover(List<Chromosome> genome, Chromosome father, double minInterval, double maxInterval)
        {
            Chromosome mother = getParents(genome, father);
            if (mother != null)
            {
                textAll.Append("Mother " + mother.BinaryValue + "\n");
                System.Console.WriteLine("Mother " + mother.BinaryValue);
                textAll.Append("Father " + father.BinaryValue + "\n");
                System.Console.WriteLine("Father " + father.BinaryValue);

                GetChildBySinglePointCrossingover(father, mother, minInterval, maxInterval);
              //  string child = GetChildByModelCrossingover(father.BinaryValue, mother.BinaryValue, CreateBinaryModel(father.BinaryValue.Length));
              //  textAll.Append("Child " + child + "\n");
              //  System.Console.WriteLine("Child " + child);

                // ????????????????????????????
               // father.reCreate(child);
               // father.DecimalValue = ValuesUtil.getChromosomeValue(child, minInterval, maxInterval);
            }
        }


        private string CreateBinaryModel(int size)
        {
            StringBuilder value = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                if (random.NextDouble() <= 0.5)
                {
                    value.Append('1');
                }
                else
                {
                    value.Append('0');
                }
            }
            return value.ToString();
        }
        private List<Chromosome> CreateGenome(int size, int count)
        {
            List<Chromosome> genome = new List<Chromosome>();
            for (int i = 0; i < count; i++)
            {
                genome.Add(new Chromosome(CreateBinaryModel(size)));
            }
            return genome;
        }


        private Chromosome getParents(List<Chromosome> genome, Chromosome father)
        {
            bool contains = false;
            foreach (Chromosome c in genome)
            {
                contains = !c.Equals(father);
            }

            if (contains)
            {
                Chromosome mother;
                do
                {
                    mother = genome[random.Next(genome.Count)];
                } while (mother.Equals(father));

                return mother;
            }
            else
            {
                return null;
            }
        }

        //Не продуманная
        private void GetChildBySinglePointCrossingover(Chromosome father, Chromosome mother, double minInterval, double maxInterval)
        {
            int k = random.Next(father.BinaryValue.Length);
            textAll.Append("K = " + k + "and size = " + father.BinaryValue.Count() +"\n");
            StringBuilder child1 = new StringBuilder();
            StringBuilder child2 = new StringBuilder();

            child1.Append(father.BinaryValue.Substring(0, k));
            child1.Append(mother.BinaryValue.Substring(k, mother.BinaryValue.Length - k));
            child2.Append(mother.BinaryValue.Substring(0, k));
            child2.Append(father.BinaryValue.Substring(k, father.BinaryValue.Length - k));
            textAll.Append("Child 1 " + father.BinaryValue.Substring(0, k) + "_" + mother.BinaryValue.Substring(k, mother.BinaryValue.Length - k) + "\n");
            textAll.Append("Child 2 " + mother.BinaryValue.Substring(0, k) + "_" + father.BinaryValue.Substring(k, father.BinaryValue.Length - k) + "\n");

            father.reCreate(child1.ToString());
            mother.reCreate(child2.ToString());
            father.DecimalValue = ValuesUtil.getChromosomeValue(child1.ToString(), minInterval, maxInterval);
            mother.DecimalValue = ValuesUtil.getChromosomeValue(child2.ToString(), minInterval, maxInterval);

        }
        private string GetChildByModelCrossingover(string father, string mother, string model)
        {
            StringBuilder child = new StringBuilder();
            for (int i = 0; i < model.Length; i++)
            {
                if (model[i] == '0')
                {
                    child.Append(father[i]);
                }
                else
                {
                    child.Append(mother[i]);
                }
            }
            return child.ToString();
        }

      
    }
}
