using Microsoft.EntityFrameworkCore;

namespace Payroll.API.DataAccess;

public static class AddPayrollContextExtension
{
	public static IServiceCollection AddPayrollContext(this IServiceCollection services, string connectionString)
	{

		services.AddDbContext<PayrollContext>(options =>
		{
			options
				.UseNpgsql(connectionString)
				.EnableSensitiveDataLogging();
		});

		return services;
	}
}