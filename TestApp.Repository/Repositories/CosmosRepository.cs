using System.Net;

using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

using TestApp.Domain.Filters;
using TestApp.Domain.Interfaces;

namespace TestApp.Repository.Repositories
{
    /// <summary>
    /// Cosmos Db Service
    /// </summary>
    public class CosmosRepository : ICosmosRepository
    {
        /// <summary>
        /// Partition Key
        /// </summary>
        private static readonly PartitionKey DefaultPartitionKey = PartitionKey.None;

        /// <summary>
        /// Client
        /// </summary>
        private static CosmosClient _client = null!;

        /// <summary>
        /// Data Base Id
        /// </summary>
        private static string _dataBaseId = null!;

        /// <summary>
        /// Default Partition Path
        /// </summary>
        private static string _defaultPartitonPath = null!;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public CosmosRepository(IConfiguration configuration)
        {
            _client = new CosmosClient(configuration.GetConnectionString("CosmosDb") ?? throw new InternalValidationException("Not found config key CosmosDb"));
            _dataBaseId = configuration.GetSection("CosmosDb:DatabaseId").Value ?? throw new InternalValidationException("Not found config key DatabaseId");
            _defaultPartitonPath = configuration.GetSection("CosmosDb:DefaultPartition").Value ?? throw new InternalValidationException("Not found config key DefaultPartition");
        }

        /// <inheritdoc />
        public async Task AddItemAsync<T>(T item, string containerName)
        {
            try
            {
                var container = await GetContainer(containerName);

                await container.CreateItemAsync(item, DefaultPartitionKey);
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error try add item in cosmosDb", ex);
            }
        }

        /// <inheritdoc />
        public async Task<T?> GetItemAsync<T>(string id, string containerName)
        {
            try
            {
                var container = await GetContainer(containerName);

                var response = await container.ReadItemAsync<T>(id, DefaultPartitionKey);

                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return default;
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error try Get item in cosmosDb", ex);
            }
        }

        /// <inheritdoc />
        public async Task DeleteItemAsync<T>(string id, string containerName)
        {
            try
            {
                var container = await GetContainer(containerName);

                await container.DeleteItemAsync<T>(id, DefaultPartitionKey);
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error try Delete item in cosmosDb", ex);
            }
        }

        /// <inheritdoc />
        public async Task UpdateItemAsync<T>(T updatedItem, string containerName)
        {
            try
            {
                var container = await GetContainer(containerName);

                await container.UpsertItemAsync(updatedItem, DefaultPartitionKey);
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error try Update item in cosmosDb", ex);
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> GetAllItemsAsync<T>(string containerName)
        {
            try
            {
                var container = await GetContainer(containerName);
                var iterator = container.GetItemQueryIterator<T>();

                var results = new List<T>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    results.AddRange(response);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error trying to get all items from Cosmos DB", ex);
            }
        }

        /// <inheritdoc />
        public async Task<List<T>> GetItemsAsync<T>(string containerName, string queryFilter, Dictionary<string, object>? parameters = null)
        {
            try
            {
                var queryDefinition = new QueryDefinition(queryFilter);

                if (parameters is not null && parameters.Any())
                {
                    // Add parameters to the query definition
                    foreach (var parameter in parameters)
                    {
                        queryDefinition.WithParameter(parameter.Key, parameter.Value);
                    }
                }

                var container = await GetContainer(containerName);

                var iterator = container.GetItemQueryIterator<T>(queryDefinition);
                var results = new List<T>();

                while (iterator.HasMoreResults)
                {
                    var response = await iterator.ReadNextAsync();
                    results.AddRange(response);
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new InternalValidationException("Error trying to get items with filter from Cosmos DB", ex);
            }
        }

        /// <summary>
        /// Get Container
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        private static async Task<Container> GetContainer(string containerName)
        {
            var database = (await _client.CreateDatabaseIfNotExistsAsync(_dataBaseId)).Database;
            return (await database.CreateContainerIfNotExistsAsync(containerName, _defaultPartitonPath)).Container;
        }
    }
}
