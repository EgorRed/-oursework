using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface ICategoryDataProviderService
    {
        Task AddCategory(AddCategoryModel categoryModel);
        Task RemoveCategory(RemoveCategoryModel categoryModel);
        Task<CategoryDto[]> GetAllCategory();
        Task<ProductModelDto[]> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel);
        Task SetCategory(ProductCategoryModel categoryModelDto);
    }
}
