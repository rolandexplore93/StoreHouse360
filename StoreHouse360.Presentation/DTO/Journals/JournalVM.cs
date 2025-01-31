using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;
using StoreHouse360.DTO.Accounts;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Currencies;

namespace StoreHouse360.DTO.Journals
{
    public class JournalVM : IViewModel, IMapFrom<Journal>
    {
        public int SourceAccountId { get; set; }
        public AccountVM? SourceAccount { get; set; }
        public int AccountId { get; set; }
        public AccountVM? Account { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int CurrencyId { get; set; }
        public CurrencyVM? Currency { get; set; }
    }
}
