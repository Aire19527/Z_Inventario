using Common.Utils.Resources;
using Domain.DTO;
using Domain.DTO.Produdct;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Handlers;

namespace Web.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class ProductController : Controller
    {
        #region Attributes
        private readonly IProductServices _productServices;
        #endregion

        #region builder
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        #endregion


        #region Views
        public IActionResult Index()
        {
            return View(new ProductDto());
        }
        #endregion


        #region services
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            List<ConsultProductDto> list = _productServices.GetAllProduts();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct(ProductDto product)
        {
            bool result = await _productServices.Insert(product);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemInserted : GeneralMessages.ItemNoInserted
            };

            IActionResult action;
            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ConsultProductDto product)
        {
            bool result = await _productServices.Update(product);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemUpdate : GeneralMessages.ItemNoUpdate
            };

            IActionResult action;
            if (result)
                action = Ok(response);
            else
                action = BadRequest(response);

            return action;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool result = await _productServices.Delete(id);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = result,
                Result = result,
                Message = result ? GeneralMessages.ItemDelete : GeneralMessages.ItemNoDelete
            };

            return Ok(response);
        }
        #endregion
    }
}
