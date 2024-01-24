using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;

namespace AccountingForExpirationDates.Service
{
    public class WarehouseProviderService : IWarehouseProviderService
    {
        public ApplicationDbContext _db;

        public WarehouseProviderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Status> CreateWarehouse(CreateWarehouseModel WarehouseModel)
        {
            throw new NotImplementedException();
        }

        public Task<Pair<Status, WarehouseDto[]>> GetAllWarehouses()
        {
            throw new NotImplementedException();
        }

        public Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel)
        {
            throw new NotImplementedException();
        }

        public Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel)
        {
            throw new NotImplementedException();
        }
    }
}
