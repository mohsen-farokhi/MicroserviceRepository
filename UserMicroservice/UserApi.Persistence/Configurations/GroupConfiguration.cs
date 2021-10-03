using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Domain.Aggregates.Groups;

namespace UserApi.Persistence.Configurations
{
    public class GroupConfiguration :
        IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder
                .Property(p => p.Name)
                .HasMaxLength(maxLength: Domain.SharedKernel.Name.MaxLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value);
        }
    }
}
