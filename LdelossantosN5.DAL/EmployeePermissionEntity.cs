using LdelossantosN5.Domain;

namespace LdelossantosN5.DAL
{
    public class EmployeePermissionEntity
        : DbEntity
    {
        public PermissionRequestStatus RequestStatus { get; set; }

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
