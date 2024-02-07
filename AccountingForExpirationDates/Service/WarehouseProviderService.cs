﻿using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Product;
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
            if (WarehouseModel != null) 
            {
                warehouse.Name = WarehouseModel.Name;
                warehouse.Description = WarehouseModel.Description;
                await _db.Warehouses.AddAsync(warehouse);
                await _db.SaveChangesAsync();
                return new Status(RequestStatus.OK, "success");
            }
            return new Status(RequestStatus.DataIsNull, "in WarehouseModel empty value");

        }


        public async Task<Outcome<Status, WarehouseDto[]>> GetAllWarehouses()
        {
            var warehouses = await _db.Warehouses.ToArrayAsync();
            var warehouseDtoList = new List<WarehouseDto>();
            foreach (var item in warehouses) 
            {
                WarehouseDto warehouseDto = new WarehouseDto();
                warehouseDto.Id = item.Id;
                warehouseDto.Name = item.Name;
                warehouseDto.Description = item.Description;
                warehouseDtoList.Add(warehouseDto);
            }

            return new Outcome<Status, WarehouseDto[]>(new Status(RequestStatus.OK, "success"), warehouseDtoList.ToArray());
        }


        public async Task<Status> RemoveWarehouse(RemoveWarehouseModel WarehouseModel)
        {
            var warehouse = await _db.Warehouses.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == WarehouseModel.Id);
            if (warehouse != null)
            {
                
                warehouse.Product.Clear();
                _db.Warehouses.Remove(warehouse);
                await _db.SaveChangesAsync();
                return new Status(RequestStatus.OK, "success");
            }
            else
            {
                return new Status(RequestStatus.DataIsNotFound, $"There is no such warehouse." +
                    $"[WarehouseID: {WarehouseModel.Id}]");
            }
            
        }


        public async Task<Status> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel)
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


        //public async Task<Outcome<Status, ProductDto[]>> GetAllProductsFromWarehouse(GetAllProductsFromWarehouseModel WarehouseModel)
        //{
        //    var warehouse = await _db.Warehouses.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == WarehouseModel.Id);
        //    List<ProductDto> products = new List<ProductDto>();
        //    if (warehouse != null)
        //    {
                
        //        foreach (var product in warehouse.Product)
        //        {
        //            ProductDto productEntity = new ProductDto();
        //            productEntity.Id = product.Id;
        //            productEntity.WarehouseID = warehouse.Id;
        //            productEntity.WarehouseName = warehouse.Name;
        //            productEntity.BarcodeType1 = product.BarcodeType1;
        //            productEntity.BarcodeType2 = product.BarcodeType2;
        //            productEntity.Name = product.Name;
        //            productEntity.SellBy = product.SellBy;
        //            productEntity.categoryName = product.Category?.Name;                   

        //            products.Add(productEntity);
        //        }

        //        return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.OK, "success"), products.ToArray());

        //    }
        //    else
        //    {

        //        return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.DataIsNotFound, $"There is no such warehouse." +
        //            $"[WarehouseID: {WarehouseModel.Id}]"), products.ToArray());

        //    }
        //}
    }
}
