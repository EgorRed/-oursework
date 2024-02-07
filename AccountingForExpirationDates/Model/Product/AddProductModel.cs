namespace AccountingForExpirationDates.Model.Product
{
    public class AddProductModel
    {
        //public int WarehouseID { get; set; }
        public string? BarcodeType1 { get; set; }
        public string? BarcodeType2 { get; set; }
        public string? Name { get; set; }
        public DateTime SellBy { get; set; }
    }
}
