namespace Payroll.API.EmployeesRoster;

public class Employee(string name, decimal hourlyRate)
{
	public long Id { get; set; }
	public string Name { get; set; } = name;
	public decimal HourlyRate { get; set; } = hourlyRate;
}