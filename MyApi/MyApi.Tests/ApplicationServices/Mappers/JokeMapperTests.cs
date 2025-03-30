using MyApi.ApplicationServices.Mappers;
using MyApi.Models;

namespace MyApi.UnitTests.ApplicationServices.Mappers
{
    /// <summary>
    /// Contains tests for the JokeMapper.
    /// </summary>
    [TestClass]
    public class JokeMapperTests
    {
        /// <summary>
        /// Tests that ToJokeDtoList correctly maps a list of JokeResponse objects to a list of JokeDto objects.
        /// </summary>
        [TestMethod]
        public void ToJokeDtoList_ShouldMapCorrectly()
        {
            // Arrange
            var jokeResponses = new List<JokeResponse>
            {
                new JokeResponse { IconUrl = "icon1", Value = "Joke 1" },
                new JokeResponse { IconUrl = "icon2", Value = "Joke 2" }
            };

            // Act
            var result = JokeMapper.ToJokeDtoList(jokeResponses);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            for (int i = 0; i < jokeResponses.Count; i++)
            {
                Assert.AreEqual(jokeResponses[i].IconUrl, result[i].IconUrl);
                Assert.AreEqual(jokeResponses[i].Value, result[i].Value);
            }
        }
    }
}
