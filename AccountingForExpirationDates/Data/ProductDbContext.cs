using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.UserData;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Data
{
    public class ProductDbContext : DbContext
    {

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<PhotoModel> Photo { get; set; }

        public ProductDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
