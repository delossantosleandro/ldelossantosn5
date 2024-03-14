using LdelossantosN5.DAL.Repositories;
using LdelossantosN5.DAL.Repositories.Impl;
using LdelossantosN5.Domain.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
