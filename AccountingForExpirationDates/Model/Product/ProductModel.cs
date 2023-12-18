using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace AccountingForExpirationDates.Model.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string BarcodeType1 { get; set; }
        public string BarcodeType2 { get; set; }
        public string Name { get; set; }

        public DateTime SellBy { get; set; }
        
        public int CategoryId { get; set; }
        public CategoryModel Category { get; set; }

        public List<PhotoModel> Photos { get; set; } = new List<PhotoModel>();
    }
}
