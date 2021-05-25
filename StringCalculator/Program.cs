using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
           var calculator = new Calculator();
           Console.WriteLine(calculator.Add("1,2,11,22"));
        }
    }
}
