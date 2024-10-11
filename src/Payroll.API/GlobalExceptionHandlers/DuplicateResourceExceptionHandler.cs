using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Payroll.API.Common.Exceptions;

namespace Payroll.API.GlobalExceptionHandler;

public class DuplicateResourceExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is not DuplicateResourceException duplicateResourceException)
			return false;

		httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;

		ProblemDetails problemDetails = new()
		{
			Title = "Duplicate resource exception",
			Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
			Status = (int)HttpStatusCode.Conflict,
			Detail = duplicateResourceException.Message,

		};

		problemDetails.Extensions["ErrorCode"] = duplicateResourceException.ErrorCode;
		await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

		return true;
	}
}
