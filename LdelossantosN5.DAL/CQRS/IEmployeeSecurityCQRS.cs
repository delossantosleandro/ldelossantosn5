using LdelossantosN5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.CQRS
{
    public interface IEmployeeSecurityCQRS
    {
        Task OptimizeReadingAsync(int employeeId);
        Task<EmployeeSecurityModel> GetEmployeeSecurityAsync(int employeeId);
    }
}
