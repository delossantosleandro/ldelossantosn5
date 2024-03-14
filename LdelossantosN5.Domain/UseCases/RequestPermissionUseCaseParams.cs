using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.UseCases
{
    public class RequestPermissionUseCaseParams
    {
        public int EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
