using NUnit.Framework;
using StringCalculator;
using StringCalculator.Interfaces;
using System;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        private ICalculator calculator;

        [SetUp]
        public void Init()
        {
            this.calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ShouldReturn0()
        {
            //Act
            var sum = calculator.Add("");

            //Assert
            Assert.AreEqual(sum, 0);
        }

        [Test]
        public void Add_OneNumber_ShouldReturnThisNumber()
        {
            //Act
            var sum = calculator.Add("7");

            //Assert
            Assert.AreEqual(sum, 7);
        }

        [Test]
        public void Add_ManyNumber_ShouldReturnCorrectSum()
        {
            //Act
            var sum = calculator.Add("1,2,11,22");

            //Assert
            Assert.AreEqual(sum, 26);
        }

        [Test]
        public void Add_SupportNewLineSymbol_ShouldReturnCorrectSum()
        {
            //Act
            var sum = calculator.Add("5\n2,11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        public void Add_CustomDelimeter_ShouldReturnSum()
        {
            //Act
            var sum = calculator.Add("//;\n5;2;11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        [TestCase("-2,10,5", "Specified argument was out of the range of valid values. (Parameter 'negative not allowed -2; ')")]
        [TestCase("-2,10,-5", "Specified argument was out of the range of valid values. (Parameter 'negative not allowed -2; -5; ')")]
        public void Add_NegativeNumber_ShouldThrowException(string numbers,string exceptionMessage)
        {
            //Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => { calculator.Add(numbers); });

            //Assert
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void Add_BigNumber_ShouldSkipIt()
        {
            //Act
            var sum = calculator.Add("1005,5");

            //Assert
            Assert.AreEqual(sum, 5);
        }

        [Test]
        public void Add_LongDelimeter_ShouldReturnSum()
        {
            //Act
            var sum = calculator.Add("//****\n5****5");

            //Assert
            Assert.AreEqual(sum, 10);
        }

        [Test]
        public void Add_ArrayOfDelimeter_ShouldReturnCorrectSum()
        {
            //Act
            var sum = calculator.Add("//[*][^]\n5*5^5");

            //Assert
            Assert.AreEqual(sum, 15);
        }

        [Test]
        public void Add_ArrayLongDelimeters_ShouldReturnCorrectSum()
        {
            //Act
            var sum = calculator.Add("//[**][af]\n5**5af5");

            //Assert
            Assert.AreEqual(sum, 15);
        }
    }
}