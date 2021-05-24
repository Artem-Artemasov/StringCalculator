using StringCalculator.Interfaces;
using System.Collections.Generic;


namespace StringCalculator
{
    public class Calculator:ICalculator
    {
        private List<int> ToIntList(string numbers)
        {
          List<int> numbers_int = new List<int>() { 0 };
          int listIndex = 0;

            foreach (char simpleNumber in numbers)
            {
                //Следующее число
                if (simpleNumber == ',')
                {
                    numbers_int.Add(0);
                    listIndex++;
                } 
                //Если число
                if (simpleNumber >= 48 && simpleNumber <= 57)
                {
                    numbers_int[listIndex] *= 10;
                    numbers_int[listIndex] += simpleNumber - 48;
                }
            }
          return numbers_int;
        }

        public int Add(string numbers)
        {
            int sum = 0;
            var numbers_int = ToIntList(numbers);

            foreach(var number in numbers_int)
            {
                sum += number;
            }

            return sum;
        }
    }
}
