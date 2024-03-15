using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.Notifications;
using LdelossantosN5.Domain.Patterns;
using LdelossantosN5.Domain.UseCases;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.UseCases
{
    public class GetPermissionUseCaseCQRS
        : UseCaseBase<GetPermissionsModel, GetPermissionNotification>,
        IGetPermissionsUseCase
    {
        private IEmployeeSecurityCQRS Cqrs { get; }
        private int EmployeeId { get; set; }
        public GetPermissionUseCaseCQRS(INotificationSender sender, IUnitOfWork uof, ILogger<GetPermissionUseCaseCQRS> logger, IEmployeeSecurityCQRS cqrs)
            :base(sender, uof, logger)
        {
            Cqrs = cqrs;
        }
        protected override GetPermissionNotification CreateNotificationMessage()
        {
            return new GetPermissionNotification()
            {
                EmployeeId = this.EmployeeId
            };
        }

        protected override async Task ExecuteUseCase(GetPermissionsModel useCaseParam, UseCaseResultModel result)
        {
            this.EmployeeId = useCaseParam.EmployeeId;
            result.ResponseData = await this.Cqrs.GetEmployeeSecurityAsync(useCaseParam.EmployeeId);
        }
    }
}
