using Microsoft.EntityFrameworkCore;

namespace ApplicationApi.Persistence
{
    public class QueryDatabaseContext : DbContext
    {
        public QueryDatabaseContext
            (DbContextOptions<QueryDatabaseContext> options) : base(options: options)
        {
        }

        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
        }
    }
}
