using LdelossantosN5.DAL.Repositories;
using LdelossantosN5.Domain;
using LdelossantosN5.Domain.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.CQRS
{
    public class EmployeeSecurityCQRS
        : IEmployeeSecurityCQRS
    {
        private IEmployeeSecurityRepository EmployeeRepo { get; }
        private IEmployeeSecurityCQRSRepository CqrsRepo { get; }
        public EmployeeSecurityCQRS(IEmployeeSecurityRepository employeeRepo, IEmployeeSecurityCQRSRepository cqrsRepo)
        {
            EmployeeRepo = employeeRepo;
            CqrsRepo = cqrsRepo;
        }
        public async Task<EmployeeSecurityModel> GetEmployeeSecurityAsync(int employeeId)
        {
            return await this.CqrsRepo.GetEmployeeSecurityAsync(employeeId);
        }
        public async Task OptimizeReadingAsync(int employeeId)
        {
            var employee = await this.EmployeeRepo.GetEmployeeOptimizedAsync(employeeId);
            var model = new EmployeeSecurityModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Permissions = employee.Permissions.Select(x => new EmployeePermissionModel()
                {
                    Id = x.Id,
                    Description = x.PermissionType!.Description,
                    PermissionTypeId = x.PermissionType.Id,
                    ShortName = x.PermissionType!.ShortName,
                    State = x.RequestStatus
                })
                .ToList()
            };
            await this.CqrsRepo.UpsertEmployeeSecurityAsync(model);
        }
    }

}
