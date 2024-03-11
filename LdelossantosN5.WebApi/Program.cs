using ldelossantosn5.Domain;
using LdelossantosN5.WebApi.AppConfigurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DatabaseConfiguration.Configure(builder);
SerilogConfiguration.Configure(builder);
JsonSerializationConfiguration.Configure(builder);

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

    app.MapPost("/employeesecurity/{employeeId}/permissions", (int employeeId) =>
    {
        return string.Empty;
    })
    .WithName("PostEmployeeSecurity")
    .WithOpenApi();

    app.MapPut("/employeesecurity/{employeeId}/permissions/{permissionId}", (int employeeId, int permissionId) =>
    {
        return string.Empty;
    })
    .WithName("PutEmployeeSecurity")
    .WithOpenApi();
}