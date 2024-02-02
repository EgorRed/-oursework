namespace AccountingForExpirationDates.DataBase.Entitys
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<ProductEntity> Product { get; set; } = new List<ProductEntity>();
    }
}
