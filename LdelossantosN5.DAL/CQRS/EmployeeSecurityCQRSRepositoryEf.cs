using LdelossantosN5.Domain;
using LdelossantosN5.Domain.Patterns;
using System.Text.Json;

namespace LdelossantosN5.DAL.CQRS
{
    public class EmployeeSecurityCQRSRepositoryEf
        : IEmployeeSecurityCQRSRepository
    {
        private UserPermissionDbContext DbContext { get; }
        public EmployeeSecurityCQRSRepositoryEf(UserPermissionDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<EmployeeSecurityModel> GetEmployeeSecurityAsync(int employeeId)
        {
            var record = await this.DbContext.CQRSSEmployeeSecurityEntitySet.FindAsync(employeeId)
                ?? throw new NotFoundException(typeof(CQRSSEmployeeSecurityEntity), employeeId);
            return JsonSerializer.Deserialize<EmployeeSecurityModel>(record.Data)!;
        }
        public async Task UpsertEmployeeSecurityAsync(EmployeeSecurityModel model)
        {
            var record = await this.DbContext.CQRSSEmployeeSecurityEntitySet.FindAsync(model.Id);
            if (record == null)
            {
                record = new CQRSSEmployeeSecurityEntity() { EmployeeId = model.Id };
                this.DbContext.CQRSSEmployeeSecurityEntitySet.Add(record);
            }
            record.Data = JsonSerializer.Serialize(model);
        }
    }
}
