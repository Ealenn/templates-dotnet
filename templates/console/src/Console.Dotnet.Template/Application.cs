using Cocona;
using Console.Dotnet.Template.Commands.Calcul;
using Microsoft.Extensions.Logging;
using System;

namespace Console.Dotnet.Template
{
    [HasSubCommands(typeof(CalculCommands), "calcul", Description = "Manipulate numbers")]
    public class Application
    {
        private readonly ILogger<Application> _logger;

        public Application(ILogger<Application> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [PrimaryCommand]
        public void PrimaryCommand()
        {
            _logger.LogInformation("To show help message, use '--help' option.");
        }
    }
}
