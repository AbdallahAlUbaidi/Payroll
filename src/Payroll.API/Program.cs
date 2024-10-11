using FluentValidation;
using Payroll.API.DataAccess;
using Payroll.API.EmployeesRoster;
using Payroll.API.GlobalExceptionHandler;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Connection string is not set");

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddValidatorsFromAssemblyContaining<AddNewEmployee.Validator>()
    .AddHttpContextAccessor()
    .AddLogging(options =>
    {
        options.AddConsole();
    })
    .AddPayrollContext(connectionString)
    .ConfigureExceptionHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEmployeeRosterEndpoints();

app.UseExceptionHandler(o => { });

app.UseHttpsRedirection();

app.Run();

