using LdelossantosN5.DAL;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class DatabaseConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("DefaultConnection setting is missing");

            builder.Services.AddDbContext<UserPermissionDbContext>(options => options
                .UseLazyLoadingProxies(true)
                .UseSqlServer(cs, sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                })
            );
        }
    }
}
