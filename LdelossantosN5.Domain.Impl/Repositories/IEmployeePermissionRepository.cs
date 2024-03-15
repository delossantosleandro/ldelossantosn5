using LdelossantosN5.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Repositories
{
    public interface IEmployeePermissionRepository
    {
        Task<int> UpdateEmployeePermissionAsync(int permissionId, int employeeId, PermissionStatus status);
    }
}
