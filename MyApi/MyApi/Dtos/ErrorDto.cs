namespace MyApi.Dtos
{
    /// <summary>
    /// Represents an error with a status code, a message, and optional additional details.
    /// </summary>
    public class ErrorDto
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a short message that describes the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional details about the error.
        /// </summary>
        public string? Details { get; set; }
    }
}
