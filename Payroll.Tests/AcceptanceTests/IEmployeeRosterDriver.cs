
namespace Payroll.Tests.AcceptanceTests;

public interface IEmployeeRosterDriver
{
	public void AddNewEmployee(string employeeName);

	public void ShowEmployeeExistsInRoster(string employeeName);
}