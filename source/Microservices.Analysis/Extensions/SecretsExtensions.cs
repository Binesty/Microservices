using Microservices.Analysis.Services;
using Microservices.Analysis.Settings;

namespace Microservices.Analysis.Extensions
{
    public static class SecretsExtensions
    {
        public static Secret? Loaded { get; private set; }

        public static IServiceCollection AddSecrets(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var vaultServices = serviceProvider.GetRequiredService<VaultServices>();

            Loaded = vaultServices.GetFromVault()
                                  .GetAwaiter()
                                  .GetResult();

            if (Loaded is null || Loaded.Data is null)
                throw new Exception("Secret not get from vault");

            return services;
        }
    }
}