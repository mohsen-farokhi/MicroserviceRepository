using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationApi.Domain.Aggregates.OperationGroups;

namespace ApplicationApi.Persistence.Configurations
{
    public class OperationGroupConfiguration :
        IEntityTypeConfiguration<OperationGroup>
    {
        public void Configure(EntityTypeBuilder<OperationGroup> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
                .IsRequired(required: true)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value);

            builder.HasOne(p => p.Application)
                .WithMany(p => p.OperationGroups)
                .IsRequired(required: true)
                .HasForeignKey(foreignKeyPropertyNames: "ApplicationId")
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
