using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationApi.Domain.Aggregates.Applications;

namespace ApplicationApi.Persistence.Configurations
{
    public class ApplicationConfiguration :
        IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
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
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.Name.Create(p).Value);
        }
    }
}
