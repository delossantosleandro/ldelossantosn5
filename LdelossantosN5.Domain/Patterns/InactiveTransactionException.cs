using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Patterns
{
    public class InactiveTransactionException
        : Exception
    {
        public InactiveTransactionException()
            : base("Inactive transaction")
        {
        }
    }
}
