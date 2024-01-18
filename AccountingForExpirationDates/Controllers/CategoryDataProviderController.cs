using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingForExpirationDates.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryDataProviderController : ControllerBase
    {
        private ICategoryDataProviderService _providerService;

        public CategoryDataProviderController(ICategoryDataProviderService providerService)
        {
            _providerService = providerService;
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
