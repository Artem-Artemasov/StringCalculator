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

        public bool ChangeDelimiters(string inputDelimeters)
        {
            delimiters = new List<string>();

            delimiters.Add(inputDelimeters);

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

            List<int> numbers_int = new List<int>();


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
        private List<int> ToIntList(string numbers)
        {
            List<int> numbers_int = new List<int>() { 0 };

            List<char> allowNumbers = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            //for (int i = 0;i < numbers.Length; i++)
            while (numbers.Length != 0)
            {
                int sizeDelimeter = 0;

                string number = numbers.Substring(0, FindIndexDelimeter(numbers, out sizeDelimeter));

                if (filter.IsNegative(number))
                {
                    try
                    {
                        throw new Exception($"negatives not allowed {number}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    numbers = numbers.Remove(0, number.Length + sizeDelimeter);
                }
                else
                {
                    numbers_int.Add(Int32.Parse(number));
                    numbers = numbers.Remove(0, number.Length + sizeDelimeter);
                }

            }
            return numbers_int;
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


/*
 char simpleNumber = numbers[i];
                //Если число
                if (allowNumbers.Contains(simpleNumber))
                {
                    numbers_int[listIndex] *= 10;
                    numbers_int[listIndex] += simpleNumber - 48;
                }
                else // Разделитель
                {
                    foreach(var delimiter in delimiters)
                    {
                        if (numbers.IndexOf(delimiter,i) != -1)
                        {
                            numbers_int.Add(0);
                            listIndex++;
                        }
                    }
 } 
*/