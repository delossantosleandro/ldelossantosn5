using LdelossantosN5.Domain.TopicsNotification;

namespace LdelossantosN5.Domain.Tests.TopicsNotification
{
    public class KafkaTopisNotificationTest
        : IClassFixture<KafkaLocalFixture>
    {
        public KafkaTopisNotificationTest(KafkaLocalFixture fixture)
        {
            this.KafkaFixture = fixture;
        }
        public KafkaLocalFixture KafkaFixture { get; }

        [Fact]
        public async Task DeliverMessageOk()
        {
            await this.KafkaFixture.Producer.DeliverMessageAsync(TopicMessage.request);
            Assert.True(true);
        }
    }
}
