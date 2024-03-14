using LdelossantosN5.Domain.TopicsNotification;
using Microsoft.Extensions.Logging;

namespace LdelossantosN5.Domain.Tests
{
    public class KafkaLocalFixture
        : IAsyncLifetime
    {
        public ILoggerFactory? LogFactory { get; private set; }
        private KafkaMessageProducer? KafkaProducer { get; set; }
        public IMessageProducer Producer { get { return this.KafkaProducer!; } }
        public async Task InitializeAsync()
        {
            this.LogFactory = LoggerFactory.Create(builder => builder.AddDebug());
            this.KafkaProducer = new KafkaMessageProducer(this.LogFactory.CreateLogger<KafkaMessageProducer>());
            await this.KafkaProducer.EnsureTopicIsCreatedAsync();
        }
        public async Task DisposeAsync()
        {
            await this.KafkaProducer!.EnsureTopicDeletedAsync();
        }

    }
}
