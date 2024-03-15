using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.ModifyPermissions
{
    /// <summary>
    /// TO REVIEW: How this behave if we made a base class
    /// </summary>
    public class ModifyPermissionsIndexHandler
        : INotificationHandler<ModifyPermissionNotification>
    {
        private IEmployeePermissionIndex Index { get; }
        public ModifyPermissionsIndexHandler(IEmployeePermissionIndex index)
        {
            Index = index;
        }

        public async Task Handle(ModifyPermissionNotification notification, CancellationToken cancellationToken)
        {
            var indexEntry = new EmployeePermisionIndexEntry()
            {
                Id = notification.PermissionId,
                EmployeeId = notification.EmployeeId,
                PermissionTypeId = notification.PermissionTypeId,
                Status = notification.Status
            };
            await Index.UpsertAsync(indexEntry);
        }
    }
}