using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AccountingForExpirationDates.Controllers
{
    [Authorize]
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
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.AddCategory(category, warehouseID, userName);

            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCategory([FromBody] RemoveCategoryModel category, [FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.RemoveCategory(category, warehouseID, userName);

            return StatusCode(StatusCodes.Status200OK, new Response { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<ActionResult<CategoryDto[]>> GetAllCategory([FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.GetAllCategory(warehouseID, userName);

            if (action.status.StatusCode == RequestStatus.OK)
            {
                return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description, Data = action.data });
            }
            else
            {
                return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description });
            }
        }


        [HttpPost]
        public async Task<IActionResult> SetCategory([FromBody] ProductCategoryModel productCategoryModel, [FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.SetCategory(productCategoryModel, warehouseID, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto[]>> GetAllProductFromCategory([FromBody] GetAllProductFromCategoryModel categoryModel, [FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.GetAllProductFromCategory(categoryModel, warehouseID, userName);

            if (action.status.StatusCode == RequestStatus.OK)
            {
                return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description, Data =  action.data});
            }
            else
            {
                return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description });
            }
        }
    }
}
