using LdelossantosN5.DAL.Patterns;
using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore;
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

        public async Task<EmployeeSecurityEntity> GetEmployeeOptimizedAsync(int employeeId)
        {
            return await this.DbSet
                .Include(x => x.Permissions)
                .ThenInclude(y => y.PermissionType)
                .FirstOrDefaultAsync(x => x.Id == employeeId)
                ?? throw new NotFoundException(typeof(EmployeeSecurityEntity), employeeId);
        }
    }
}