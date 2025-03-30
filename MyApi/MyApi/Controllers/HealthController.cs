using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    /// <summary>
    /// Controller for health checking endpoints
    /// </summary>
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Simple health check endpoint that returns 200 OK when the API is running
        /// </summary>
        /// <returns>200 OK status when the API is healthy</returns>
        [HttpGet]
        public IActionResult Check()
        {
            return Ok(new { status = "healthy" });
        }
    }
}