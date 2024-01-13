using AccountingForExpirationDates.Model.Category;

namespace AccountingForExpirationDates.Model.Product
{
    public class ProductModelDto
    {
        public int Id { get; set; }
        public string? BarcodeType1 { get; set; }
        public string? BarcodeType2 { get; set; }
        public string? Name { get; set; }
        public DateTime SellBy { get; set; }
        public string? categoryName { get; set; }
        public int? categoryId { get; set; }
    }
}
