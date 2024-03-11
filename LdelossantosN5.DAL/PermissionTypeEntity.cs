using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL
{
    public class PermissionTypeEntity
        : DbEntity
    {
        public string ShortName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
