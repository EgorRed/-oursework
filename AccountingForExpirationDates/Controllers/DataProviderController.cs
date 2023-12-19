using AccountingForExpirationDates.Data.Entitys;
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
            await _providerService.AddProduct(productModelDto);
            return Ok();
        }

        [HttpPost]
        public async Task<ProductEntity[]> GetAllProduct()
        {
            return await _providerService.GetAllProduct();
        }
    }
}
