using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Patterns;
using Microsoft.EntityFrameworkCore;

namespace LdelossantosN5.Domain.Repositories.Impl
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