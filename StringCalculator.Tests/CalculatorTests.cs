using NUnit.Framework;
using StringCalculator;
using StringCalculator.Interfaces;

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
        public void Add_EmptyString_0Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("");

            //Assert
            Assert.AreEqual(sum, 0);
        }

        [Test]
        public void Add_7_7Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("7");

            //Assert
            Assert.AreEqual(sum, 7);
        }

        [Test]
        public void Add_5Plus11_16Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("5,11");

            //Assert
            Assert.AreEqual(sum, 16);
        }

        [Test]
        public void Add__1Plus2Plus11_14Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("1,2,11");

            //Assert
            Assert.AreEqual(sum, 14);
        }

        [Test]
        public void Add_5PlusNewLine6Plus7_18Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("5\n2,11");

            //Assert
            Assert.AreEqual(sum, 18);
        }

        [Test]
        public void Add_CustomDelimeter_18Returned()
        {
            //Arrange
            int sum = -1;

            //Act
            sum = calculator.Add("//;\n5;2;11");

            //Assert
            Assert.AreEqual(sum, 18);
        }
    }
}