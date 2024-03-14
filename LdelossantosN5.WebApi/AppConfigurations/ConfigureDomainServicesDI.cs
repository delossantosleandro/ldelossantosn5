
using LdelossantosN5.DAL.Patterns;
using LdelossantosN5.DAL.Repositories;
using LdelossantosN5.DAL.Repositories.Impl;
using LdelossantosN5.Domain.Impl.UseCases;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.UseCases;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class ConfigureDomainServicesDI
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            builder.Services.AddScoped<IEmployeeSecurityRepository, EmployeeSecurityRepository>();
            builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            builder.Services.AddScoped<IRequestPermissionUseCase, RequestPermissionUseCaseUsingByTheBook>();
        }
    }
}
