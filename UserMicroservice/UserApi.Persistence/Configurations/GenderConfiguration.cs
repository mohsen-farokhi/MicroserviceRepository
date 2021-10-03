using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserApi.Persistence.SharedKernel
{
	internal class GenderConfiguration : 
		IEntityTypeConfiguration<Domain.SharedKernel.Gender>
	{
		public GenderConfiguration()
		{
		}

		public void Configure
			(EntityTypeBuilder<Domain.SharedKernel.Gender> builder)
		{
			builder.ToTable(name: "Genders");

			builder.HasKey(p => p.Value);

			builder
				.Property(p => p.Value)
				.ValueGeneratedNever()
				.IsRequired(required: true);

			builder
				.Property(p => p.Name)
				.IsRequired(required: true)
				.HasMaxLength(maxLength: Domain.SharedKernel.Gender.MaxLength);

			builder.HasData(Domain.SharedKernel.Gender.Male);
			builder.HasData(Domain.SharedKernel.Gender.Female);
		}
	}
}
