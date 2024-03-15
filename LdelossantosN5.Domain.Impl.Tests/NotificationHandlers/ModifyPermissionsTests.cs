using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Impl.NotificationHandlers.ModifyPermissions;
using LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest;
using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.TopicsNotification;
using MediatR;
using Moq;

namespace LdelossantosN5.Domain.Impl.Tests.NotificationHandlers
{
    public class ModifyPermissionsTests
    {

        private ModifyPermissionNotification GetNotificationMessage()
        {
            return new ModifyPermissionNotification()
            {
                EmployeeId = 1,
                PermissionId = 2,
                PermissionTypeId = 3,
                Status = PermissionStatus.permissionGranted.ToString()
            };
        }

        [Fact]
        public async Task ModifyPermissionCQRSOptimizeHandlerTest()
        {
            var producer = new Mock<IEmployeeSecurityCQRS>();
            producer.Setup(x => x.OptimizeReadingAsync(It.IsAny<int>()))
                .Callback<int>((msg) => Assert.Equal(1, msg));

            var handler = new ModifyPermissionCQRSOptimizeHandler(producer.Object);
            Assert.IsAssignableFrom<INotificationHandler<ModifyPermissionNotification>>(handler);

            ModifyPermissionNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);
        }
        [Fact]
        public async Task ModifyPermissionTopicHandlerTest()
        {
            var producer = new Mock<IMessageProducer>();
            producer.Setup(x => x.DeliverMessageAsync(It.IsAny<TopicMessage>()))
                .Callback<TopicMessage>((msg) => Assert.Equal(TopicMessage.modify, msg));

            var handler = new ModifyPermissionsTopicHandler(producer.Object);
            Assert.IsAssignableFrom<INotificationHandler<ModifyPermissionNotification>>(handler);

            ModifyPermissionNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);
        }
        [Fact]
        public async Task ModifyPermissionUpsertIndex()
        {
            var index = new Mock<IEmployeePermissionIndex>();
            index
                .Setup(idx => idx.UpsertAsync(It.IsAny<EmployeePermisionIndexEntry>()))
                .Callback<EmployeePermisionIndexEntry>((idxEntry) =>
                {
                    Assert.Equal(1, idxEntry.EmployeeId);
                    Assert.Equal(2, idxEntry.Id);
                    Assert.Equal(3, idxEntry.PermissionTypeId);
                    Assert.Equal(PermissionStatus.permissionGranted.ToString(), idxEntry.Status);
                });

            var handler = new ModifyPermissionsIndexHandler(index.Object);
            Assert.IsAssignableFrom<INotificationHandler<ModifyPermissionNotification>>(handler);

            ModifyPermissionNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);

        }

    }
}
