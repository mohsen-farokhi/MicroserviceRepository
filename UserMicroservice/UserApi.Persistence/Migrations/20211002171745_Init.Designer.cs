﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserApi.Persistence;

namespace UserApi.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211002171745_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserApi.Domain.Aggregates.Groups.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UserApi.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CellPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(false)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UserApi.Domain.Aggregates.Users.ValueObjects.Role", b =>
                {
                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Value");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Value = 4,
                            Name = "برنامه‌نویس"
                        },
                        new
                        {
                            Value = 3,
                            Name = "مدیر"
                        },
                        new
                        {
                            Value = 2,
                            Name = "مسئول"
                        },
                        new
                        {
                            Value = 1,
                            Name = "نماینده"
                        },
                        new
                        {
                            Value = 0,
                            Name = "مشتری"
                        });
                });

            modelBuilder.Entity("UserApi.Domain.SharedKernel.Gender", b =>
                {
                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Value");

                    b.ToTable("Genders");

                    b.HasData(
                        new
                        {
                            Value = 0,
                            Name = "آقا"
                        },
                        new
                        {
                            Value = 1,
                            Name = "خانم"
                        });
                });

            modelBuilder.Entity("UsersOfGroups", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersOfGroups");
                });

            modelBuilder.Entity("UserApi.Domain.Aggregates.Users.User", b =>
                {
                    b.HasOne("UserApi.Domain.Aggregates.Users.ValueObjects.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("UserApi.Domain.SharedKernel.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<bool>("IsVerified")
                                .HasColumnType("bit")
                                .HasColumnName("IsEmailAddressVerified");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(250)
                                .IsUnicode(false)
                                .HasColumnType("varchar(250)")
                                .HasColumnName("EmailAddress");

                            b1.Property<string>("VerificationKey")
                                .IsRequired()
                                .HasMaxLength(32)
                                .IsUnicode(false)
                                .HasColumnType("varchar(32)")
                                .HasColumnName("EmailAddressVerificationKey");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("UserApi.Domain.SharedKernel.FullName", "FullName", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("FirstName");

                            b1.Property<int>("GenderId")
                                .HasColumnType("int")
                                .HasColumnName("GenderId");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.HasIndex("GenderId");

                            b1.ToTable("Users");

                            b1.HasOne("UserApi.Domain.SharedKernel.Gender", "Gender")
                                .WithMany()
                                .HasForeignKey("GenderId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("UserId");

                            b1.Navigation("Gender");
                        });

                    b.Navigation("EmailAddress");

                    b.Navigation("FullName");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UsersOfGroups", b =>
                {
                    b.HasOne("UserApi.Domain.Aggregates.Users.User", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("UserApi.Domain.Aggregates.Groups.Group", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}