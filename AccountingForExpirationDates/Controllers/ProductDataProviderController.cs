﻿using AccountingForExpirationDates.DataBase.Entitys;
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
    public class ProductDataProviderController : ControllerBase
    {
        private IProductDataProviderService _providerService;

        public ProductDataProviderController(IProductDataProviderService providerService) 
        {
            _providerService = providerService;
        }



        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductModel productModelDto, [FromQuery]  WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.AddProduct(productModelDto, warehouseID, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<ActionResult<ProductDto[]>> GetAllProduct([FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.GetAllProduct(warehouseID, userName); 
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
        public async Task<IActionResult> RemoveProduct([FromBody] DeleteProductModel deleteProductModelDto, [FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.DeleteProduct(deleteProductModelDto, warehouseID, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }


        [HttpPost]
        public async Task<IActionResult> EditSellBy([FromBody] EditSellByModel editSellByModelDto, [FromQuery] WarehouseID warehouseID)
        {
            UserNameModel userName = new UserNameModel();
            userName.Name = User.Identity?.Name;

            var action = await _providerService.EditSellByProduct(editSellByModelDto, warehouseID, userName);
            return Ok(new { Status = action.StatusCode.ToString(), Message = action.Description });
        }
    }
}
