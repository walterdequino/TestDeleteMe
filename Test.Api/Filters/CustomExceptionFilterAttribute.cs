using System.Net;
using System.Text;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Test.Api.Enums;

using TestApp.Domain.Filters;

namespace Test.Api.Filters
{
    /// <summary>
    /// Exception Filter
    /// </summary>
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public CustomExceptionFilterAttribute(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// On Exception
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(ExceptionContext actionExecutedContext)
        {
            try
            {
                ExceptionModel responseData;

                var id = Guid.NewGuid().ToString();

                var method = $"{actionExecutedContext.HttpContext.Request.Method} {actionExecutedContext.HttpContext.Request.GetEncodedUrl()}";
                var title = $"Error en la ejecución de {actionExecutedContext.HttpContext.Request.Method} {actionExecutedContext.HttpContext.Request.GetEncodedUrl()}";

                switch (actionExecutedContext.Exception)
                {
                    case BusinessException appEx:
                        responseData = new ExceptionModel
                        {
                            TxId = id,
                            Type = ErrorTypes.Business,
                            StatusCode = HttpStatusCode.BadRequest,
                            Title = title,
                            Description = appEx.Message,
                            Detail = string.Empty,
                            ErrorCode = 400,
                            ExType = actionExecutedContext.Exception.GetType().Name,
                            Trace = actionExecutedContext.Exception.StackTrace ?? string.Empty,
                            InnerEx = actionExecutedContext.Exception.InnerException?.ToString() ?? string.Empty
                        };

                        break;
                    case ServiceDownException appEx:
                        responseData = new ExceptionModel
                        {
                            TxId = id,
                            Type = ErrorTypes.ServiceDown,
                            StatusCode = HttpStatusCode.InternalServerError,
                            Title = title,
                            Description = appEx.Message,
                            Detail = "Servicio externo no disponible.",
                            ErrorCode = -1,
                            ExType = actionExecutedContext.Exception.GetType().Name,
                            Trace = actionExecutedContext.Exception.StackTrace ?? string.Empty,
                            InnerEx = actionExecutedContext.Exception.InnerException?.ToString() ?? string.Empty
                        };

                        break;
                    case InternalValidationException appEx:
                        responseData = new ExceptionModel
                        {
                            TxId = id,
                            Type = ErrorTypes.Technical,
                            StatusCode = HttpStatusCode.InternalServerError,
                            Title = title,
                            Description = appEx.Message,
                            Detail = "Excepción interna controlada.",
                            ErrorCode = -2,
                            ExType = actionExecutedContext.Exception.GetType().Name,
                            Trace = actionExecutedContext.Exception.StackTrace ?? string.Empty,
                            InnerEx = actionExecutedContext.Exception.InnerException?.ToString() ?? string.Empty
                        };

                        break;
                    case HttpCustomException appEx:
                        responseData = new ExceptionModel
                        {
                            TxId = id,
                            Type = ErrorTypes.HttpResponse,
                            StatusCode = HttpStatusCode.InternalServerError,
                            Title = title,
                            Description = appEx.Message,
                            Detail = "La respuesta http no fue OK (<>200)",
                            ErrorCode = -3,
                            ExType = actionExecutedContext.Exception.GetType().Name,
                            Trace = actionExecutedContext.Exception.StackTrace ?? string.Empty,
                            InnerEx = actionExecutedContext.Exception.InnerException?.ToString() ?? string.Empty
                        };

                        break;
                    default:
                        responseData = new ExceptionModel
                        {
                            TxId = id,
                            Type = ErrorTypes.Technical,
                            StatusCode = HttpStatusCode.InternalServerError,
                            Title = title,
                            Detail = "Excepción no Controlada",
                            Description = actionExecutedContext.Exception.Message,
                            ErrorCode = -999999999,
                            ExType = actionExecutedContext.Exception.GetType().Name,
                            Trace = actionExecutedContext.Exception.StackTrace ?? string.Empty,
                            InnerEx = actionExecutedContext.Exception.InnerException?.ToString() ?? string.Empty
                        };

                        break;
                }

                var sb = new StringBuilder();

                sb.AppendLine(string.Empty);
                sb.AppendLine($"================== START ERROR TRACE {responseData.TxId} ==================");
                sb.AppendLine($"-> TxId: {responseData.TxId}");
                sb.AppendLine($"-> Method: {method}");
                sb.AppendLine($"-> Error Type: {responseData.Type}");
                sb.AppendLine($"-> Status Code: {(int)responseData.StatusCode} ({responseData.StatusCode.ToString()})");
                sb.AppendLine($"-> Title: {responseData.Title}");
                sb.AppendLine($"-> Description: {responseData.Description}");
                sb.AppendLine($"-> Detail: {responseData.Detail}");
                sb.AppendLine($"-> ErrorCode: {responseData.ErrorCode}");
                sb.AppendLine($"-> EXCEPTION:");
                sb.AppendLine($"--> Type: {actionExecutedContext.Exception.GetType().Name}");
                sb.AppendLine($"--> Message: {actionExecutedContext.Exception.Message}");
                sb.AppendLine($"--> StackTrace: {actionExecutedContext.Exception.StackTrace ?? string.Empty}");

                if (actionExecutedContext.Exception.InnerException != null)
                    sb.AppendLine($"--> InnerException: {actionExecutedContext.Exception.InnerException}");

                sb.AppendLine($"================== END ERROR TRACE {responseData.TxId} ==================");
                sb.AppendLine(string.Empty);

                actionExecutedContext.HttpContext.Response.StatusCode = (int)responseData.StatusCode;
                actionExecutedContext.Result = new ObjectResult(responseData)
                {
                    StatusCode = (int)responseData.StatusCode
                };

                _logger.LogError(sb.ToString());

                base.OnException(actionExecutedContext);
            }
            catch
            {
                throw actionExecutedContext.Exception;
            }
        }
    }
}