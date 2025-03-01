using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Products;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Queries.Invoicing.DTO;
using StoreHouse360.Application.Queries.Products;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.ProductQuantity;
using StoreHouse360.DTO.Products;
using StoreHouse360.Presentation.DTO.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Controllers.Api
{
    [Authorize]
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<ProductJoinedVM>>>> GetAllProducts([FromQuery] ProductsQueryParams request)
        {
            var productEntities = await Mediator.Send(request.AsQuery<GetAllProductsQuery>(_mapper));
            return Ok(productEntities.ToViewModels<ProductJoinedVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<ProductJoinedVM>>> GetProduct(int id)
        {
            var query = new GetProductQuery { Id = id };
            var productEntity = await Mediator.Send(query);
            return Ok(productEntity.ToViewModel<ProductJoinedVM>(_mapper));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<ProductJoinedVM>>> CreateProduct(CreateProductRequestDTO request)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            var createdProductId = await Mediator.Send(command);
            return await GetProduct(createdProductId);
        }

        [HttpGet("inStoragePlace")]
        public async Task<ActionResult<BaseResponse<PaginationVM<ProductJoinedVM>>>> GetAllProducts([FromQuery] GetProductsInStorageQueryParams request)
        {
            var productEntities = await Mediator.Send(request.AsQuery<GetAllProductsInStoragePlaceQuery>(_mapper));
            return Ok(productEntities.ToViewModel<ProductJoinedVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ProductJoinedVM>>> UpdateProduct(UpdateProductRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var resultId = await Mediator.Send(command);
            var query = new GetProductQuery { Id = id };
            var productEntity = await Mediator.Send(query);
            return Ok(productEntity.ToViewModel<ProductJoinedVM>(_mapper));
        }

        [HttpGet("{id}/checkQuantity")]
        public async Task CheckQuantity(int id, [FromQuery][Required] int quantity)
        {
            await Mediator.Send(new CheckProductQuantityQuery
            {
                ProductQuantities = new[] { new CheckProductQuantityDTO { ProductId = id, Quantity = quantity } }
            });
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(int id)
        {
            await Mediator.Send(new DeleteProductCommand() { key = id });
        }
    }
}
