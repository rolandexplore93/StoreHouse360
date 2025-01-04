using System.Linq.Expressions;

namespace StoreHouse360.Infrastructure
{
    public class FilterConditionAdapter<TFilter, TModel>
    {
        private readonly TFilter _filter;
        private readonly TModel _model;

        public FilterConditionAdapter(TFilter filter, TModel model)
        {
            _filter = filter;
            _model = model;
        }

        public Expression<Func<TModel, bool>> GenerateCondition()
        {
            Expression<Func<TModel, bool>> expression = model => model != null;

            foreach (var propertyInfo in _filter!.GetType().GetProperties())
            {
                var modelProperties = _model!.GetType().GetProperties();
                var filterValue = propertyInfo.GetValue(_filter);
            }

            return expression;
        }
    }
}
