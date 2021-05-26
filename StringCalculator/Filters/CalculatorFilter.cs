using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Filtres
{
    public class CalculatorFilter
    {
        private int MaxValue = 1000;
        public bool IsNegative(int number) 
        {
            if (number < 0) return true;

            return false;
        }

        public bool IsSoBigger(int number)
        {
            if (number > MaxValue) return true;

            return false;
        }
    }
}
