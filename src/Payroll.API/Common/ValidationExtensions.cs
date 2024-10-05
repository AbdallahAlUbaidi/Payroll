namespace Payroll.API.Common;

public static class ValidationExtensions
{
	public static RouteHandlerBuilder WithRequestValidation<TRequest>(this RouteHandlerBuilder routeBuilder)
		=> routeBuilder
			.AddEndpointFilter<ValidationFilter<TRequest>>()
			.ProducesValidationProblem();
}