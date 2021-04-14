﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoleBasedAuthorization.Data;

namespace RoleBasedAuthorization.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20210406064444_dataMobile")]
    partial class dataMobile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RoleBasedAuthorization.Model.AuthenticateModel", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("username");

                    b.ToTable("AuthenticateModel");
                });

            modelBuilder.Entity("RoleBasedAuthorization.Model.Cart", b =>
                {
                    b.Property<int>("cart_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("product_id")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("cart_id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("RoleBasedAuthorization.Model.Product", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("product_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("product_price")
                        .HasColumnType("float");

                    b.Property<string>("product_rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("product_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RoleBasedAuthorization.Model.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("user_avt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<string>("user_fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("user_otp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("user_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("user_picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_username")
                        .IsRequired()
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
