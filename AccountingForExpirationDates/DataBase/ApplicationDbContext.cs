using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using AccountingForExpirationDates.DataBase.Entitys;

namespace AccountingForExpirationDates.DataBase
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<PhotoEntity> Photo { get; set; }
        public DbSet<WarehouseEntity> Warehouses { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ProductEntity>()
                .HasOne(x => x.Category)
                .WithMany(t => t.Product)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<ProductEntity>()
                .HasMany(x => x.Photos)
                .WithOne(t => t.Product)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<ProductEntity>()
                .HasOne(x => x.Warehouse)
                .WithMany(t => t.Product)
                .HasForeignKey(p => p.WarehouseId);

            base.OnModelCreating(builder);
        }
    }
}
