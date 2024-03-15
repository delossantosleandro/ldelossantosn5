using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using LdelossantosN5.Domain.Impl.Indexes;
using LdelossantosN5.Domain.Indexes;

namespace LdelossantosN5.Domain.Impl.Indexes
{
    public class EmployeePermissionIndex
        : IEmployeePermissionIndex
    {
        public const string K_IndexName = "employeeindex";
        private readonly ElasticsearchClient Client;
        public EmployeePermissionIndex(ElasticIndexSettings serverSettings)
        {
            //var certificate = new X509Certificate2(@"Indexes/elasticcertificate.crt");
            var settings = new ElasticsearchClientSettings(new Uri(serverSettings.ServerName))
                .Authentication(new BasicAuthentication(serverSettings.UserName, serverSettings.Password))
                .ServerCertificateValidationCallback((a, b, c, d) => true);
            //.ClientCertificate(certificate);

            this.Client = new ElasticsearchClient(settings);
        }
        public async Task EnsureIndexCreationAsync()
        {
            var indexExistsResponse = await this.Client.Indices.ExistsAsync(K_IndexName);

            if (!indexExistsResponse.Exists)
            {
                var idxResponse = await this.Client.Indices.CreateAsync<EmployeePermisionIndexEntry>(K_IndexName, desc =>
                    desc.Mappings(map => map
                        .Properties(prop => prop
                            .IntegerNumber(y => y.Id)
                            .IntegerNumber(y => y.EmployeeId)
                            .IntegerNumber(y => y.PermissionTypeId)
                            .Keyword(y => y.Status)
                        )
                    )
                );
            }
        }
        public async Task UpsertAsync(EmployeePermisionIndexEntry entry)
        {
            await this.Client.IndexAsync(entry, idx => idx.Index(K_IndexName).Id(entry.Id));
        }
        public ElasticsearchClient GetClient() { return this.Client; }

        public async Task EnsureDeleteAsync()
        {
            await Client.Indices.DeleteAsync(K_IndexName);
        }
    }
}
