using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface ICategoryDataProviderService
    {
        Task<Status> AddCategory(AddCategoryModel categoryModel);
        Task<Status> RemoveCategory(RemoveCategoryModel categoryModel);
        Task<Pair<Status, CategoryDto[]>> GetAllCategory();
        Task<Pair<Status, ProductModelDto[]>> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel);
        Task<Status> SetCategory(ProductCategoryModel categoryModelDto);
    }
}
