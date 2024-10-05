using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payroll.API.EmployeesRoster;

namespace Payroll.API.DataAccess.DataMapping;


public class EmployeeMap : IEntityTypeConfiguration<Employee>
{
	public void Configure(EntityTypeBuilder<Employee> builder)
	{
		builder.ToTable("Employees");
		builder.HasKey(e => e.Id);
		builder.Property(e => e.Name);
		builder.Property(e => e.HourlyRate);
	}
}