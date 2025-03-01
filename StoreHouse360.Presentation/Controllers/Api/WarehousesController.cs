using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Warehouses;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Queries.Warehouses;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.ProductQuantity;
using StoreHouse360.DTO.Warehouses;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class WarehousesController : ApiControllerBase
    {
        public WarehousesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<WarehouseVM>>> Create(CreateWarehouseRequestDTO request)
        {
            var warehouseId = await Mediator.Send(_mapper.Map<CreateWarehouseCommand>(request));
            var warehouse = await Mediator.Send(new GetWarehouseQuery { Id = warehouseId });
            return Ok(warehouse.ToViewModel<WarehouseVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<WarehouseVM>>>> GetAll([FromQuery] WarehousesQueryParams request)
        {
            //var warehouses = await Mediator.Send(request.AsQuery(new GetAllWarehousesQuery()));
            var warehouses = await Mediator.Send(request.AsQuery<GetAllWarehousesQuery>(_mapper));
            return Ok(warehouses.ToViewModels<WarehouseVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<WarehouseVM>>> Get(int id)
        {
            var warehouse = await Mediator.Send(new GetWarehouseQuery { Id = id });
            return Ok(warehouse.ToViewModel<WarehouseVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<WarehouseVM>>> Update(int id, UpdateWarehouseRequestDTO request)
        {
            var command = _mapper.Map<UpdateWarehouseCommand>(request);
            command.Id = id;

            var warehouseId = await Mediator.Send(command);
            var warehouse = await Mediator.Send(new GetWarehouseQuery { Id = warehouseId });
            return Ok(warehouse.ToViewModel<WarehouseVM>(_mapper));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await Mediator.Send(new DeleteWarehouseCommand() { key = id });
        }

        [HttpGet("inventory")]
        public async Task<ActionResult<BaseResponse<PaginationVM<ProductQuantityVM>>>> InventoryWarehouse([FromQuery] PaginationRequestParams paginationParams, [FromQuery] ProductMovementFiltersDTO filtersDTO)
        {
            var query = paginationParams.AsQuery(new InventoryWarehouseQuery { Filters = filtersDTO });
            var productQuantities = await Mediator.Send(query);
            return Ok(productQuantities.ToViewModel<ProductQuantityVM>(_mapper));
        }

    }
}
