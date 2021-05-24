using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Filtres
{
    class CalculatorFilter
    {
        public bool IsNegative(string number) 
        {
            if (number.StartsWith('-')) return true;

            return false;
        }
    }
}
