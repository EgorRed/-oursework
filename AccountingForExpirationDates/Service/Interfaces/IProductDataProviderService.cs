using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IProductDataProviderService
    {
        Task<Status> AddProduct(AddProductModel productModelDto , WarehouseID warehouseID);
        Task<Status> EditSellByProduct(EditSellByModel editSellByModel , WarehouseID warehouseID);
        Task<Status> DeleteProduct(DeleteProductModel deleteProductModel , WarehouseID warehouseID);
        
        Task<Outcome<Status, ProductDto[]>> GetAllProduct(WarehouseID warehouseID);
    }
}
