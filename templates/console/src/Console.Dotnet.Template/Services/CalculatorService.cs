using Console.Dotnet.Template.Services.Abstractions;

namespace Console.Dotnet.Template.Services
{
    public class CalculatorService : ICalculatorService
    {
        public decimal Add(int a, int b)
        {
            return a + b;
        }

        public decimal Sub(int a, int b)
        {
            return a - b;
        }
    }
}
