using ApiAggregator.Models;
using ApiAggregator.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiAggregator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AggregationController : ControllerBase
    {
        private readonly IApiAggregatorService _apiAggregatorService;

        public AggregationController(IApiAggregatorService apiAggregatorService)
        {
            _apiAggregatorService = apiAggregatorService;
        }

        [HttpGet("aggregated-data")]
        public async Task<IActionResult> GetAggregatedData()
        {
            try
            {
                var result = await _apiAggregatorService.GetAggregatedDataAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<AggregatedResponse>> GetAggregatedData(
        [FromQuery] string filterByTitle = null,
        [FromQuery] string filterByAuthor = null,
        [FromQuery] string filterByName = null,
        [FromQuery] string sortBy = null,
        [FromQuery] bool sortDescending = false)
        {
            var aggregatedData = await _apiAggregatorService.GetAggregatedDataAsync();

            // Filter the data
            if (!string.IsNullOrEmpty(filterByTitle))
            {
                aggregatedData.News.Articles = aggregatedData.News.Articles
                    .Where(a => a.Title.Contains(filterByTitle, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                aggregatedData.JSONPlaceholder = aggregatedData.JSONPlaceholder
                .Where(p => p.Title.Contains(filterByTitle, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }

            if (!string.IsNullOrEmpty(filterByAuthor))
            {
                aggregatedData.News.Articles = aggregatedData.News.Articles
                    .Where(a => a.Author.Contains(filterByAuthor, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            if (!string.IsNullOrEmpty(filterByName))
            {
                aggregatedData.Phones = aggregatedData.Phones
                    .Where(p => p.Name.Contains(filterByName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Sort the data
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    // Sorting News Articles
                    case "title":
                        aggregatedData.News.Articles = sortDescending
                            ? aggregatedData.News.Articles.OrderByDescending(a => a.Title).ToList()
                            : aggregatedData.News.Articles.OrderBy(a => a.Title).ToList();
                        aggregatedData.JSONPlaceholder = sortDescending
                            ? aggregatedData.JSONPlaceholder.OrderByDescending(p => p.Title).ToList()
                            : aggregatedData.JSONPlaceholder.OrderBy(p => p.Title).ToList();
                        break;

                    case "author":
                        aggregatedData.News.Articles = sortDescending
                            ? aggregatedData.News.Articles.OrderByDescending(a => a.Author).ToList()
                            : aggregatedData.News.Articles.OrderBy(a => a.Author).ToList();
                        break;

                    case "publishedat":
                        aggregatedData.News.Articles = sortDescending
                            ? aggregatedData.News.Articles.OrderByDescending(a => a.PublishedAt).ToList()
                            : aggregatedData.News.Articles.OrderBy(a => a.PublishedAt).ToList();
                        break;

                    // Sorting Phone Data
                    case "name":
                        aggregatedData.Phones = sortDescending
                            ? aggregatedData.Phones.OrderByDescending(p => p.Name).ToList()
                            : aggregatedData.Phones.OrderBy(p => p.Name).ToList();
                        break;

                    case "color":
                        aggregatedData.Phones = sortDescending
                            ? aggregatedData.Phones.OrderByDescending(p => p.Data.Color).ToList()
                            : aggregatedData.Phones.OrderBy(p => p.Data.Color).ToList();
                        break;

                    case "capacity":
                        aggregatedData.Phones = sortDescending
                            ? aggregatedData.Phones.OrderByDescending(p => p.Data.Capacity).ToList()
                            : aggregatedData.Phones.OrderBy(p => p.Data.Capacity).ToList();
                        break;

                    // Sorting JSONPlaceholder Posts
                    case "userid":
                        aggregatedData.JSONPlaceholder = sortDescending
                            ? aggregatedData.JSONPlaceholder.OrderByDescending(p => p.UserId).ToList()
                            : aggregatedData.JSONPlaceholder.OrderBy(p => p.UserId).ToList();
                        break;

                    case "postid":
                        aggregatedData.JSONPlaceholder = sortDescending
                            ? aggregatedData.JSONPlaceholder.OrderByDescending(p => p.Id).ToList()
                            : aggregatedData.JSONPlaceholder.OrderBy(p => p.Id).ToList();
                        break;
                }
            }

            return Ok(aggregatedData);
        }

    }
}
