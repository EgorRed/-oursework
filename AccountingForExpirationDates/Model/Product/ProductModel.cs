using System.Data;

namespace AccountingForExpirationDates.Model.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public ulong BarcodeType1 { get; set; }
        public ulong BarcodeType2 { get; set; }
        public string? Name { get; set; }

        public DateTime SellBy { get; set; }

        public CategoryModel? Category { get; set; }
        public PhotoModel? Photos { get; set; }
    }
}
