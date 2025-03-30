using MyApi.Models;

namespace MyApi.BackingServices
{
    /// <summary>
    /// Defines methods for retrieving joke data from a JSON-based data source.
    /// </summary>
    public interface IJsonFeed
    {
        /// <summary>
        /// Retrieves a list of joke categories from the JSON feed asynchronously.
        /// </summary>
        /// <returns>a list of joke categories.</returns>
        Task<LinkedList<string>> GetCategoriesAsync();

        /// <summary>
        /// Retrieves random joke for the specified category from the JSON feed asynchronously.
        /// </summary>
        /// <param name="category">The category from which to retrieve jokes.</param>
        /// <returns>>Retrieved joke</returns>
        Task<JokeResponse?> GetRandomJokesAsync(string category);
    }
}
