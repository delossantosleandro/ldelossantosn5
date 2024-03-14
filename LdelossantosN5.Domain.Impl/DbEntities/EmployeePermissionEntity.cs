using LdelossantosN5.Domain.Models;

namespace LdelossantosN5.Domain.Impl.DbEntities
{
    public class EmployeePermissionEntity
        : DbEntity
    {
        public PermissionStatus RequestStatus { get; set; }

        #region Employee relation
        public virtual EmployeeSecurityEntity? Employee { get; set; }
        public int EmployeeId { get; set; }
        #endregion

        #region PermissionTypeEntity
        public virtual PermissionTypeEntity? PermissionType { get; set; }
        public int PermissionTypeId { get; set; }
        #endregion
    }
}
