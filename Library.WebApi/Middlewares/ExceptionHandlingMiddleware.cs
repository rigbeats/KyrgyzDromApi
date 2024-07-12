using Library.Application.Common.Exceptions;
using Library.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace Library.WebApi.Middlewares
{
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
			catch (NotFoundException ex)
			{
				await HandleExceptionAsync(httpContext,
					ex.Message,
					HttpStatusCode.NotFound);
			}
			catch (AlreadyExistsException ex)
			{
				await HandleExceptionAsync(httpContext,
					ex.Message,
					HttpStatusCode.Conflict);
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
}
