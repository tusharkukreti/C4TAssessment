namespace C4TAssessment.Models.Enquiries
{
    /// <summary>
    /// Error Item 
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        /// Initialize the fields
        /// </summary>
        public ErrorItem(int statusCode, string errorMessage, string stackTrace)
        {
            this.StatusCode = statusCode;
            this.ErrorMessage = errorMessage;
            this.StackTrace = stackTrace;
        }
        /// <summary>
        /// Status code of the response
        /// </summary>
        public int StatusCode { get; }
        /// <summary>
        /// Error message
        /// </summary>
        public string ErrorMessage { get; }
        /// <summary>
        /// Stack trace
        /// </summary>
        public string StackTrace { get; }
    }
}
