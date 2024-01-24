using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Product;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IProductDataProviderService
    {
        Task<Status> AddProduct(ProductModelDto productModelDto);
        Task<Status> EditSellByProduct(EditSellByModel editSellByModel);
        Task<Status> DeleteProduct(DeleteProductModel deleteProductModel);
        
        Task<Pair<Status, ProductModelDto[]>> GetAllProduct();
    }
}
