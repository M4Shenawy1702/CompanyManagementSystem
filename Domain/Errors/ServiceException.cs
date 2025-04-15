namespace Domain.Errors
{
    public class ServiceException : Exception
    {
        public int StatusCode { get; }
        public string? ErrorCode { get; }
        public IDictionary<string, string>? ErrorDetails { get; }

        public ServiceException(
            int statusCode,
            string message,
            string? errorCode = null,
            IDictionary<string, string>? errorDetails = null,
            Exception? innerException = null)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            ErrorDetails = errorDetails;
        }
    }
}
