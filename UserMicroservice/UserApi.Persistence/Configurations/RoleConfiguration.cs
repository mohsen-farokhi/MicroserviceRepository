using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Domain.Aggregates.Users.ValueObjects;

namespace UserApi.Persistence.Configurations
{
    internal class RoleConfiguration :
        IEntityTypeConfiguration<Role>
    {
        public void Configure
            (EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(name: "Roles");

            builder.HasKey(p => p.Value);

            builder
                .Property(p => p.Value)
                .ValueGeneratedNever()
                .IsRequired(required: true);

            builder
                .Property(p => p.Name)
                .IsRequired(required: true)
                .HasMaxLength(maxLength: Role.MaxLength);

            builder.HasData(Role.Programmer);
            builder.HasData(Role.Administrator);
            builder.HasData(Role.Supervisor);
            builder.HasData(Role.Agent);
            builder.HasData(Role.Customer);
        }
    }
}