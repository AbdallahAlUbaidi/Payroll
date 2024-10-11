using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Payroll.API.GlobalExceptionHandler;


public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
	private readonly ILogger<GlobalExceptionHandler> _logger = logger;

	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		LogException(exception);

		var status = (int)HttpStatusCode.InternalServerError;
		httpContext.Response.StatusCode = status;

		ProblemDetails problemDetails = new()
		{
			Title = "Internal server error",
			Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
			Status = status,
			Detail = exception.Message,
		};

		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

		return true;
	}

	private void LogException(Exception exception)
	{
		_logger.LogInformation($"Unhandled exception with type {exception.GetType().Name} was thrown with message : {exception.Message} \n");
		_logger.LogDebug($"Stack Trace: {exception.StackTrace} \n");
	}
}