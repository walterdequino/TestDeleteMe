using System.Linq.Expressions;

namespace TestApp.Domain.Interfaces
{
    /// <summary>
    /// Interface of Generic Repository
    /// </summary>
    /// <typeparam name="T">Type of Repository</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Find Queryable
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<T> FindQueryable(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        /// <summary>
        /// Find List Async
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<List<T>> FindListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        /// <summary>
        /// Find All Async
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<T>> FindAllAsync(string includeProperties = "", CancellationToken cancellationToken = default);

        /// <summary>
        /// Single Or Default Async
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, string includeProperties = "");

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Insert(T entity);

        /// <summary>
        /// Inser Range
        /// </summary>
        /// <param name="entities"></param>
        void InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Update Range
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
    }
}
