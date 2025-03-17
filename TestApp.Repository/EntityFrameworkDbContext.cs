using Microsoft.EntityFrameworkCore;

namespace TestApp.Repository
{
    /// <summary>
    /// Entity Framework Db Context
    /// </summary>
    public sealed class EntityFrameworkDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public EntityFrameworkDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}