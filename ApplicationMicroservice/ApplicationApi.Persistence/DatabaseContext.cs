using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApplicationApi.Persistence
{
    public class DatabaseContext : DbContext
    {
        private static readonly System.Type[] EnumerationTypes =
        {
            typeof(Domain.Aggregates.Operations.ValueObjects.AccessType),
        };

        public DatabaseContext
            (DbContextOptions<DatabaseContext> options,
            MediatR.IMediator mediator) : base(options: options)
        {
            //Database.EnsureCreated();

            Mediator = mediator;
        }

        private MediatR.IMediator Mediator { get; }

        public DbSet<Domain.Aggregates.Applications.Application> Applications { get; set; }

        public DbSet<Domain.Aggregates.OperationGroups.OperationGroup> OperationGroups { get; set; }

        public DbSet<Domain.Aggregates.Operations.Operation> Operations { get; set; }

        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (typeof(Configurations.ApplicationConfiguration).Assembly);

            #region HasQueryFilter
            #endregion
        }

        public override async System.Threading.Tasks.Task<int>
            SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default)
        {
            var enumerationEntries =
                ChangeTracker.Entries()
                .Where(current => EnumerationTypes.Contains(current.Entity.GetType()));

            foreach (var enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State = EntityState.Unchanged;
            }

            int affectedRows =
                await base.SaveChangesAsync(cancellationToken: cancellationToken);

            if (affectedRows > 0)
            {
                var aggregateRoots =
                    ChangeTracker.Entries()
                    .Where(current => current.Entity is IAggregateRoot)
                    .Select(current => current.Entity as IAggregateRoot)
                    .ToList();

                foreach (var aggregateRoot in aggregateRoots)
                {
                    // Dispatch Events!
                    foreach (var domainEvent in aggregateRoot.DomainEvents)
                    {
                        await Mediator.Publish(domainEvent, cancellationToken);
                    }

                    // Clear Events!
                    aggregateRoot.ClearDomainEvents();
                }
            }

            return affectedRows;
        }
    }
}
