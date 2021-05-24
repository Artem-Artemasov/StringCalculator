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
            int Add = -1;

            //Act
            Add = calculator.Add("");

            //Assert
            Assert.AreEqual(Add, 0);
        }

        [Test]
        public void Add_7_7Returned()
        {
            //Arrange
            int Add = -1;

            //Act
            Add = calculator.Add("7");

            //Assert
            Assert.AreEqual(Add, 7);
        }

        [Test]
        public void Add_5Plus11_16Returned()
        {
            //Arrange
            int Add = -1;

            //Act
            Add = calculator.Add("5,11");

            //Assert
            Assert.AreEqual(Add, 16);
        }

        [Test]
        public void Add__1Plus2Plus11_14Returned()
        {
            //Arrange
            int Add = -1;

            //Act
            Add = calculator.Add("1,2,11");

            //Assert
            Assert.AreEqual(Add, 14);
        }

        [Test]
        public void Add_5PlusNewLine6Plus7_18Returned()
        {
            //Arrange
            int Add = -1;

            //Act
            Add = calculator.Add("5,\n2,11");

            //Assert
            Assert.AreEqual(Add, 18);
        }

        [Test]
        public void Add_

    }
}