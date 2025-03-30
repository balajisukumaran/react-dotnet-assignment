using MyApi.BackingServices;
using MyApi.Models;

namespace MyApi.ApplicationServices.Helpers
{
    /// <summary>
    /// Contains helper methods to handle jokes.
    /// </summary>
    public static class JokeHelper
    {
        /// <summary>
        /// Handles fetching jokes (initial and retries) while ensuring uniqueness.
        /// </summary>
        /// <param name="numberOfJokes">Number of required jokes</param>
        /// <param name="maxRetries">maximum retries allowed</param>
        /// <param name="categories">joke category</param>
        /// <param name="jsonFeed">json feed service</param>
        /// <param name="jokeList">unique final joke list</param>
        /// <returns></returns>
        public static async Task FetchUniqueJokesAsync(
            int numberOfJokes,
            int maxRetries,
            string[] categories,
            IJsonFeed jsonFeed,
            List<JokeResponse> jokeList)
        {
            int retryCount = 0;

            while (jokeList.Count < numberOfJokes && retryCount <= maxRetries)
            {
                // Only fetch the missing count
                int remaining = numberOfJokes - jokeList.Count;

                var tasks = Enumerable.Range(0, remaining)
                    .Select(jokeCount => {
                        // Randomly select a category from the provided categories
                        string selectedCategory = categories[new Random().Next(categories.Length)];
                        return jsonFeed.GetRandomJokesAsync(selectedCategory);
                    })
                    .ToList();

                // Fetch in parallel
                var results = await Task.WhenAll(tasks);

                // Check if generated jokes are unique
                ProcessJokes(results, jokeList);

                retryCount++;
            }
        }

        /// <summary>
        /// Ensuring uniqueness before adding them to the joke list.
        /// </summary>
        /// <param name="jokeResponses"></param>
        /// <param name="jokeList"></param>
        public static void ProcessJokes(JokeResponse[] jokeResponses, List<JokeResponse> jokeList)
        {
            foreach (var joke in jokeResponses)
            {
                if (!string.IsNullOrWhiteSpace(joke.Value) &&
                    !string.IsNullOrWhiteSpace(joke.IconUrl) &&
                    !jokeList.Any(existingJoke => existingJoke.Value == joke.Value && existingJoke.IconUrl == joke.IconUrl))
                {
                    jokeList.Add(joke);
                }
            }
        }
    }
}
