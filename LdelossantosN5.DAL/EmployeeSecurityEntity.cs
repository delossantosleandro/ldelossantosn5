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
        /// <summary>
        /// Request a permission
        /// </summary>
        /// <param name="type">The type of requested permission</param>
        /// <returns>True when succeed, false if the permission already exists</returns>
        public bool RequestPermission(PermissionTypeEntity type)
        {
            if (this.Permissions.Count(x => x.PermissionType == type) > 0)
                return false;
            this.Permissions.Add(new EmployeePermissionEntity() { Employee = this, PermissionType = type, RequestStatus = PermissionStatus.permissionRequested });
            return true;
        }
    }
}
