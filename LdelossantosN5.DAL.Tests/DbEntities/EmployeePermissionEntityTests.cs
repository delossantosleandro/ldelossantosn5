using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Tests.DbEntities
{
    public class EmployeePermissionEntityTests
        : IClassFixture<SqlLocalDbFixture>
    {
        public SqlLocalDbFixture DbFixture { get; }
        public EmployeePermissionEntityTests(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }

        [Fact]
        public async Task PermissionTypeConstraintRestrictCascade()
        {
            var newTypeId = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var newTypePermission = new PermissionTypeEntity()
                {
                    Description = "A new permission for my tests",
                    ShortName = "New Permission"
                };
                ctx.PermissionTypeSet.Add(newTypePermission);
                var newEmployee = new EmployeeSecurityEntity()
                {
                    Name = "Test",
                    StartDate = DateTime.Now,
                };
                //I made this just to show we can add logic to our model... but there are better ways for high volume transactional approach
                newEmployee.AddPermission(newTypePermission);
                ctx.EmployeeSecuritySet.Add(newEmployee);
                await ctx.SaveChangesAsync();
                newTypeId = newTypePermission.Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var recordToRemove = await ctx.PermissionTypeSet.FindAsync(newTypeId);
                Assert.NotNull(recordToRemove);
                ctx.PermissionTypeSet.Remove(recordToRemove);
                await Assert.ThrowsAsync<DbUpdateException>(() => ctx.SaveChangesAsync());
            }
        }
        [Fact]
        public async Task EmployeeConstrainCascadePermissions()
        {
            var newEmployeeId = 0;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var newTypePermission = new PermissionTypeEntity()
                {
                    Description = "A new permission for my tests",
                    ShortName = "New Permission"
                };
                ctx.PermissionTypeSet.Add(newTypePermission);
                var newEmployee = new EmployeeSecurityEntity()
                {
                    Name = "Test",
                    StartDate = DateTime.Now,
                };
                //I made this just to show we can add logic to our model... but there are better ways for high volume transactional approach
                newEmployee.AddPermission(newTypePermission);
                ctx.EmployeeSecuritySet.Add(newEmployee);
                await ctx.SaveChangesAsync();
                newEmployeeId = newEmployee.Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var recordToRemove = await ctx.EmployeeSecuritySet.FindAsync(newEmployeeId);
                Assert.NotNull(recordToRemove);
                ctx.EmployeeSecuritySet.Remove(recordToRemove);
                var res = await ctx.SaveChangesAsync();
                Assert.True(res > 0);
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                var removed = await ctx.EmployeeSecuritySet.FindAsync(newEmployeeId);
                Assert.Null(removed);
            }
        }
    }
}
