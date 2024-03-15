using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.TopicsNotification;
using MediatR;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.ModifyPermissions
{
    public class ModifyPermissionsTopicHandler
        : INotificationHandler<ModifyPermissionNotification>
    {
        private IMessageProducer Producer { get; }
        public ModifyPermissionsTopicHandler(IMessageProducer producer)
        {
            Producer = producer;
        }
        public async Task Handle(ModifyPermissionNotification notification, CancellationToken cancellationToken)
        {
            await Producer.DeliverMessageAsync(TopicMessage.modify);
        }
    }
}
