using H5_Webshop.Database.Entities;
using H6_ChicBotique.Database.Entities;
using H6_ChicBotique.Helpers;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Database
{
    public class ChicBotiqueDatabaseContext : DbContext
    {
        public ChicBotiqueDatabaseContext() { }
        public ChicBotiqueDatabaseContext(DbContextOptions<ChicBotiqueDatabaseContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<PasswordEntity> PasswordEntity { get; set; }
        public DbSet<HomeAddress> HomeAddress { get; set; }

        public DbSet<AccountInfo> Account { get; set; }
        public DbSet<Payment> Payment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();  //making email as unique entity
            modelBuilder.Entity<PasswordEntity>(entity =>
            {
                entity.HasOne(e => e.User).WithOne().HasForeignKey<PasswordEntity>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            });  // specify the configuration for the PasswordEntity and relationship with the User entity

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany(e => e.Products).WithOne(e => e.Category).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });
            // modelBuilder.Entity<Category>().HasIndex(u => u.CategoryName).IsUnique();
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });


            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("getdate()");
                //entity.HasIndex(e => e.CreatedDate);
                //entity.HasKey(e=>e.Id);
                entity.HasOne(e => e.User).WithOne(e => e.Account).HasForeignKey<AccountInfo>(e => e.UserId).OnDelete(DeleteBehavior.Restrict);
            }); // specify the configuration for the AccountInfo and rules for this entity


            modelBuilder.Entity<HomeAddress>(entity =>
            {
                entity.HasOne(e => e.Account).WithOne(e => e.HomeAddress).HasForeignKey<HomeAddress>(e => e.AccountInfoId).OnDelete(DeleteBehavior.Cascade);
            });


            /*modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Order).WithOne(e => e.Payment).HasForeignKey<Order>(e => e.PaymentId).OnDelete(DeleteBehavior.Restrict);
            });
            */
            // Seeding data for the Category entity
            modelBuilder.Entity<Category>().HasData(
                new()
                {
                    Id = 1,
                    CategoryName = "Kids"


                },
                new()
                {
                    Id = 2,
                    CategoryName = "Men"
                },
                new()
                {
                    Id = 3,
                    CategoryName = "Women"
                }
                );
            // Seeding data for the Product entity
            modelBuilder.Entity<Product>().HasData(
                new()
                {
                    Id = 1,
                    Title = " Fancy dress",
                    Price = 299.99M,
                    Description = "kids dress",
                    Image = "dress1.jpg",
                    Stock = 10,
                    CategoryId = 1

                },

                new()
                {
                    Id = 2,
                    Title = "Blue T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for men",
                    Image = "BlueTShirt.jpg",
                    Stock = 10,
                    CategoryId = 2

                },

                new()
                {
                    Id = 3,
                    Title = "Skirt",
                    Price = 159.99M,
                    Description = "Girls skirt",
                    Image = "skirt1.jpg",
                    Stock = 10,
                    CategoryId = 1

                },
                new()
                {
                    Id = 4,
                    Title = "Jumpersuit",
                    Price = 279.99M,
                    Description = "kids jumpersuit",
                    Image = "jumpersuit1.jpg",
                    Stock = 10,
                    CategoryId = 1

                },
                new()
                {
                    Id = 5,
                    Title = "Red T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for men",
                    Image = "RedT-Shirt.jpg",
                    Stock = 10,
                    CategoryId = 2
                },
                 new Product
                 {
                     Id = 6,
                     Title = "Long dress",
                     Price = 299.99M,
                     Description = "Summer clothing",
                     Image = "floral-dress.jpg",
                     Stock = 10,
                     CategoryId = 3
                 },
                new Product
                {
                    Id = 7,
                    Title = "Red dress",
                    Price = 299.99M,
                    Description = "Party dress for women",
                    Image = "Red-dress.jpg",
                    Stock = 10,
                    CategoryId = 3
                }
            );
            // Seed Data for User Entity
            modelBuilder.Entity<User>().HasData(
                // User 1: Administrator
                new User
                {
                    Id = 1,
                    FirstName = "Peter",
                    LastName = "Aksten",
                    Email = "peter@abc.com",
                    Role = Role.Administrator
                },
                // User 2: Member
                new User
                {
                    Id = 2,
                    FirstName = "Rizwanah",
                    LastName = "Mustafa",
                    Email = "riz@abc.com",
                    Role = Role.Member
                },
                // User 3: Guest
                new User
                {
                    Id = 3,
                    FirstName = "Afrina",
                    LastName = "Rahaman",
                    Email = "afr@abc.com",
                    Role = Role.Guest
                }
            );

            Guid acc1id = Guid.NewGuid();
            Guid acc2id = Guid.NewGuid();

            // Seed Data for AccountInfo Entity
          modelBuilder.Entity<AccountInfo>().HasData(
               new()
               {
                   // Id = Guid.Parse("3e79cea4 - d1a1 - 4954 - bad2 - d2ca09aff5d3"),
                   Id = acc1id,
                   UserId = 1
               },

               new()
               {
                   //Id =Guid.Parse("c8c1fe00-599d-480f-9fe5-cc0a5a6d9f45"),
                   Id = acc2id,
                   UserId = 2

               }
               );
            modelBuilder.Entity<HomeAddress>().HasData(
                new()
                {
                    AccountInfoId = acc1id,
                    Id = 1,

                    Address = "Husum",
                    City = "Copenhagen",
                    PostalCode = "2200",
                    Country = "Danmark",
                    TelePhone = "+228415799"

                },
                new()
                {

                    AccountInfoId = acc2id,
                    Id = 2,
                    Address = "Husum",
                    City = "Copenhagen",
                    PostalCode = "2200",
                    Country = "Danmark",
                    TelePhone = "+228415799"
                }
                );
            // Seed Data for PasswordEntity
            // Generate a unique salt using the current date and time
            var salt = DateTime.Now.ToString();

            modelBuilder.Entity<PasswordEntity>().HasData(
                // PasswordEntity 1 for User 1
                new PasswordEntity
                {
                    PasswordId = 1,
                    UserId = 1,
                    Password = PasswordHelpers.HashPassword("password" + salt),
                    Salt = salt,
                },
                // PasswordEntity 2 for User 2
                new PasswordEntity
                {
                    PasswordId = 2,
                    UserId = 2,
                    Password = PasswordHelpers.HashPassword("password1" + salt),
                    Salt = salt,
                }
            );




        }



    }
    }


        
 


