using MyApi.Dtos;

namespace MyApi.ApplicationServices.Mappers
{
    /// <summary>
    /// Mapper class for categories
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Maps Category to Category data transfer objects.
        /// </summary>
        /// <param name="categories">list of categories</param>
        /// <returns>list of category DTO</returns>
        public static List<CategoryDto> ToCategoryDtoList(LinkedList<string> categories)
        {
            return categories
                .Select((category, index) => new CategoryDto
                {
                    Id = index + 1,
                    Name = category
                })
                .ToList();
        }
    }
}
