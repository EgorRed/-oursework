﻿using AccountingForExpirationDates.DataBase;
using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AccountingForExpirationDates.Service
{
    public class ProductDataProviderService : IProductDataProviderService
    {

        public ApplicationDbContext _db;

        public ProductDataProviderService(ApplicationDbContext db) 
        {
            _db = db;
        }


        public async Task AddProduct(ProductModelDto productModelDto)
        {
            var code1 = await _db.Products.Where(x => x.BarcodeType1 == productModelDto.BarcodeType1).FirstOrDefaultAsync();
            var code2 = await _db.Products.Where(x => x.BarcodeType2 == productModelDto.BarcodeType2).FirstOrDefaultAsync();

            if (code1 == null && code2 == null)
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
            foreach (var product in await _db.Products.Include(x => x.Category).ToArrayAsync()) 
            {
                ProductModelDto productModelDto = new ProductModelDto();
                productModelDto.Id = product.Id;
                productModelDto.BarcodeType1 = product.BarcodeType1;
                productModelDto.BarcodeType2 = product.BarcodeType2;
                productModelDto.Name = product.Name;
                productModelDto.SellBy = product.SellBy;
                productModelDto.categoryId = product.CategoryId;
                productModelDto.categoryName = product.Category?.Name;
                allProduct.Products.Add(productModelDto);
            }

            return allProduct.Products.ToArray();
        }


        public async Task DeleteProduct(DeleteProductModel deleteProductModel)
        {
            var product = await _db.Products.Where(x => x.Id == deleteProductModel.Id).FirstOrDefaultAsync();
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"The product was not found. " +
                    $"[ productID: {deleteProductModel.Id} ]");
            }
        }

        public async Task EditSellByProduct(EditSellByModel editSellByModel)
        {
             var Product = await _db.Products.Where(x => x.Id == editSellByModel.Id).FirstOrDefaultAsync();
            if (Product != null) 
            {
                Product.SellBy = editSellByModel.SellBy;
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"The product was not found. " +
                    $"[ productID: {editSellByModel.Id} ]");
            }

        }

    }
}
