using Microservices.Analysis.Services;
using Microservices.Analysis.Settings;
using Microsoft.Extensions.Options;

namespace Microservices.Analysis.Extensions
{
    public static class ServicesExtension
    {
        private static string? _vaultToken = "hvs.NEEecyuKLrRAb09LeX8dby6J";
        private static string? _vaultAddress = "http://vault.binesty.net";

        private static readonly SocketsHttpHandler DefaultSocketsHttpHandler = new()
        {
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
        };

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            LoadEnvironmentVariables();

            services.AddVaultServices();

            return services;
        }

        private static void LoadEnvironmentVariables()
        {
            _vaultAddress = Environment.GetEnvironmentVariable("VAULT_ADDRESS");
            _vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");
        }

        private static IServiceCollection AddVaultServices(this IServiceCollection services)
        {
            if (_vaultAddress is null || _vaultToken is null)
                throw new Exception("Vault address or token is null");

            services.AddHttpClient<VaultServices>((servicesProvider, httpClient) =>
            {
                var settings = servicesProvider.GetRequiredService<IOptions<Secret>>().Value;

                httpClient.DefaultRequestHeaders.Add("X-Vault-Token", _vaultToken);
                httpClient.BaseAddress = new Uri(_vaultAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() => { return DefaultSocketsHttpHandler; });

            return services;
        }
    }
}