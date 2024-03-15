using LdelossantosN5.Domain.Impl.DbEntities;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.Repositories;
using LdelossantosN5.Domain.UseCases;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LdelossantosN5.Domain.Impl.UseCases
{
    /// <summary>
    /// By the book implementation of the use case
    /// We use repositories and the logic is solved trough trough models
    /// </summary>
    public class RequestPermissionUseCaseUsingByTheBook
        : UseCaseBase<RequestPermissionUseCaseParams, PermissionRequestNotification>,
        IRequestPermissionUseCase
    {
        private IEmployeeSecurityRepository EmployeeRepository { get; }
        private IPermissionTypeRepository PermissionTypeRepository { get; }
        private EmployeeSecurityEntity? Employee { get; set; }
        private PermissionTypeEntity? PermissionType { get; set; }
        public EmployeePermissionEntity? EmployeePermission { get; private set; }

        public RequestPermissionUseCaseUsingByTheBook(
                INotificationSender notificationSender,
                IUnitOfWork uof,
                ILogger<RequestPermissionUseCaseUsingByTheBook> logger,
                IEmployeeSecurityRepository employeeRepository,
                IPermissionTypeRepository permissionTypeRepository
            )
            : base(notificationSender, uof, logger)
        {
            this.EmployeeRepository = employeeRepository;
            this.PermissionTypeRepository = permissionTypeRepository;
        }
        protected override async Task ExecuteUseCase(RequestPermissionUseCaseParams useCaseParam, UseCaseResultModel result)
        {
            this.Employee = await this.EmployeeRepository.FindAsync(useCaseParam.EmployeeId);
            this.PermissionType = await this.PermissionTypeRepository.FindAsync(useCaseParam.PermissionTypeId);
            this.EmployeePermission = this.Employee.RequestPermission(this.PermissionType);
            if (this.EmployeePermission is null)
                result.AddError("K_RecordAlreadyExist", $"The permission ({this.PermissionType.Id}) - {this.PermissionType.ShortName} already exists");
        }

        /// <summary>
        /// This event is executed after the commit...
        /// </summary>
        /// <returns></returns>
        protected override PermissionRequestNotification CreateNotificationMessage()
        {
            return new PermissionRequestNotification()
            {
                EmployeeId = this.Employee!.Id,
                PermissionId = this.EmployeePermission!.Id,
                PermissionTypeId = this.PermissionType!.Id
            };
        }

        public const string K_RecordAlreadyExist = "RecordAlreadyExist";
    }
}
