using AccountingForExpirationDates.HelperClasses;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface IMovingProductsService
    {
        //в процессе
        Task<Status> AddProductOnTheWarehouse();
        
    }
}
