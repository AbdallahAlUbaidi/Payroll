namespace Payroll.API.Common.Exceptions;


public class DuplicateResourceException(string entityName, string message)
	: CustomException($"{entityName.Trim().ToUpper()}_ALREADY_EXISTS", message);