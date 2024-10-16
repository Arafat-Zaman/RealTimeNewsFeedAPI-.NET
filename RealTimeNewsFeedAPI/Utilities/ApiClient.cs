using Newtonsoft.Json.Linq;
using RealTimeNewsFeedAPI.Models;

namespace RealTimeNewsFeedAPI.Utilities
{
    public static class ApiClient
    {
        public static async Task<IEnumerable<NewsArticle>> ParseNewsApiResponse(HttpContent content)
        {
            var jsonString = await content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(jsonString);
            var articles = jsonObject["articles"]
                            .Select(a => new NewsArticle
                            {
                                Title = (string)a["title"],
                                Description = (string)a["description"],
                                Url = (string)a["url"],
                                Source = (string)a["source"]["name"],
                                PublishedAt = (DateTime)a["publishedAt"]
                            })
                            .ToList();
            return articles;
        }
    }
}
