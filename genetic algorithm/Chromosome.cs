using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic_algorithm
{
    class Chromosome
    {
        private string binaryValue;
        private double decimalValue;
        private double functionValue;
        private int copiesCount;

        public Chromosome(string binaryValue, double decimalValue, double functionValue, int copiesCount)
        {
            this.binaryValue = binaryValue;
            this.decimalValue = decimalValue;
            this.functionValue = functionValue;
            this.copiesCount = copiesCount;
        }

        public Chromosome(string binaryValue)
        {
            this.binaryValue = binaryValue;
        }

        public void ReCreate(string binaryValue)
        {
            this.binaryValue = binaryValue;
            this.decimalValue = 0;
            this.functionValue = 0;
        }

        public string BinaryValue { get => binaryValue; set => binaryValue = value; }
        public double DecimalValue { get => decimalValue; set => decimalValue = value; }
        public double FunctionValue { get => functionValue; set => functionValue = value; }
        public int CopiesCount { get => copiesCount; set => copiesCount = value; }
    }
}
