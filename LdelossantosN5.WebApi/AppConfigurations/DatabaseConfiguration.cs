using LdelossantosN5.Domain.CQRS;
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

        public static async Task Initialize(IServiceProvider services)
        {
            var db = services.GetService<UserPermissionDbContext>();
            await db!.Database.EnsureCreatedAsync();
        }

        public static async Task InitializeCQRS(IServiceProvider services)
        {
            var db = services.GetService<UserPermissionDbContext>()!;
            var idToInitialize = db.EmployeeSecuritySet.Select(x => x.Id).ToList();
            foreach (var id in idToInitialize)
            {
                var cqrs = services.GetService<IEmployeeSecurityCQRS>();
                await cqrs.OptimizeReadingAsync(id);
            }
            await db.SaveChangesAsync();
        }
    }
}
