using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitDemo
{
    [TestFixture]
    public class NUnitTest
    {
        public double NumA { get; set; }

        public double NumB { get; set; }

        [SetUp]
        public void SetUp()
        {
            NumA = 10;
            NumB = 20;
        }

        [Test]
        public void TestAdd()
        {
            double result = Calculator.Add(NumA, NumB);
            Assert.AreEqual(result, 30);
        }

        [Test]
        public void TestSub()
        {
            double result = Calculator.Sub(NumA, NumB);
            Assert.LessOrEqual(result, 0);
        }

        [Test]
        public void TestMutiply()
        {
            double result = Calculator.Mutiply(NumA, NumB);
            Assert.GreaterOrEqual(result, 200);
        }

        [Test]
        public void TestDivide()
        {
            double result = Calculator.Divide(NumA, NumB);
            Assert.IsTrue(0.5 == result);
        }
    }
}
