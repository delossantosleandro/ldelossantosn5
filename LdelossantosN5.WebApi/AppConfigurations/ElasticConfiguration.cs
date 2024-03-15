using LdelossantosN5.Domain.Impl.Indexes;
using LdelossantosN5.Domain.Impl.TopicsNotification;
using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.TopicsNotification;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class ElasticConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var elasticIndex = new ElasticIndexSettings()
            {
                ServerName = builder.Configuration["ElasticIndexServer"] ?? "https://localhost:9200"
            };

            builder.Services.AddSingleton(elasticIndex);
            builder.Services.AddSingleton<IEmployeePermissionIndex, EmployeePermissionIndex>();
        }

        public static async Task Initialize(IServiceProvider services)
        {
            var index = services.GetService<IEmployeePermissionIndex>();
            await (index as EmployeePermissionIndex)!.EnsureIndexCreationAsync();
        }
    }
}
