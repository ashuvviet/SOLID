﻿// <auto-generated />
using EmployeeManagementApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagementApi.Migrations
{
    [DbContext(typeof(EmployeeDBContext))]
    [Migration("20220919114215_TypeUpdate")]
    partial class TypeUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EmployeeManagementApi.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeeManagementApi.Models.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Insurance");
                });

            modelBuilder.Entity("EmployeeManagementApi.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BasicPay")
                        .HasColumnType("int");

                    b.Property<int>("Bonus")
                        .HasColumnType("int");

                    b.Property<int>("HRA")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("EmployeeManagementApi.Models.Insurance", b =>
                {
                    b.HasOne("EmployeeManagementApi.Models.Employee", null)
                        .WithOne("Insurance")
                        .HasForeignKey("EmployeeManagementApi.Models.Insurance", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeManagementApi.Models.Payment", b =>
                {
                    b.HasOne("EmployeeManagementApi.Models.Employee", null)
                        .WithOne("Salary")
                        .HasForeignKey("EmployeeManagementApi.Models.Payment", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeManagementApi.Models.Employee", b =>
                {
                    b.Navigation("Insurance");

                    b.Navigation("Salary");
                });
#pragma warning restore 612, 618
        }
    }
}
