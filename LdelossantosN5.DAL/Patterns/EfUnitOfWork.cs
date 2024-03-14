using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Patterns
{
    public class EfUnitOfWork
        : IUnitOfWork

    {
        protected DbContext Ctx { get; }
        protected ILogger<EfUnitOfWork> Logger { get; }
        private bool IsTransactionActive { get; set; }
        protected IDbContextTransaction? Transaction { get; private set; }
        public EfUnitOfWork(UserPermissionDbContext ctx, ILogger<EfUnitOfWork> logger)
        {
            this.Ctx = ctx;
            this.Logger = logger;
            this.IsTransactionActive = false;
        }
        public async Task BeginTransaction()
        {
            this.Transaction = await this.Ctx.Database.BeginTransactionAsync();
            this.IsTransactionActive = true;
        }
        public async Task<bool> SaveAndCommitAsync()
        {
            if (!this.IsTransactionActive)
                throw new InactiveTransactionException();
            try
            {
                bool result = await this.Ctx.SaveChangesAsync() > 0;
                await this.Transaction!.CommitAsync();
                this.Logger.LogInformation("Commit Transaction");
                this.IsTransactionActive = false;
                return true;

            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Transaction error");
                await this.ForceRollbackAsync();
                this.Logger.LogInformation(ex, "Transaction Rollback");
                return false;
            }
        }
        public async Task ForceRollbackAsync()
        {
            if (this.IsTransactionActive)
            {
                await this.Transaction!.RollbackAsync();
                this.IsTransactionActive = false;
            }
        }

    }
}
