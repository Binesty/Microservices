using Packages.Microservices;
using Packages.Microservices.Extensions;

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

                    services.AddOptions<Settings>()
                            .BindConfiguration(Settings.SectionName)
                            .AddPackagesMicroservices()
                            .ValidateFluently()
                            .ValidateOnStart();
                })
                .Build();

            host.Run();
        }
    }
}