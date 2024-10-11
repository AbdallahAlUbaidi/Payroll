namespace Payroll.API.Common.Exceptions;


public class CustomException(string errorCode, string message) : Exception(message)
{
	public string ErrorCode { get; init; } = errorCode;
}