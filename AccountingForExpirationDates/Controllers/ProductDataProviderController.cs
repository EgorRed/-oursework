using AccountingForExpirationDates.DataBase.Entitys;
using AccountingForExpirationDates.HelperClasses;
using AccountingForExpirationDates.Model.Category;
using AccountingForExpirationDates.Model.Product;
using AccountingForExpirationDates.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AccountingForExpirationDates.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductDataProviderController : ControllerBase
    {
        private IProductDataProviderService _providerService;

        public ProductDataProviderController(IProductDataProviderService providerService) 
        {
            _providerService = providerService;
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModelDto productModelDto)
        {
            var action = await _providerService.AddProduct(productModelDto);
            if (action.StatusCode == RequestStatus.OK) 
            {
                await _providerService.AddProduct(productModelDto);
                return Ok(action.Description);
            }
            else
            {
                return BadRequest(action.Description);
            }

        }


        [HttpPost]
        public async Task<ActionResult<ProductModelDto[]>> GetAllProduct()
        {
            var action = await _providerService.GetAllProduct(); 
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
        public async Task<IActionResult> RemoveProduct(DeleteProductModel deleteProductModelDto)
        {
            var action = await _providerService.DeleteProduct(deleteProductModelDto);

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
        public async Task<IActionResult> EditSellBy(EditSellByModel editSellByModelDto)
        {
            var action = await _providerService.EditSellByProduct(editSellByModelDto);

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
