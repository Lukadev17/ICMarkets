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
            _httpClient.BaseAddress = new Uri("https://api.blockcypher.com/v1/");
        }

        

        public async Task<string> GetRawJsonAsync(string path)
        {
            var response = await _httpClient.GetAsync(path);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
