using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service
{
    public class CategoryDataProviderService : ICategoryDataProviderService
    {
        public ApplicationDbContext _db;
        public AccessToWarehouse _access;

        public CategoryDataProviderService(ApplicationDbContext db)
        {
            _db = db;
            _access = new AccessToWarehouse(db);
        }


        public async Task<Outcome<Status, CategoryDto[]>> GetAllCategory(WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
            {

                var Warehouse = await _db.Warehouses.Include(x => x.Category).FirstOrDefaultAsync(w => w.Id == warehouseID.WarehouseIndex);
                if (Warehouse != null)
                {
                    AllCategoryModel AllCategory = new AllCategoryModel();

                    foreach (var category in Warehouse.Category)
                    {
                        CategoryDto categoryDto = new CategoryDto();
                        categoryDto.Id = category.Id;
                        categoryDto.Name = category.Name;

                        AllCategory.Categories.Add(categoryDto);
                    }

                    return new Outcome<Status, CategoryDto[]>(new Status(RequestStatus.OK, "success"), AllCategory.Categories.ToArray());
                }
                else
                {
                    return new Outcome<Status, CategoryDto[]>(new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found. " +
                        $"WarehouseID: {warehouseID.WarehouseIndex}"), new CategoryDto[0]);
                }
            }
            else
            {
                return new Outcome<Status, CategoryDto[]>
                    (
                        new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}"),
                        new CategoryDto[0]
                    );
            }
        }


        public async Task<Status> AddCategory(AddCategoryModel categoryModel, WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
            {

                var Warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
                if (Warehouse != null)
                {

                    var category = await _db.Category.Where(x => x.Name.Equals(categoryModel.categoryName)).FirstOrDefaultAsync();
                    if (category == null)
                    {
                        CategoryEntity categoryEntity = new CategoryEntity();
                        categoryEntity.Name = categoryModel.categoryName;
                        categoryEntity.WarehouseId = Warehouse.Id;
                        categoryEntity.Warehouse = Warehouse;
                        Warehouse.Category.Add(categoryEntity);
                        await _db.Category.AddAsync(categoryEntity);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return new Status(RequestStatus.DataRepetition, $"this category already exists. " +
                            $"[ category name: {categoryModel.categoryName} ]");
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


        public async Task<Status> RemoveCategory(RemoveCategoryModel categoryModel, WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
            {

                var Warehouse = await _db.Warehouses.Include(c => c.Category)
                                                .Include(p => p.Product)
                                                .FirstOrDefaultAsync(x => x.Id == warehouseID.WarehouseIndex);
                if (Warehouse != null)
                {
                    var category = Warehouse.Category.Where(x => x.Id == categoryModel.CategoryId).FirstOrDefault();
                    if (category != null)
                    {

                        category.Product.Clear();
                        var res = Warehouse.Category.Remove(category);
                        _db.Category.Remove(category);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return new Status(RequestStatus.DataIsNotFound, $"The category was not found. " +
                            $"[ categoryID: {categoryModel.CategoryId} ]");
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


        public async Task<Status> SetCategory(ProductCategoryModel categoryModelDto, WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
            {

                var Warehouse = await _db.Warehouses.Include(c => c.Category)
                                                .Include(p => p.Product)
                                                .FirstOrDefaultAsync(x => x.Id == warehouseID.WarehouseIndex);
                if (Warehouse != null)
                {
                    var product = Warehouse.Product.Where(x => x.Id == categoryModelDto.productId).FirstOrDefault();
                    if (product == null)
                    {
                        return new Status(RequestStatus.DataIsNotFound, $"The product was not found" +
                            $"[ productID: {categoryModelDto.productId} ]");
                    }
                    var category = Warehouse.Category.Where(x => x.Id == categoryModelDto.categoryId).FirstOrDefault();
                    if (category == null)
                    {
                        return new Status(RequestStatus.DataIsNotFound, $"The category was not found" +
                            $"[ categoryID: {categoryModelDto.categoryId} ]");
                    }

                    product.Category = category;
                    category.Product.Add(product);
                    await _db.SaveChangesAsync();

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


        public async Task<Outcome<Status, ProductDto[]>> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel, WarehouseID warehouseID, UserNameModel userName)
        {
            var CAccess = await _access.CheckAccess(userName, warehouseID);
            if (CAccess.data && CAccess.status.StatusCode == RequestStatus.OK)
            {

                var warehouse = await _db.Warehouses.Where(x => x.Id == warehouseID.WarehouseIndex).FirstOrDefaultAsync();
                if (warehouse != null)
                {
                    List<ProductDto> products = new List<ProductDto>();

                    var category = await _db.Category.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == categoryModel.Id);
                    if (category != null)
                    {
                        if (category.Product != null && category.Product.Count != 0)
                        {
                            foreach (var item in category.Product)
                            {
                                ProductDto productModel = new ProductDto()
                                {
                                    Id = item.Id,
                                    WarehouseId = warehouseID.WarehouseIndex,
                                    WarehouseName = item.Warehouse?.Name,
                                    BarcodeType1 = item.BarcodeType1,
                                    BarcodeType2 = item.BarcodeType2,
                                    Name = item.Name,
                                    SellBy = item.SellBy,
                                    categoryId = item.CategoryId,
                                    categoryName = item.Category?.Name
                                };
                                products.Add(productModel);
                            }

                            return new Outcome<Status, ProductDto[]>
                                (
                                    new Status(RequestStatus.OK, "success"), 
                                    products.ToArray()
                                );

                        }
                        else
                        {
                            return new Outcome<Status, ProductDto[]>
                                (
                                    new Status(RequestStatus.DataIsNull, $"the category is empty. [ categoryID: {categoryModel.Id} ]"),
                                    products.ToArray()
                                );
                        }
                    }
                    else
                    {
                        return new Outcome<Status, ProductDto[]>
                            (
                                new Status(RequestStatus.DataIsNotFound, $"The category was not found. [ categoryID: {categoryModel.Id} ]"),
                                products.ToArray()
                            );
                    }
                }
                else
                {
                    return new Outcome<Status, ProductDto[]>
                        (
                            new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found. WarehouseID: {categoryModel.Id}"), 
                            new ProductDto[0]
                        );
                }
            }
            else
            {
                return new Outcome<Status, ProductDto[]>
                    (
                        new Status(RequestStatus.AccessIsDenied, $"Access is denied. Access verification status: {CAccess.status.StatusCode}"),
                        new ProductDto[0]
                    );
            }
        }


    }
}
