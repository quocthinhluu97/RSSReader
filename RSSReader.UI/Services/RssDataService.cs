using RSSReader.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSSReader.UI.Services
{
    public class RssDataService : IRssDataService
    {
        private readonly HttpClient _httpClient;

        public RssDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Post>> GetNewsFeed()
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<Post>>
                    (await _httpClient.GetStreamAsync($"api/rss"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception: {e.Message} {e.InnerException}");
            }
            return default;
        }
    }
}
