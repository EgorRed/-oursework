namespace AccountingForExpirationDates.DataBase.Entitys
{
    public class AccessToWarehouseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<WarehouseEntity> Warehouses { get; set; } = new List<WarehouseEntity>();
    }
}
