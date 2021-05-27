using System;
using System.Collections.Generic;
using StringCalculator.Filtres;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private readonly CalculatorFilter filter = new CalculatorFilter();

        public virtual int Add(string numbers)
        {
            numbers = DetachNumbers(numbers, out string delimiters_str);

            var delimiters = SelectDelimiters(delimiters_str).ToArray();
            var stringNumbers = numbers.Split(delimiters, StringSplitOptions.None);

            if (String.IsNullOrEmpty(stringNumbers.First())) 
                    return 0;

            var unvalidNumbers = ToIntList(stringNumbers);
            var validNumbers = RemoveNotValidNumbers(unvalidNumbers, out string negativeNumbers);
           
            if (negativeNumbers.Length > 0) 
                    throw new ArgumentOutOfRangeException("negative not allowed " + negativeNumbers);

            return validNumbers.Sum();
        }

        /// <summary>
        ///  Move negative numbers from input list to output string
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="negativeNumbers"></param>
        /// <returns></returns>
        private List<int> RemoveNotValidNumbers(in List<int> numbers, out string negativeNumbers)
        {
            negativeNumbers = "";
            foreach (var number in numbers)
            {
                if (filter.IsNegative(number))
                {
                    negativeNumbers += number + "; ";
                }
            }
            numbers.RemoveAll(p => filter.IsNegative(p) || filter.IsSoBigger(p));

            return numbers;
        }

        private IEnumerable<string> SelectDelimiters(string delimiters_str)
        {
            var foundDelimeters = FindDelimiters(delimiters_str);
            if (foundDelimeters.Count != 0)
            {
               return foundDelimeters;
            }

            if (String.IsNullOrEmpty(delimiters_str) == false)
                         return new List<string>() { delimiters_str };

            return new List<string>() { ",", "\n" };
        }

        private string DetachNumbers(string inputString, out string delimeters_str)
        {
            delimeters_str = "";

            if (inputString.StartsWith("//") == false)
                    return inputString;

            var specialSymbols = new string[] { "//", "\n" };
            var splittedStrings = inputString.Split(specialSymbols, StringSplitOptions.None);

            if (splittedStrings.Length > 1)
            {
               // in splited array, numbers staying at second pos, delimiters at first pos
                delimeters_str = splittedStrings[1];
                return splittedStrings[2];
            }
            else
            {
                return splittedStrings[0];
            }
        }

        /// <summary>
        /// Find and split all delimiters in input string 
        /// </summary>
        /// <param name="delimiters"></param>
        /// <returns></returns>
        private List<string> FindDelimiters(string delimiters)
        {
            List<string> splitedDelimiters = new List<string>();

            if (delimiters.IndexOf('[') == -1 && delimiters.IndexOf(']') == -1) return splitedDelimiters;
            //Example [x][a] => x][a
            delimiters = delimiters[1..(delimiters.Length - 1)];

            if (delimiters.Length == 1)
            {
                splitedDelimiters.Add(delimiters);
            }
            else
            {
                splitedDelimiters = (delimiters.Split("][")).ToList();
            }

            return splitedDelimiters;
        }

        private List<int> ToIntList(in string[] numbers)
        {
            List<int> convertedNumbers = new List<int>();

           foreach(var number in numbers)
            {
                int number_int = Int32.Parse(number);
                convertedNumbers.Add(number_int);
            }

            return convertedNumbers;
        }
    }
}
