namespace StoreHouse360.Infrastructure.Persistence.Database.Models.Common
{
    public static class DatabaseModelsExtensions
    {
        public static IQueryable<T> FilterSoftDeleteMethods<T>(this IQueryable<T> set)
        {
            if(typeof(T).GetInterface(nameof(ISoftDelete)) == null)
            {
                return set;
            }

            return set.Where(model => !((ISoftDelete)model).IsDeleted);
        }
    }
}
