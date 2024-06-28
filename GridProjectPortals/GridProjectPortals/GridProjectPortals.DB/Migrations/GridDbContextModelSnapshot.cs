﻿// <auto-generated />
using System;
using GridProjectPortals.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GridProjectPortals.DB.Migrations
{
    [DbContext(typeof(GridDbContext))]
    partial class GridDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FailedLoginAttempts")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("tblEmployees");
                });

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblGridColumnDef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GridName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PageId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("tblGridColumnDefs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            GridName = "grdEmployeeDetails",
                            PageId = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblGridColumnDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColumnDataType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColumnName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GridId")
                        .HasColumnType("int");

                    b.Property<int?>("Sequence")
                        .HasColumnType("int");

                    b.Property<bool?>("isFix")
                        .HasColumnType("bit");

                    b.Property<bool?>("isSearchable")
                        .HasColumnType("bit");

                    b.Property<bool?>("isVisible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tblGridColumnDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColumnDataType = "int",
                            ColumnName = "id",
                            GridId = 1,
                            Sequence = 1,
                            isFix = false,
                            isSearchable = false,
                            isVisible = false
                        },
                        new
                        {
                            Id = 2,
                            ColumnDataType = "string",
                            ColumnName = "firstname",
                            GridId = 1,
                            Sequence = 2,
                            isFix = false,
                            isSearchable = true,
                            isVisible = true
                        },
                        new
                        {
                            Id = 3,
                            ColumnDataType = "string",
                            ColumnName = "lastname",
                            GridId = 1,
                            Sequence = 3,
                            isFix = false,
                            isSearchable = true,
                            isVisible = true
                        },
                        new
                        {
                            Id = 4,
                            ColumnDataType = "string",
                            ColumnName = "username",
                            GridId = 1,
                            Sequence = 4,
                            isFix = false,
                            isSearchable = true,
                            isVisible = true
                        },
                        new
                        {
                            Id = 5,
                            ColumnDataType = "string",
                            ColumnName = "phone",
                            GridId = 1,
                            Sequence = 5,
                            isFix = false,
                            isSearchable = true,
                            isVisible = true
                        });
                });

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblGridDataTypeOperator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DataType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Operator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorInWords")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblGridDataTypeOperators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataType = "int",
                            Operator = ">",
                            OperatorInWords = "GreaterThan"
                        },
                        new
                        {
                            Id = 2,
                            DataType = "int",
                            Operator = "<",
                            OperatorInWords = "LessThan"
                        },
                        new
                        {
                            Id = 3,
                            DataType = "int",
                            Operator = "=",
                            OperatorInWords = "EqualsTo"
                        },
                        new
                        {
                            Id = 4,
                            DataType = "string",
                            Operator = "",
                            OperatorInWords = "StartsWith"
                        },
                        new
                        {
                            Id = 5,
                            DataType = "string",
                            Operator = "",
                            OperatorInWords = "EndsWith"
                        },
                        new
                        {
                            Id = 6,
                            DataType = "string",
                            Operator = "",
                            OperatorInWords = "Contains"
                        },
                        new
                        {
                            Id = 7,
                            DataType = "string",
                            Operator = "",
                            OperatorInWords = "In"
                        });
                });

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblGridPages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Page")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tblGridPages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Page = "Employees"
                        });
                });

            modelBuilder.Entity("GridProjectPortals.Domain.Tables.tblEmployee", b =>
                {
                    b.HasOne("GridProjectPortals.Domain.Tables.tblEmployee", "UserCreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("GridProjectPortals.Domain.Tables.tblEmployee", "UserUpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedBy")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("UserCreatedBy");

                    b.Navigation("UserUpdatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
