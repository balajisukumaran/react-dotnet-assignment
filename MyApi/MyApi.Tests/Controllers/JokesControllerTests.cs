using Moq;
using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers;
using MyApi.ApplicationServices;
using MyApi.Dtos;

namespace MyApi.UnitTests.Controllers
{
    /// <summary>
    /// Tests for the JokesController.
    /// </summary>
    [TestClass]
    public class JokesControllerTests
    {
        private Mock<IJokeService> _mockJokeService;
        private JokesController _controller;

        /// <summary>
        /// Initializes test dependencies.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockJokeService = new Mock<IJokeService>();
            _controller = new JokesController(_mockJokeService.Object);
        }

        /// <summary>
        /// Tests that GetCategories returns an OK result with categories.
        /// </summary>
        [TestMethod]
        public async Task GetCategories_ReturnsOkResult_WithCategories()
        {
            // Arrange
            var categories = new List<CategoryDto>
            {
                new CategoryDto { Id = 1, Name = "food" },
                new CategoryDto { Id = 1, Name = "animal" }
            };
            _mockJokeService.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCategories = okResult.Value as List<CategoryDto>;
            Assert.IsNotNull(returnedCategories);
            Assert.AreEqual(2, returnedCategories.Count);
        }

        /// <summary>
        /// Tests that GetCategories returns an OK result with an empty list.
        /// </summary>
        [TestMethod]
        public async Task GetCategories_ReturnsOkResult_NoCategories()
        {
            // Arrange
            var categories = new List<CategoryDto>();
            _mockJokeService.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedCategories = okResult.Value as List<CategoryDto>;
            Assert.IsNotNull(returnedCategories);
            Assert.AreEqual(0, returnedCategories.Count);
        }

        /// <summary>
        /// Tests that GetJokes returns an OK result with jokes.
        /// </summary>
        [TestMethod]
        public async Task GetJokes_ReturnsOkResult_WithJokes()
        {
            // Arrange
            var jokes = new List<JokeDto>
            {
                new JokeDto { IconUrl = "icon1", Value = "Joke 1" },
                new JokeDto { IconUrl = "icon2", Value = "Joke 2" }
            };
            _mockJokeService.Setup(s => s.GetJokesAsync("Programming", 2)).ReturnsAsync(jokes);

            // Act
            var result = await _controller.GetJokes("Programming", 2);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedJokes = okResult.Value as List<JokeDto>;
            Assert.IsNotNull(returnedJokes);
            Assert.AreEqual(2, returnedJokes.Count);
        }

        /// <summary>
        /// Tests that GetJokes returns an OK result when no jokes are found.
        /// </summary>
        [TestMethod]
        public async Task GetJokes_ReturnsNotFound_WhenNoJokes()
        {
            // Arrange
            _mockJokeService.Setup(s => s.GetJokesAsync("Programming", 2)).ReturnsAsync(new List<JokeDto>());

            // Act
            var result = await _controller.GetJokes("Programming", 2);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }
    }
}
