
using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Impl.Indexes;
using LdelossantosN5.Domain.Impl.TopicsNotification;
using LdelossantosN5.Domain.Impl.UseCases;
using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Repositories;
using LdelossantosN5.Domain.Repositories.Impl;
using LdelossantosN5.Domain.TopicsNotification;
using LdelossantosN5.Domain.UseCases;
using LdelossantosN5.WebApi.BackgroundProcess;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class ConfigureDomainServicesDI
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotificationSender, MediatRBackgroundNotificationSender>();
            builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            builder.Services.AddScoped<IEmployeeSecurityRepository, EmployeeSecurityRepository>();
            builder.Services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            builder.Services.AddScoped<IRequestPermissionUseCase, RequestPermissionUseCaseUsingByTheBook>();
            builder.Services.AddScoped<IEmployeeSecurityCQRSRepository, EmployeeSecurityCQRSRepositoryEf>();
            builder.Services.AddScoped<IEmployeeSecurityCQRS, EmployeeSecurityCQRS>();
            builder.Services.AddScoped<IGetPermissionsUseCase, GetPermissionUseCaseCQRS>();
        }
    }
}
