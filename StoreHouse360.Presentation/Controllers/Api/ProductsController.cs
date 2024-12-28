using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Products;
using StoreHouse360.Application.Queries.Invoicing;
using StoreHouse360.Application.Queries.Invoicing.DTO;
using StoreHouse360.Application.Queries.Products;
using StoreHouse360.DTO.Common;
using StoreHouse360.DTO.Pagination;
using StoreHouse360.DTO.Products;
using StoreHouse360.Presentation.DTO.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StoreHouse360.Controllers.Api
{
    public class ProductsController : ApiControllerBase
    {
        public ProductsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
            
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<PaginationVM<ProductJoinedVM>>>> GetAllProducts([FromQuery] PaginationRequestParams request)
        {
            var productEntities = await Mediator.Send(request.AsQuery(new GetAllProductsQuery()));
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

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ProductJoinedVM>>> UpdateProduct(UpdateProductRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var updatedProductId = await Mediator.Send(command);
            return await GetProduct(updatedProductId);
        }

        [HttpGet("{id}/checkQuantity")]
        public async Task CheckQuantity(int id, [FromQuery][Required] int quantity)
        {
            await Mediator.Send(new CheckProductQuantityQuery
            {
                ProductQuantities = new[] { new CheckProductQuantityDTO { ProductId = id, Quantity = quantity } }
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<ProductJoinedVM>>> UpdateProduct(int id, UpdateProductRequestDTO request)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;
            var resultId = await Mediator.Send(command);
            return await GetProduct(resultId);
        }
    }
}
