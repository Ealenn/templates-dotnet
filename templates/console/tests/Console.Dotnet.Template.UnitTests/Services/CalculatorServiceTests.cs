using Console.Dotnet.Template.Services;
using Console.Dotnet.Template.Services.Abstractions;
using Xunit;

namespace Console.Dotnet.Template.UnitTests.Services
{
    public class CalculatorServiceTests
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorServiceTests()
        {
            _calculatorService = new CalculatorService();
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(5, 5, 10)]
        [InlineData(-5, 5, 0)]
        public void Add(int a, int b, decimal result)
        {
            Assert.Equal(result, _calculatorService.Add(a, b));
        }

        [Theory]
        [InlineData(1, 1, 0)]
        [InlineData(10, 5, 5)]
        [InlineData(0, -5, 5)]
        [InlineData(10, -5, 15)]
        public void Sub(int a, int b, decimal result)
        {
            Assert.Equal(result, _calculatorService.Sub(a, b));
        }
    }
}
