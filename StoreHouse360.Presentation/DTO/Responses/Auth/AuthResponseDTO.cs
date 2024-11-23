using StoreHouse360.Presentation.DTO.Responses.Common;
using StoreHouse360.Presentation.DTO.ViewModels;

namespace StoreHouse360.Presentation.DTO.Responses.Auth
{
    public class AuthResponseDTO : BaseResponse<AuthenticatedUserVM>
    {
        public AuthResponseDTO(AuthenticatedUserVM data) : base(new ResponseMetaData {Time = DateTime.UtcNow.ToString(), Message = "Authenticated Successfully"}, data)
        {
            
        }
    }
}