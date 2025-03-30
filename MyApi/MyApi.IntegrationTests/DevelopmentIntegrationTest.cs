using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using MyApi.BackingServices;
using MyApi.Dtos;
using System.Text.Json;

namespace MyApi.IntegrationTests
{
    /// <summary>
    /// Integration tests for the Development environment.
    /// </summary>
    [TestClass]
    public class DevelopmentIntegrationTest
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
            /// Sets up the test environment by initializing the web application factory and HTTP client.
            /// </summary>
            [TestInitialize]
            public void Setup()
            {
                var (factory, mock) = MockWebApplicationFactory.Create("Development");
                _factory = factory;
                _mockJsonFeed = mock;
                _client = _factory.CreateClient();
            }

            /// <summary>
            /// Tests that getting joke categories returns a successful response with a non-empty list.
            /// </summary>
            [TestMethod]
            public async Task GetCategories_Development_ReturnsOkAndNonEmpty()
            {
                // Act
                var response = await _client.GetAsync("api/jokes/categories");

                // Assert
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<CategoryDto>>(body);
                Assert.IsNotNull(categories);
                Assert.IsTrue(categories.Count > 0, "Expected at least one category in Development environment.");

                _mockJsonFeed.Verify(m => m.GetCategoriesAsync(), Times.Once);
            }

            /// <summary>
            /// Tests that getting jokes for a valid category returns a successful response with one joke.
            /// </summary>
            [TestMethod]
            public async Task GetJokes_Development_ReturnsOkAndNonEmpty()
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

            /// <summary>
            /// Tests that getting jokes with an invalid category returns an error response.
            /// </summary>
            [TestMethod]
            public async Task GetJokes_Development_Error()
            {
                // Act
                var response = await _client.GetAsync("api/jokes/asdasd/1");

                // Assert
                var body = await response.Content.ReadAsStringAsync();
                var error = JsonSerializer.Deserialize<ErrorDto>(body);
                Assert.IsNotNull(error);
                Assert.AreEqual(error.StatusCode, 500);
                Assert.AreEqual(error.Message, "An internal server error occurred. Please try again later.");

                _mockJsonFeed.Verify(m => m.GetRandomJokesAsync("asdasd"), Times.Once);
            }

            /// <summary>
            /// Cleans up resources by disposing the HTTP client and web application factory.
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