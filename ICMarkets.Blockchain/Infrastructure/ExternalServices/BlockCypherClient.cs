using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices
{
    public  class BlockCypherClient
    {
        private readonly HttpClient _httpClient;

        public BlockCypherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetBlockchainDataRawAsync(string path)
        {
            // Path will be like "eth/main" or "btc/test3"
            var response = await _httpClient.GetAsync($"/v1/{path}");
            response.EnsureSuccessStatusCode();

            // Requirement 3: We must store the "Main data as provided in JSON"
            return await response.Content.ReadAsStringAsync();
        }
    }
}
