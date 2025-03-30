using Moq;
using MyApi.ApplicationServices.Helpers;
using MyApi.BackingServices;
using MyApi.Models;

namespace MyApi.UnitTests.ApplicationServices.Helpers
{
    /// <summary>
    /// Contains unit tests for the JokeHelper class.
    /// </summary>
    [TestClass]
    public class JokeHelperTests
    {
        private Mock<IJsonFeed> _mockRepository;

        /// <summary>
        /// Initializes the test setup by creating a mock repository.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IJsonFeed>();
        }

        /// <summary>
        /// Tests that FetchUniqueJokesAsync fetches the expected number of unique jokes.
        /// </summary>
        [TestMethod]
        public async Task FetchUniqueJokesAsync_ShouldFetchUniqueJokes()
        {
            // Arrange
            var category = "Programming";
            int numberOfJokes = 3;
            int maxRetries = 2;
            var jokeList = new List<JokeResponse>();

            var jokes = new List<JokeResponse>
            {
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 1" },
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 2" },
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 3" }
            };

            _mockRepository.SetupSequence(repo => repo.GetRandomJokesAsync(category))
                .ReturnsAsync(jokes[0])
                .ReturnsAsync(jokes[1])
                .ReturnsAsync(jokes[2]);

            // Act
            await JokeHelper.FetchUniqueJokesAsync(numberOfJokes, maxRetries, new string[] { category }, _mockRepository.Object, jokeList);

            // Assert
            Assert.AreEqual(numberOfJokes, jokeList.Count);
        }


        /// <summary>
        /// Tests that FetchUniqueJokesAsync fetches the expected number of unique jokes.
        /// </summary>
        [TestMethod]
        public async Task FetchUniqueJokesAsync_ShouldFetchUniqueJokes_RetryZero()
        {
            // Arrange
            var category = "Programming";
            int numberOfJokes = 3;
            int maxRetries = 0;
            var jokeList = new List<JokeResponse>();

            var jokes = new List<JokeResponse>
            {
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 1" },
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 2" },
                new JokeResponse { IconUrl = "testUrl", Value = "Joke 3" }
            };

            _mockRepository.SetupSequence(repo => repo.GetRandomJokesAsync(category))
                .ReturnsAsync(jokes[0])
                .ReturnsAsync(jokes[1])
                .ReturnsAsync(jokes[2]);

            // Act
            await JokeHelper.FetchUniqueJokesAsync(numberOfJokes, maxRetries, new string[] { category }, _mockRepository.Object, jokeList);

            // Assert
            Assert.AreEqual(numberOfJokes, jokeList.Count);
        }

        /// <summary>
        /// Tests that ProcessJokes filters out duplicate jokes and retains only unique jokes.
        /// </summary>
        [TestMethod]
        public void ProcessJokes_ShouldFilterUniqueJokes()
        {
            // Arrange
            var jokeResponses = new JokeResponse[]
            {
                new JokeResponse {IconUrl = "testUrl",  Value = "Joke A" },
                new JokeResponse {IconUrl = "testUrl",  Value = "Joke B" },
                new JokeResponse {IconUrl = "testUrl",  Value = "Joke A" }
            };
            var jokeList = new List<JokeResponse>();

            // Act
            JokeHelper.ProcessJokes(jokeResponses, jokeList);

            // Assert
            Assert.AreEqual(2, jokeList.Count);
        }
    }
}
