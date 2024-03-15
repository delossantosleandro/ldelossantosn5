using Confluent.Kafka;
using Confluent.Kafka.Admin;
using LdelossantosN5.Domain.TopicsNotification;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace LdelossantosN5.Domain.Impl.TopicsNotification
{
    [ExcludeFromCodeCoverage]
    public class KafkaMessageProducer
        : IMessageProducer
    {

        public const string K_TopicName = "usersecurityapicalls";
        private IProducer<string, string> Producer { get; }
        private ILogger<KafkaMessageProducer> Logs { get; }
        public string BootstrapServers { get; }
        public KafkaMessageProducer(ILogger<KafkaMessageProducer> logs, KafkaSettings settings)
        {
            //Inject a configuration Object
            this.BootstrapServers = settings.BoostrapServers;
            this.Logs = logs;

            var config = new ProducerConfig { BootstrapServers = this.BootstrapServers };
            this.Producer = new ProducerBuilder<string, string>(config).Build();
        }
        public async Task EnsureTopicIsCreatedAsync()
        {
            var config = new AdminClientConfig { BootstrapServers = this.BootstrapServers };

            using var adminClient = new AdminClientBuilder(config).Build();
            try
            {
                await adminClient.CreateTopicsAsync(new[] { new TopicSpecification { Name = K_TopicName, NumPartitions = 1, ReplicationFactor = 1 } });
                Logs.LogInformation($"Topic {K_TopicName} successfully deleted.");
            }
            catch (Exception e)
            {
                Logs.LogError(e, $"An error occurred while creating topic {K_TopicName}");
            }

        }
        public async Task EnsureTopicDeletedAsync()
        {
            var config = new AdminClientConfig { BootstrapServers = this.BootstrapServers };

            using var adminClient = new AdminClientBuilder(config).Build();
            try
            {
                await adminClient.DeleteTopicsAsync(new List<string> { K_TopicName });
                Logs.LogInformation($"Topic {K_TopicName} successfully deleted.");
            }
            catch (Exception e)
            {
                Logs.LogError(e, $"An error occurred while deleting topic {K_TopicName}");
            }
        }
        public async Task DeliverMessageAsync(TopicMessage message)
        {
            var theMessage = new Message<string, string>() { Key = Guid.NewGuid().ToString(), Value = message.ToString() };
            try
            {
                await this.Producer.ProduceAsync(K_TopicName, theMessage);
                Logs.LogInformation($"{K_TopicName} {theMessage.Key} {theMessage.Value} delivered");

            }
            catch (ProduceException<string, string> e)
            {
                Logs.LogCritical(e, "Failed to deliver");
            }
            catch (Exception ex)
            {
                Logs.LogError(ex, "Produce Message Exception");
            }
        }
    }
}
