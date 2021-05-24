using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
           var calculator = new Calculator();
           Console.WriteLine(calculator.Add("-5\n2,1001"));

        }
    }
}
