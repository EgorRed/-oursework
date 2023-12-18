using System.ComponentModel.DataAnnotations;

namespace AccountingForExpirationDates.Model.Product
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ProductModel> Product { get; set; } = new List<ProductModel>();
    }
}
