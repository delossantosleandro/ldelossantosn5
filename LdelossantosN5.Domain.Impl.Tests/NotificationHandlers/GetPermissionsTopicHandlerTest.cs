using LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest;
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
    public class GetPermissionsTopicHandlerTest
    {
        [Fact]
        public async Task GetPermissionsTopicHandlerNotifyGet()
        {
            var mokProducer = new Mock<IMessageProducer>();
            mokProducer.Setup(x => x.DeliverMessageAsync(It.IsAny<TopicMessage>()))
                .Callback<TopicMessage>((msg) => Assert.Equal(TopicMessage.get, msg));

            var handler = new GetPermissionsTopicHandler(mokProducer.Object);
            await handler.Handle(new GetPermissionNotification(), CancellationToken.None);
            Assert.IsAssignableFrom<INotificationHandler<GetPermissionNotification>>(handler);
        }
    }
}
