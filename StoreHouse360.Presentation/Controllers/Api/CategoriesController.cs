﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Application.Commands.Categories;
using StoreHouse360.Application.Commands.Users;
using StoreHouse360.Application.Queries.Categories;
using StoreHouse360.Dto.Category;
using StoreHouse360.Dto.Common;
using StoreHouse360.DTO.Category;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.Controllers.Api
{
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<CategoryVM>>> CreateCategory(CreateCategoryRequestDTO requestDTO)
        {
            var command = _mapper.Map<CreateCategoryCommand>(requestDTO);
            var categoryId = await Mediator.Send(command);
            var query = new GetCategoryQuery { Id = categoryId };
            var categoryEntity = await Mediator.Send(query);
            return Ok(categoryEntity.ToViewModel<CategoryVM>(_mapper));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryVM>>> GetCategory(int id)
        {
            var query = new GetCategoryQuery { Id = id };

            var categoryEntity = await Mediator.Send(query);

            return Ok(categoryEntity.ToViewModel<CategoryVM>(_mapper));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<CategoryVM>>>> GetCategories()
        {
            var query = new GetAllCategoriesQuery();
            var categoryEntities = await Mediator.Send(query);
            return Ok(categoryEntities.ToViewModels<CategoryVM>(_mapper));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BaseResponse<CategoryVM>>> UpdateCategory(UpdateCategoryRequestDTO request, int id)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            command.Id = id;
            var updatedCategoryId = await Mediator.Send(command);
            return await GetCategory(updatedCategoryId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BaseResponse<object?>>> DeleteCategory(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok("Category deleted successfully");
        }
    }
}
