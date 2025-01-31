using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Common.DTO;
using StoreHouse360.Application.Queries.StoragePlaces;
using StoreHouse360.Application.Queries.Warehouses;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.ProductQuantity;
using StoreHouse360.DTO.StoragePlaceQuantity;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class InventoryController : ApiControllerBase
    {
        public InventoryController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpGet("byProduct")]
        public async Task<ActionResult<BaseResponse<PaginationVM<ProductQuantityVM>>>> InventoryWarehouse(
            [FromQuery] PaginationRequestParams paginationParams,
            [FromQuery] ProductMovementFiltersDTO filters)
        {
            var query = paginationParams.AsQuery(new InventoryWarehouseQuery { Filters = filters });
            var productQuantities = await Mediator.Send(query);
            return Ok(productQuantities.ToViewModel<ProductQuantityVM>(_mapper));
        }
        [HttpGet("byProductAndStoragePlace")]
        public async Task<ActionResult<BaseResponse<PaginationVM<StoragePlaceQuantityVM>>>> Inventory(
            [FromQuery] PaginationRequestParams paginationParams,
            [FromQuery] ProductMovementFiltersDTO filters
        )
        {
            var query = paginationParams.AsQuery(new InventoryStoragePlaceQuery(filters));
            var storagePlaceQuantities = await Mediator.Send(query);
            return Ok(storagePlaceQuantities.ToViewModel<StoragePlaceQuantityVM>(_mapper));
        }
    }
}
