using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Notification
{
    public class PermissionModifyNotification
        : INotification
    {
        public int PermissionId { get; set; }
        public int EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
