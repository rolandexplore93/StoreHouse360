using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.StoragePlaces;
using StoreHouse360.Application.Queries.StoragePlaces;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.StoragePlaces;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    //[Authorize]
    [Route("api/warehouses/{warehouseId}/places")]
    public class StoragePlacesController : ApiControllerBase
    {
        public StoragePlacesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<StoragePlaceVM>>> CreateStoragePlace(int warehouseId, CreateStoragePlaceRequestDTO request)
        {
            var command = _mapper.Map<CreateStoragePlaceCommand>(request);
            command.WarehouseId = warehouseId;

            var placeId = await Mediator.Send(command);
            var place = await Mediator.Send(new GetStoragePlaceQuery { Id = placeId });

            return Ok(place.ToViewModel<StoragePlaceVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<StoragePlaceVM>>>> GetAllStoragePlaces(int warehouseId, [FromQuery] PaginationRequestParams request)
        {
            var places = await Mediator.Send(request.AsQuery(new GetAllStoragePlacesQuery { WarehouseId = warehouseId }));

            return Ok(places.ToViewModels<StoragePlaceVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<StoragePlaceVM>>> GetStoragePlace(int id)
        {
            var place = await Mediator.Send(new GetStoragePlaceQuery { Id = id });

            return Ok(place.ToViewModel<StoragePlaceVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<StoragePlaceVM>>> UpdateStoragePlace(int id, int warehouseId, UpdateStoragePlaceRequestDTO request)
        {
            var command = _mapper.Map<UpdateStoragePlaceCommand>(request);
            command.Id = id;
            command.WarehouseId = warehouseId;
            var placeId = await Mediator.Send(command);
            return await GetStoragePlace(placeId);
        }
    }
}