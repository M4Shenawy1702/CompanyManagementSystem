using Domain.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Proceed to next middleware
        }
        catch (ServiceException ex)
        {
            await HandleExceptionAsync(context, ex.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, "Something went wrong!", HttpStatusCode.InternalServerError);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var result = JsonSerializer.Serialize(new
        {
            status = context.Response.StatusCode,
            error = message
        });

        return context.Response.WriteAsync(result);
    }
}
