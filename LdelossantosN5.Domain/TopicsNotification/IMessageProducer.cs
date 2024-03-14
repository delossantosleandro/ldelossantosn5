
namespace LdelossantosN5.Domain.TopicsNotification
{
    public interface IMessageProducer
    {
        Task DeliverMessage(TopicMessage message);
    }
}