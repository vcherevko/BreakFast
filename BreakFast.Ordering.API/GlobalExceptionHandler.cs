﻿using System.Net;
using BreakFast.Ordering.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace BreakFast.Ordering.API;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		logger.LogError("An error occurred while processing your request: {Message}", exception.Message);

		var errorResponse = new ErrorResponse
		{
			Message = exception.Message
		};

		switch (exception)
		{
			case OrderAccessDeniedException:
				errorResponse.StatusCode = (int)HttpStatusCode.Forbidden;
				errorResponse.Title = exception.GetType().Name;
				break;
			case OrderNotFoundException:
			case BreakFastAppException:
			case BadHttpRequestException:
				errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
				errorResponse.Title = exception.GetType().Name;
				break;
			default:
				errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
				errorResponse.Title = "Internal Server Error";
				break;
		}

		httpContext.Response.StatusCode = errorResponse.StatusCode;

		await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
		return true;
	}
}
