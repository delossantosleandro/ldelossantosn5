namespace LdelossantosN5.Domain.Models
{
    public class EmployeeSecurityModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<EmployeePermissionModel> Permissions { get; set; } = [];
    }
}