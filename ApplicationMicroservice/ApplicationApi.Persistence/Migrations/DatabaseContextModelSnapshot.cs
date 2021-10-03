﻿// <auto-generated />
using System;
using ApplicationApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationApi.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.Applications.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.OperationGroups.OperationGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OperationGroups");
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.Operations.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessTypeId")
                        .HasColumnType("int")
                        .HasColumnName("AccessTypeId");

                    b.Property<int?>("ApplicationId")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AccessTypeId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.Operations.ValueObjects.AccessType", b =>
                {
                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Value");

                    b.ToTable("AccessTypes");

                    b.HasData(
                        new
                        {
                            Value = 0,
                            Name = "عمومی"
                        },
                        new
                        {
                            Value = 1,
                            Name = "ثبت نام شده"
                        },
                        new
                        {
                            Value = 2,
                            Name = "خصوصی"
                        });
                });

            modelBuilder.Entity("OperationsOfGroups", b =>
                {
                    b.Property<int>("OperationGroupId")
                        .HasColumnType("int");

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.HasKey("OperationGroupId", "OperationId");

                    b.HasIndex("OperationId");

                    b.ToTable("OperationsOfGroups");
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.OperationGroups.OperationGroup", b =>
                {
                    b.HasOne("ApplicationApi.Domain.Aggregates.Applications.Application", "Application")
                        .WithMany("OperationGroups")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Application");
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.Operations.Operation", b =>
                {
                    b.HasOne("ApplicationApi.Domain.Aggregates.Operations.ValueObjects.AccessType", "AccessType")
                        .WithMany()
                        .HasForeignKey("AccessTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationApi.Domain.Aggregates.Applications.Application", "Application")
                        .WithMany("Operations")
                        .HasForeignKey("ApplicationId");

                    b.Navigation("AccessType");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("OperationsOfGroups", b =>
                {
                    b.HasOne("ApplicationApi.Domain.Aggregates.OperationGroups.OperationGroup", null)
                        .WithMany()
                        .HasForeignKey("OperationGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ApplicationApi.Domain.Aggregates.Operations.Operation", null)
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationApi.Domain.Aggregates.Applications.Application", b =>
                {
                    b.Navigation("OperationGroups");

                    b.Navigation("Operations");
                });
#pragma warning restore 612, 618
        }
    }
}
