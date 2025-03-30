using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Moq;
using MyApi.BackingServices;
using MyApi.Models;

namespace MyApi.IntegrationTests
{
    /// <summary>
    /// Factory for creating test instances of the web application with mocked dependencies.
    /// </summary>
    public class MockWebApplicationFactory
    {
        public static (WebApplicationFactory<Program>, Mock<IJsonFeed>) Create(string environment)
        {
            var mockJsonFeed = CreateMockJsonFeed();

            var factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment(environment);
                    builder.ConfigureServices(services =>
                    {
                        // Remove the real implementation
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(IJsonFeed));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        // Add the mock
                        services.AddSingleton<IJsonFeed>(mockJsonFeed.Object);
                    });
                });

            return (factory, mockJsonFeed);
        }

        /// <summary>
        /// Creates and configures a mock IJsonFeed with standard test responses.
        /// </summary>
        /// <returns>Configured Mock<IJsonFeed></returns>
        private static Mock<IJsonFeed> CreateMockJsonFeed()
        {
            var mockJsonFeed = new Mock<IJsonFeed>();
            var categories = new List<string> { "animal", "programming", "dad" };
            // Setup mock responses
            mockJsonFeed.Setup(m => m.GetCategoriesAsync())
                .ReturnsAsync(new LinkedList<string>(categories));

            mockJsonFeed.Setup(m => m.GetRandomJokesAsync("animal"))
                .ReturnsAsync(new JokeResponse
                {
                    IconUrl = "https://test_url",
                    Value = "Joke 1"
                });

            mockJsonFeed.Setup(m => m.GetRandomJokesAsync("asdasd"))
                .ThrowsAsync(new Exception("Category not found"));

            return mockJsonFeed;
        }
    }
}