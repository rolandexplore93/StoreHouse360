namespace StoreHouse360.Domain.Exceptions
{
    public static class StatusCodes
    {
        public const int UnknownExceptionCode = 500;
        public const int ProductMinimumLevelExceededExceptionCode = 432;
        public const int OverPaymentExceptionCode = 433;
        public const int IncompatiblePaymentIoTypeExceptionCode = 434;
        public const int InvoiceClosedException = 435;
    }
}
