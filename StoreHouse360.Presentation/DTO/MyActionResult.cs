using Microsoft.AspNetCore.Mvc;
using StoreHouse360.Presentation.DTO.Responses.Common;

namespace StoreHouse360.DTO
{
    public class MyActionResult<T>
    {
        public ActionResult<BaseResponse<T>> Value { get; }
    }
}
