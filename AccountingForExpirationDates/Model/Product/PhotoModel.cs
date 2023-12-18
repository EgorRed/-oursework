using System.ComponentModel.DataAnnotations;

namespace AccountingForExpirationDates.Model.Product
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public int FileId { get; set; }

        public ProductModel Product { get; set; }
    }
}
