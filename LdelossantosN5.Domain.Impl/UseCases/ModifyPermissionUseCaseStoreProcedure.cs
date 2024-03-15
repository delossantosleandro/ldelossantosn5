using LdelossantosN5.Domain.Impl.Repositories;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.UseCases;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.UseCases
{
    public class ModifyPermissionUseCaseStoreProcedure
        : UseCaseBase<ModifyPermissionModel, ModifyPermissionNotification>,
        IModifyPermissionUseCase
    {
        private IEmployeePermissionRepository EmployeePermissionRepo { get; }
        private ModifyPermissionModel? theUseCaseParam;
        private int PermissionTypeId { get; set; }
        public ModifyPermissionUseCaseStoreProcedure(
            INotificationSender notificationSender,
            IUnitOfWork uOF,
            ILogger<ModifyPermissionUseCaseStoreProcedure> logger,
            IEmployeePermissionRepository employeePermissionRepo)
            : base(notificationSender, uOF, logger)
        {
            this.EmployeePermissionRepo = employeePermissionRepo;
        }
        protected override ModifyPermissionNotification CreateNotificationMessage()
        {
            return new ModifyPermissionNotification()
            {
                EmployeeId = this.theUseCaseParam!.EmployeeId,
                PermissionId = this.theUseCaseParam!.PermissionId,
                PermissionTypeId = this.PermissionTypeId,
                Status = this.theUseCaseParam!.NewStatus.ToString()
            };
        }

        protected override async Task ExecuteUseCase(ModifyPermissionModel useCaseParam, UseCaseResultModel result)
        {
            this.theUseCaseParam = useCaseParam;
            if (useCaseParam.NewStatus == PermissionStatus.permissionRequested)
            {
                result.AddError("InvalidStatus", 
                    $"{PermissionStatus.permissionRequested} is an Invalid value, only {PermissionStatus.permissionGranted} or {PermissionStatus.permissionDenied} are allowed");
                return;
            }
            this.PermissionTypeId = await this.EmployeePermissionRepo.UpdateEmployeePermissionAsync(useCaseParam.PermissionId, useCaseParam.EmployeeId, useCaseParam.NewStatus);
        }
    }
}
