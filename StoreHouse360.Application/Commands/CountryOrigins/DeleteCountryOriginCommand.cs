using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.CountryOrigins
{
    public class DeleteCountryOriginCommand : DeleteEntityCommand<int>
    {

    }
    public class DeleteCountryOriginCommandHandler : DeleteEntityCommandHandler<DeleteCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
    {
        public DeleteCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
        {
        }
    }
}
