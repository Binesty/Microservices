
using Microservices.Analysis.Domain;
using Microsoft.Extensions.Options;
using Packages.Microservices;
using System.Reflection;

namespace Microservices.Analysis
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<Settings> _settings;

        public Worker(ILogger<Worker> logger, IOptions<Settings> settings)
        {
            _logger = logger;
            _settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Microservices in execution Version: {Assembly.GetExecutingAssembly().GetName().Version}");

            await Microservice<Opportunity>.Configure(_settings)
                                           .Start();

            Console.ReadLine();
        }
    }
}