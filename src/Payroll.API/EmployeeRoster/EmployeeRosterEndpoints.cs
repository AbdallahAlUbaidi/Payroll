namespace Payroll.API.EmployeesRoster;


public static class EmployeesRosterEndpoints
{
	private static readonly string baseUrl = "api/v1/employees";
	public static WebApplication MapEmployeeRosterEndpoints(this WebApplication app)
		=> app.MapAddNewEmployee(baseUrl);
}