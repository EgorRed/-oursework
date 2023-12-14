namespace AccountingForExpirationDates.Model.Product
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string? FileID { get; set; }

        public ProductModel? Product { get; set; }
    }
}
