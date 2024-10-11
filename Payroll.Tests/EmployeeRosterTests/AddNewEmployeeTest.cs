using System.Reflection.Metadata;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Payroll.API.Common.Exceptions;
using Payroll.API.DataAccess;
using Payroll.API.EmployeesRoster;

namespace Payroll.Tests.EmployeeRosterTests;

public class AddNewEmployeeTests : IDisposable
{
	private readonly PayrollContext _payrollContext;


	public AddNewEmployeeTests()
	{
		var options = new DbContextOptionsBuilder<PayrollContext>()
			.UseInMemoryDatabase("TestDb")
			.Options;

		_payrollContext = new PayrollContext(options);
	}

	public void Dispose()
	{
		_payrollContext.Database.EnsureDeleted();
		_payrollContext.Dispose();
		GC.SuppressFinalize(this);
	}

	[Fact]
	public async void ShouldSaveAddedEmployee()
	{
		string employeeName = "Abdullah";
		decimal hourlyRate = 6250;

		await AddNewEmployee.Handle(new(employeeName, hourlyRate), _payrollContext, default);

		_payrollContext.Employees
			.Where(employee => employee.Name == employeeName)
			.Any()
			.Should().BeTrue();

	}

	[Fact]
	public async void ShouldThrowDuplicateResourceException_WhenTryingToAddSameEmployeeTwice()
	{
		string employeeName = "Abdullah";
		decimal hourlyRate = 6250;
		await AddEmployee(employeeName, hourlyRate);

		Func<Task> action = async () => await AddNewEmployee.Handle(new(employeeName, hourlyRate), _payrollContext, default);

		await action.Should().ThrowAsync<DuplicateResourceException>();
	}

	private async Task AddEmployee(string name, decimal hourlyRate)
	{
		await AddNewEmployee.Handle(new(name, hourlyRate), _payrollContext, default);
	}

}