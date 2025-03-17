namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Unit of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Get Repository
        /// </summary>
        /// <typeparam name="T">Type of the Repository</typeparam>
        /// <returns></returns>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// Save Async Operation
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
