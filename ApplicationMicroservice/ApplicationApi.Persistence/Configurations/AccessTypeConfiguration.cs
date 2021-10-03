using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationApi.Domain.Aggregates.Operations.ValueObjects;

namespace ApplicationApi.Persistence.Configurations
{
    public class AccessTypeConfiguration :
        IEntityTypeConfiguration<AccessType>
    {
        public void Configure
            (EntityTypeBuilder<AccessType> builder)
        {
            builder.ToTable(name: "AccessTypes");

            builder.HasKey(p => p.Value);

            builder
                .Property(p => p.Value)
                .ValueGeneratedNever()
                .IsRequired(required: true);

            builder
                .Property(p => p.Name)
                .IsRequired(required: true)
                .HasMaxLength(maxLength: AccessType.MaxLength);

            builder.HasData(AccessType.Public);
            builder.HasData(AccessType.Registered);
            builder.HasData(AccessType.Special);
        }
    }
}
