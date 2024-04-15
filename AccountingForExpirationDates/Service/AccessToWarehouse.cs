using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Warehouse;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class AccessToWarehouse
    {
        private ApplicationDbContext _db;


        public AccessToWarehouse(ApplicationDbContext db)
        {
            _db = db;
        }


        private async Task<string> GetID(UserNameModel userName)
        {
            var user = await _db.Users.Where(x => x.UserName.Equals(userName.Name)).FirstOrDefaultAsync();
            return user.Id;
        }


        public async Task<Outcome<Status, WarehouseEntity[]>> GetUsersWarehouses(UserNameModel userName)
        {
            var userID = await GetID(userName);
            var access = await _db.AccessToWarehouse.Where(x => x.UserId.Equals(userID)).Include(y => y.Warehouses).FirstOrDefaultAsync();
            if (access != null)
            {
                return new Outcome<Status, WarehouseEntity[]>(new Status(RequestStatus.OK, "success"), access.Warehouses.ToArray());
            }
            else
            {
                return new Outcome<Status, WarehouseEntity[]>(new Status(RequestStatus.DataIsNull, "There are no warehouses"), new WarehouseEntity[0]);
            }
        }


        public async Task<Outcome<Status, AccessToWarehouseEntity[]>> GetWarehouseUsers(WarehouseID warehouseID)
        {
            var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).Include(y => y.AccessToWarehouse).FirstOrDefaultAsync();
            if (warehouse != null)
            {
                return new Outcome<Status, AccessToWarehouseEntity[]>(new Status(RequestStatus.OK, "success"), warehouse.AccessToWarehouse.ToArray());
            }
            else
            {
                return new Outcome<Status, AccessToWarehouseEntity[]>(new Status(RequestStatus.DataIsNull, "There are no warehouses"), new AccessToWarehouseEntity[0]);
            }
        }


        public async Task<Status> RegistrationUserInWarehouse(UserNameModel userName, WarehouseID warehouseID)
        {
            var userID = await GetID(userName);

            var access = await _db.AccessToWarehouse.Where(x => x.UserId.Equals(userID)).FirstOrDefaultAsync();
            if (access == null)
            {
                AccessToWarehouseEntity newAccess = new AccessToWarehouseEntity();
                var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
                newAccess.UserId = userID;
                newAccess.Warehouses.Add(warehouse);
                warehouse.AccessToWarehouse.Add(newAccess);
                await _db.SaveChangesAsync();
                return new Status(RequestStatus.OK, "success");
            }
            else
            {
                var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
                access.Warehouses.Add(warehouse);
                warehouse.AccessToWarehouse.Add(access);
                await _db.SaveChangesAsync();
                return new Status(RequestStatus.OK, "success");
            }
        }


        public async Task<Status> RemoveAccess(UserNameModel userName, WarehouseID warehouseID)
        {
            var userID = await GetID(userName);
            var access = await _db.AccessToWarehouse.Where(x => x.UserId.Equals(userID)).FirstOrDefaultAsync();
            var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();

#warning It's not safe!
            access.Warehouses.Clear();
            warehouse.AccessToWarehouse.Clear(); 
            return new Status(RequestStatus.OK, "success");
        }


        public async Task<Outcome<Status, bool>> CheckAccess(UserNameModel userName, WarehouseID warehouseID)
        {
            var action = await GetWarehouseUsers(warehouseID);
            if (action.status.StatusCode == RequestStatus.OK)
            {
                var userID = await GetID(userName);

                return new Outcome<Status, bool>(new Status(RequestStatus.OK, "success"),
                    action.data.Select(x => x.UserId.Equals(userID)).FirstOrDefault());
            }
            else
            {
                return new Outcome<Status, bool>(action.status, false);
            }        
        }
    }
}
