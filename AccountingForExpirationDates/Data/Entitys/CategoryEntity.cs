using System.ComponentModel.DataAnnotations;

namespace AccountingForExpirationDates.Data.Entitys
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<ProductEntity> Product { get; set; } = new List<ProductEntity>();
    }
}
