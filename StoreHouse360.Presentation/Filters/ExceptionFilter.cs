using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreHouse360.Application.Exceptions;
using StoreHouse360.Domain.Exceptions;
using StoreHouse360.Presentation.DTO.Common.Responses;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace StoreHouse360.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionMap;

        public ExceptionFilter(IHostEnvironment hostEnvironment, ILogger<ExceptionFilter> logger)
        {
            _hostEnvironment = hostEnvironment;
            _logger = logger;

            _exceptionMap = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ProductMinimumLevelExceededException), HandleProductMinimumLevelExceededException},
                {typeof(ValidationException), HandleValidationException},
                {typeof(ForbiddenAccessException), HandleForbiddenAccessException}
            };
        }

        public override void OnException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionMap.ContainsKey(type)) // Exception is any of the types declared in the dictionary.
            {
                _exceptionMap[type].Invoke(context);
            }
            else if (context.Exception is BaseException)
            {
                HandleCustomException(context);
            }
            else // Exception is unknown
            {
                HandleUnknownException(context);
            }
            base.OnException(context);
        }
        private void HandleUnknownException(ExceptionContext context)
        {
            if (_hostEnvironment.IsDevelopment() || _hostEnvironment.IsStaging())
            {
                _logger.LogError(context.Exception, null);
            }

            string message = $"Something went wrong, an unknown error occured, please try again later.";
            var responseBody = new NoDataResponse(message);
            context.Result = new ObjectResult(responseBody) { StatusCode = StatusCodes.Status500InternalServerError };
            context.ExceptionHandled = true;
        }

        private void HandleCustomException(ExceptionContext context)
        {
            BaseException exception = (BaseException)context.Exception;
            var responseBody = new NoDataResponse(exception.Message);
            context.Result = new ObjectResult(responseBody) { StatusCode = exception.Code };
            context.ExceptionHandled = true;
        }

        private void HandleProductMinimumLevelExceededException(ExceptionContext context)
        {
            var exception = context.Exception as ProductMinimumLevelExceededException;
            var responseBody = new BaseResponse<IList<int>>(new ResponseMetaData { Message = exception!.Message }, exception.ProductsWithExceededMinimumLevel);
            context.Result = new ObjectResult(responseBody) { StatusCode = exception.Code };
            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = (ValidationException) context.Exception;
            var responseBody = new NoDataResponse(string.Join("\n", exception.Errors.Select(e => e.ErrorMessage)));
            context.Result = new ObjectResult(responseBody) { StatusCode = StatusCodes.Status400BadRequest };
            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            var exception = (ForbiddenAccessException)context.Exception;

            var responseBody = new NoDataResponse(exception.Message);

            context.Result = new ObjectResult(responseBody) { StatusCode = StatusCodes.Status403Forbidden };

            context.ExceptionHandled = true;
        }
    }
}
