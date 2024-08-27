using ApiAggregator.Models;
using Newtonsoft.Json;


namespace ApiAggregator.Services
{
    public class ApiAggregatorService : IApiAggregatorService 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ApiAggregatorService> _logger;

        public ApiAggregatorService(IHttpClientFactory httpClientFactory, ILogger<ApiAggregatorService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<AggregatedResponse> GetAggregatedDataAsync()
        {
            var phoneTask = GetPhoneDataAsync();
            var newsTask = GetNewsDataAsync(); 
            var jsonPlaceholderTask = GetJSONPlaceholderDataAsync();  

            await Task.WhenAll(phoneTask, newsTask, jsonPlaceholderTask);

            return new AggregatedResponse
            {
                // Assign the fetched Data
                Phones = phoneTask.Result,
                News = newsTask.Result,
                JSONPlaceholder = jsonPlaceholderTask.Result
            };
        }

        // Method for fetching Data from Phones API
        private async Task<List<PhoneModels>> GetPhoneDataAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://api.restful-api.dev/objects");
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful (200-299)
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<PhoneModels>>(content);
            }
            catch (HttpRequestException httpEx)
            {
                // Log the detailed error message
                _logger.LogError($"Phone API request error: {httpEx.Message}");
                //Console.WriteLine($"Request error: {httpEx.Message}");
                //Console.WriteLine($"Status Code: {httpEx.StatusCode}");
                // Return an empty list as a fallback
                return new List<PhoneModels>
            {
                new PhoneModels
                {
                     Id = "fallback",
                    Name = "Fallback Phone",
                    Data = new ApiObjectData
                    {
                        Color = "Unknown",
                        Capacity = "N/A"
                    }
                }
            };
            }

            catch (Exception ex)
            {
                // Log the detailed error message
                _logger.LogError($"Phone API request error: {ex.Message}");
                //Console.WriteLine($"Unexpected error: {ex.Message}");
                //Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                // Return an empty list as a fallback
                return new List<PhoneModels>
                {
                    new PhoneModels
                    {
                        Id = "fallback",
                        Name = "Fallback Phone",
                        Data = new ApiObjectData
                        {
                            Color = "Unknown",
                            Capacity = "N/A"
                        }
                    }
                };
            }
        }

            // Method for fetching Data from News Data API
            private async Task<NewsResponse> GetNewsDataAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://newsapi.org/v2/everything?q=apple&from=2024-08-25&to=2024-08-25&sortBy=popularity&apiKey=28f6bb625b5d48edb5469a1222611eac");
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful (200-299)
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<NewsResponse>(content);
            }
            catch (HttpRequestException httpEx)
            {
                // Log the detailed error message
                _logger.LogError($"News service API request error: {httpEx.Message}");
                // Return a fallback response
                return new NewsResponse
                {
                    Articles = new List<Article>
            {
                new Article
                {
                    Source = new Source { Id = "unknown", Name = "Service Unavailable" },
                    Author = "N/A",
                    Title = "News service unavailable",
                    Description = "The news service is currently unavailable. Please try again later.",
                    Url = "",
                    UrlToImage = "",
                    PublishedAt = DateTime.UtcNow,
                    Content = "No content available"
                }
            }
                };
            }
            catch (Exception ex)
            {
                // Log the detailed error message
                _logger.LogError($"News service API request error: {ex.Message}");
                // Return a fallback response
                return new NewsResponse
                {
                    Articles = new List<Article>
            {
                new Article
                {
                    Source = new Source { Id = "unknown", Name = "Service Unavailable" },
                    Author = "N/A",
                    Title = "News service unavailable",
                    Description = "An unexpected error occurred. Please try again later.",
                    Url = "",
                    UrlToImage = "",
                    PublishedAt = DateTime.UtcNow,
                    Content = "No content available"
                }
            }
                };
            }
        }

        //Method for fetching Data from JSONPlaceholder API
        private async Task<List<Post>> GetJSONPlaceholderDataAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                response.EnsureSuccessStatusCode(); // Throws an exception if the status code is not successful (200-299)
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Post>>(content);
            }
            catch (HttpRequestException httpEx)
            {
                // Log the detailed error message
                _logger.LogError($"JSONPlaceholder service API request error: {httpEx.Message}");
                // Return an empty list as a fallback
                return new List<Post>
                {
                    new Post
                    {
                     UserId = 0,
                     Id = 0,
                     Title = "Fallback Post",
                     Body = "The JSONPlaceholder service is currently unavailable. This is fallback content."
                     }
                };
            }
            catch (Exception ex)
            {
                // Log the detailed error message
                _logger.LogError($"JSONPlaceholder service API request error: {ex.Message}");
                // Return an empty list as a fallback
                return new List<Post>
                {
                    new Post
                    {
                     UserId = 0,
                     Id = 0,
                     Title = "Fallback Post",
                     Body = "The JSONPlaceholder service is currently unavailable. This is fallback content."
                     }
                };
            }
        }
    }
}


