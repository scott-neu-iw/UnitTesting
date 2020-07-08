using Calculator;
using Xunit;

namespace Unit.Tests.Other
{
    public class Theories
    {
        [Theory]
        [InlineData(5, 5)]
        [InlineData(15, 15)]
        [InlineData(25, 25)]
        public void Add_WithValue_ShouldBeAddedToTotal(decimal value, decimal result)
        {
            // Arrange
            var calc = new BasicCalculator();

            // Act
            calc.Add(value);

            // Assert
            Assert.Equal(result, calc.CurrentResult);
        }
    }
}
