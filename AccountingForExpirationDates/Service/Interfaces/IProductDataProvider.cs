using AccountingForExpirationDates.Data.Entitys;
using AccountingForExpirationDates.Model.Product;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IProductDataProvider
    {
        Task AddProduct(ProductModelDto productModelDto);
        Task EditSellByProduct(EditSellByModel editSellByModel);
        Task DeleteProduct(DeleteProductModel deleteProductModel);
        Task SetCategory(ProductCategoryModel categoryModelDto);
        Task<ProductModelDto[]> GetAllProduct();
    }
}
