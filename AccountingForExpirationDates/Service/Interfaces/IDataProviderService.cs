using AccountingForExpirationDates.Data.Entitys;
using System.Net.Http.Headers;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IDataProviderService
    {
        Task AddProduct(ProductEntity product);
        Task DeleteProduct(int barcode);
    }
}
