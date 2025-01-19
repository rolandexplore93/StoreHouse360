using StoreHouse360.Application.Common.Mappings;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Infrastructure.Persistence.Database.Models
{
    public class JournalDb : IMapFrom<Journal>, IDatabaseModel
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int AccountId { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public int CurrencyId { get; set; }
    }
}
