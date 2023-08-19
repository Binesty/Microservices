using Microservices.Analysis.Extensions;

namespace Microservices.Analysis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<Worker>();

                    services.AddServices()
                            .AddSecrets();
                })
                .Build();

            host.Run();
        }
    }
}