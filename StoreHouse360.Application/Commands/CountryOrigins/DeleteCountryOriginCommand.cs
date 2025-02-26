using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.CountryOrigins
{
    [Authorize(Method = Method.Delete, Resource = Resource.Countries)]
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
