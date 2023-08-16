using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendon.Core.Repository.Models;

namespace Vendon.Core.Repository.Repositories
{
    public class RepositoryResults : IRepositoryResults
    {

        private readonly Container _container;

        public RepositoryResults(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _container = cosmosClient.GetContainer(configuration["Settings:DataBaseId"], configuration["Settings:CollectionResultsId"]);
        }
        public async Task<bool> registerResult(ResultTest? resultTest)
        {
            if (resultTest != null)
            {
                await _container.UpsertItemAsync(resultTest);
                return true;
            }
            return false;
        }

    }
}
