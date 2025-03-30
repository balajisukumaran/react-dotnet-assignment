using Moq;
using MyApi.ApplicationServices;
using MyApi.BackingServices;
using Microsoft.Extensions.Configuration;
using MyApi.Models;

namespace MyApi.UnitTests.ApplicationServices
{
    /// <summary>
    /// Tests for JokeService.
    /// </summary>
    [TestClass]
    public class JokeServiceTests
    {
        private Mock<IJsonFeed> _mockRepository;
        private Mock<IConfiguration> _mockConfiguration;
        private JokeService _jokeService;

        /// <summary>
        /// Sets up mocks and creates an instance of JokeService.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            mockConfigurationSection.Setup(x => x.Value).Returns("3");

            _mockRepository = new Mock<IJsonFeed>();

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(x => x.GetSection("ApiEndpoints:MaxRetries"))
                  .Returns(mockConfigurationSection.Object);

            _jokeService = new JokeService(_mockRepository.Object, _mockConfiguration.Object);
        }

        /// <summary>
        /// Tests GetCategoriesAsync returns a category list.
        /// </summary>
        [TestMethod]
        public async Task GetCategoriesAsync_ShouldReturnCategoryList()
        {
            // Arrange
            var categories = new List<string> { "Animal", "Programming", "General" };
            var categoriesResult = new List<string> { "any", "Animal", "Programming", "General" };

            _mockRepository.Setup(r => r.GetCategoriesAsync()).ReturnsAsync(new LinkedList<string>(categories));

            // Act
            var result = await _jokeService.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
            CollectionAssert.AreEqual(categoriesResult, result.Select(c => c.Name).ToList());
        }

        /// <summary>
        /// Tests GetJokesAsync returns a joke list.
        /// </summary>
        [TestMethod]
        public async Task GetJokesAsync_ShouldReturnJokeList()
        {
            // Arrange
            var category = "Programming";
            var number = 2;
            var jokeResponse = new JokeResponse { IconUrl = "1", Value = "asdasda." };
            List<JokeResponse> jokeResponses = new List<JokeResponse> { jokeResponse };
            _mockRepository.Setup(repo => repo.GetRandomJokesAsync(category)).ReturnsAsync(jokeResponse);

            // Act
            var result = await _jokeService.GetJokesAsync(category, number);

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(jokeResponses.Select(j => j.Value).ToList(), result.Select(j => j.Value).ToList());
        }
    }
}
