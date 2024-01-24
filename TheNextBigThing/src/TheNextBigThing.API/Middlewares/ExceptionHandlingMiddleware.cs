using System.Net;
using TheNextBigThing.Application.Utilities;
using TheNextBigThing.Domain.Exceptions;
using TheNextBigThing.Domain.Responses;

#pragma warning disable 1591

namespace TheNextBigThing.API.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/xml";

            response.StatusCode = error switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                CurrencyClientException e => e.Code,
                CurrencyDataException => (int)HttpStatusCode.NotFound,
                InvalidOperationException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            await response.WriteAsync(XmlUtility.Serialize(new MessageResponse() { Message = error.Message }));
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
