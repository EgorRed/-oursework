using AccountingForExpirationDates.Data.Entitys;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingForExpirationDates.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DataProviderController : ControllerBase
    {
        private IDataProviderService _providerService;

        public DataProviderController(IDataProviderService providerService) 
        {
            _providerService = providerService;
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModelDto productModelDto)
        {
            try
            {
                await _providerService.AddProduct(productModelDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<ProductModelDto[]> GetAllProduct()
        {
            return await _providerService.GetAllProduct();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveProduct(DeleteProductModel deleteProductModelDto)
        {
            try
            {
                await _providerService.DeleteProduct(deleteProductModelDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> EditSellBy(EditSellByModel editSellByModelDto)
        {
            try
            {
                await _providerService.EditSellByProduct(editSellByModelDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryModel category)
        {
            await _providerService.AddCategory(category);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCategory(RemoveCategoryModel category)
        {
            try
            {
                await _providerService.RemoveCategory(category);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public async Task<CategoryDto[]> GetAllCategory()
        {
            return await _providerService.GetAllCategory();
        }


        [HttpPost]
        public async Task<IActionResult> SetCategory(ProductCategoryModel productCategoryModel)
        {
            try
            {
                await _providerService.SetCategory(productCategoryModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ProductModelDto[]> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel)
        {

            return await _providerService.GetAllProductFromCategory(categoryModel);
        }
    }
}
