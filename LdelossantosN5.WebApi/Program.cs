using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.UseCases;
using LdelossantosN5.WebApi.AppConfigurations;
using LdelossantosN5.WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DatabaseConfiguration.Configure(builder);
SerilogConfiguration.Configure(builder);
JsonSerializationConfiguration.Configure(builder);

ConfigureDomainServicesDI.Configure(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

SerilogConfiguration.ConfigureMiddleware(app);

ConfigureEndpoints(app);

app.Run();

static void ConfigureEndpoints(WebApplication app)
{
    app.MapGet("/employeesecurity/{employeeId}", (int employeeId) =>
    {
        return new EmployeeSecurityModel() { Id = employeeId };
    })
    .WithName("GetEmployeeSecurity")
    .WithOpenApi();

    app.MapPost("/employeesecurity/{employeeId}/permissions", async (int employeeId, RequestPermissionModel theModel, IRequestPermissionUseCase useCase) =>
    {
        var param = new RequestPermissionUseCaseParams() { EmployeeId = employeeId, PermissionTypeId = theModel.PermissionTypeId };
        var result = await useCase.ExecuteAsync(param);
        return result.ToHttpResponse();
    })
    .WithName("Request Employee Permission")
    .WithOpenApi();

    app.MapPut("/employeesecurity/{employeeId}/permissions/{permissionId}", (int employeeId, int permissionId) =>
    {
        return string.Empty;
    })
    .WithName("PutEmployeeSecurity")
    .WithOpenApi();
}