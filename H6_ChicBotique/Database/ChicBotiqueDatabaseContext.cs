using H5_Webshop.Database.Entities;
using H6_ChicBotique.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace H6_ChicBotique.Database
{
    public class ChicBotiqueDatabaseContext : DbContext
    {
        public ChicBotiqueDatabaseContext() { }
        public ChicBotiqueDatabaseContext(DbContextOptions<ChicBotiqueDatabaseContext> options) : base(options) { }
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
            
           
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Order).WithOne(e => e.Payment).HasForeignKey<Order>(e => e.PaymentId).OnDelete(DeleteBehavior.Restrict);
            });

        }
    }
 

}
