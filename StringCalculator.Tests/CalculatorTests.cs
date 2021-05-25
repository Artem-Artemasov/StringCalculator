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
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("");

            //Assert
            Assert.AreEqual(sum, 0);
        }

        [Test]
        public void Add_7_ShouldReturn7()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("7");

            //Assert
            Assert.AreEqual(sum, 7);
        }

        [Test]
        public void Add_5Plus11_ShouldReturn16()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("5,11");

            //Assert
            Assert.AreEqual(sum, 16);
        }

        [Test]
        public void Add__1Plus2Plus11_ShouldReturn14()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("1,2,11");

            //Assert
            Assert.AreEqual(sum, 14);
        }

        [Test]
        public void Add_5PlusNewLine6Plus7_ShouldReturn18()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("5\n2,11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        public void Add_CustomDelimeter_ShouldReturn18()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("//;\n5;2;11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        [TestCase("-2,10,5", "Specified argument was out of the range of valid values. (Parameter 'negative not allowed -2; ')")]
        [TestCase("-2,10,-5", "Specified argument was out of the range of valid values. (Parameter 'negative not allowed -2; -5; ')")]
        public void Add_NegativeNumber_ShouldThrowException(string numbers,string exceptionMessage)
        {
            //Arrange
            int sum = -1;

            //Act
            
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => { sum = calculator.Add(numbers); });

            //Assert
            Assert.AreEqual(exceptionMessage, exception.Message);
        }

        [Test]
        public void Add_1005Plus5_ShouldReturn5()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("1005,5");

            //Assert
            Assert.AreEqual(sum, 5);
        }

        [Test]
        public void Add_LongDelimeter_ShouldReturn10()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("//****\n5****5");

            //Assert
            Assert.AreEqual(sum, 10);
        }

        [Test]
        public void Add_ArrayDelimeter_ShouldReturn15()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("//[*][^]\n5*5^5");

            //Assert
            Assert.AreEqual(sum, 15);
        }

        [Test]
        public void Add_ArrayLongDelimeter_ShouldReturn15()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("//[**][af]\n5**5af5");

            //Assert
            Assert.AreEqual(sum, 15);
        }
    }
}