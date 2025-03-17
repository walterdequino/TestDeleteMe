namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Cosmos Repository
    /// </summary>
    public interface ICosmosRepository
    {
        /// <summary>
        /// Add Item Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task AddItemAsync<T>(T item, string containerName);

        /// <summary>
        /// Get Item Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<T?> GetItemAsync<T>(string id, string containerName);

        /// <summary>
        /// Delete Item Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task DeleteItemAsync<T>(string id, string containerName);

        /// <summary>
        /// Update Item Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="updatedItem"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task UpdateItemAsync<T>(T updatedItem, string containerName);

        /// <summary>
        /// Get Items Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<List<T>> GetAllItemsAsync<T>(string containerName);

        /// <summary>
        /// Get Items Async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containerName"></param>
        /// <param name="queryDefinition"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<List<T>> GetItemsAsync<T>(string containerName, string queryDefinition, Dictionary<string, object>? parameters = null);
    }
}
