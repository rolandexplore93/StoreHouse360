using StoreHouse360.Application.Commands.Common;
using StoreHouse360.Application.Common.Security;
using StoreHouse360.Application.Repositories;
using StoreHouse360.Domain.Entities;

namespace StoreHouse360.Application.Commands.CountryOrigins
{
    [Authorize(Method = Method.Write, Resource = Resource.Countries)]
    public class CreateCountryOriginCommand : ICreateEntityCommand<int>
    {
        public string Name { get; init; }
    }
    public class CreateCountryOriginCommandHandler : CreateEntityCommandHandler<CreateCountryOriginCommand, CountryOrigin, int, ICountryOriginRepository>
    {
        public CreateCountryOriginCommandHandler(ICountryOriginRepository repository) : base(repository)
        {
        }
        protected override CountryOrigin CreateEntity(CreateCountryOriginCommand request)
        {
            return new CountryOrigin
            {
                Name = request.Name
            };
        }
    }
}