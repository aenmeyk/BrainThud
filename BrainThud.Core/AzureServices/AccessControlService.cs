using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BrainThud.Core.AzureServices
{
    public class AccessControlService
    {
        private readonly HttpClient httpClient;

        public AccessControlService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<IdentityProvider>> GetIdentityProviders()
        {
            var response = await httpClient.GetAsync(Urls.IDENTITY_PROVIDERS);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<IdentityProvider>>(json);
        }
    }
}