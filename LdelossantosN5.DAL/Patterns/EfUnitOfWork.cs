using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Patterns
{
    public class EfUnitOfWork
        : IUnitOfWork

    {
        protected DbContext Ctx { get; }
        protected ILogger<EfUnitOfWork> Logger { get; }
        protected IDbContextTransaction Transaction { get; private set; }
        private EfUnitOfWork(DbContext ctx, IDbContextTransaction transaction, ILogger<EfUnitOfWork> logger)
        {
            this.Ctx = ctx;
            this.Transaction = transaction;
            this.Logger = logger;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                bool result = await this.Ctx.SaveChangesAsync() > 0;
                await this.Transaction!.CommitAsync();
                this.Logger.LogInformation("Commit Transaction");
                return true;

            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Transaction error");
                await this.Transaction!.RollbackAsync();
                this.Logger.LogInformation(ex, "Transaction Rollback");
                return false;
            }
        }
        public static async Task<EfUnitOfWork> CreateAsync(DbContext ctx, ILogger<EfUnitOfWork> logger)
        {
            var transaction = await ctx.Database.BeginTransactionAsync();
            var instance = new EfUnitOfWork(ctx, transaction, logger);
            return instance;
        }
    }
}
