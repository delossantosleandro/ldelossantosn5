using LdelossantosN5.DAL.Repositories.Impl;
using LdelossantosN5.DAL.Repositories;
using LdelossantosN5.Domain.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Tests.DbEntities
{
    public class PermissionTypeRepositoryTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public PermissionTypeRepositoryTest(SqlLocalDbFixture fixture)
        {
            this.DbFixture = fixture;
        }
        public SqlLocalDbFixture DbFixture { get; }
        [Fact]
        public void PermissionTypeRepositoryIsBaseRepository()
        {
            using var ctx = this.DbFixture.CreateContext();
            var newRepo = new PermissionTypeRepository(ctx);
            Assert.IsAssignableFrom<IPermissionTypeRepository>(newRepo);
            Assert.IsAssignableFrom<IRepository<PermissionTypeEntity>>(newRepo);
        }
    }
}
