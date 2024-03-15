using Elastic.Clients.Elasticsearch;
using LdelossantosN5.Domain.Impl.Indexes;
using LdelossantosN5.Domain.Indexes;

namespace LdelossantosN5.Domain.Tests
{
    public class ElasticLocalFixture
        : IAsyncLifetime
    {
        private EmployeePermissionIndex? elasticIndex;
        public IEmployeePermissionIndex? ElasticIndex { get; private set; }
        public ElasticsearchClient? ElasticsearchClient { get; private set; }
        public string IndexName { get; private set; } = string.Empty;
        public async Task InitializeAsync()
        {
            //It's not a good practice to put this here, but no need to add additional complexity to this challenge...
            this.elasticIndex = new EmployeePermissionIndex(new ElasticIndexSettings());
            await elasticIndex.EnsureIndexCreationAsync();
            this.ElasticIndex = elasticIndex;
            this.ElasticsearchClient = elasticIndex.GetClient();
            this.IndexName = EmployeePermissionIndex.K_IndexName;
        }
        public async Task DisposeAsync()
        {
            await this.elasticIndex!.EnsureDeleteAsync();
        }
    }
}