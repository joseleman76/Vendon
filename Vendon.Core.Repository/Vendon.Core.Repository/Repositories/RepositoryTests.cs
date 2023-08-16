using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Repository.Models;
using Container = Microsoft.Azure.Cosmos.Container;

namespace Vendon.Core.Repository.Repositories
{
    public class RepositoryTests : IRepositoryTests
    {
        private readonly Container _container;

        public RepositoryTests(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _container = cosmosClient.GetContainer(configuration["Settings:DataBaseId"], configuration["Settings:CollectionId"]);
        }

        /// <summary>
        /// Recover all Test in BD
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Test>> getAllTest()
        {
            var query = _container.GetItemQueryIterator<Test>(new QueryDefinition(" select * from c"));
            List<Test> result = new List<Test>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }
            return result;

        }

        public async Task<IEnumerable<TestDefinition>> getAllTestDefinition()
        {
            var query = _container.GetItemQueryIterator<TestDefinition>(new QueryDefinition(" select c.name, c.testId from c"));
            List<TestDefinition> result = new List<TestDefinition>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                result.AddRange(response);
            }
            return result;

        }

        public async Task<IEnumerable<Test>> getTest(int testId)
        {
            List<Test> result = new List<Test>();

            using (FeedIterator<Test> setIterator = _container.GetItemLinqQueryable<Test>()
                     .Where(c => c.testId == testId).ToFeedIterator<Test>())
            {
                //Asynchronous query execution
                while (setIterator.HasMoreResults)
                {
                    foreach (var item in await setIterator.ReadNextAsync())
                    {
                        result.Add(item);
                    }
                }
            }
            return result;


        }
    }
}
