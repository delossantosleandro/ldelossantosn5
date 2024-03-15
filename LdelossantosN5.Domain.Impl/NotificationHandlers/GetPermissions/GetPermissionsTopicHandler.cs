using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.TopicsNotification;
using MediatR;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest
{
    public class GetPermissionsTopicHandler
        : INotificationHandler<GetPermissionNotification>
    {
        private IMessageProducer Producer { get; }
        public GetPermissionsTopicHandler(IMessageProducer producer)
        {
            Producer = producer;
        }
        public async Task Handle(GetPermissionNotification notification, CancellationToken cancellationToken)
        {
            await Producer.DeliverMessageAsync(TopicMessage.get);
        }
    }
}
