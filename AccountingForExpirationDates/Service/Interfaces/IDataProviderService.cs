using AccountingForExpirationDates.Model.Product;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IDataProviderService
    {
        Task AddProduct(ProductModel product);
        Task DeleteProduct(int barcode);
    }
}
