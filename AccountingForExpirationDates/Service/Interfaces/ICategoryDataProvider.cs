using AccountingForExpirationDates.Data.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface ICategoryDataProvider
    {
        Task AddCategory(AddCategoryModel categoryModel);
        Task RemoveCategory(RemoveCategoryModel categoryModel);
        Task<CategoryDto[]> GetAllCategory();
        Task<ProductModelDto[]> GetAllProductFromCategory(CategoryDto category);
    }
}
