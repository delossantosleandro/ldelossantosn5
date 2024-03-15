using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.UseCases;
using LdelossantosN5.WebApi.AppConfigurations;
using LdelossantosN5.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DatabaseConfiguration.Configure(builder);
SerilogConfiguration.Configure(builder);
JsonSerializationConfiguration.Configure(builder);
KafkaConfiguration.Configure(builder);
ElasticConfiguration.Configure(builder);
MediatRConfiguration.Configure(builder);
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

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        await KafkaConfiguration.Initialize(scope.ServiceProvider);
        await ElasticConfiguration.Initialize(scope.ServiceProvider);
        await DatabaseConfiguration.Initialize(scope.ServiceProvider);
        await DatabaseConfiguration.InitializeCQRS(scope.ServiceProvider);
    }
}
ConfigureEndpoints(app);

app.Run();

static void ConfigureEndpoints(WebApplication app)
{
    app.MapGet("/employeesecurity/{employeeId}",
        async (int employeeId, IGetPermissionsUseCase useCase) =>
    {
        var request = new GetPermissionsModel() { EmployeeId = employeeId };
        var result = await useCase.ExecuteAsync(request);
        return result.ToHttpResponse();
    })
    .WithName("Get Employee Permissions")
    .WithOpenApi();

    app.MapPost("/employeesecurity/{employeeId}/permissions",
        async (int employeeId, [FromBody] int permissionTypeId, IRequestPermissionUseCase useCase) =>
    {
        var param = new RequestPermissionUseCaseParams() { EmployeeId = employeeId, PermissionTypeId = permissionTypeId };
        var result = await useCase.ExecuteAsync(param);
        return result.ToHttpResponse();
    })
    .WithName("Request Employee Permission")
    .WithOpenApi();

    app.MapPut("/employeesecurity/{employeeId}/permissions/{permissionId}",
        async (int employeeId, int permissionId, [FromBody] ModifyPermissionRequest request, IModifyPermissionUseCase useCase) =>
    {
        var param = new ModifyPermissionModel() { EmployeeId = employeeId, PermissionId = permissionId, NewStatus = request.Status };
        var result = await useCase.ExecuteAsync(param);
        return result.ToHttpResponse();
    })
    .WithName("PutEmployeeSecurity")
    .WithOpenApi();
}