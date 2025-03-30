using Microsoft.AspNetCore.Mvc;
using MyApi.Controllers;

namespace MyApi.UnitTests.Controllers
{
    [TestClass]
    public class HealthControllerTests
    {

        private HealthController _controller;

        /// <summary>
        /// Initializes test dependencies.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _controller = new HealthController();
        }

        /// <summary>
        /// Tests that GetCategories returns an OK result with categories.
        /// </summary>
        [TestMethod]
        public async Task GetCategories_ReturnsOkResult_WithCategories()
        {
            // Act
            var result = _controller.Check();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
        }
    }
}
