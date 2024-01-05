using AccountingForExpirationDates.Data.Entitys;
using AccountingForExpirationDates.Model.Category;

namespace AccountingForExpirationDates.Service.Interfaces
{
    public interface ICategoryDataProvider
    {
        Task AddCategory(AddCategoryModel categoryModel);
        Task RemoveCategory(RemoveCategoryModel categoryModel);
        Task<CategoryDto[]> GetAllCategory();
    }
}
