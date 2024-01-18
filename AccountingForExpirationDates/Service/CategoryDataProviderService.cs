using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class CategoryDataProviderService : ICategoryDataProviderService
    {
        public ApplicationDbContext _db;

        public CategoryDataProviderService(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<CategoryDto[]> GetAllCategory()
        {
            AllCategoryModel AllCategory = new AllCategoryModel();

            foreach (var category in await _db.Category.ToArrayAsync())
            {
                CategoryDto categoryDto = new CategoryDto();
                categoryDto.Id = category.Id;
                categoryDto.Name = category.Name;

                AllCategory.Categories.Add(categoryDto);
            }

            return AllCategory.Categories.ToArray();
        }


        public async Task AddCategory(AddCategoryModel categoryModel)
        {
            var category = await _db.Category.Where(x => x.Name.Equals(categoryModel.categoryName)).FirstOrDefaultAsync();
            if (category == null)
            {
                CategoryEntity categoryEntity = new CategoryEntity();
                categoryEntity.Name = categoryModel.categoryName;
                await _db.Category.AddAsync(categoryEntity);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"this category already exists. " +
                    $"[ category name: {categoryModel.categoryName} ]");
            }
        }


        public async Task RemoveCategory(RemoveCategoryModel categoryModel)
        {

            var category = await _db.Category.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == categoryModel.CategoryId);

            if (category != null)
            {
                category.Product.Clear();
                _db.Category.Remove(category);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"The category was not found. " +
                    $"[ categoryID: {categoryModel.CategoryId} ]");
            }
        }


        public async Task SetCategory(ProductCategoryModel categoryModelDto)
        {
            var product = await _db.Products.Where(x => x.Id == categoryModelDto.productId).FirstAsync();
            var category = await _db.Category.Where(x => x.Id == categoryModelDto.categoryId).FirstAsync();

            product.Category = category;
            category.Product.Add(product);

            await _db.SaveChangesAsync();
        }


        public async Task<ProductModelDto[]> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel)
        {
            List<ProductModelDto> products = new List<ProductModelDto>();

            var category = await _db.Category.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == categoryModel.Id);
            if (category != null)
            {
                if (category.Product != null && category.Product.Count != 0)
                {
                    foreach (var item in category.Product)
                    {
                        ProductModelDto productModel = new ProductModelDto();
                        productModel.Id = item.Id;
                        productModel.BarcodeType1 = item.BarcodeType1;
                        productModel.BarcodeType2 = item.BarcodeType2;
                        productModel.Name = item.Name;
                        if (item.Category != null)
                        {
                            productModel.categoryName = item.Category.Name;
                        }
                        productModel.categoryId = item.CategoryId;
                        products.Add(productModel);
                    }

                    return products.ToArray();

                }
                else
                {
                    throw new Exception($"the category is empty. " +
                                        $"[ categoryID: {categoryModel.Id} ]");
                }
            }
            else
            {
                throw new Exception($"The category was not found. " +
                                    $"[ categoryID: {categoryModel.Id} ]");
            }
        }
    }
}
