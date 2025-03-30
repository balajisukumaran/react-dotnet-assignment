namespace MyApi.Dtos
{
    /// <summary>
    /// Represents a joke with an icon and text.
    /// </summary>
    public class JokeDto
    {
        /// <summary>
        /// Gets or sets the URL for the joke icon.
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// Gets or sets the text content of the joke.
        /// </summary>
        public string Value { get; set; }
    }
}
