using StringCalculator.Interfaces;
using System;
using System.Collections.Generic;
using StringCalculator.Filtres;

namespace StringCalculator
{
    public class Calculator:ICalculator
    {
        private readonly CalculatorFilter filter = new CalculatorFilter();

        public virtual int Add(string numbers)
        {
            int sum = 0;

            SplitString(numbers, out numbers, out string delimiters_str);

            var delimiters = ChangeDelimiters(delimiters_str);

            var numberList = SplitNumbers(numbers, delimiters);

            var dirtyNumbers = ToIntList(numberList);

            var cleanNumbers = CleanList(dirtyNumbers, out string negativeNumbers);
           
            if (negativeNumbers.Length > 0) throw new ArgumentOutOfRangeException("negative not allowed " + negativeNumbers);

            foreach(var number in cleanNumbers)
            {
                sum += number;
            }

            return sum;
        }

        public List<int> CleanList(in List<int> numbers, out string negativeNumbers)
        {
            negativeNumbers = "";
            foreach (var number in numbers)
            {
                if (filter.IsNegative(number))
                {
                    negativeNumbers += number + "; ";
                }
            }
            numbers.RemoveAll(p => filter.IsNegative(p));

            return numbers;
        }
        private List<string> ChangeDelimiters(string delimiters_str)
        {
            var delimiters = new List<string>() { ",","\n"};

            if (delimiters_str.StartsWith("//"))
            {
                var allDelimeters = FindAllDelimiters(delimiters_str);

                if (allDelimeters.Count != 0)
                {
                    delimiters.AddRange(allDelimeters);
                }
            }

            return delimiters;
        }

        private bool SplitString(string inputString, out string numbers, out string delimeters_str)
        {
            //Setup default variable
            numbers = inputString;
            delimeters_str = "";

            string startDelimiters = "//";
            string endDelimiters   = "\n";

            int startPosDelimiters = inputString.IndexOf(startDelimiters);
            int endPosDelimiters   = inputString.IndexOf(endDelimiters);

            if (startPosDelimiters == -1 || endPosDelimiters == -1) return false;

            delimeters_str = inputString.Substring(startPosDelimiters , endPosDelimiters + endDelimiters.Length);

            numbers = inputString.Substring(endPosDelimiters + endDelimiters.Length, inputString.Length - endPosDelimiters - endDelimiters.Length);

            return true;
        }

        //Find and split delimiters
        private List<string> FindAllDelimiters(string delimeters)
        {
            List<string> splitedDelimiters = new List<string>();

            var dirtySplitedDelimiters = delimeters.Split(new char[] { '[', ']' });

            //First and end symbol // and \n
            for (int i = 1; i < dirtySplitedDelimiters.Length - 1; i++)
            {
                if (dirtySplitedDelimiters[i] != "") splitedDelimiters.Add(dirtySplitedDelimiters[i]);
            }

            return splitedDelimiters;
        }

        private List<int> ToIntList(in List<string> numbers)
        {
            List<int> convertedNumbers = new List<int>();

           foreach(var number in numbers)
            {
                int number_int = Int32.Parse(number);

                if (filter.IsSoBigger(number_int) == false)
                {
                    convertedNumbers.Add(number_int);
                }
            }

            return convertedNumbers;
        }

        private List<string> SplitNumbers(string numbers,List<string> delimiters)
        {
            List<string> numberList = new List<string>();
            while (numbers.Length != 0)
            {
                int sizeDelimeter = 0;

                string number = numbers.Substring(0, FindPositionDelimeter(numbers,delimiters, out sizeDelimeter));

                numberList.Add(number);

                numbers = numbers.Remove(0, number.Length + sizeDelimeter);
            }

            return numberList;
        }

        //Find first injection on input and return pos and size
        private int FindPositionDelimeter(string input,List<string> delimiters,out int sizeDelimeter)
        {
            int minIndexDelimiter = input.Length;
            sizeDelimeter = 0;

            foreach (string delimeter in delimiters)
            {
                if ((input.IndexOf(delimeter) < minIndexDelimiter) && (input.IndexOf(delimeter) != -1))
                {
                    minIndexDelimiter = input.IndexOf(delimeter);
                    sizeDelimeter = delimeter.Length;
                }
            }

            if (minIndexDelimiter == -1) minIndexDelimiter = input.Length;

            return minIndexDelimiter;
        }

    }
}
