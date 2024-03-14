using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain
{
    public class EmployeePermissionModel
    {
        public int Id { get; set; }
        public int PermissionTypeId { get; set; }
        public PermissionStatus State { get; set; }
        public string ShortName { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
