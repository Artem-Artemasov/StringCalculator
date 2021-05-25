using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
           var calculator = new Calculator();
           Console.WriteLine(calculator.Add("//[s][z]\n5,5,10"));
        }
    }
}
