﻿// <auto-generated />
using System;
using LdelossantosN5.Domain.Impl.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LdelossantosN5.Domain.Impl.Migrations
{
    [DbContext(typeof(UserPermissionDbContext))]
    [Migration("20240315135500_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.CQRSSEmployeeSecurityEntity", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("CQRSSEmployeeSecurityEntitySet");
                });

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.EmployeePermissionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("int");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasAlternateKey("EmployeeId", "PermissionTypeId");

                    b.HasIndex("PermissionTypeId");

                    b.ToTable("SEC_EmployeePermissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            PermissionTypeId = 1,
                            RequestStatus = 0,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 1,
                            PermissionTypeId = 2,
                            RequestStatus = 1,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 3,
                            EmployeeId = 1,
                            PermissionTypeId = 3,
                            RequestStatus = 2,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 4,
                            EmployeeId = 2,
                            PermissionTypeId = 2,
                            RequestStatus = 0,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 5,
                            EmployeeId = 3,
                            PermissionTypeId = 3,
                            RequestStatus = 0,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 6,
                            EmployeeId = 4,
                            PermissionTypeId = 4,
                            RequestStatus = 2,
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        });
                });

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.EmployeeSecurityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("SEC_Employees", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Leandro",
                            StartDate = new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 2,
                            Name = "Mariela",
                            StartDate = new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 3,
                            Name = "Alberto",
                            StartDate = new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 4,
                            Name = "Isabel",
                            StartDate = new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Local),
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        });
                });

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.PermissionTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("SEC_PermissionTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Enable the creation of new clients",
                            ShortName = "Create Client",
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 2,
                            Description = "Enable the edition of clients personal data",
                            ShortName = "Edit Client",
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 3,
                            Description = "Enable to block the client requesting authorization for any financial operation",
                            ShortName = "Block Client",
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        },
                        new
                        {
                            Id = 4,
                            Description = "Signal the client as suspicios of fraudulent activities",
                            ShortName = "Mark Client as suspect",
                            Timestamp = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }
                        });
                });

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.EmployeePermissionEntity", b =>
                {
                    b.HasOne("LdelossantosN5.Domain.Impl.DbEntities.EmployeeSecurityEntity", "Employee")
                        .WithMany("Permissions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LdelossantosN5.Domain.Impl.DbEntities.PermissionTypeEntity", "PermissionType")
                        .WithMany()
                        .HasForeignKey("PermissionTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("PermissionType");
                });

            modelBuilder.Entity("LdelossantosN5.Domain.Impl.DbEntities.EmployeeSecurityEntity", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
