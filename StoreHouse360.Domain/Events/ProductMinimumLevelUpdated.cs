namespace StoreHouse360.Domain.Events
{
    public class ProductMinimumLevelUpdated : DomainEvent
    {
        public int ProductId { get; }
        public int MinimumLevelBefore { get; }
        public int MinimumLevelAfter { get; }

        public ProductMinimumLevelUpdated(int productId, int minimumLevelBefore, int minimumLevelAfter)
        {
            ProductId = productId;
            MinimumLevelBefore = minimumLevelBefore;
            MinimumLevelAfter = minimumLevelAfter;
        }

        public bool MinimumLevelIncreased() =>  MinimumLevelAfter > MinimumLevelBefore;
        public bool MinimumLevelDecreased() => MinimumLevelAfter < MinimumLevelBefore;
    }
}
