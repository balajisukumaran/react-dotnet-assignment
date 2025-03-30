using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using MyApi.BackingServices;
using MyApi.Dtos;
using System.Text.Json;

namespace MyApi.IntegrationTests
{
    /// <summary>
    /// Integration tests for the Production environment.
    /// </summary>
    [TestClass]
    public class ProductionIntegrationTest
    {
        /// <summary>
        /// Contains integration tests for the jokes endpoints.
        /// </summary>
        [TestClass]
        public class JokesIntegrationTests
        {
            private WebApplicationFactory<Program> _factory;
            private HttpClient _client;
            private Mock<IJsonFeed> _mockJsonFeed;

            /// <summary>
            /// Sets up the test environment by creating an in-memory instance of the application in Production.
            /// </summary>
            [TestInitialize]
            public void Setup()
            {
                var (factory, mock) = MockWebApplicationFactory.Create("Production");
                _factory = factory;
                _mockJsonFeed = mock;
                _client = _factory.CreateClient();
            }

            /// <summary>
            /// Tests that the GET categories endpoint returns a successful response with a non-empty list.
            /// </summary>
            [TestMethod]
            public async Task GetCategories_Production_ReturnsOkAndNonEmpty()
            {
                // Act
                var response = await _client.GetAsync("api/jokes/categories");

                // Assert
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<CategoryDto>>(body);
                Assert.IsNotNull(categories);
                Assert.IsTrue(categories.Count > 0, "Expected at least one category in Production environment.");

                _mockJsonFeed.Verify(m => m.GetCategoriesAsync(), Times.Once);
            }

            /// <summary>
            /// Tests that the GET jokes endpoint returns a successful response with one joke.
            /// </summary>
            [TestMethod]
            public async Task GetJokes_Production_ReturnsOkAndNonEmpty()
            {
                // Act
                var response = await _client.GetAsync("api/jokes/animal/1");

                // Assert
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var jokes = JsonSerializer.Deserialize<List<JokeDto>>(body);
                Assert.IsNotNull(jokes);
                Assert.AreEqual(jokes.Count, 1);

                _mockJsonFeed.Verify(m => m.GetRandomJokesAsync("animal"), Times.Once);
            }

            [TestMethod]
            public async Task RateLimiting_Production_Returns429AfterLimit()
            {
                //Arrange
                const int allowedRequests = 10;
                HttpResponseMessage lastResponse = null;

                //Act
                for (int i = 0; i < allowedRequests + 1; i++)
                {
                    lastResponse = await _client.GetAsync("api/jokes/categories");
                }

                // Assert
                Assert.AreEqual((int)HttpStatusCode.TooManyRequests, (int)lastResponse.StatusCode,
                    "Expected a 429 Too Many Requests response after exceeding the rate limit.");
            }

            /// <summary>
            /// Cleans up resources after tests by disposing of the HTTP client and web application factory.
            /// </summary>
            [TestCleanup]
            public void Cleanup()
            {
                _client.Dispose();
                _factory.Dispose();
            }
        }
    }
}