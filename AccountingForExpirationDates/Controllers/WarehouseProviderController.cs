using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Auth;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace AccountingForExpirationDates.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WarehouseProviderController : Controller
    {
        IWarehouseProviderService _providerService;
        

        public WarehouseProviderController(IWarehouseProviderService providerService)
        {
            _providerService = providerService;
            
        }



        [HttpPost]
        public async Task<IActionResult> CreateWarehouse(CreateWarehouseModel WarehouseModel)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.CreateWarehouse(WarehouseModel, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }



        [HttpPost]
        public async Task<ActionResult<WarehouseDto[]>> GetAllWarehouses()
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.GetAllWarehouses(userName);
            return Ok(new { Status = action.status.StatusCode.ToString(), Message = action.status.Description, Data = action.data });
        }


    
        [HttpPost]
        public async Task<IActionResult> RemoveWarehouse(RemoveWarehouseModel WarehouseModel)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var actions = new List<Status>();

            var action = await _providerService.RemoveWarehouse(WarehouseModel, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description }); 
        }

      

        [HttpPost]
        public async Task<IActionResult> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.UpdateWarehouseDescription(WarehouseModel, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }
    }
}
