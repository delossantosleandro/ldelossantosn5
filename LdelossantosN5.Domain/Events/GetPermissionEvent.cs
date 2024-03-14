using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Events
{
    public class GetPermissionEvent
        : IRequest
    {
        public int EmployeeId { get; set; }
    }
}
