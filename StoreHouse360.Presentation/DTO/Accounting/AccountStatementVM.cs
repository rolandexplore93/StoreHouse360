using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Aggregations;
using StoreHouse360.DTO.Accounts;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Currencies;

namespace StoreHouse360.DTO.Accounting
{
    public class AccountStatementVM : IViewModel, IMapFrom<AggregateAccountStatement>
    {
        public int Id => Account.Id;
        public AccountVM Account { get; set; }
        public IEnumerable<AccountStatementDetailVM> Details { get; set; }
        public double DebitSum { get; set; }
        public double CreditSum { get; set; }
        public double Result { get; set; }
        public CurrencyVM Currency { get; set; }
    }
    public class AccountStatementDetailVM : IViewModel, IMapFrom<AggregateAccountStatementDetail>
    {
        public int Id { get; set; }
        public AccountVM Account { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public CurrencyVM Currency { get; set; }
    }
}
