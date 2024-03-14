using Elastic.Clients.Elasticsearch;
using LdelossantosN5.Domain.Indexes;

namespace LdelossantosN5.Domain.Tests.Indexes
{
    public class EmployeePermissionIndexTest
        : IClassFixture<ElasticLocalFixture>
    {
        public EmployeePermissionIndexTest(ElasticLocalFixture fixture)
        {
            this.ElasticIndex = fixture.ElasticIndex!;
            this.Client = fixture.ElasticsearchClient!;
            this.IndexName = fixture.IndexName;
        }
        public IEmployeePermissionIndex ElasticIndex { get; }
        public ElasticsearchClient Client { get; }
        public string IndexName { get; }

        [Fact]
        public async Task ICanUpsertMyIndex()
        {
            var randId = Random.Shared.Next(150000, 200000);
            var randEmployee = Random.Shared.Next(1, 50000);
            var randPermission = Random.Shared.Next(100000, 150000);
            var value = DateTime.Now.ToString();

            var indexEntry = new EmployeePermisionIndexEntry()
            {
                Id = randId,
                EmployeeId = randEmployee,
                PermissionTypeId = randPermission,
                Status = value
            };
            await this.ElasticIndex.UpsertAsync(indexEntry);
            var retrievedEntry = await this.Client.GetAsync<EmployeePermisionIndexEntry>(randId, g => g.Index(this.IndexName));
            Assert.True(retrievedEntry.IsValidResponse);
            Assert.True(retrievedEntry.Found);
            Assert.NotNull(retrievedEntry.Source);
            Assert.Equal(randId, retrievedEntry.Source.Id);
            Assert.Equal(randEmployee, retrievedEntry.Source.EmployeeId);
            Assert.Equal(randPermission, retrievedEntry.Source.PermissionTypeId);
            Assert.Equal(value, retrievedEntry.Source.Status);
        }
    }
}
