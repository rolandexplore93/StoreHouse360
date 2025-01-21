using MediatR;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Aggregations;

namespace StoreHouse360.Application.Queries.Accounting
{
    public class GetAccountStatementQuery : IRequest<AggregateAccountStatement>
    {
        public int AccountId { get; }
        public GetAccountStatementQuery(int accountId)
        {
            AccountId = accountId;
        }
    }
    public class GetAccountStatementQueryHandler : IRequestHandler<GetAccountStatementQuery, AggregateAccountStatement>
    {
        private readonly IJournalRepository _journalRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyRepository _currencyRepository;
        public GetAccountStatementQueryHandler(IJournalRepository journalRepository, IAccountRepository accountRepository,
            ICurrencyRepository currencyRepository)
        {
            _journalRepository = journalRepository;
            _accountRepository = accountRepository;
            _currencyRepository = currencyRepository;
        }
        public async Task<AggregateAccountStatement> Handle(GetAccountStatementQuery request,
            CancellationToken cancellationToken)
        {
            var detailEntries = await _journalRepository.GetAllAsync();
            detailEntries.Where(journal => journal.SourceAccountId == request.AccountId)
                .GroupBy(
                    journal => new
                    {
                        journal.SourceAccountId,
                        journal.AccountId,
                        journal.CurrencyId,
                    },
                    journal => new { journal.Debit, journal.Credit },
                    (key, entries) => new
                    {
                        key.AccountId,
                        Debit = entries.Sum(j => j.Debit),
                        Credit = entries.Sum(j => j.Credit),
                        key.CurrencyId
                    }
                ).ToList();

            var detailsAccounts1 = await _accountRepository.GetAllAsync();

            // Filter detailsAccounts based on detailEntries
            var detailsAccounts = detailsAccounts1.Where(account => detailEntries.Select(d => d.AccountId).Contains(account.Id)).ToList();

            // Get currency details based on the first entry in detailEntries
            var currency = await _currencyRepository.FindByIdAsync(detailEntries.First().CurrencyId);



            //var sourceAccount = await _accountRepository.FindByIdAsync(request.AccountId);
            //var statement = new AggregateAccountStatement(
            //    sourceAccount,
            //    details,
            //    details.Sum(d => d.Debit),
            //    details.Sum(d => d.Credit),
            //    currency
            //);
            //return statement;
        }
    }
}
