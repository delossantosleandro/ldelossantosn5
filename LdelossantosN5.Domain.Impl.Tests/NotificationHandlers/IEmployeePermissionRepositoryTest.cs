using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Impl.Repositories;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Tests;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Tests.NotificationHandlers
{
    public class IEmployeePermissionRepositoryTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public IEmployeePermissionRepositoryTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }

        public SqlLocalDbFixture DbFixture { get; }


        [Fact]
        public void RepositoryIsImplemented()
        {
            using var ctx = this.DbFixture.CreateContext();
            Assert.IsAssignableFrom<IEmployeePermissionRepository>(ctx);
        }
        [Fact]
        public async Task RepositoryDontAcceptRequestPermission()
        {
            using var ctx = this.DbFixture.CreateContext();
            await Assert.ThrowsAsync<ArgumentException>(() => 
                (ctx as IEmployeePermissionRepository)
                .UpdateEmployeePermissionAsync(1, 2, PermissionStatus.permissionRequested)
            );
        }

        [Fact]
        public async Task RepositoryRemovesWhenRejectPermission()
        {
            int employeeId;
            int permissionId;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var newEmployee = CreateNewEmployee(ctx);
                await ctx.SaveChangesAsync();
                await dbTrans.CommitAsync();
                employeeId = newEmployee.Id;
                permissionId = newEmployee.Permissions.First().Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var res = await (ctx as IEmployeePermissionRepository).UpdateEmployeePermissionAsync(permissionId, employeeId, Models.PermissionStatus.permissionDenied);
                await dbTrans.CommitAsync();
                //When records are removed, we receive a negative value
                Assert.True(res < 0);
            }

            //Check the record it's not there
            using (var ctx = this.DbFixture.CreateContext())
            {
                var employee = await ctx.EmployeeSecuritySet.FindAsync(employeeId);
                Assert.NotNull(employee);
                Assert.Empty(employee.Permissions);
            }
        }

        [Fact]
        public async Task RepositoryReturnsPermissionTypeIdWhenGranted()
        {
            int employeeId;
            int permissionId;
            int permissionType;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var newEmployee = CreateNewEmployee(ctx);
                await ctx.SaveChangesAsync();
                await dbTrans.CommitAsync();
                employeeId = newEmployee.Id;
                permissionId = newEmployee.Permissions.First().Id;
                permissionType = newEmployee.Permissions.First().PermissionType!.Id;
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var res = await (ctx as IEmployeePermissionRepository).UpdateEmployeePermissionAsync(permissionId, employeeId, Models.PermissionStatus.permissionGranted);
                await dbTrans.CommitAsync();
                //When records are removed, we receive a negative value
                Assert.Equal(permissionType, res);
            }

            //Check the record it's not there
            using (var ctx = this.DbFixture.CreateContext())
            {
                var employee = await ctx.EmployeeSecuritySet.FindAsync(employeeId);
                Assert.NotNull(employee);
                Assert.Single(employee.Permissions);
            }
        }

        [Fact]
        public async Task IdPermissionAndEmployeeMustMatch()
        {
            int employeeId;
            int permissionId;
            int permissionType;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var newEmployee = CreateNewEmployee(ctx);
                await ctx.SaveChangesAsync();
                await dbTrans.CommitAsync();
                employeeId = newEmployee.Id;
                permissionId = newEmployee.Permissions.First().Id;
                permissionType = newEmployee.Permissions.First().PermissionType!.Id;
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                await Assert.ThrowsAsync<NotFoundException>(() =>
                    (ctx as IEmployeePermissionRepository)
                    .UpdateEmployeePermissionAsync(permissionId, employeeId + 1, PermissionStatus.permissionGranted)
                );
            }
        }
        [Fact]
        public async Task IdPermissionNotFound()
        {
            int employeeId;
            int permissionId;
            int permissionType;
            using (var ctx = this.DbFixture.CreateContext())
            {
                var dbTrans = await ctx.Database.BeginTransactionAsync();
                var newEmployee = CreateNewEmployee(ctx);
                await ctx.SaveChangesAsync();
                await dbTrans.CommitAsync();
                employeeId = newEmployee.Id;
                permissionId = newEmployee.Permissions.First().Id;
                permissionType = newEmployee.Permissions.First().PermissionType!.Id;
            }

            using (var ctx = this.DbFixture.CreateContext())
            {
                await Assert.ThrowsAsync<NotFoundException>(() =>
                    (ctx as IEmployeePermissionRepository)
                    .UpdateEmployeePermissionAsync(permissionId+1, employeeId, PermissionStatus.permissionGranted)
                );
            }
        }


        private EmployeeSecurityEntity CreateNewEmployee(UserPermissionDbContext ctx)
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
            newEmployee.RequestPermission(newTypePermission);
            ctx.EmployeeSecuritySet.Add(newEmployee);
            return newEmployee;
        }
    }
}
