namespace StoreHouse360.Domain.Exceptions
{
    public class ProductMinimumLevelExceededException : BaseException
    {
        public IList<int> ProductsWithExceededMinimumLevel { get; }
        public ProductMinimumLevelExceededException(int productId) : this(new List<int> { productId })
        {
            
        }

        public ProductMinimumLevelExceededException(IList<int> productsWithExceededMinimumLevel, string? message = "Product minimum level exceeded") : base( message, StatusCodes.ProductMinimumLevelExceededExceptionCode)
        {
            ProductsWithExceededMinimumLevel = productsWithExceededMinimumLevel;
        }
    }
}
