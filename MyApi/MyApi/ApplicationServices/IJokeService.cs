using MyApi.Dtos;

namespace MyApi.ApplicationServices
{
    /// <summary>
    /// Provides methods for retrieving jokes and joke categories.
    /// </summary>
    public interface IJokeService
    {
        /// <summary>
        /// Retrieves a list of joke categories asynchronously.
        /// </summary>
        /// <returns>objects representing available joke categories</returns>
        Task<List<CategoryDto>> GetCategoriesAsync();

        /// <summary>
        /// Retrieves a list of jokes for a specified category and quantity.
        /// </summary>
        /// <param name="category">The category of jokes to retrieve.</param>
        /// <param name="number">The number of jokes to retrieve.</param>
        /// <returns>list of jokes</returns>
        Task<List<JokeDto>> GetJokesAsync(string category, int number);
    }
}
