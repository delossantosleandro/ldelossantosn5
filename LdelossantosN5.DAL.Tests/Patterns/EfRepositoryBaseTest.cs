using LdelossantosN5.DAL.Patterns;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Tests.Patterns
{
    public class EfRepositoryBaseTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public SqlLocalDbFixture DbFixture { get; }
        public EfRepositoryBaseTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }

        [Fact]
        public async Task AddWithRepository()
        {
            using var ctx = this.DbFixture.CreateContext();
            var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
            var newPermission = new PermissionTypeEntity()
            {
                ShortName = "Short Name",
                Description = "Long Description"
            };
            repository.Add(newPermission);
            await ctx.SaveChangesAsync();
            Assert.True(newPermission.Id > 0);
        }
        [Fact]
        public async Task FindWithRepository()
        {
            var newID = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
                var newPermission = new PermissionTypeEntity()
                {
                    ShortName = "Short Name",
                    Description = "Long Description"
                };
                repository.Add(newPermission);
                await ctx.SaveChangesAsync();
                newID = newPermission.Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
                var record = await repository.FindAsync(newID);
                Assert.NotNull(record);
                Assert.Equal("Short Name", record.ShortName);
                Assert.Equal("Long Description", record.Description);
            }
        }
        [Fact]
        public async Task RemoveWithRepository()
        {
            var newID = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
                var newPermission = new PermissionTypeEntity()
                {
                    ShortName = "Short Name",
                    Description = "Long Description"
                };
                repository.Add(newPermission);
                await ctx.SaveChangesAsync();
                newID = newPermission.Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
                var record = await repository.FindAsync(newID);
                repository.Remove(record);
                var res = await ctx.SaveChangesAsync();
                Assert.True(res > 0);
            }
        }
        [Fact]
        public async Task FindRaisesExceptionWhenNotfound()
        {
            using var ctx = this.DbFixture.CreateContext();
            var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
            await Assert.ThrowsAsync<ArgumentException>(() => repository.FindAsync(-1));
        }
    }
}
