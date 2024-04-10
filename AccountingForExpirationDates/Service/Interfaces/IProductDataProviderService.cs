using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IProductDataProviderService
    {
        Task<Status> AddProduct(AddProductModel productModelDto , WarehouseID warehouseID, UserNameModel userName);
        Task<Status> EditSellByProduct(EditSellByModel editSellByModel , WarehouseID warehouseID, UserNameModel userName);
        Task<Status> DeleteProduct(DeleteProductModel deleteProductModel , WarehouseID warehouseID, UserNameModel userName);
        
        Task<Outcome<Status, ProductDto[]>> GetAllProduct(WarehouseID warehouseID, UserNameModel userName);
    }
}
