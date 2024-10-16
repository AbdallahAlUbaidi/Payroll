using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Common;
using Payroll.API.Common.Exceptions;
using Payroll.API.DataAccess;

namespace Payroll.API.EmployeesRoster;

public static class AddNewEmployee
{
	public record Request(string Name, decimal HourlyRate);

	public class Validator : AbstractValidator<Request>
	{
		public Validator()
		{
			RuleFor(req => req.Name)
				.NotEmpty()
				.WithMessage("Employee name cannot be empty")
				.WithErrorCode("EMPTY_EMPLOYEE_NAME");

			const int maxNameLength = 100;
			RuleFor(req => req.Name)
				.MaximumLength(maxNameLength)
				.WithMessage($"Employee name cannot longer than {maxNameLength} characters")
				.WithErrorCode("NAME_TOO_LONG");

			RuleFor(req => req.HourlyRate)
				.GreaterThan(0)
				.WithMessage("Hourly rate must be greater than zero")
				.WithErrorCode("INVALID_HOURLY_RATE");
		}
	}

	public static WebApplication MapAddNewEmployee(this WebApplication app, string baseUrl)
	{
		app
			.MapPost(baseUrl, Handle)
			.WithSummary("Adds new employee to the employee roster")
			.WithRequestValidation<Request>();
		return app;
	}

	public static async Task<IResult> Handle(
			Request request,
			PayrollContext context,
			CancellationToken cancellationToken
		)
	{
		bool employeeAlreadyExists = await context.Employees
			.AnyAsync(e => e.Name == request.Name, cancellationToken: cancellationToken);
		if (employeeAlreadyExists)
			throw new EmployeeAlreadyExists();
		Employee employee = new(request.Name, request.HourlyRate);
		context.Add(employee);
		await context.SaveChangesAsync(cancellationToken);
		return Results.Created();
	}

	public class EmployeeAlreadyExists()
		: DuplicateResourceException(nameof(Employee), "Employee with specified name already in use");

}
