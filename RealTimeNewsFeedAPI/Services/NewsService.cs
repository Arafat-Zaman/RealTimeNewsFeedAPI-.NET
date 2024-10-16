using Microsoft.Extensions.Caching.Memory;
using RealTimeNewsFeedAPI.Models;
using RealTimeNewsFeedAPI.Utilities;

namespace RealTimeNewsFeedAPI.Services
{
    public class NewsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey = "Your-Third-Party-News-API-Key";
        private readonly string _newsApiUrl = "https://newsapi.org/v2/top-headlines?country=us&apiKey=";

        public NewsService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public async Task<IEnumerable<NewsArticle>> GetLatestNewsAsync()
        {
            if (!_cache.TryGetValue("LatestNews", out IEnumerable<NewsArticle> cachedNews))
            {
                // Fetch news from API if not in cache
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"{_newsApiUrl}{_apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    var newsData = await ApiClient.ParseNewsApiResponse(response.Content);
                    _cache.Set("LatestNews", newsData, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
                    return newsData;
                }
                return Enumerable.Empty<NewsArticle>();
            }

            return cachedNews; // Return cached news
        }
    }
}
