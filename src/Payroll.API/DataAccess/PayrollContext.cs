using Microsoft.EntityFrameworkCore;
using Payroll.API.DataAccess.DataMapping;
using Payroll.API.EmployeesRoster;

namespace Payroll.API.DataAccess;

public class PayrollContext(DbContextOptions options) : DbContext(options)
{
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder.ApplyConfiguration(new EmployeeMap());
	}
}