using StringCalculator.Interfaces;
using System.Collections.Generic;


namespace StringCalculator
{
    public class Calculator:ICalculator
    {
        private List<char> delimiters = new List<char>() { ',' };


        public bool ChangeDelimiters(string inputDelimeters)
        {
            delimiters = new List<char>();

            for (int i = 0; i < inputDelimeters.Length; i++)
            {
                delimiters.Add(inputDelimeters[i]);
            }

            return true;
        }
       
        public int Add(string numbers)
        {
            int sum = 0;

            if (numbers.StartsWith("//"))
            {
                string delimeters_str = "";

                SplitString(numbers, out numbers, out delimeters_str);

                ChangeDelimiters(delimeters_str);
            }

            var numbers_int = ToIntList(numbers);

            foreach(var number in numbers_int)
            {
                sum += number;
            }

            return sum;
        }

        private bool SplitString(string inputString, out string numbers, out string delimeters_str)
        {
            int startIndex = inputString.IndexOf("//");
            int endIndex = inputString.IndexOf("\n");

            delimeters_str = inputString.Substring(startIndex + 2, endIndex - startIndex - 2);

            numbers = inputString.Substring(endIndex + 1, inputString.Length - endIndex - 1);

            return true;
        }
        private List<int> ToIntList(string numbers)
        {
            List<int> numbers_int = new List<int>() { 0 };
            int listIndex = 0;

            foreach (char simpleNumber in numbers)
            {
                //Следующее число
                if (delimiters.Contains(simpleNumber))
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


    }
}
