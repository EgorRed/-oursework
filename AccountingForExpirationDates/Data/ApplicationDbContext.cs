using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AccountingForExpirationDates.Model.Product;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace AccountingForExpirationDates.UserData
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<PhotoModel> Photo { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ProductModel>()
                .HasOne(x => x.Category)
                .WithMany(t => t.Product)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<ProductModel>()
                .HasMany(x => x.Photos)
                .WithOne(t => t.Product)
                .HasForeignKey(p => p.Product);

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
