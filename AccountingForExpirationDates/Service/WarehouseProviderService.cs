using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class WarehouseProviderService : IWarehouseProviderService
    {
        public ApplicationDbContext _db;

        public WarehouseProviderService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Status> CreateWarehouse(CreateWarehouseModel WarehouseModel)
        {
            WarehouseEntity warehouse = new WarehouseEntity();
            warehouse.Description = WarehouseModel.Description;
            await _db.Warehouses.AddAsync(warehouse);
            await _db.SaveChangesAsync();
            return new Status(1, "success");
        }

        public async Task<Outcome<Status, WarehouseDto[]>> GetAllWarehouses()
        {
            var warehouses = await _db.Warehouses.ToArrayAsync();
            var warehouseDtoList = new List<WarehouseDto>();
            foreach (var item in warehouses) 
            {
                WarehouseDto warehouseDto = new WarehouseDto();
                warehouseDto.Id = item.Id;
                warehouseDto.Description = item.Description;
                warehouseDtoList.Add(warehouseDto);
            }

            return new Outcome<Status, WarehouseDto[]>(new Status(1, "success"), warehouseDtoList.ToArray());
        }

        public async Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel)
        {
            var warehouse = await _db.Warehouses.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == WarehouseModel.Id);
            if (warehouse != null)
            {
                warehouse.Product.Clear();
                _db.Warehouses.Remove(warehouse);
                await _db.SaveChangesAsync();
            }
            else
            {
                return new Status(0, $"There is no such warehouse." +
                    $"[WarehouseID: {WarehouseModel.Id}]");
            }
            return new Status(1, "success");
        }

        public Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel)
        {
            throw new NotImplementedException();
        }
    }
}
