using AssetManagement.Server.Infrastructure.Exceptions;
using AssetManagement.Server.Infrastructure.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;
using Newtonsoft.Json.Linq;

namespace AssetManagement.Server.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            //LogException(context);

            switch (context.Exception.GetType().Name)
            {
                case nameof(KeyNotFoundHelperException):
                    NotFoundException(context);
                    break;
                case nameof(BadRequestHelperException):
                    BadRequestException(context);
                    break;
                case nameof(ConflictHelperException):
                    ConflictException(context);
                    break;
                case nameof(InvalidViewModelHelperException):
                    BadRequestException(context);
                    break;
                case nameof(AlreadyExistHelperException):
                    ConflictException(context);
                    break;
                case nameof(UnauthorizedAccessException):
                    UnauthorizedException(context);
                    break;
                case nameof(NotModifiedHelperException):
                    NotModifiedException(context);
                    break;
                case nameof(UnprocessableEntityHelperException):
                    UnprocessableEntityException(context);
                    break;
                default:
                    GlobalException(context);
                    break;
            }

            context.ExceptionHandled = true;
        }

        private void LogException(ExceptionContext context) =>
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, $"{context.Exception.Message}");

        private void GlobalException(ExceptionContext context)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.InternalServerError);

            context.Result = new UnprocessableEntityObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        private void NotFoundException(ExceptionContext context)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.NotFound);

            context.Result = new NotFoundObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private void BadRequestException(ExceptionContext context, string responseJson = null)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.BadRequest);

            context.Result = new BadRequestObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        private void ConflictException(ExceptionContext context)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.Conflict);

            context.Result = new ConflictObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
        }

        private void UnprocessableEntityException(ExceptionContext context)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.UnprocessableEntity);

            context.Result = new UnprocessableEntityObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        }

        protected static void NotModifiedException(ExceptionContext context)
        {
            var json = CreateErrorJson(context, context.Exception.Message, HttpStatusCode.NotModified);

            context.Result = new UnprocessableEntityObjectResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotModified;
        }

        private static void UnauthorizedException(ExceptionContext context) =>
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

        private static ResponseTypeView<JObject> CreateErrorJson(ExceptionContext context, string msg, HttpStatusCode statusCode)
        {
            try
            {
                var json = JObject.Parse(msg);

                return new ResponseTypeView<JObject>()
                {
                    Result = json,
                    MessageHeader = "Error Occured",
                    MessageDescription = msg,
                    StatusCode = (int)statusCode
                };

                //return json;
            }
            catch (JsonReaderException)
            {
                return new ResponseTypeView<JObject>()
                {
                    Result = null,
                    MessageHeader = "Error Occured",
                    MessageDescription = msg,
                    StatusCode = (int)statusCode
                };
            }
        }
    }
}
