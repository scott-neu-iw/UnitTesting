using Calculator;
using Xunit;

namespace CalculatorTests
{
    public class BasicCalculatorTests
    {
        [Fact]
        public void Add_WithValue_ShouldBeAddedToTotal()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal value = 5.0M;

            // Act
            calc.Add(value);

            // Assert
            Assert.Equal(value, calc.CurrentResult);
        }

        [Fact]
        public void Subtract_WithValue_ShouldBeSubtractedFromTotal()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal value = 5.0M;

            // Act
            calc.Subtract(value);

            // Assert
            Assert.Equal(-5M, calc.CurrentResult);

        }

        [Fact]
        public void Multiply_WithValue_ShouldShouldBeMultipliedFromTotal()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal starterValue = 5.0M;
            decimal value = 5.0M;

            // Act
            calc.Add(starterValue);
            calc.Multiply(value);

            // Assert
            Assert.Equal(25M, calc.CurrentResult);
        }

        [Fact]
        public void Divide_WithValue_ShouldShouldBeDividedFromTotal()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal starterValue = 15.0M;
            decimal value = 5.0M;

            // Act
            calc.Add(starterValue);
            calc.Divide(value);

            // Assert
            Assert.Equal(3M, calc.CurrentResult);
        }

        [Fact]
        public void Add_WithValue_ShouldAddExpressionToHistory()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal value = 5.0M;

            // Act
            calc.Add(value);

            // Assert
            Assert.Contains("0 + 5.0", calc.History);
        }

        [Fact]
        public void Subtract_WithValue_ShouldAddExpressionToHistory()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal value = 5.0M;

            // Act
            calc.Subtract(value);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Multiply_WithValue_ShouldAddExpressionToHistory()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal starterValue = 5.0M;
            decimal value = 5.0M;

            // Act
            calc.Add(starterValue);
            calc.Multiply(value);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void Divide_WithValue_ShouldAddExpressionToHistory()
        {
            // Arrange
            var calc = new BasicCalculator();
            decimal starterValue = 15.0M;
            decimal value = 5.0M;

            // Act
            calc.Add(starterValue);
            calc.Divide(value);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void MultipleOperations_ShouldKeepRunningTotal_AndAddExpressionsToHistory()
        {
            // Arrange
            var calc = new BasicCalculator();

            calc.Add(5);
            Assert.True(calc.CurrentResult == 5M, "1st Operation CurrentResult");
            Assert.True(calc.History.Count == 1, "1st Operation History Count");

            calc.Multiply(5);
            Assert.True(calc.CurrentResult == 25M, "2nd Operation CurrentResult");
            Assert.True(calc.History.Count == 2, "2nd Operation History Count");

            calc.Subtract(5);
            Assert.True(calc.CurrentResult == 20M, "3rd Operation CurrentResult");
            Assert.True(calc.History.Count == 3, "3rd Operation History Count");

            calc.Divide(4);
            Assert.True(calc.CurrentResult == 5M, "4th Operation CurrentResult");
            Assert.True(calc.History.Count == 4, "4th Operation History Count");
        }
    }
}
