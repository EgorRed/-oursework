using AccountingForExpirationDates.Model.Warehouse;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IWarehouseProviderService
    {
        Task CreateWarehouse(CreateWarehouseModel WarehouseModel);
        Task RemoveWarehouse(RemoveWarehouseModel WarehouseModel);
        Task UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel);
        Task<WarehouseDto[]> GetAllWarehouses();
    }
}
