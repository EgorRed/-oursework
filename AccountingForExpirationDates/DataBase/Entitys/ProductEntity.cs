using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AccountingForExpirationDates.DataBase.Entitys
{
    public class ProductEntity
    {

        public int Id { get; set; }
        public string? BarcodeType1 { get; set; }
        public string? BarcodeType2 { get; set; }
        public string? Name { get; set; }

        public DateTime SellBy { get; set; }

        public int? CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }

        public List<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();

        public int? WarehouseId { get; set; }
        public WarehouseEntity? Warehouse { get; set; }

    }
}
