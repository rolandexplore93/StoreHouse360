using Microsoft.AspNetCore.Identity;

namespace StoreHouse360.Infrastructure.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string? GetErrorsAsString(this IdentityResult result)
        {
            //var errors = result.Errors.Select(err => err.Description).ToString();
            var errors = result.Errors.FirstOrDefault(e => true)?.Description;
            return errors;
        }
    }
}
