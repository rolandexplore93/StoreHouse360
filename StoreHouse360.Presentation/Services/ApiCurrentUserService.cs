using IdentityModel;
using StoreHouse360.Application.Services.Identity;
using System.Security.Claims;

namespace StoreHouse360.Services
{
    public class ApiCurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiCurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int? UserId 
        {
            get
            {
                //var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier); // (JwtClaimTypes.Id)
                var id = _httpContextAccessor.HttpContext?.User.FindFirstValue("Id");
                return id == null ? null : int.Parse(id);
            }
        }
    }
}
