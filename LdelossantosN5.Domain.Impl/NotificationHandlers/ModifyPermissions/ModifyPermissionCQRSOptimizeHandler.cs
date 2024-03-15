using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.ModifyPermissions
{
    public class ModifyPermissionCQRSOptimizeHandler
        : INotificationHandler<ModifyPermissionNotification>
    {
        public ModifyPermissionCQRSOptimizeHandler(IEmployeeSecurityCQRS cqrs)
        {
            this.CQRS = cqrs;
        }
        private IEmployeeSecurityCQRS CQRS { get; }
        public async Task Handle(ModifyPermissionNotification notification, CancellationToken cancellationToken)
        {
            await this.CQRS.OptimizeReadingAsync(notification.EmployeeId);
        }
    }
}