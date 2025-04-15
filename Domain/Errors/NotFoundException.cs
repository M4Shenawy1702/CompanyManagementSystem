using Microsoft.AspNetCore.Http;

namespace Domain.Errors
{
    public class NotFoundException : ServiceException
    {
        public NotFoundException(string message, string? errorCode = null)
            : base(StatusCodes.Status404NotFound, message, errorCode)
        {
        }
    }
}
