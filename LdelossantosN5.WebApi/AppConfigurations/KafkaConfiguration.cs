using LdelossantosN5.Domain.Impl.Indexes;
using LdelossantosN5.Domain.Impl.TopicsNotification;
using LdelossantosN5.Domain.TopicsNotification;

namespace LdelossantosN5.WebApi.AppConfigurations
{
    public static class KafkaConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            var elasticIndex = new ElasticIndexSettings()
            {
                ServerName = builder.Configuration["ElasticIndexServer"] ?? "https://localhost:9200"
            };

            var kafkaSettings = new KafkaSettings()
            {
                BoostrapServers = builder.Configuration["KafkaSettings"] ?? "localhost:9092"
            };

            builder.Services.AddSingleton(kafkaSettings);
            builder.Services.AddSingleton<IMessageProducer, KafkaMessageProducer>();
        }

        public static async Task Initialize(IServiceProvider services)
        {
            var kafkaProducer = services.GetService<IMessageProducer>()!;
            await (kafkaProducer as KafkaMessageProducer)!.EnsureTopicIsCreatedAsync();
        }
    }
}
