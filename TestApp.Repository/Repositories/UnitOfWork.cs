using TestApp.Domain.Interfaces;

namespace TestApp.Repository.Repositories
{
    /// <summary>
    /// Unit Of Work
    /// </summary>
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Database Context
        /// </summary>
        private readonly EntityFrameworkDbContext _databaseContext;

        /// <summary>
        /// disposed
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseContext"></param>
        public UnitOfWork(EntityFrameworkDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        /// <inheritdoc/>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_databaseContext);
        }

        /// <inheritdoc/>
        public async Task<int> SaveAsync()
        {
            return await _databaseContext.SaveChangesAsync();
        }

        /// <summary>
        /// Destructor Unit of Work
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _databaseContext.Dispose();
            _disposed = true;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
