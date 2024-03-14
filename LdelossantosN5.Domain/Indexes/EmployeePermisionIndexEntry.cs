namespace LdelossantosN5.Domain.Indexes
{
    public class EmployeePermisionIndexEntry
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int PermissionTypeId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
