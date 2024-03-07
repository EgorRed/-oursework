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
    //[Authorize]
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
            userName.Name = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;

            var action = await _providerService.CreateWarehouse(WarehouseModel, userName);
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
        public async Task<ActionResult<WarehouseDto[]>> GetAllWarehouses()
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;

            var action = await _providerService.GetAllWarehouses(userName);
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
        public async Task<IActionResult> RemoveWarehouse(RemoveWarehouseModel WarehouseModel)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;

            var actions = new List<Status>();

            var action = await _providerService.RemoveWarehouse(WarehouseModel, userName);
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
        public async Task<IActionResult> UpdateWarehouseDescription(UpdateWarehouseDescriptionModel WarehouseModel)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name)?.Value;

            var action = await _providerService.UpdateWarehouseDescription(WarehouseModel, userName);
            if (action.StatusCode == RequestStatus.OK)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }
        }
    }
}
