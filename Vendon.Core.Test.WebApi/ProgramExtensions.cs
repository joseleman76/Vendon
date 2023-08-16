using Microsoft.Azure.Cosmos;
using Vendon.Core.Application.Mappers;
using Vendon.Core.Application.Services;
using Vendon.Core.Repository.Repositories;

namespace Vendon.Core.Test.WebApi
{
    public static class ProgramExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            string EndPointDataBase = configuration["Settings:DataBaseUrl"]??"";
            string KeyDataBase = configuration["Settings:DataBaseKey"] ?? "";

            var cosmosClient = new CosmosClient(
                        EndPointDataBase,
                        KeyDataBase
                    );
            services.AddSingleton<IRepositoryTests>(options =>
            {
                return new RepositoryTests(cosmosClient, configuration);
            });
            services.AddSingleton<IRepositoryResults>(options =>
            {
                return new RepositoryResults(cosmosClient, configuration);
            });
            services.AddScoped<IServiceTest, ServiceTest>();
            services.AddScoped<IMapperTest, MapperTest>();


            return services;
        }
    }
}
