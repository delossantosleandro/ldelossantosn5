using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Tests.CQRS
{
    public class EmployeeSecurityCQRSRepositoryEfTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public EmployeeSecurityCQRSRepositoryEfTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }
        public SqlLocalDbFixture DbFixture { get; }

        [Fact]
        public async Task UpsertEmployeeSecurityAsyncCreatesNewRecords()
        {
            using (var ctx = this.DbFixture.CreateContext())
            {
                var model = new EmployeeSecurityModel() { Id = 25 };
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                Assert.True(ctx.CQRSSEmployeeSecurityEntitySet.Count(x => x.EmployeeId == 25) == 0);
                await repo.UpsertEmployeeSecurityAsync(model);
                ctx.SaveChanges();
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                Assert.True(ctx.CQRSSEmployeeSecurityEntitySet.Count(x => x.EmployeeId == 25) == 1);
            }
        }

        [Fact]
        public async Task UpsertEmployeeSecurityAsyncCreatesUpdateExistingRecord()
        {
            var model = new EmployeeSecurityModel() { Id = 555 };
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                Assert.True(ctx.CQRSSEmployeeSecurityEntitySet.Count(x => x.EmployeeId == 555) == 0);
                await repo.UpsertEmployeeSecurityAsync(model);
                ctx.SaveChanges();
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                model.Name = "Juan";
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                Assert.True(ctx.CQRSSEmployeeSecurityEntitySet.Count(x => x.EmployeeId == 555) == 1);
                await repo.UpsertEmployeeSecurityAsync(model);
                ctx.SaveChanges();
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                Assert.Contains("Juan", ctx.CQRSSEmployeeSecurityEntitySet.Single(x => x.EmployeeId == 555).Data);
            }
        }

        [Fact]
        public async Task GetEmployeeSecurityAsyncReturnsModel()
        {
            Action<EmployeePermissionModel, int, PermissionStatus> assertEmployeePermission = (EmployeePermissionModel permission, int iVal, PermissionStatus status) =>
            {
                Assert.Equal(iVal, permission.Id);
                Assert.Equal(iVal, permission.PermissionTypeId);
                Assert.Equal(iVal.ToString(), permission.ShortName);
                Assert.Equal(iVal.ToString(), permission.Description);
                Assert.Equal(status, permission.State);
            };

            var model = new EmployeeSecurityModel()
            {
                Id = 888,
                Name = "Jhon",
                Permissions = new List<EmployeePermissionModel>()
                {
                    new() { Id = 1, Description = "1", PermissionTypeId = 1, ShortName = "1", State = PermissionStatus.permissionGranted },
                    new() { Id = 2, Description = "2", PermissionTypeId = 2, ShortName = "2", State = PermissionStatus.permissionDenied },
                    new() { Id = 3, Description = "3", PermissionTypeId = 3, ShortName = "3", State = PermissionStatus.permissionRequested }
                }
            };
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                await repo.UpsertEmployeeSecurityAsync(model);
                ctx.SaveChanges();
            }
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                var readModel = await repo.GetEmployeeSecurityAsync(888);
                Assert.Equal(model.Id, readModel.Id);
                Assert.Equal(model.Name, readModel.Name);
                Assert.Equal(3, readModel.Permissions.Count);
                assertEmployeePermission(readModel.Permissions[0], 1, PermissionStatus.permissionGranted);
                assertEmployeePermission(readModel.Permissions[1], 2, PermissionStatus.permissionDenied);
                assertEmployeePermission(readModel.Permissions[2], 3, PermissionStatus.permissionRequested);
            }
        }

        [Fact]
        public async Task GetEmployeeSecurityAsyncThrowsNotFound()
        {
            using (var ctx = this.DbFixture.CreateContext())
            {
                var repo = new EmployeeSecurityCQRSRepositoryEf(ctx);
                await Assert.ThrowsAsync<NotFoundException>(() => repo.GetEmployeeSecurityAsync(-255));
            }
        }
    }
}