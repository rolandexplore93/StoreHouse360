using StoreHouse360.Domain.Exceptions;

namespace StoreHouse360.Application.Exceptions
{
    public class InvalidCredentialException : BaseException
    {
        public InvalidCredentialException() : base("Username or Password is not correct", StatusCodes.InvalidCredentialExceptionCode)
        {
            
        }
    }
}
