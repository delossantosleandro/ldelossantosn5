using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.Domain.Impl.TopicsNotification
{
    public class KafkaSettings
    {
        public string BoostrapServers { get; set; } = "localhost:9092";
    }
}
