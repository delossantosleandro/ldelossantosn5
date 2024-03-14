
namespace LdelossantosN5.DAL.Indexes
{
    /// <summary>
    /// Elastic search index for employee permissions
    /// </summary>
    public interface IEmployeePermissionIndex
    {
        Task UpsertAsync(EmployeePermisionIndexEntry entry);
    }
}