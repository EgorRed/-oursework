using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class WarehouseProviderService : IWarehouseProviderService
    {
        private ApplicationDbContext _db;
        public AccessToWarehouse _access;

        public WarehouseProviderService(ApplicationDbContext db)
        {
            _db = db;
            _access = new AccessToWarehouse(db);
        }

        public async Task<Status> CreateWarehouse(CreateWarehouseModel WarehouseModel, UserNameModel userName)
        {
            
            WarehouseEntity warehouse = new WarehouseEntity();
            if (WarehouseModel != null)
            {
                warehouse.Name = WarehouseModel.Name;
                warehouse.Description = WarehouseModel.Description;              
                await _db.Warehouses.AddAsync(warehouse);
                await _db.SaveChangesAsync();
                
                var accessResult = await _access.RegistrationUserInWarehouse(userName, new WarehouseID(warehouse.Id));
                if (accessResult.StatusCode != RequestStatus.OK) 
                {
                    return accessResult;
                }
                return new Status(RequestStatus.OK, "success");
            }
            return new Status(RequestStatus.DataIsNull, "in WarehouseModel empty value");

        }


        public async Task<Outcome<Status, WarehouseDto[]>> GetAllWarehouses(UserNameModel userName)
        {

            var warehouses = await _access.GetUsersWarehouses(userName);
            if (warehouses.status.StatusCode == RequestStatus.OK)
            {
                var warehouseDtoList = new List<WarehouseDto>();

                foreach (var item in warehouses.data)
                {
                    WarehouseDto warehouseDto = new WarehouseDto();
                    warehouseDto.Id = item.Id;
                    warehouseDto.Name = item.Name;
                    warehouseDto.Description = item.Description;
                    warehouseDtoList.Add(warehouseDto);
                }

                return new Outcome<Status, WarehouseDto[]>(new Status(RequestStatus.OK, "success"), warehouseDtoList.ToArray());
            }
            else
            {
                return new Outcome<Status, WarehouseDto[]>(warehouses.status, new WarehouseDto[0]);
            }
        }


        public async Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, new WarehouseID(WarehouseModel.Id));
            if (CAccess.data)
            {

                var warehouse = await _db.Warehouses.Include(p => p.Product)
                                                    .Include(c => c.Category)
                                                    .FirstOrDefaultAsync(x => x.Id == WarehouseModel.Id);

                if (warehouse != null)
                {
                    var action = await _access.RemoveAccess(userName, new WarehouseID(WarehouseModel.Id));
                    if (action.StatusCode == RequestStatus.OK)
                    {
                        warehouse.Product.Clear();
                        warehouse.Category.Clear();
                        _db.Warehouses.Remove(warehouse);
                        await _db.SaveChangesAsync();
                        return new Status(RequestStatus.OK, "success");
                    }
                    else
                    {
                        return action;
                    }
                }
                else
                {
                    return new Status(RequestStatus.DataIsNotFound, $"There is no such warehouse." +
                        $"[WarehouseID: {WarehouseModel.Id}]");
                }
            }
            else
            {
                return new Status(RequestStatus.AccessIsDenied, "Access is denied");
            }
            
        }


        public async Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, new WarehouseID(WarehouseModel.Id));
            if (CAccess.data)
            {
                var warehouse = await _db.Warehouses.Where(x => x.Id == WarehouseModel.Id).FirstOrDefaultAsync();
                if (warehouse != null)
                {

                    warehouse.Description = WarehouseModel.Description;
                    await _db.SaveChangesAsync();

                    return new Status(RequestStatus.OK, "success");
                }
                else
                {
                    return new Status(RequestStatus.DataIsNotFound, $"There is no such warehouse." +
                        $"[WarehouseID: {WarehouseModel.Id}]");
                }
            }
            else
            {
                return new Status(RequestStatus.AccessIsDenied, "Access is denied");
            }
        }
    }
}
