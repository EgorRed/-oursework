using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface ICategoryDataProviderService
    {
        Task<Status> AddCategory(AddCategoryModel categoryModel, WarehouseID warehouseID);
        Task<Status> RemoveCategory(RemoveCategoryModel categoryModel, WarehouseID warehouseID);
        Task<Outcome<Status, CategoryDto[]>> GetAllCategory(WarehouseID warehouseID);
        Task<Outcome<Status, ProductDto[]>> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel, WarehouseID warehouseID);
        Task<Status> SetCategory(ProductCategoryModel categoryModelDto, WarehouseID warehouseID);
    }
}
