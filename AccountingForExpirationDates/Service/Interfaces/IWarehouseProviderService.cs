using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Warehouse;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IWarehouseProviderService
    {
        Task<Status> CreateWarehouse(CreateWarehouseModel WarehouseModel);
        Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel);
        Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel);
        Task<Pair<Status,WarehouseDto[]>> GetAllWarehouses();
    }
}
