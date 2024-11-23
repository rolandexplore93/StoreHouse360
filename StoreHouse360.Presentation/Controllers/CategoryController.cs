using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreHouse360.DTO.Requests.Category;
using StoreHouse360.DTO.Responses.Category;
using StoreHouse360.DTO.ViewModels;
using StoreHouse360.Presentation.Controllers;
using StoreHouse360.Presentation.DTO.Responses.Common;

namespace StoreHouse360.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public ActionResult<BaseResponse<CategoryVM>> CreateCategory(CreateCategoryRequestDTO requestDTO)
        {
            var createdCategory = new CategoryVM() { Id = "iducengjihkodfkeihj", Name = requestDTO.Name };
            return Ok(createdCategory, "Category created successfully.");
        }
    }
}
