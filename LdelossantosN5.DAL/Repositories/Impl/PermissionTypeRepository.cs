using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Impl.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Repositories.Impl
{
    public class PermissionTypeRepository
        : EfRepositoryBase<PermissionTypeEntity>,
        IPermissionTypeRepository

    {
        public PermissionTypeRepository(UserPermissionDbContext ctx)
            : base(ctx)
        {
        }
    }
}
