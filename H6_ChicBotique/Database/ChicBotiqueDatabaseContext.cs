using H5_Webshop.Database.Entities;
using H6_ChicBotique.Database.Entities;
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

        public DbSet<AccountInfo> AccountInfo { get; set; }
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
                // Konfigurer relationen mellem Category og Products.
                entity.HasMany(e => e.Products).WithOne(e => e.Category).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });

            // modelBuilder.Entity<Category>().HasIndex(u => u.CategoryName).IsUnique();

            modelBuilder.Entity<Product>(entity =>
            {
                // Konfigurer relationen mellem Product og Category.
                entity.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Restrict);
            });




            modelBuilder.Entity<AccountInfo>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("getdate()"); //setting default value in the database table
                //entity.HasIndex(e => e.CreatedDate);
                //entity.HasKey(e=>e.Id);
                entity.HasOne(e => e.User).WithOne(e => e.Account).HasForeignKey<AccountInfo>(e => e.UserId).OnDelete(DeleteBehavior.Restrict).IsRequired(false);

            }); // specify the configuration for the AccountInfo and rules for this entity
           
         
            modelBuilder.Entity<HomeAddress>(entity =>
            {
                entity.HasOne(e => e.AccountInfo).WithOne(e => e.HomeAddress).HasForeignKey<HomeAddress>(e => e.AccountInfoId).OnDelete(DeleteBehavior.Cascade);
            });


            /*modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Order).WithOne(e => e.Payment).HasForeignKey<Order>(e => e.PaymentId).OnDelete(DeleteBehavior.Restrict);
            });
            */
            // Seeding data for the Category entity
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CategoryName = "Kids"
                },
                new Category
                {
                    Id = 2,
                    CategoryName = "Men"
                },
                new Category
                {
                    Id = 3,
                    CategoryName = "Women"
                }
            );

            // Seeding data for the Product entity
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fancy dress",
                    Price = 299.99M,
                    Description = "Kids dress",
                    Image = "dress1.jpg",
                    Stock = 10,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Title = "Blue T-Shirt",
                    Price = 199.99M,
                    Description = "T-Shirt for men",
                    Image = "BlueTShirt.jpg",
                    Stock = 10,
                    CategoryId = 2
                },
                new Product
                {
                    Id = 3,
                    Title = "Skirt",
                    Price = 159.99M,
                    Description = "Girls skirt",
                    Image = "skirt1.jpg",
                    Stock = 10,
                    CategoryId = 1
                },
                new Product
                {
                    Id = 4,
                    Title = "Jumpersuit",
                    Price = 279.99M,
                    Description = "Kids jumpersuit",
                    Image = "jumpersuit1.jpg",
                    Stock = 10,
                    CategoryId = 1
                },
                new Product
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


        }
    }
 

}
