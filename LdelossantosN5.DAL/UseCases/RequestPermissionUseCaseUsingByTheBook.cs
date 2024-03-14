using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Repositories;
using LdelossantosN5.Domain.UseCases;
using Microsoft.Extensions.Logging;

namespace LdelossantosN5.Domain.Impl.UseCases
{
    /// <summary>
    /// By the book implementation of the use case
    /// We use repositories and the logic is solved trough trough models
    /// </summary>
    public class RequestPermissionUseCaseUsingByTheBook
        : UseCaseBase<RequestPermissionUseCaseParams>,
        IRequestPermissionUseCase
    {
        private IEmployeeSecurityRepository EmployeeRepository { get; }
        private IPermissionTypeRepository PermissionTypeRepository { get; }
        public RequestPermissionUseCaseUsingByTheBook(
            IEmployeeSecurityRepository employeeRepository,
            IPermissionTypeRepository permissionTypeRepository,
            IUnitOfWork uof,
            ILogger<RequestPermissionUseCaseUsingByTheBook> logger)
            : base(uof, logger)
        {
            this.EmployeeRepository = employeeRepository;
            this.PermissionTypeRepository = permissionTypeRepository;
        }
        protected override async Task ExecuteUseCase(RequestPermissionUseCaseParams useCaseParam, UseCaseResultModel result)
        {
            var employee = await this.EmployeeRepository.FindAsync(useCaseParam.EmployeeId);
            var permissionType = await this.PermissionTypeRepository.FindAsync(useCaseParam.PermissionTypeId);
            if (!employee.RequestPermission(permissionType))
                result.AddError("K_RecordAlreadyExist", $"The permission ({permissionType.Id}) - {permissionType.ShortName} already exists");
        }

        public const string K_RecordAlreadyExist = "RecordAlreadyExist";
    }
}
