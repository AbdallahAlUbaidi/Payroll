
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Payroll.Tests.AcceptanceTests;

public class EmployeeRosterWebDriver(string siteUrl) : IEmployeeRosterDriver
{

	private readonly string siteUrl = siteUrl;
	private readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(5);
	private readonly ChromeDriver chromeDriver = new();


	private void WaitUntilTableLoadsOrTimeOut()
	{
		var wait = new WebDriverWait(chromeDriver, TIMEOUT);
		wait.Until((driver) => driver.FindElement(By.Id("employees-table")).Displayed);
	}

	public void AddNewEmployee(string employeeName)
	{
		chromeDriver.Navigate().GoToUrl($"{siteUrl}/employee-roster");
		WaitUntilTableLoadsOrTimeOut();


		var addNewEmployeeButton = chromeDriver.FindElement(By.Id("add-employee-btn"));
		addNewEmployeeButton.Click();

		var employeeNameInputField = chromeDriver.FindElement(By.Id("employee-name"));
		var employeeHourlyRateInputField = chromeDriver.FindElement(By.Id("employee-hourly-rate"));

		employeeHourlyRateInputField.SendKeys(employeeName);
		employeeNameInputField.SendKeys("5000");

		var saveEmployeeButton = chromeDriver.FindElement(By.Id("save-employee-btn"));
		saveEmployeeButton.Click();
	}

	public void ShowEmployeeExistsInRoster(string employeeName)
	{
		WaitUntilTableLoadsOrTimeOut();
		var employeeTable = chromeDriver.FindElement(By.Id("employee-table"));
		var employeeRow = employeeTable.FindElements(By.XPath($"//tr[td[text()='{employeeName}']]"));
		Assert.True(employeeRow.Count > 0);
	}

}