using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic_algorithm
{
    class FitnessFunction
    {
        public static readonly double minInterval = 0.0;
        public static readonly double maxInterval = 20.0;
        public static double Func(double x)
        {
            return Math.Pow(x, 3) - 100;
        }
    }
}
