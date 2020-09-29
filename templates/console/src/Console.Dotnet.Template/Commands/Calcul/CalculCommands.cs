using Cocona;
using Console.Dotnet.Template.Services.Abstractions;
using Microsoft.Extensions.Logging;
using System;

namespace Console.Dotnet.Template.Commands.Calcul
{
    public class CalculCommands
    {
        private readonly ILogger<CalculCommands> _logger;
        private readonly ICalculatorService _calculatorService;

        public CalculCommands(ILogger<CalculCommands> logger, ICalculatorService calculatorService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _calculatorService = calculatorService ?? throw new ArgumentNullException(nameof(calculatorService));
        }

        [Command(nameof(Add), Description = "Add two values")]
        public void Add(int a, int b)
        {
            _logger.LogInformation(_calculatorService.Add(a, b).ToString());
        }

        [Command(nameof(Sub), Description = "Sub two values")]
        public void Sub(int a, int b)
        {
            _logger.LogInformation(_calculatorService.Sub(a, b).ToString());
        }
    }
}
