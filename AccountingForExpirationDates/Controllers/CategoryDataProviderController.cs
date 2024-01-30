using AccountingForExpirationDates.HelperClasses;
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

            var action = await _providerService.AddCategory(category);

            if (action.StatusCode == 1) 
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCategory(RemoveCategoryModel category)
        {
            var action = await _providerService.RemoveCategory(category);

            if (action.StatusCode == 1)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }

        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto[]>> GetAllCategory()
        {
            var action = await _providerService.GetAllCategory();

            if (action.status.StatusCode == 1)
            {
                return Ok(action.status.Description);
            }
            else
            {
                return BadRequest(action.status.Description);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SetCategory(ProductCategoryModel productCategoryModel)
        {
            var action = await _providerService.SetCategory(productCategoryModel);

            if (action.StatusCode == 1)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProductModelDto[]>> GetAllProductFromCategory(GetAllProductFromCategoryModel categoryModel)
        {
            var action = await _providerService.GetAllProductFromCategory(categoryModel);

            if (action.status.StatusCode == 1)
            {
                return Ok(action.status.Description);
            }
            else
            {
                return BadRequest(action.status.Description);
            }
        }
    }
}
