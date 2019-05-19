using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace genetic_algorithm.utils
{
    class ValuesUtil
    {
        public static int getChromosomeSize(int num)
        {
            int size = 0;
            while (num > 0)
            {
                num >>= 1;
                size++;
            }
            return size;
        }

        public static double getChromosomeValue(string s, double minInterval, double maxInterval)
        {
            int sum = 0;
          
            for (int i = 0; i < s.Count(); i++)
            {
                sum += (int)(char.GetNumericValue(s[i]) * Math.Pow(2, s.Count() - i - 1));
            }
            return minInterval + sum * ((maxInterval - minInterval) / (Math.Pow(2, s.Count()) - 1));
        }
    }
}
