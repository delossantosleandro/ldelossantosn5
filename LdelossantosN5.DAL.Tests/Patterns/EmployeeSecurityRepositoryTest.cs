using LdelossantosN5.DAL.Repositories;
using LdelossantosN5.DAL.Repositories.Impl;
using LdelossantosN5.Domain;
using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Tests.Patterns
{
    public class EmployeeSecurityRepositoryTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public EmployeeSecurityRepositoryTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }

        public SqlLocalDbFixture DbFixture { get; }
        [Fact]
        public void EmployeeRepositoryIsBaseRepository()
        {
            using var ctx = this.DbFixture.CreateContext();
            var newRepo = new EmployeeSecurityRepository(ctx);
            Assert.IsAssignableFrom<IEmployeeSecurityRepository>(newRepo);
            Assert.IsAssignableFrom<IRepository<EmployeeSecurityEntity>>(newRepo);
        }

        [Fact]
        public async Task GetEmployeeOptimizedAsyncRetrieveTheRecord()
        {
            int newEmployeeId = 0;

            using (var ctx = this.DbFixture.CreateContext())
            {
                var newEmployee = new EmployeeSecurityEntity() { Name = "Juan", StartDate = DateTime.Now.AddYears(-10) };
                foreach (var permissionType in ctx.PermissionTypeSet)
                    newEmployee.RequestPermission(permissionType);
                ctx.EmployeeSecuritySet.Add(newEmployee);
                await ctx.SaveChangesAsync();
                newEmployeeId = newEmployee.Id;
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                IEmployeeSecurityRepository repo = new EmployeeSecurityRepository(ctx);
                var employee = await repo.GetEmployeeOptimizedAsync(newEmployeeId);
                Assert.Equal(newEmployeeId, employee.Id);
                Assert.Equal("Juan", employee.Name);
                foreach (var permissionType in ctx.PermissionTypeSet)
                {
                    var record = employee.Permissions.SingleOrDefault(x => x.PermissionTypeId == permissionType.Id);
                    Assert.NotNull(record);
                    Assert.NotNull(record.PermissionType);
                    Assert.Equal(permissionType.ShortName, record.PermissionType.ShortName);
                    Assert.Equal(permissionType.Description, record.PermissionType.Description);
                    Assert.Equal(PermissionStatus.permissionRequested, record.RequestStatus);
                }
                Assert.Equal(ctx.PermissionTypeSet.Count(), employee.Permissions.Count);
            }
        }

        [Fact]
        public async Task GetEmployeeOptimizedAsyncThrowsIfNotFound()
        {
            using (var ctx = this.DbFixture.CreateContext())
            {
                IEmployeeSecurityRepository repo = new EmployeeSecurityRepository(ctx);
                await Assert.ThrowsAsync<NotFoundException>(() => repo.GetEmployeeOptimizedAsync(-55));
            }
        }
    }
}
