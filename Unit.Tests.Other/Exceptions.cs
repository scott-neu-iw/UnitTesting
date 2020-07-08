using System;
using Calculator;
using Xunit;

namespace Unit.Tests.Other
{
    public class Exceptions
    {

        [Fact]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            var calc = new BasicCalculator();
            calc.Add(10);

            Assert.Throws<DivideByZeroException>(() => calc.Divide(0));
        }
    }
}
