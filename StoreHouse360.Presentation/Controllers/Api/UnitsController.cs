using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Units;
using StoreHouse360.Application.Queries.Units;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Units;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    //[Authorize]
    public class UnitsController : ApiControllerBase
    {
        public UnitsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<UnitVM>>> CreateUnit(CreateUnitRequestDTO request)
        {
            var command = _mapper.Map<CreateUnitCommand>(request);
            var unitId = await Mediator.Send(command);
            return await GetUnit(unitId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<UnitVM>>> GetUnit(int id)
        {
            var query = new GetUnitQuery { Id = id };
            var unitEntity = await Mediator.Send(query);
            return Ok(unitEntity.ToViewModel<UnitVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<UnitVM>>>> GetUnits([FromQuery] UnitsQueryParams request)
        {
            var query = request.AsQuery<GetAllUnitsQuery>(_mapper);
            var unitEntities = await Mediator.Send(request.AsQuery(query));
            return Ok(unitEntities.ToViewModels<UnitVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<UnitVM>>> UpdateUnit(UpdateUnitRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateUnitCommand>(request);
            command.Id = id;
            var updatedUnitId = await Mediator.Send(command);
            return await GetUnit(updatedUnitId);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUnit(int id)
        {
            await Mediator.Send(new DeleteUnitCommand() { key = id });
        }
    }
}
