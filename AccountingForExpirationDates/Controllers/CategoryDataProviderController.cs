using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
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
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryModel category, [FromQuery] WarehouseID warehouseID)
        {

            var action = await _providerService.AddCategory(category, warehouseID);

            if (action.StatusCode == RequestStatus.OK) 
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCategory([FromBody] RemoveCategoryModel category, [FromQuery] WarehouseID warehouseID)
        {
            var action = await _providerService.RemoveCategory(category, warehouseID);

            if (action.StatusCode == RequestStatus.OK)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }

        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto[]>> GetAllCategory([FromBody] WarehouseID warehouseID)
        {
            var action = await _providerService.GetAllCategory(warehouseID);

            if (action.status.StatusCode == RequestStatus.OK)
            {
                return action.data;
            }
            else
            {
                return BadRequest(action.status.Description);
            }
        }


        [HttpPost]
        public async Task<IActionResult> SetCategory([FromBody] ProductCategoryModel productCategoryModel, [FromQuery] WarehouseID warehouseID)
        {
            var action = await _providerService.SetCategory(productCategoryModel, warehouseID);

            if (action.StatusCode == RequestStatus.OK)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProductDto[]>> GetAllProductFromCategory([FromBody] GetAllProductFromCategoryModel categoryModel, [FromQuery] WarehouseID warehouseID)
        {
            var action = await _providerService.GetAllProductFromCategory(categoryModel, warehouseID);

            if (action.status.StatusCode == RequestStatus.OK)
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
