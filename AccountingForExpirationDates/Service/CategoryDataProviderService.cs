using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
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


        public async Task<Outcome<Status, CategoryDto[]>> GetAllCategory(WarehouseID warehouseID)
        {
            AllCategoryModel AllCategory = new AllCategoryModel();

            foreach (var category in await _db.Category.ToArrayAsync())
            {
                CategoryDto categoryDto = new CategoryDto();
                categoryDto.Id = category.Id;
                categoryDto.Name = category.Name;

                AllCategory.Categories.Add(categoryDto);
            }

            return new Outcome<Status, CategoryDto[]>(new Status(RequestStatus.OK, "success"), AllCategory.Categories.ToArray());
        }


        public async Task<Status> AddCategory(AddCategoryModel categoryModel, WarehouseID warehouseID)
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
                return new Status(RequestStatus.DataRepetition, $"this category already exists. " +
                    $"[ category name: {categoryModel.categoryName} ]");
            }
            return new Status(RequestStatus.OK, "success");
        }


        public async Task<Status> RemoveCategory(RemoveCategoryModel categoryModel, WarehouseID warehouseID)
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
                return new Status(RequestStatus.DataIsNotFound, $"The category was not found. " +
                    $"[ categoryID: {categoryModel.CategoryId} ]");
            }
            return new Status(RequestStatus.OK, "success");
        }


        public async Task<Status> SetCategory(ProductCategoryModel categoryModelDto, WarehouseID warehouseID)
        {
            var product = await _db.Products.Where(x => x.Id == categoryModelDto.productId).FirstOrDefaultAsync();
            if (product == null)
            {
                return new Status(RequestStatus.DataIsNotFound, $"The product was not found" +
                    $"[ productID: {categoryModelDto.productId} ]");
            }
            var category = await _db.Category.Where(x => x.Id == categoryModelDto.categoryId).FirstOrDefaultAsync();
            if(category == null)
            {
                return new Status(RequestStatus.DataIsNotFound, $"The category was not found" +
                    $"[ categoryID: {categoryModelDto.categoryId} ]");
            }

            product.Category = category;
            category.Product.Add(product);
            await _db.SaveChangesAsync();

            return new Status(RequestStatus.OK, "success");
        }


        public async Task<Outcome<Status, ProductDto[]>> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel, WarehouseID warehouseID)
        {
            var warehouse = await _db.Warehouses.Where(x => x.Id == categoryModel.Id).FirstOrDefaultAsync();
            if (warehouse != null)
            {


                List<ProductDto> products = new List<ProductDto>();

                var category = await _db.Category.Include(p => p.Product).FirstOrDefaultAsync(x => x.Id == categoryModel.Id);
                if (category != null)
                {
                    if (category.Product != null && category.Product.Count != 0)
                    {
                        foreach (var item in category.Product)
                        {
                            ProductDto productModel = new ProductDto();
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

                        return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.OK, "success"), products.ToArray());

                    }
                    else
                    {
                        return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.DataIsNull, $"the category is empty. " +
                                           $"[ categoryID: {categoryModel.Id} ]"),
                                           products.ToArray());
                    }
                }
                else
                {
                    return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.DataIsNotFound, $"The category was not found. " +
                                        $"[ categoryID: {categoryModel.Id} ]"),
                                        products.ToArray());
                }
            }
            else
            {
                return new Outcome<Status, ProductDto[]>(new Status(RequestStatus.DataIsNotFound, $"The warehouse was not found." +
                    $"WarehouseID: {categoryModel.Id}"), new ProductDto[0]);
            }
        }
    }
}
