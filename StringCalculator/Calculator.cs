using StringCalculator.Interfaces;
using System;
using System.Collections.Generic;
using StringCalculator.Filtres;

namespace StringCalculator
{
    public class Calculator:ICalculator
    {
        private readonly CalculatorFilter filter = new CalculatorFilter();
        private List<string> delimiters = new List<string>() { ",", "\n" };
        private List<int> numbersPull = new List<int>();

        public virtual int Add(string numbers)
        {
            int sum = 0;

            if (numbers.StartsWith("//"))
            {
                string delimeters_str = "";
                SplitString(numbers, out numbers, out delimeters_str);

                ChangeDelimiters(delimeters_str);
            }

            AddToPull(numbers);

            //add all negative numbers to string and remove it from pull
            string negativeNumbers = "";
            foreach(var number in numbersPull)
            {
                if (filter.IsNegative(number))
                {
                    negativeNumbers += number + "; ";
                }
            }
            numbersPull.RemoveAll(p => filter.IsNegative(p));

            if (negativeNumbers.Length > 0) throw new ArgumentOutOfRangeException("negative not allowed " + negativeNumbers);

            foreach(var number in numbersPull)
            {
                sum += number;
            }

            return sum;
        }
        private bool ChangeDelimiters(string inputDelimeters)
        {
            delimiters = new List<string>();

            var allDelimeters = FindAllDelimiters(inputDelimeters);

            if (allDelimeters.Count == 0)
            {
                delimiters.Add(inputDelimeters);
            }
            else
            {
                delimiters.AddRange(allDelimeters);
            }

            return true;
        }
        private bool SplitString(string inputString, out string numbers, out string delimeters_str)
        {
            int startIndex = inputString.IndexOf("//");
            int endIndex = inputString.IndexOf("\n");

            delimeters_str = inputString.Substring(startIndex + 2, endIndex - startIndex - 2);

            numbers = inputString.Substring(endIndex + 1, inputString.Length - endIndex - 1);

            return true;
        }
        //Ищет и разделяет все разделители чисел
        private List<string> FindAllDelimiters(string delimeters)
        {
            List<string> splitedDelimiters = new List<string>();

            while (delimeters.Length != 0)
            {
                int endIndexDelimeter = delimeters.IndexOf(']');

                if (endIndexDelimeter == -1) break;

                splitedDelimiters.Add(delimeters[1..endIndexDelimeter]);
                delimeters = delimeters.Remove(0, endIndexDelimeter+1);
            }

            return splitedDelimiters;
        }
        // Конвертирует числа и добавляет их в пул
        private bool AddToPull(string numbers)
        {

            while (numbers.Length != 0)
            {
                int sizeDelimeter = 0;

                string number = numbers.Substring(0, FindPositionDelimeter(numbers, out sizeDelimeter));

                int number_int = Int32.Parse(number);

                if (filter.IsSoBigger(number_int) == false)
                {
                    numbersPull.Add(number_int);
                }

                numbers = numbers.Remove(0, number.Length + sizeDelimeter);
            }

            return true;
        }
        private int FindPositionDelimeter(string input,out int sizeDelimeter)
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
