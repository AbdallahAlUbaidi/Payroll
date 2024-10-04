
namespace Payroll.Tests.AcceptanceTests;

public class EmployeeRosterTests
{
    private readonly IEmployeeRosterDriver driver = new EmployeeRosterWebDriver("http://localhost:5000");


    [Fact]
    public void WhenAddingNewEmployee_TheEmployeeShouldExistInTheRoster()
    {
        string employeeName = "John Doe";
        driver.AddNewEmployee(employeeName);
        driver.ShowEmployeeExistsInRoster(employeeName);
    }
}
