using MyApi.ApplicationServices.Mappers;

namespace MyApi.UnitTests.ApplicationServices.Mappers
{
    /// <summary>
    /// Contains tests for the CategoryMapper methods.
    /// </summary>
    [TestClass]
    public class CategoriesMapperTests
    {
        /// <summary>
        /// Tests that ToCategoryDtoList correctly maps a list of category names
        /// to a list of category DTOs with proper Ids and names.
        /// </summary>
        [TestMethod]
        public void ToCategoryDtoList_ShouldMapCorrectly()
        {
            // Arrange
            var categories = new List<string> { "Funny", "Tech", "Science" };

            // Act
            var result = CategoryMapper.ToCategoryDtoList(new LinkedList<string>(categories));

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            for (int i = 0; i < categories.Count; i++)
            {
                Assert.AreEqual(i + 1, result[i].Id);
                Assert.AreEqual(categories[i], result[i].Name);
            }
        }
    }
}
