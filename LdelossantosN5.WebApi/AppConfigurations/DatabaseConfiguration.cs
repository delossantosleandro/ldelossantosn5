using LdelossantosN5.Domain.Impl.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class DatabaseConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var cs = Environment.GetEnvironmentVariable("DOCKER_ConnectionString")
                ?? builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<UserPermissionDbContext>(options => options
                .UseLazyLoadingProxies(true)
                .UseSqlServer(cs)
            );
        }
    }
}
