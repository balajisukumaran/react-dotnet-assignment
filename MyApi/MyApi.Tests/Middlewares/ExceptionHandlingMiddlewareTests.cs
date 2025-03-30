using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyApi.Dtos;

namespace MyApi.UnitTests.Middlewares
{
    /// <summary>
    /// Tests for ExceptionMiddleware.
    /// </summary>
    [TestClass]
    public class ExceptionMiddlewareTests
    {
        private Mock<RequestDelegate> _mockNext;
        private Mock<ILogger<ExceptionMiddleware>> _mockLogger;
        private ExceptionMiddleware _middleware;

        /// <summary>
        /// Sets up test dependencies.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockNext = new Mock<RequestDelegate>();
            _mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
            _middleware = new ExceptionMiddleware(_mockNext.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests that Invoke returns a 500 error when an exception occurs.
        /// </summary>
        [TestMethod]
        public async Task Invoke_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            _mockNext.Setup(n => n(It.IsAny<HttpContext>())).Throws(new Exception("Test Exception"));
            var context = new DefaultHttpContext();
            var responseStream = new MemoryStream();
            context.Response.Body = responseStream;

            // Act
            await _middleware.Invoke(context);

            // Assert
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
            responseStream.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(responseStream, Encoding.UTF8);
            var responseJson = await reader.ReadToEndAsync();
            var responseObject = JsonSerializer.Deserialize<ErrorDto>(responseJson);

            Assert.IsNotNull(responseObject);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, responseObject.StatusCode);
            Assert.AreEqual("An internal server error occurred. Please try again later.", responseObject.Message);
        }
    }
}
