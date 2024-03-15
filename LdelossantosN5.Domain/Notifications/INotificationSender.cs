using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Notifications
{
    public interface INotificationSender
    {
        void Publish<TMessage>(TMessage theMessage) where TMessage : class, INotification;
    }
}
