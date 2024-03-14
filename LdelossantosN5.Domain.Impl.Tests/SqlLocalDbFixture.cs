using LdelossantosN5.DAL;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.Domain.Impl.Tests
{
    public class SqlLocalDbFixture
    : IAsyncLifetime
    {
        public string ConnectionString { get; private set; }
        private string DatabaseName { get; set; }

        public SqlLocalDbFixture()
        {
            // Generate a unique database name
            DatabaseName = $"TestDb_{Guid.NewGuid()}";
            ConnectionString = $"Server=(localdb)\\mssqllocaldb;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public async Task InitializeAsync()
        {
            using (var context = CreateContext())
            {
                //await context.Database.EnsureCreatedAsync();
                await context.Database.MigrateAsync();
            }
        }

        public async Task DisposeAsync()
        {
            // Drop the database
            using (var context = CreateContext())
            {
                await context.Database.EnsureDeletedAsync();
            }
        }

        public UserPermissionDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<UserPermissionDbContext>()
                .UseSqlServer(ConnectionString)
                .UseLazyLoadingProxies()
                .Options;

            return new UserPermissionDbContext(options);
        }
    }
}