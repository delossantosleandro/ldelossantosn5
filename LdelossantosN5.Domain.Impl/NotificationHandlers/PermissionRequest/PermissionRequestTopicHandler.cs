using Elastic.Clients.Elasticsearch;
using LdelossantosN5.Domain.Notification;
using LdelossantosN5.Domain.TopicsNotification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.NotificationHandlers.PermissionRequest
{
    public class PermissionRequestTopicHandler
        : INotificationHandler<PermissionRequestNotification>
    {
        private IMessageProducer Producer { get; }
        public PermissionRequestTopicHandler(IMessageProducer producer)
        {
            Producer = producer;
        }
        public async Task Handle(PermissionRequestNotification notification, CancellationToken cancellationToken)
        {
            await Producer.DeliverMessageAsync(TopicMessage.request);
        }
    }
}
