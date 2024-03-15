using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Notification
{
    public class GetPermissionNotification
        : INotification
    {
        public int EmployeeId { get; set; }
    }
}
