using StoreHouse360.Presentation.DTO.Responses.Common;

namespace StoreHouse360.Presentation.DTO.Responses.Common
{
    public class NoDataResponse : BaseResponse<object>
    {
        public NoDataResponse(string? message = default) : base(new ResponseMetaData() { Message = message }, null)
        {
        }
    }
}
