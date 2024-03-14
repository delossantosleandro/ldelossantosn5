using Confluent.Kafka;
using Confluent.Kafka.Admin;
using LdelossantosN5.Domain.TopicsNotification;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace LdelossantosN5.Domain.TopicsNotification
{
    public class KafkaMessageProducer
        : IMessageProducer
    {

        public const string K_TopicName = "usersecurityapicalls";
        private IProducer<string, string> Producer { get; }
        private ILogger<KafkaMessageProducer> Logs { get; }
        public string BootstrapServers { get; }
        public KafkaMessageProducer(ILogger<KafkaMessageProducer> logs, string boostrapServers = "localhost:9092")
        {
            this.BootstrapServers = boostrapServers;
            this.Logs = logs;

            var config = new ProducerConfig { BootstrapServers = boostrapServers, };
            this.Producer = new ProducerBuilder<string, string>(config).Build();
        }
        [ExcludeFromCodeCoverage]
        public async Task EnsureTopicIsCreatedAsync()
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = this.BootstrapServers }).Build();
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
        [ExcludeFromCodeCoverage]
        public async Task EnsureTopicDeletedAsync()
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = this.BootstrapServers }).Build();
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
        [ExcludeFromCodeCoverage]
        public async Task DeliverMessage(TopicMessage message)
        {
            var theMessage = new Message<string, string>() { Key = Guid.NewGuid().ToString(), Value = message.ToString() };
            try
            {
                await this.Producer.ProduceAsync(K_TopicName, theMessage);
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
