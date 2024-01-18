using System.ComponentModel.DataAnnotations;

namespace AccountingForExpirationDates.DataBase.Entitys
{
    public class PhotoEntity
    {
        public int Id { get; set; }
        public int FileId { get; set; }

        public int ProductId { get; set; }
        public ProductEntity? Product { get; set; }
    }
}
