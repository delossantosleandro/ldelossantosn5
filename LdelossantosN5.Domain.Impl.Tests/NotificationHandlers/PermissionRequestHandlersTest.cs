using LdelossantosN5.Domain.CQRS;
using LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest;
using LdelossantosN5.Domain.Indexes;
using LdelossantosN5.Domain.Models;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.TopicsNotification;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.Tests.NotificationHandlers
{
    public class PermissionRequestHandlersTest
    {
        private PermissionRequestNotification GetNotificationMessage()
        {
            return new PermissionRequestNotification()
            {
                EmployeeId = 1,
                PermissionId = 2,
                PermissionTypeId = 3,
            };
        }

        [Fact]
        public async Task PermissionRequestIndexHandlerTest()
        {
            var index = new Mock<IEmployeePermissionIndex>();
            index
                .Setup(idx => idx.UpsertAsync(It.IsAny<EmployeePermisionIndexEntry>()))
                .Callback<EmployeePermisionIndexEntry>((idxEntry) =>
                {
                    Assert.Equal(1, idxEntry.EmployeeId);
                    Assert.Equal(2, idxEntry.Id);
                    Assert.Equal(3, idxEntry.PermissionTypeId);
                    Assert.Equal(PermissionStatus.permissionRequested.ToString(), idxEntry.Status);
                });

            var handler = new PermissionRequestIndexHandler(index.Object);
            Assert.IsAssignableFrom<INotificationHandler<PermissionRequestNotification>>(handler);

            PermissionRequestNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);
        }

        [Fact]
        public async Task PermissionRequestTopicHandlerTest()
        {
            var producer = new Mock<IMessageProducer>();
            producer.Setup(x => x.DeliverMessageAsync(It.IsAny<TopicMessage>()))
                .Callback<TopicMessage>((msg) => Assert.Equal(TopicMessage.request, msg));

            var handler = new PermissionRequestTopicHandler(producer.Object);
            Assert.IsAssignableFrom<INotificationHandler<PermissionRequestNotification>>(handler);

            PermissionRequestNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);
        }

        [Fact]
        public async Task PermissionRequestCQRSOptimizeHandlerTest()
        {
            var producer = new Mock<IEmployeeSecurityCQRS>();
            producer.Setup(x => x.OptimizeReadingAsync(It.IsAny<int>()))
                .Callback<int>((msg) => Assert.Equal(1, msg));

            var handler = new PermissionRequestCQRSOptimizeHandler(producer.Object);
            Assert.IsAssignableFrom<INotificationHandler<PermissionRequestNotification>>(handler);

            PermissionRequestNotification reqNotification = GetNotificationMessage();
            await handler.Handle(reqNotification, CancellationToken.None);
        }
    }
}
