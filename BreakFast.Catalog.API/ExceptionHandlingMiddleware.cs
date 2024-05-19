using System.Net;
using System.Text.Json;

namespace BreakFast.Catalog.API;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlingMiddleware> _logger;

	public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
		catch (Exception exception)
		{
			_logger.LogError("An error occurred while processing your request: {Message}", exception.Message);
			await HandleExceptionAsync(httpContext, exception);
		}
	}

	private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
	{
		var errorResponse = new ErrorResponse
		{
			Message = exception.Message
		};

		switch (exception)
		{
			case BadHttpRequestException:
				errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
				errorResponse.Title = exception.GetType().Name;
				break;
			default:
				errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
				errorResponse.Title = "Internal Server Error";
				break;
		}

		httpContext.Response.ContentType = "application/json";
		httpContext.Response.StatusCode = errorResponse.StatusCode;

		await httpContext.Response.WriteAsJsonAsync(errorResponse.ToString());
	}

	private class ErrorResponse
	{
		public string Message { get; set; }

		public string Title { get; set; }

		public int StatusCode { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
