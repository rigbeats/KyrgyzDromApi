using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Dto;
using System.Net;
using System.Text.Json;

namespace KDrom.WebApi.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (InnerException ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.NotFound);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext,
                ex.Message,
                HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext,
        string exMessage,
        HttpStatusCode httpStatusCode)
    {
        _logger.LogError(exMessage);

        var response = httpContext.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        var errorDto = new ErrorDto()
        {
            Message = exMessage,
            StatusCode = (int)httpStatusCode,
        };

        string result = JsonSerializer.Serialize(errorDto);

        await response.WriteAsync(result);
    }
}
