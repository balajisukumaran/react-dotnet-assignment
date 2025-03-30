using MyApi.BackingServices;
using MyApi.Dtos;
using MyApi.Models;
using MyApi.ApplicationServices.Helpers;
using MyApi.ApplicationServices.Mappers;
using System.Collections.Generic;

namespace MyApi.ApplicationServices
{
    /// <summary>
    /// Provides services for retrieving jokes and joke categories.
    /// </summary>
    public class JokeService : IJokeService
    {
        /// <summary>
        /// A JSON feed interface used to retrieve joke data and categories.
        /// </summary>
        private readonly IJsonFeed _jsonFeed;

        /// <summary>
        /// The application configuration used to retrieve settings (e.g., max retries).
        /// </summary>
        private readonly int _maxRetries;

        /// <summary>
        /// Initializes a new instance of the JokeService class.
        /// </summary>
        /// <param name="jsonFeed">The IJssonFeed implementation for retrieving jokes and categories.</param>
        /// <param name="configuration">The instance for accessing app settings.</param>
        public JokeService(IJsonFeed jsonFeed, IConfiguration configuration)
        {
            _jsonFeed = jsonFeed;
            _maxRetries = configuration?.GetValue<int>("ApiEndpoints:MaxRetries") ?? 0;
        }

        /// <summary>
        /// Retrieves a list of available joke categories asynchronously.
        /// </summary>
        /// <returns>a list of CategoryDto objects representing joke categories</returns>
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            LinkedList<string> rawCategories = await _jsonFeed.GetCategoriesAsync();
            rawCategories.AddFirst("any");

            return CategoryMapper.ToCategoryDtoList(rawCategories);
        }

        /// <summary>
        /// Retrieves a specified number of jokes for a given category asynchronously.
        /// </summary>
        /// <param name="category">The category from which to retrieve jokes.</param>
        /// <param name="number">The number of jokes to retrieve.</param>
        /// <returns>a list of JokeDto objects representing the jokes.</returns>
        public async Task<List<JokeDto>> GetJokesAsync(string category, int number)
        {
            var jokeList = new List<JokeResponse>();
            string[] categories;

            if (category.Equals("any"))
            {
                LinkedList<string> rawCategories = await _jsonFeed.GetCategoriesAsync();
                categories = rawCategories.ToArray();
            }
            else
                categories = new string[] { category };

            await JokeHelper.FetchUniqueJokesAsync(number, _maxRetries, categories, _jsonFeed, jokeList);

            return Mappers.JokeMapper.ToJokeDtoList(jokeList.Take(number).ToList());
        }
    }
}
