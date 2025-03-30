using Moq;
using Moq.Protected;
using MyApi.BackingServices;
using MyApi.Models;
using Microsoft.Extensions.Configuration;
using System.Net;
using Newtonsoft.Json;

namespace MyApi.UnitTests.BackingServices
{
    /// <summary>
    /// Tests for JsonFeed.
    /// </summary>
    [TestClass]
    public class JsonFeedTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;
        private Mock<IConfiguration> _mockConfiguration;
        private JsonFeed _jsonFeed;

        /// <summary>
        /// Sets up test dependencies.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["ApiEndpoints:JokeApi"]).Returns("https://test/jokes");
            _mockConfiguration.Setup(c => c["ApiEndpoints:CategoryApi"]).Returns("https://test/categories");

            _jsonFeed = new JsonFeed(_httpClient, _mockConfiguration.Object);
        }

        /// <summary>
        /// Tests that GetCategoriesAsync returns a category list.
        /// </summary>
        [TestMethod]
        public async Task GetCategoriesAsync_ShouldReturnCategoryList()
        {
            // Arrange
            var mockCategories = new List<string> { "Tech", "Science", "General" };
            var mockResponse = JsonConvert.SerializeObject(mockCategories);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockResponse)
                });

            // Act
            var result = await _jsonFeed.GetCategoriesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            CollectionAssert.AreEqual(mockCategories, result);
        }

        /// <summary>
        /// Tests that GetRandomJokesAsync returns a valid JokeResponse.
        /// </summary>
        [TestMethod]
        public async Task GetRandomJokesAsync_ShouldReturnJokeResponse()
        {
            // Arrange
            var category = "Tech";
            var mockJoke = new JokeResponse { Value = "Why do programmers prefer dark mode? Because light attracts bugs." };
            var mockResponse = JsonConvert.SerializeObject(mockJoke);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(mockResponse)
                });

            // Act
            var result = await _jsonFeed.GetRandomJokesAsync(category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(mockJoke.Value, result.Value);
        }
    }
}
