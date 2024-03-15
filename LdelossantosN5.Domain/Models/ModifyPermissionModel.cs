using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Models
{
    public class ModifyPermissionModel
    {
        public int PermissionId { get; set; }
        public int EmployeeId { get; set; }
        public PermissionStatus NewStatus { get; set; }
    }
}
