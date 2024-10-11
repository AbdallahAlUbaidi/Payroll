namespace Payroll.API.GlobalExceptionHandler;


public static class ExceptionHandlingConfiguration
{
	public static IServiceCollection ConfigureExceptionHandlers(this IServiceCollection services)
		=> services
				.AddExceptionHandler<DuplicateResourceExceptionHandler>()
				.AddExceptionHandler<GlobalExceptionHandler>();
}