using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreHouse360.Presentation.DTO.Common.Responses;

namespace StoreHouse360.DTO.Common.Responses.Validation
{
    public class BadRequestResponse : NoDataResponse
    {
        public BadRequestResponse(ModelStateDictionary modelState) : base(MapModelStateToMessage(modelState))
        {
        }
        private static string MapModelStateToMessage(ModelStateDictionary modelState)
        {
            return modelState.Keys.Aggregate(
                "",
                (s, key) => modelState[key].Errors.Aggregate(
                    s,
                    (ss, modelError) => ss + (modelError.ErrorMessage + ' ')
                )
            )[..^1];
        }
    }
}
