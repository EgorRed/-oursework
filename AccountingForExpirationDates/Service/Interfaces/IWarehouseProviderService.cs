using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IWarehouseProviderService
    {
        Task<Status> CreateWarehouse(CreateWarehouseModel WarehouseModel, UserNameModel userName);
        Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel, UserNameModel userName);
        Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel, UserNameModel userName);
        Task<Outcome<Status,WarehouseDto[]>> GetAllWarehouses(UserNameModel userName);
    }
}
