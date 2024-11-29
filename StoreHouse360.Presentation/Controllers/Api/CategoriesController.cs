using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Categories;
using StoreHouse360.Application.Queries.Categories;
using StoreHouse360.DTO.Category;
using StoreHouse360.Infrastructure.Models;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<Category>>> CreateCategory(CreateCategoryRequestDTO requestDTO)
        {
            var command = _mapper.Map<CreateCategoryCommand>(requestDTO);
            var categoryId = await Mediator.Send(command);
            var query = new GetCategoryQuery { Id = categoryId };
            var queryResult = await Mediator.Send(query);
            return Ok(queryResult);
        }
    }
}
