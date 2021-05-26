using NUnit.Framework;
using StringCalculator;
using System;

namespace StringCalculator.Tests
{
    public class CalculatorTests
    {
        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }

        [Test]
        public void Add_EmptyString_ReturnZero()
        {
            //Act
            var sum = calculator.Add("");

            //Assert
            Assert.AreEqual(sum, 0);
        }

        [Test]
        public void Add_OneNumber_ReturnThisNumber()
        {
            //Act
            var sum = calculator.Add("7");

            //Assert
            Assert.AreEqual(sum, 7);
        }

        [Test]
        public void Add_ManyNumber_ReturnCorrectSumIt()
        {
            //Act
            var sum = calculator.Add("1,4,15");

            //Assert
            Assert.AreEqual(sum, 20);
        }

        [Test]
        public void Add_NewLineSymbol_ShouldSupportIt()
        {
            //Act
            var sum = calculator.Add("5\n2,11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        public void Add_CustomDelimiter_ShouldSupportIt()
        {
            //Act
            var sum = calculator.Add("//;\n5;2;11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        public void Add_NegativeNumber_ShouldThrowException()
        {
            //Act
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => { calculator.Add("-2,10,-5"); });

            //Assert
            Assert.AreEqual("Specified argument was out of the range of valid values. (Parameter 'negative not allowed -2; -5; ')", exception.Message);
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
        public void Add_MultiCharacterDelimiter_ShouldSupportIt()
        {
            //Act
            var sum = calculator.Add("//****\n5****5");

            //Assert
            Assert.AreEqual(sum, 10);
        }

        [Test]
        public void Add_ManyDelimiters_ShouldSupportIt()
        {
            //Act
            var sum = calculator.Add("//[**][af]\n5**5af5");

            //Assert
            Assert.AreEqual(sum, 15);
        }

        [Test]
        public void Add_BracketAsDelimiter_ShouldSupportIt()
        {
            //Act
            var sum = calculator.Add("//[[]\n5[5[5");

            //Assert
            Assert.AreEqual(sum, 15);
        }
    }
}