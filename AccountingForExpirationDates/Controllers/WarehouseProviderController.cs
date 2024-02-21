using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Model.Warehouse;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var action = await _providerService.CreateWarehouse(WarehouseModel);
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
            var action = await _providerService.GetAllWarehouses();
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
            var action = await _providerService.RemoveWarehouse(WarehouseModel);
            if(action.StatusCode == RequestStatus.OK)
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
            var action = await _providerService.UpdateWarehouseDescription(WarehouseModel);
            if (action.StatusCode == RequestStatus.OK)
            {
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }
        }

        //[HttpPost]
        //public async Task<ActionResult<ProductDto[]>> GetAllProductsFromWarehouse(GetAllProductsFromWarehouseModel WarehouseModel)
        //{
        //    var action = await _providerService.GetAllProductsFromWarehouse(WarehouseModel);
        //    if (action.status.StatusCode == RequestStatus.OK)
        //    {
        //        return action.data;
        //    }
        //    else
        //    {
        //        return BadRequest(action.status.Description);
        //    }
        //}
    }
}
