using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Application.Common.Exceptions;

namespace AspNetCoreWeb.Api.Middlewares
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var logger = context.RequestServices.GetRequiredService<IAppLogger<Startup>>();

            switch (exception)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    logger.LogInformation($"Bad request: {badRequestException.Message}");
                    break;
                case ArgumentNullException argNullException:
                    logger.LogInformation($"Argument null: {argNullException.Message}");
                    break;
                case AppException appException:
                    logger.LogInformation($"Application exception: {appException.Message}");
                    break;
                case SqlException sqlException:
                    logger.LogInformation($"Database Exception: {sqlException.Message}");
                    break;
                default :
                    logger.LogInformation($"{nameof(exception)}: {exception.Message + exception.StackTrace}");
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = exception.Message }));
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
