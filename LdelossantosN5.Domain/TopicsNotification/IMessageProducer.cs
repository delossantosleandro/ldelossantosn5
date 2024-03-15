
namespace LdelossantosN5.Domain.TopicsNotification
{
    public interface IMessageProducer
    {
        Task DeliverMessageAsync(TopicMessage message);
    }
}