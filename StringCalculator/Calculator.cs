using StringCalculator.Interfaces;
using System;
using System.Collections.Generic;
using StringCalculator.Filtres;

namespace StringCalculator
{
    public class Calculator:ICalculator
    {
        private List<string> delimiters = new List<string>() { ",", "\n" };
        private CalculatorFilter filter = new CalculatorFilter();
        private List<int> numbers_int = new List<int>();

        public bool ChangeDelimiters(string inputDelimeters)
        {
            delimiters = new List<string>();

            delimiters.Add(inputDelimeters);

            return true;
        }
        public bool ChangeDelimiters(IEnumerable<string> inputDelimeters)
        {
            delimiters = new List<string>();

            delimiters.AddRange(inputDelimeters);

            return true;
        }

        public int Add(string numbers)
        {
            int sum = 0;

            if (numbers.StartsWith("//"))
            {
                string delimeters_str = "";

                SplitString(numbers, out numbers, out delimeters_str);

                var allDelimeters = FindDelimiters(delimeters_str);
                if (allDelimeters.Count == 0) 
                {
                    ChangeDelimiters(delimeters_str);
                }
                else
                {
                    ChangeDelimiters(allDelimeters);
                }


            }

            numbers_int = ToIntList(numbers);

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
        private List<string> FindDelimiters(string delimeters)
        {
            List<string> splitedDelimiters = new List<string>();

            while (delimeters.Length != 0)
            {
                int endIndexDelimeter = delimeters.IndexOf(']');

                if (endIndexDelimeter == -1) break;

                splitedDelimiters.Add(delimeters.Substring(1, endIndexDelimeter-1));

                delimeters = delimeters.Remove(0, endIndexDelimeter+1);
            }

            return splitedDelimiters;
        }
        private List<int> ToIntList(string numbers)
        {
            while (numbers.Length != 0)
            {
                int sizeDelimeter = 0;

                string number = numbers.Substring(0, FindIndexDelimeter(numbers, out sizeDelimeter));

                if (filter.IsNegative(number))
                {
                    try
                    {
                        throw new ArgumentOutOfRangeException($"negatives not allowed {number}");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    AddToNumbers(number);
                }

                numbers = numbers.Remove(0, number.Length + sizeDelimeter);
            }
            return numbers_int;
        }

        private bool AddToNumbers(string number)
        {
            int number_int = Int32.Parse(number);

            if (filter.IsSoBigger(number_int) == false)
            {
                numbers_int.Add(number_int);
            }

            return true;
        }
        private int FindIndexDelimeter(string input,out int sizeDelimeter)
        {
            int indexDelimeter = input.Length;
            sizeDelimeter = 0;


            foreach (string delimeter in delimiters)
            {
                if (input.IndexOf(delimeter) < indexDelimeter && input.IndexOf(delimeter)!= -1)
                {
                    indexDelimeter = input.IndexOf(delimeter);
                    sizeDelimeter = delimeter.Length;
                }

            }

            if (indexDelimeter == -1) indexDelimeter = input.Length;

            return indexDelimeter;
        }

    }
}
