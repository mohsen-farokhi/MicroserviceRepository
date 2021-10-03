using Microsoft.EntityFrameworkCore;

namespace UserApi.Persistence
{
    public class QueryDatabaseContext : DbContext
    {
		public QueryDatabaseContext
			(DbContextOptions<QueryDatabaseContext> options) :
			base(options: options)
		{
		}

		public DbSet<Domain.Aggregates.Users.User> Users { get; set; }

		protected override void OnModelCreating
			(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly
				(typeof(Configurations.UserConfiguration).Assembly);
		}
	}
}
