﻿// <auto-generated />
using System;
using H6_ChicBotique.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace H6_ChicBotique.Migrations
{
    [DbContext(typeof(ChicBotiqueDatabaseContext))]
    partial class ChicBotiqueDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.AccountInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("AccountInfo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("46e295b3-d03d-46d8-9144-c7e66e438d9a"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 1
                        },
                        new
                        {
                            Id = new Guid("619ebb7b-c2bd-4732-9a7a-ca245a751a07"),
                            CreatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = 2
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Kids"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Men"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Women"
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.HomeAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AccountInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountInfoId")
                        .IsUnique();

                    b.ToTable("HomeAddress");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountInfoId = new Guid("46e295b3-d03d-46d8-9144-c7e66e438d9a"),
                            Address = "Husum",
                            City = "Copenhagen",
                            Country = "Danmark",
                            PostalCode = "2200",
                            TelePhone = "+228415799"
                        },
                        new
                        {
                            Id = 2,
                            AccountInfoId = new Guid("619ebb7b-c2bd-4732-9a7a-ca245a751a07"),
                            Address = "Gladsaxe",
                            City = "Copenhagen",
                            Country = "Danmark",
                            PostalCode = "2400",
                            TelePhone = "+228515798"
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("AccountInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("Date")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountInfoId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Order");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<short>("Quantity")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.PasswordEntity", b =>
                {
                    b.Property<int>("PasswordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasswordId"), 1L, 1);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PasswordId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("PasswordEntity");

                    b.HasData(
                        new
                        {
                            PasswordId = 1,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "89D969DDBF37717FD4089D17C4614924CB82E6B23A07C829F1F01F5B13DA2B4E",
                            Salt = "Nu9JDjXoDpphDlMYcg1y9g==",
                            UserId = 1
                        },
                        new
                        {
                            PasswordId = 2,
                            LastUpdated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "98C805B460789EE55E07947A708EF145E8813E19F4878ED3AF3E1CA197E3012D",
                            Salt = "Nu9JDjXoDpphDlMYcg1y9g==",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("Decimal(10,3)");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("TimePaid")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6,2)");

                    b.Property<short>("Stock")
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "kids dress",
                            Image = "dress1.jpg",
                            Price = 299.99m,
                            Stock = (short)10,
                            Title = " Fancy dress"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Description = "T-Shirt for men",
                            Image = "BlueTShirt.jpg",
                            Price = 199.99m,
                            Stock = (short)10,
                            Title = "Blue T-Shirt"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Girls skirt",
                            Image = "skirt1.jpg",
                            Price = 159.99m,
                            Stock = (short)10,
                            Title = "Skirt"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "kids jumpersuit",
                            Image = "jumpersuit1.jpg",
                            Price = 279.99m,
                            Stock = (short)10,
                            Title = "Jumpersuit"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "T-Shirt for men",
                            Image = "RedT-Shirt.jpg",
                            Price = 199.99m,
                            Stock = (short)10,
                            Title = "Red T-Shirt"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            Description = "Summer clothing",
                            Image = "womendress2.jpg",
                            Price = 299.99m,
                            Stock = (short)10,
                            Title = "Long dress"
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 3,
                            Description = "Spring Floral dress for women",
                            Image = "womendress2.jpg",
                            Price = 299.99m,
                            Stock = (short)10,
                            Title = "Floral dress"
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.ShippingDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("ShippingDetails");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "peter@abc.com",
                            FirstName = "Peter",
                            LastName = "Aksten",
                            Role = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "riz@abc.com",
                            FirstName = "Rizwanah",
                            LastName = "Mustafa",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            Email = "afr@abc.com",
                            FirstName = "Afrina",
                            LastName = "Rahaman",
                            Role = 2
                        });
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.AccountInfo", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.User", "User")
                        .WithOne("AccountInfo")
                        .HasForeignKey("H6_ChicBotique.Database.Entities.AccountInfo", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("User");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.HomeAddress", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.AccountInfo", "AccountInfo")
                        .WithOne("HomeAddress")
                        .HasForeignKey("H6_ChicBotique.Database.Entities.HomeAddress", "AccountInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountInfo");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Order", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.AccountInfo", "AccountInfo")
                        .WithMany("Orders")
                        .HasForeignKey("AccountInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("H6_ChicBotique.Database.Entities.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("H6_ChicBotique.Database.Entities.Order", "PaymentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AccountInfo");

                    b.Navigation("Payment");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.OrderDetails", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("H6_ChicBotique.Database.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.PasswordEntity", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("H6_ChicBotique.Database.Entities.PasswordEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Product", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.ShippingDetails", b =>
                {
                    b.HasOne("H6_ChicBotique.Database.Entities.Order", "Order")
                        .WithOne("ShippingDetails")
                        .HasForeignKey("H6_ChicBotique.Database.Entities.ShippingDetails", "OrderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.AccountInfo", b =>
                {
                    b.Navigation("HomeAddress");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ShippingDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.Payment", b =>
                {
                    b.Navigation("Order")
                        .IsRequired();
                });

            modelBuilder.Entity("H6_ChicBotique.Database.Entities.User", b =>
                {
                    b.Navigation("AccountInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
