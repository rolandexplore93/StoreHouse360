namespace StoreHouse360.Domain.Exceptions
{
    public class OverPaymentException : BaseException
    {
        public OverPaymentException(string? message = "Amount you entered is more than the remaining amount", int code = StatusCodes.OverPaymentExceptionCode ) : base(message, code)
        {
            
        }
    }
}
