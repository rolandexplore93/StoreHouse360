using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.CountryOrigins;
using StoreHouse360.Application.Queries.Categories;
using StoreHouse360.Application.Queries.CountryOrigins;
using StoreHouse360.DTO.Categories;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.CountryOrigins;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class CountryOriginsController : ApiControllerBase
    {
        public CountryOriginsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CountryOriginVM>>> CreateCountryOrigin(CreateCountryOriginRequest request)
        {
            var command = _mapper.Map<CreateCountryOriginCommand>(request);
            var countryOriginId = await Mediator.Send(command);
            return await GetCountryOrigin(countryOriginId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CountryOriginVM>>> GetCountryOrigin(int id)
        {
            var query = new GetCountryOriginQuery { Id = id };
            var countryOriginEntity = await Mediator.Send(query);
            return Ok(countryOriginEntity.ToViewModel<CountryOriginVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<CountryOriginVM>>>> GetCategories([FromQuery] CategoriesQueryParams request)
        {
            var query = request.AsQuery<GetAllCategoriesQuery>(_mapper);
            var countryOriginEntities = await Mediator.Send(query);
            return Ok(countryOriginEntities.ToViewModel<CountryOriginVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CountryOriginVM>>> UpdateCountryOrigin(UpdateCountryOriginRequest request, int id)
        {
            var command = _mapper.Map<UpdateCountryOriginCommand>(request);
            command.Id = id;
            var updatedCountryOriginId = await Mediator.Send(command);
            return await GetCountryOrigin(updatedCountryOriginId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteCountryOrigin(int id)
        {
            await Mediator.Send(new DeleteCountryOriginCommand() { key = id });
        }
    }
}
