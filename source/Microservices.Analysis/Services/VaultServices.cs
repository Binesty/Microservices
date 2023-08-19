using Microservices.Analysis.Settings;
using System.Net.Http.Json;

namespace Microservices.Analysis.Services
{
    public class VaultServices
    {
        private readonly HttpClient _httpClient;

        public VaultServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Secret> GetFromVault()
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT") ?? "development";

            var secret = await _httpClient.GetFromJsonAsync<Secret>($"v1/{environment}/api-catalogs");

            return secret ?? throw new Exception("Not found secrets");
        }
    }
}