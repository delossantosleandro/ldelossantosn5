using LdelossantosN5.Domain.Impl.DbEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Tests.DbEntities
{
    public class PermissionTypeEntityTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public SqlLocalDbFixture DbFixture { get; }
        public PermissionTypeEntityTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }

        [Fact]
        public async Task CreatePermissionType()
        {
            using var ctx = this.DbFixture.CreateContext();
            var newPermission = new PermissionTypeEntity()
            {
                Description = "A new permission for my tests",
                ShortName = "New Permission"
            };
            ctx.PermissionTypeSet.Add(newPermission);
            await ctx.SaveChangesAsync();
            Assert.True(newPermission.Id > 0);
            Assert.True(newPermission.Timestamp.Length > 0);
        }

        [Fact]
        public async Task UpdatePermissionType()
        {
            var newId = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var newPermission = new PermissionTypeEntity()
                {
                    Description = "A new permission for my tests",
                    ShortName = "New Permission"
                };
                ctx.PermissionTypeSet.Add(newPermission);
                await ctx.SaveChangesAsync();
                newId = newPermission.Id;
            }

            var newDescription = DateTime.Now.ToString();
            using (var ctx = this.DbFixture.CreateContext())
            {
                var currRecord = await ctx.PermissionTypeSet.FindAsync(newId);
                Assert.NotNull(currRecord);
                currRecord.Description = newDescription + "long";
                currRecord.ShortName = newDescription + "short";
                await ctx.SaveChangesAsync();
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                var currRecord = await ctx.PermissionTypeSet.FindAsync(newId);
                Assert.NotNull(currRecord);
                Assert.Equal(newDescription + "long", currRecord.Description);
                Assert.Equal(newDescription + "short", currRecord.ShortName);
            }
        }
        [Fact]
        public async Task RemovePermissionType()
        {
            var newId = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var newPermission = new PermissionTypeEntity()
                {
                    Description = "A new permission for my tests",
                    ShortName = "New Permission"
                };
                ctx.PermissionTypeSet.Add(newPermission);
                await ctx.SaveChangesAsync();
                newId = newPermission.Id;
            }

            var newDescription = DateTime.Now.ToString();
            using (var ctx = this.DbFixture.CreateContext())
            {
                var toRemove = await ctx.PermissionTypeSet.FindAsync(newId);
                Assert.NotNull(toRemove);
                ctx.PermissionTypeSet.Remove(toRemove);
                await ctx.SaveChangesAsync();
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                var removed = await ctx.PermissionTypeSet.FindAsync(newId);
                Assert.Null(removed);
            }
        }

    }
}
