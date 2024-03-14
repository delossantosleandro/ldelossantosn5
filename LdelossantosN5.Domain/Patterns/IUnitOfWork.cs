using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Patterns
{
    //DbContext already support this, but still helps to encapsulate a transaction.
    public interface IUnitOfWork
    {
        Task BeginTransaction();
        /// <summary>
        /// Executes the save and commit transaction, in case of error, throws an exception and rollback transaction
        /// </summary>
        /// <returns>True if it's ok, false in case of error</returns>
        Task<bool> SaveAndCommitAsync();
        Task ForceRollbackAsync();
    }
}
