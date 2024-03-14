using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Patterns
{
    public class NotFoundException
        : Exception
    {
        public NotFoundException(Type TEntity, int recordID)
            : base($"Record not found {TEntity} / {recordID}")
        {
        }
    }
}
