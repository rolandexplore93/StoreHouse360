using StoreHouse360.DTO.ViewModels;
using StoreHouse360.Presentation.DTO.Responses.Common;

namespace StoreHouse360.DTO.Responses.Category
{
    public class CreateCategoryResponseDTO : BaseResponse<CategoryVM>
    {
        public CreateCategoryResponseDTO(CategoryVM data) : base(new ResponseMetaData() { Time = DateTime.UtcNow.ToString(), Message = "Category created successfully." }, data)
        {
            
        }
    }
}
