using FluentValidation;
using Payroll.API.DataAccess;
using Payroll.API.EmployeesRoster;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new Exception("Connection string is not set");

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddValidatorsFromAssemblyContaining<AddNewEmployee.Validator>()
    .AddHttpContextAccessor()
    .AddPayrollContext(connectionString);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEmployeeRosterEndpoints();

app.UseHttpsRedirection();

app.Run();

