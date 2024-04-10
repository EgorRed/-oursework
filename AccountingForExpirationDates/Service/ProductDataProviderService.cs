using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
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
        public AccessToWarehouse _access;

        public ProductDataProviderService(ApplicationDbContext db) 
        {
            _db = db;
            _access = new AccessToWarehouse(db);
        }



        public async Task<Status> AddProduct(AddProductModel productModelDto, WarehouseID warehouseID, UserNameModel userName)
        {

            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
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
            else
            {
                return new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}");
            }    
            

        }



        public async Task<Outcome<Status, ProductDto[]>> GetAllProduct(WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
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
                        ProductDto productEntity = new ProductDto()
                        {
                            Id = product.Id,
                            WarehouseId = warehouseID.WarehouseIndex,
                            WarehouseName = product.Warehouse?.Name,
                            BarcodeType1 = product.BarcodeType1,
                            BarcodeType2 = product.BarcodeType2,
                            Name = product.Name,
                            SellBy = product.SellBy,
                            categoryId = product.CategoryId,
                            categoryName = product.Category?.Name
                        };

                        products.Add(productEntity);
                    }

                    return 
                        new Outcome<Status, ProductDto[]>
                        (
                            new Status(RequestStatus.OK, "success"), 
                            products.ToArray()
                        );
                }
                else
                {
                    return 
                        new Outcome<Status, ProductDto[]>
                        (
                            new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found. \nWarehouseID: {warehouseID.WarehouseIndex}"), 
                            new ProductDto[0]
                        );
                }
            }
            else
            {
                return 
                    new Outcome<Status, ProductDto[]>
                    (
                        new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}"),
                        new ProductDto[0]
                    );
            }

        }



        public async Task<Status> DeleteProduct(DeleteProductModel deleteProductModel, WarehouseID warehouseID, UserNameModel userName)
        {

            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
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
            else
            {
                return new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}");
            }

        }



        public async Task<Status> EditSellByProduct(EditSellByModel editSellByModel, WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
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
            else
            {
                return new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}");
            }
        }
    }
}
