using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using AccountingForExpirationDates.Data.Entitys;

namespace AccountingForExpirationDates.UserData
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<PhotoEntity> Photo { get; set; }

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

            //builder.Entity<ProductModel>()
            //    .HasKey(x => x.Id);

            //builder.Entity<ProductModel>()
            //    .Property(x => x.SellBy).IsRequired().HasColumnType("datetime2");


            //builder.Entity<PhotoModel>()
            //    .HasKey(x => x.Id);

            //builder.Entity<CategoryModel>()
            //    .HasKey(x => x.Id);

            base.OnModelCreating(builder);
        }
    }
}
