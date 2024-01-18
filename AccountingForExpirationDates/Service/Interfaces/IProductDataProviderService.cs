using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.Model.Product;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IProductDataProviderService
    {
        Task AddProduct(ProductModelDto productModelDto);
        Task EditSellByProduct(EditSellByModel editSellByModel);
        Task DeleteProduct(DeleteProductModel deleteProductModel);
        
        Task<ProductModelDto[]> GetAllProduct();
    }
}
