namespace AccountingForExpirationDates.DataBase.Entitys
{
    public class AccessToWarehouseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<int> WarehouseId { get; set; } = new List<int>();
    }
}
