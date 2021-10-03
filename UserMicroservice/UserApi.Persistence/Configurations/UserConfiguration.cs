using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using UserApi.Domain.Aggregates.Groups;
using UserApi.Domain.Aggregates.Users;

namespace UserApi.Persistence.Configurations
{
    internal class UserConfiguration :
        IEntityTypeConfiguration<User>
    {
        public void Configure
            (EntityTypeBuilder<User> builder)
        {
            builder
                .Property(p => p.Username)
                .HasMaxLength(maxLength: Domain.Aggregates.Users.ValueObjects.Username.MaxLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.Aggregates.Users.ValueObjects.Username.Create(p).Value);

            builder
                .Property(p => p.Password)
                .HasMaxLength(maxLength: Domain.Aggregates.Users.ValueObjects.Password.MaxLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.Aggregates.Users.ValueObjects.Password.Create(p).Value);

            builder
                .Property(p => p.CellPhoneNumber)
                .HasMaxLength(maxLength: Domain.SharedKernel.CellPhoneNumber.FixLength)
                .IsRequired(required: true)
                .IsUnicode(unicode: false)
                .HasConversion(p => p.Value,
                    p => Domain.SharedKernel.CellPhoneNumber.Create(p).Value);

            builder.HasOne(p => p.Role)
                .WithMany()
                .HasForeignKey(foreignKeyPropertyNames: "RoleId")
                .IsRequired(required: true);

            builder.Property<int>("RoleId")
                .HasColumnName("RoleId");

            builder
                .OwnsOne(p => p.FullName, p =>
                {
                    p.HasOne(pp => pp.Gender)
                        .WithMany()
                        .HasForeignKey(foreignKeyPropertyNames: "GenderId")
                        .IsRequired(required: true);

                    p.Property<int>("GenderId")
                        .HasColumnName("GenderId");

                    p.Property(pp => pp.FirstName)
                        .HasColumnName(nameof(Domain.SharedKernel.FullName.FirstName))
                        .HasMaxLength(maxLength: Domain.SharedKernel.FirstName.MaxLength)
                        .IsRequired(required: true)
                        .HasConversion(p => p.Value,
                            p => Domain.SharedKernel.FirstName.Create(p).Value);

                    p.Property(pp => pp.LastName)
                        .HasColumnName(nameof(Domain.SharedKernel.FullName.LastName))
                        .HasMaxLength(maxLength: Domain.SharedKernel.LastName.MaxLength)
                        .IsRequired(required: true)
                        .HasConversion(p => p.Value, p => Domain.SharedKernel.LastName.Create(p).Value);
                });

            builder
                .OwnsOne(p => p.EmailAddress, p =>
                {
                    p.Property(pp => pp.Value)
                        .HasColumnName(nameof(Domain.SharedKernel.EmailAddress))
                        .HasMaxLength(maxLength: Domain.SharedKernel.EmailAddress.MaxLength)
                        .IsRequired(required: true)
                        .IsUnicode(unicode: false);

                    p.Property(pp => pp.IsVerified)
                        .HasColumnName(nameof(Resources.DataDictionary.IsEmailAddressVerified))
                        .IsRequired(required: true);

                    p.Property(pp => pp.VerificationKey)
                        .HasColumnName(nameof(Resources.DataDictionary.EmailAddressVerificationKey))
                        .HasMaxLength(maxLength: Domain.SharedKernel.EmailAddress.VerificationKeyMaxLength)
                        .IsRequired(required: true)
                        .IsUnicode(unicode: false);
                });

            builder
                .HasMany(p => p.Groups)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>
                (joinEntityName: "UsersOfGroups",
                    p =>
                        p.HasOne<Group>()
                        .WithMany()
                        .IsRequired(required: true)
                        .HasForeignKey(foreignKeyPropertyNames: "UserId")
                        .OnDelete(deleteBehavior: DeleteBehavior.NoAction),
                    p =>
                        p.HasOne<User>()
                        .WithMany()
                        .IsRequired(required: true)
                        .HasForeignKey(foreignKeyPropertyNames: "GroupId")
                        .OnDelete(deleteBehavior: DeleteBehavior.NoAction));
        }
    }
}
