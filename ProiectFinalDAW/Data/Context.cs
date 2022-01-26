using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectFinalDAW.Models;
using ProiectFinalDAW.Models.Base;

namespace ProiectFinalDAW.Data
{
    public class Context:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<FavouriteAddress> FavouriteAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<User> Users { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .HasDefaultValue("Produsul nu exista");

            modelBuilder.Entity<Product>()
                .Property(e => e.BarCode)
                .HasDefaultValueSql(null);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .HasDefaultValue("Produsul nu exista");

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasDefaultValue("0");

            modelBuilder.Entity<Product>()
                .Property(e => e.Active)
                .HasDefaultValue(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order_Details)
                .WithOne(e => e.Product)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
        
        public override int SaveChanges() 
        {
            var entries = ChangeTracker
        .Entries()
        .Where(e => e.Entity is BaseEntity && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).DateModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).DateCreated = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }
    }
}
