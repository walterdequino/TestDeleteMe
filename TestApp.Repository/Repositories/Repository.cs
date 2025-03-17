using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using TestApp.Domain.Interfaces;

namespace TestApp.Repository.Repositories
{
    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Db Context
        /// </summary>
        private readonly EntityFrameworkDbContext _dbContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(EntityFrameworkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public IQueryable<T> FindQueryable(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _dbContext.Set<T>().Where(expression);

            return orderBy != null ? orderBy(query) : query;
        }

        /// <inheritdoc/>
        public Task<List<T>> FindListAsync(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            var query = expression != null ? _dbContext.Set<T>().Where(expression) : _dbContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return orderBy != null ? orderBy(query).ToListAsync() : query.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<T>> FindAllAsync(string includeProperties = "", CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            foreach (var includeProperty in includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return await query.ToListAsync(cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, string includeProperties = "")
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                query = includeProperties.Split([','], StringSplitOptions.RemoveEmptyEntries).Aggregate(query, static (current, includeProperty) => current.Include(includeProperty));
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        /// <inheritdoc/>
        public T Insert(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        /// <inheritdoc/>
        public void InsertRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        /// <inheritdoc/>
        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <inheritdoc/>
        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}
