using LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class MediatRConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(PermissionRequestCQRSOptimizeHandler).Assembly);
            });
        }
    }
}
