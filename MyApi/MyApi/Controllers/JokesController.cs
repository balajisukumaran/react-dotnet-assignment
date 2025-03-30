using Microsoft.AspNetCore.Mvc;
using MyApi.Dtos;
using MyApi.ApplicationServices;

namespace MyApi.Controllers
{
    /// <summary>
    /// Controller for handling joke-related endpoints.
    /// </summary>
    [ApiController]
    [Route("api/jokes")]
    public class JokesController : ControllerBase
    {
        private readonly IJokeService _jokeService;

        /// <summary>
        /// Initializes a new instance of the JokesController.
        /// </summary>
        /// <param name="jokeService">The service used to retrieve jokes and categories.</param>
        public JokesController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        /// <summary>
        /// Retrieves a list of joke categories.
        /// </summary>
        /// <returns>A list of category names.</returns>
        [HttpGet("categories")]
        public async Task<ActionResult<List<CategoryDto>>> GetCategories()
        {
            var categories = await _jokeService.GetCategoriesAsync();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves jokes for a specific category.
        /// </summary>
        /// <param name="category">The category of jokes to retrieve.</param>
        /// <param name="number">The number of jokes to retrieve.</param>
        /// <returns>A list of jokes.</returns>
        [HttpGet("{category}/{number}")]
        public async Task<ActionResult<List<JokeDto>>> GetJokes(string category, int number)
        {
            var jokes = await _jokeService.GetJokesAsync(category, number);
            return Ok(jokes);
        }
    }
}
