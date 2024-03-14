using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Patterns;

namespace LdelossantosN5.Domain.Repositories
{
    public interface IEmployeeSecurityRepository
        : IRepository<EmployeeSecurityEntity>
    {
        Task<EmployeeSecurityEntity> GetEmployeeOptimizedAsync(int employeeId);
    }
}
