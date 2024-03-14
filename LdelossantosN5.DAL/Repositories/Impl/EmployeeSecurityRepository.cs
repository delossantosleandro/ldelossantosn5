using LdelossantosN5.DAL.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Repositories.Impl
{
    public class EmployeeSecurityRepository
        : EfRepositoryBase<EmployeeSecurityEntity>,
        IEmployeeSecurityRepository
    {
        public EmployeeSecurityRepository(UserPermissionDbContext ctx)
            : base(ctx)
        {
        }
    }
}
