using Newtonsoft.Json;

namespace MyApi.Models
{
    /// <summary>
    /// Represents the response received from the joke API.
    /// </summary>
    public class JokeResponse
    {
        /// <summary>
        /// Gets or sets the URL for the joke icon.
        /// </summary>
        [JsonProperty("icon_url")] // Maps JSON key to C# property
        public string IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the text content of the joke.
        /// </summary>
        [JsonProperty("value")] // Maps JSON key to C# property
        public string Value { get; set; }
    }
}
