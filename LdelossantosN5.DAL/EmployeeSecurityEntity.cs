using LdelossantosN5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL
{
    public class EmployeeSecurityEntity
        : DbEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public virtual ICollection<EmployeePermissionEntity> Permissions { get; set; } = [];
        //Permissions are always requested
        public void AddPermission(PermissionTypeEntity type)
            => this.Permissions.Add(new EmployeePermissionEntity() { Employee = this, PermissionType = type, RequestStatus = PermissionRequestStatus.permissionRequested });
    }
}
