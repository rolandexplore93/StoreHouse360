namespace StoreHouse360.Presentation.DTO.Common.Responses
{
    public class NoDataResponse : BaseResponse<object>
    {
        public NoDataResponse(string? message = default) : base(new ResponseMetaData() { Message = message }, null)
        {
        }
    }
}
