using LdelossantosN5.DAL.Patterns;
using LdelossantosN5.Domain;
using LdelossantosN5.Domain.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Repositories
{
    public interface IEmployeeSecurityRepository
        : IRepository<EmployeeSecurityEntity>
    {
        Task<EmployeeSecurityEntity> GetEmployeeOptimizedAsync(int employeeId);
    }
}
