using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

namespace StoreHouse360.DTO.Common.Responses.Validation
{
    public class BadRequestObjectResult : ObjectResult
    {
        public BadRequestObjectResult(ModelStateDictionary modelState) : base(new BadRequestResponse(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
