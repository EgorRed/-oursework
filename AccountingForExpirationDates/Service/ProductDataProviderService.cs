﻿using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class ProductDataProviderService : IProductDataProviderService
    {

        public ApplicationDbContext _db;

        public ProductDataProviderService(ApplicationDbContext db) 
        {
            _db = db;
        }


        public async Task<Status> AddProduct(AddProductModel productModelDto, WarehouseID warehouseID)
        {
            var Warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
            if (Warehouse != null)
            {
                var code1 = await _db.Products.Where(x => x.BarcodeType1 == productModelDto.BarcodeType1).FirstOrDefaultAsync();
                var code2 = await _db.Products.Where(x => x.BarcodeType2 == productModelDto.BarcodeType2).FirstOrDefaultAsync();

                if (code1 == null && code2 == null)
                {
                    ProductEntity productEntity = new ProductEntity();
                    productEntity.BarcodeType1 = productModelDto.BarcodeType1;
                    productEntity.BarcodeType2 = productModelDto.BarcodeType2;
                    productEntity.Name = productModelDto.Name;
                    productEntity.SellBy = productModelDto.SellBy;
                    productEntity.WarehouseId = Warehouse.Id;
                    productEntity.Warehouse = Warehouse;

                    Warehouse.Product.Add(productEntity);
                    await _db.Products.AddAsync(productEntity);

                    await _db.SaveChangesAsync();
                }
                else
                {
                    return new Status(RequestStatus.DataRepetition, $"such a product already exists. " +
                        $"[ {productModelDto.BarcodeType1} " +
                        $"{productModelDto.BarcodeType2} " +
                        $"{productModelDto.Name} ]");

                }
                return new Status(RequestStatus.OK, "success");
            }
            else
            {
                return new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found. " +
                    $"WarehouseID: {warehouseID.WarehouseIndex}");
            }
        }


        public async Task<Outcome<Status, ProductDto[]>> GetAllProduct(WarehouseID warehouseID)
        {
            var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();

            
            if (warehouse != null)
            {
                List<ProductDto> products = new List<ProductDto>();

                var productsInWarehouse = await _db.Products.Include(w => w.Warehouse)
                                                            .Include(c => c.Category)
                                                            .Where(x => x.WarehouseId == warehouseID.WarehouseIndex)
                                                            .ToArrayAsync();

                foreach (var product in productsInWarehouse)
                {
                    ProductDto productEntity = new ProductDto();
                    productEntity.Id = product.Id;
                    productEntity.WarehouseId = (int)product.WarehouseId;
                    productEntity.WarehouseName = product.Warehouse?.Name;
                    productEntity.BarcodeType1 = product.BarcodeType1;
                    productEntity.BarcodeType2 = product.BarcodeType2;
                    productEntity.Name = product.Name;
                    productEntity.SellBy = product.SellBy;
                    productEntity.categoryName = product.Category?.Name;
                    productEntity.categoryId = product.Category?.Id;

                    products.Add(productEntity);
                }

                return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.OK, "success"), products.ToArray());
            }
            else
            {
                return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found." +
                    $"WarehouseID: {warehouseID.WarehouseIndex}"), new ProductDto[0]);
            }

        }


        public async Task<Status> DeleteProduct(DeleteProductModel deleteProductModel, WarehouseID warehouseID)
        {

            var Warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
            if (Warehouse != null)
            {

                var product = await _db.Products.Where(x => x.Id == deleteProductModel.Id).FirstOrDefaultAsync();
                if (product != null)
                {
                    _db.Products.Remove(product);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    return new Status(RequestStatus.DataIsNotFound, "$The product was not found. " +
                        $"[ productID: {deleteProductModel.Id} ]");
                }
                return new Status(RequestStatus.OK, "success");
            }
            else
            {
                return new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found." +
                    $"WarehouseID: {warehouseID.WarehouseIndex}");
            }
        }

        public async Task<Status> EditSellByProduct(EditSellByModel editSellByModel, WarehouseID warehouseID)
        {

            var Warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
            if (Warehouse != null)
            {
                var Product = await _db.Products.Where(x => x.Id == editSellByModel.Id).FirstOrDefaultAsync();
                if (Product != null)
                {
                    Product.SellBy = editSellByModel.SellBy;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    return new Status(RequestStatus.DataIsNotFound, "$The product was not found. " +
                        $"[ productID: {editSellByModel.Id} ]");
                }
                return new Status(RequestStatus.OK, "success");
            }
            else
            {
                return new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found." +
                    $"WarehouseID: {warehouseID.WarehouseIndex}");
            }

        }

    }
}
