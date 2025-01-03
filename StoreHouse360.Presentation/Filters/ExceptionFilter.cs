﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StoreHouse360.Domain.Exceptions;
using StoreHouse360.Presentation.DTO.Common.Responses;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace StoreHouse360.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostEnvironment _hostEnvironment;
        private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionMap;

        public ExceptionFilter(IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _exceptionMap = new Dictionary<Type, Action<ExceptionContext>>
            {
                // {typeof(NotFoundException), HandleNotFoundException}
                {typeof(ProductMinimumLevelExceededException), HandleProductMinimumLevelExceededException}
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
            string message;
            if (_hostEnvironment.IsDevelopment() || _hostEnvironment.IsStaging())
            {
                message = $"{context.Exception.Message} {context.Exception.StackTrace}";
            }
            else
            {
                message = $"Something went wrong, an unknown error occured, please try again later.";
            }
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
    }
}
