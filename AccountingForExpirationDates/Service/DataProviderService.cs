using AccountingForExpirationDates.Data.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using AccountingForExpirationDates.UserData;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class DataProviderService : IDataProviderService
    {

        public ApplicationDbContext _db;

        public DataProviderService(ApplicationDbContext db) 
        {
            _db = db;
        }


        public async Task AddProduct(ProductModelDto productModelDto)
        {
            ProductEntity productEntity = new ProductEntity();
            productEntity.BarcodeType1 = productModelDto.BarcodeType1;
            productEntity.BarcodeType2 = productModelDto.BarcodeType2;
            productEntity.Name = productModelDto.Name;
            productEntity.SellBy = productModelDto.SellBy;
            productEntity.Category = productEntity.Category;
            await _db.Products.AddAsync(productEntity);
            await _db.SaveChangesAsync();
        }

        public async Task<ProductEntity[]> GetAllProduct()
        {
            return await _db.Products.ToArrayAsync();
        }

        public async Task DeleteProduct(DeleteProductModel deleteProductModel)
        {
            _db.Products.Remove(_db.Products.Where(x => x.Id == deleteProductModel.Id).First());
            await _db.SaveChangesAsync();
        }

        public async Task EditSellByProduct(EditSellByModel editSellByModel)
        {
             var Product = await _db.Products.Where(x => x.Id == editSellByModel.Id).FirstAsync();
            if (Product != null) 
            {
                Product.SellBy = editSellByModel.SellBy;
                await _db.SaveChangesAsync();
            }
        }

        //public async Task AddCategory(CategoryModel categoryModelDto)
        //{
        //    var categoryEntity = new CategoryEntity();
        //    categoryEntity.Name = categoryModelDto.categoryName;
        //    await _db.Category.AddAsync(categoryEntity);
        //    await _db.SaveChangesAsync();
        //}

        //public async Task SetCategory(ProductCategoryModel categoryModelDto)
        //{
        //    var categoryEntity = new CategoryEntity();

        //    var productEntity = await _db.Products.Where(x => x.Id == categoryModelDto.productId).FirstAsync();
        //    categoryEntity = await _db.Category.Where(x => x.Name == categoryModelDto.categoryName).FirstAsync();
        //    productEntity.CategoryId = categoryEntity.Id;
        //    await _db.SaveChangesAsync();
        //}

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
            CategoryEntity categoryEntity = new CategoryEntity();
            categoryEntity.Name = categoryModel.categoryName;
            await _db.Category.AddAsync(categoryEntity);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveCategory(RemoveCategoryModel categoryModel)
        {
            _db.Category.Remove(_db.Category.Where(x => x.Id == categoryModel.CategoryId).First());
            await _db.SaveChangesAsync();
        }

        public async Task SetCategory(ProductCategoryModel categoryModelDto)
        {
            var product = await _db.Products.Where(x => x.Id == categoryModelDto.productId).FirstAsync();
            var category = await _db.Category.Where(x => x.Id == categoryModelDto.categoryId).FirstAsync();

            category.Product.Add(product);
            await _db.SaveChangesAsync();
        }
    }
}
