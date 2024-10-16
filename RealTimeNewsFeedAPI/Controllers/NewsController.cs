using Microsoft.AspNetCore.Mvc;
using RealTimeNewsFeedAPI.Services;

namespace RealTimeNewsFeedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsService _newsService;

        public NewsController(NewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestNews()
        {
            var news = await _newsService.GetLatestNewsAsync();
            if (news.Any())
                return Ok(news);
            return NoContent();
        }
    }
}
