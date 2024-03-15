using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest
{
    public class PermissionRequestCQRSOptimizeHandler
        : INotificationHandler<PermissionRequestNotification>
    {
        public PermissionRequestCQRSOptimizeHandler(IEmployeeSecurityCQRS cqrs)
        {
            this.CQRS = cqrs;
        }
        private IEmployeeSecurityCQRS CQRS { get; }
        public async Task Handle(PermissionRequestNotification notification, CancellationToken cancellationToken)
        {
            await this.CQRS.OptimizeReadingAsync(notification.EmployeeId);
        }
    }
}