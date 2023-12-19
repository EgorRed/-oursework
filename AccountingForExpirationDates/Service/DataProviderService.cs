using AccountingForExpirationDates.Data.Entitys;
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
            _db.SaveChanges();
        }

        public async Task<ProductEntity[]> GetAllProduct()
        {
            //AllProductModel allProductModel = new AllProductModel();
            //ProductEntity[] productEntities = 
            //allProductModel.Products = productEntities;

            return await _db.Products.ToArrayAsync();
        }

        public Task DeleteProduct(DeleteProductModel deleteProductModel)
        {
            throw new NotImplementedException();
        }

        public Task EditSellByProduct(EditSellByModel editSellByModel)
        {
            throw new NotImplementedException();
        }

        public Task SetCategory(ProductCategoryModel categoryModelDto)
        {
            throw new NotImplementedException();
        }


    }
}
