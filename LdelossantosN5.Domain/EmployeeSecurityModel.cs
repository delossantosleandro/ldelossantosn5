using LdelossantosN5.Domain;

namespace ldelossantosn5.Domain
{
    public class EmployeeSecurityModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<EmployeePermissionModel> Permissions { get; set; } = [
            new EmployeePermissionModel() { Id = 1, PermissionTypeId = 1, ShortName = "Something", Description = "Just testing", State = PermissionRequestStatus.permissionDenied },
            new EmployeePermissionModel() { Id = 1, PermissionTypeId = 1, ShortName = "Something", Description = "Just testing", State = PermissionRequestStatus.permissionGranted },
            new EmployeePermissionModel() { Id = 1, PermissionTypeId = 1, ShortName = "Something", Description = "Just testing", State = PermissionRequestStatus.permissionRequested }
        ];
    }
}