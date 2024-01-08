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
            var code1 = await _db.Products.Where(x => x.BarcodeType1 == productModelDto.BarcodeType1).FirstOrDefaultAsync();
            var code2 = await _db.Products.Where(x => x.BarcodeType2 == productModelDto.BarcodeType2).FirstOrDefaultAsync();

            if (code1 != null && code2 != null)
            {
                ProductEntity productEntity = new ProductEntity();
                productEntity.BarcodeType1 = productModelDto.BarcodeType1;
                productEntity.BarcodeType2 = productModelDto.BarcodeType2;
                productEntity.Name = productModelDto.Name;
                productEntity.SellBy = productModelDto.SellBy;
                await _db.Products.AddAsync(productEntity);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"such a product already exists. " +
                    $"[ {productModelDto.BarcodeType1} " +
                    $"{productModelDto.BarcodeType2} " +
                    $"{productModelDto.Name} ]");
            }
        }


        public async Task<ProductModelDto[]> GetAllProduct()
        {
            AllProductModel allProduct = new AllProductModel();
            foreach (var product in await _db.Products.ToArrayAsync()) 
            {
                ProductModelDto productModelDto = new ProductModelDto();
                productModelDto.BarcodeType1 = product.BarcodeType1;
                productModelDto.BarcodeType2 = product.BarcodeType2;
                productModelDto.Name = product.Name;
                productModelDto.SellBy = product.SellBy;
                productModelDto.categoryId = product.CategoryId;
                if (product.Category != null)
                {
                    productModelDto.categoryName = product.Category.Name;
                }
                allProduct.Products.Add(productModelDto);
            }

            return allProduct.Products.ToArray();
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
            var category = _db.Category.Where(x => x.Id == categoryModel.CategoryId).First();
            category.Product.Clear();
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
        }


        public async Task SetCategory(ProductCategoryModel categoryModelDto)
        {
            //var product = await _db.Products.Where(x => x.Id == categoryModelDto.productId).FirstAsync();
            //var category = await _db.Category.Where(x => x.Id == categoryModelDto.categoryId).FirstAsync();

            //category.Product.Add(product);
            //await _db.SaveChangesAsync();
            throw new NotImplementedException();
        }
    }
}
