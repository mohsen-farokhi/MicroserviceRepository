using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using ApplicationApi.Domain.Aggregates.OperationGroups;
using ApplicationApi.Domain.Aggregates.Operations;

namespace ApplicationApi.Persistence.Configurations
{
    public class OperationConfiguration :
        IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value);

            builder
                .Property(p => p.DisplayName)
                .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: true)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value);

            builder.HasOne(p => p.AccessType)
                .WithMany()
                .HasForeignKey(foreignKeyPropertyNames: "AccessTypeId")
                .IsRequired(required: true);

            builder.Property<int>("AccessTypeId")
                .HasColumnName("AccessTypeId");

            builder
                .HasMany(p => p.OperationGroups)
                .WithMany(p => p.Operations)
                .UsingEntity<Dictionary<string, object>>
                (joinEntityName: "OperationsOfGroups",
                    p =>
                        p.HasOne<OperationGroup>()
                        .WithMany()
                        .IsRequired(required: true)
                        .HasForeignKey(foreignKeyPropertyNames: "OperationGroupId")
                        .OnDelete(deleteBehavior: DeleteBehavior.NoAction),
                    p =>
                        p.HasOne<Operation>()
                        .WithMany()
                        .IsRequired(required: true)
                        .HasForeignKey(foreignKeyPropertyNames: "OperationId")
                        .OnDelete(deleteBehavior: DeleteBehavior.NoAction));

        }
    }
}
