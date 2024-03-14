﻿using LdelossantosN5.DAL.Patterns;
using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Tests.Patterns
{
    public class EfUnitOfWorkTest
        : IClassFixture<SqlLocalDbFixture>
    {
        public SqlLocalDbFixture DbFixture { get; }
        public ILoggerFactory LogFactory { get; }

        public EfUnitOfWorkTest(SqlLocalDbFixture fixture)
        {
            DbFixture = fixture;
            this.LogFactory = LoggerFactory.Create(builder => builder.AddDebug());
        }

        [Fact]
        public async Task UOFESaveAndCommit()
        {
            using var ctx = this.DbFixture.CreateContext();
            var logger = this.LogFactory.CreateLogger<EfUnitOfWork>();
            var uof = new EfUnitOfWork(ctx, logger);
            await uof.BeginTransaction();

            //Create a repository with a valid record
            var repository = new EfRepositoryBase<PermissionTypeEntity>(ctx);
            var newPermission = new PermissionTypeEntity()
            {
                ShortName = "Short Name",
                Description = "Long Description"
            };
            repository.Add(newPermission);
            var result = await uof.SaveAndCommitAsync();
            Assert.True(result);
        }

        [Fact]
        public async Task UOFRollbackOnFailure()
        {
            using var ctx = this.DbFixture.CreateContext();
            var logger = this.LogFactory.CreateLogger<EfUnitOfWork>();
            var uof = new EfUnitOfWork(ctx, logger);
            await uof.BeginTransaction();

            //Create a repository with a valid record
            var repository = new EfRepositoryBase<EmployeePermissionEntity>(ctx);
            var newPermission = new EmployeePermissionEntity(); //Invalid everywhere
            repository.Add(newPermission);
            var result = await uof.SaveAndCommitAsync();
            Assert.False(result);
        }

        [Fact]
        public async Task SaveValidatesExistingTransaction()
        {
            using var ctx = this.DbFixture.CreateContext();
            var logger = this.LogFactory.CreateLogger<EfUnitOfWork>();
            var uof = new EfUnitOfWork(ctx, logger);
            await Assert.ThrowsAsync<InactiveTransactionException>(() => uof.SaveAndCommitAsync());
        }
    }
}
