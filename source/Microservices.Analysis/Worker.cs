using Microservices.Analysis.Services;

namespace Microservices.Analysis
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQServices _rabbitMQServices;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _rabbitMQServices = new();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {
                _rabbitMQServices.StartGetMessages(_logger);
            },
            stoppingToken);
        }
    }
}