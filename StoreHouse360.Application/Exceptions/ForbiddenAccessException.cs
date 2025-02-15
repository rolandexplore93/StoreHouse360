using StoreHouse360.Domain.Exceptions;

namespace StoreHouse360.Application.Exceptions
{
    public class ForbiddenAccessException : BaseException
    {
        public ForbiddenAccessException() : base("Forbidden Access", StatusCodes.ForbiddenAccessExceptionCode)
        {
        }
    }
}
