using LdelossantosN5.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.CQRS
{
    public interface IEmployeeSecurityCQRSRepository
    {
        Task<EmployeeSecurityModel> GetEmployeeSecurityAsync(int employeeId);
        Task UpsertEmployeeSecurityAsync(EmployeeSecurityModel employeePermissionModel);
    }
}
