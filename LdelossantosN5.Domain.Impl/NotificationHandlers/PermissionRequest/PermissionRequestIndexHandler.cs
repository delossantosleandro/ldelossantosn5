using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest
{
    public class PermissionRequestIndexHandler
        : INotificationHandler<PermissionRequestNotification>
    {
        private IEmployeePermissionIndex Index { get; }
        public PermissionRequestIndexHandler(IEmployeePermissionIndex index)
        {
            Index = index;
        }

        public async Task Handle(PermissionRequestNotification notification, CancellationToken cancellationToken)
        {
            var indexEntry = new EmployeePermisionIndexEntry()
            {
                Id = notification.PermissionId,
                EmployeeId = notification.EmployeeId,
                PermissionTypeId = notification.PermissionTypeId,
                Status = PermissionStatus.permissionRequested.ToString()
            };
            await Index.UpsertAsync(indexEntry);
        }
    }
}
