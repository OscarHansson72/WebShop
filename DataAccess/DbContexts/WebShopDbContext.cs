using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbContexts
{
    public class WebShopDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>()
               .HasOne(sc => sc.User)
               .WithOne(u => u.ShoppingCart)
               .HasForeignKey<ShoppingCart>(sc => sc.UserId);

            modelBuilder.Entity<ShoppingCart>()
                .Property(s => s.Version)
                .IsRowVersion();

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.ShoppingCart)
                .WithMany(sc => sc.Items)
                .HasForeignKey(sci => sci.ShoppingCartId);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);
        }
    }
}
