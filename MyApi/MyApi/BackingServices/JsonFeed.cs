using MyApi.Models;
using Newtonsoft.Json;

namespace MyApi.BackingServices
{
    /// <summary>
    /// Retrieves joke data from an external JSON API.
    /// </summary>
    public class JsonFeed : IJsonFeed
    {
        private readonly HttpClient _httpClient;
        private readonly string? _jokeApiUrl;
        private readonly string? _categoryApiUrl;

        /// <summary>
        /// Initializes the JSON feed with an HTTP client and configuration settings.
        /// </summary>
        /// <param name="httpClient">The client used to make HTTP requests.</param>
        /// <param name="configuration">Provides the API endpoint URLs.</param>
        public JsonFeed(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _jokeApiUrl = configuration["ApiEndpoints:JokeApi"];
            _categoryApiUrl = configuration["ApiEndpoints:CategoryApi"];
        }

        /// <summary>
        /// Gets a list of joke categories.
        /// </summary>
        /// <returns>A list of category names.</returns>
        public async Task<LinkedList<string>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetStringAsync(_categoryApiUrl);
            return JsonConvert.DeserializeObject<LinkedList<string>>(response)
                   ?? new LinkedList<string>();
        }

        /// <summary>
        /// Gets random jokes from a specific category.
        /// </summary>
        /// <param name="category">The category of jokes to retrieve.</param>
        /// <returns>A response containing the jokes.</returns>
        public async Task<JokeResponse?> GetRandomJokesAsync(string category)
        {
            var response = await _httpClient.GetStringAsync($"{_jokeApiUrl}?category={category}");
            return JsonConvert.DeserializeObject<JokeResponse>(response);
        }
    }
}
