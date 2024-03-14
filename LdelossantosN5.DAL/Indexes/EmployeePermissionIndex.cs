using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LdelossantosN5.DAL.Indexes
{
    public class EmployeePermissionIndex
        : IEmployeePermissionIndex
    {
        public const string K_IndexName = "employeeindex";
        private readonly ElasticsearchClient Client;
        public EmployeePermissionIndex(string serverUri= "https://localhost:9200")
        {
            //var certificate = new X509Certificate2(@"Indexes/elasticcertificate.crt");
            var settings = new ElasticsearchClientSettings(new Uri(serverUri))
                .Authentication(new BasicAuthentication("elastic", "AJ+h-rwFBJpOdGw5G3MD"))
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
