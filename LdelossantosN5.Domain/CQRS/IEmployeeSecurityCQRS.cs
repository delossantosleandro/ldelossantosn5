using LdelossantosN5.Domain.Models;

namespace LdelossantosN5.Domain.CQRS
{
    public interface IEmployeeSecurityCQRS
    {
        Task OptimizeReadingAsync(int employeeId);
        Task<EmployeeSecurityModel> GetEmployeeSecurityAsync(int employeeId);
    }
}
