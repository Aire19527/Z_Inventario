using Common.Utils.Resources;
using Domain.DTO;
using Domain.DTO.Invoice;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Handlers;

namespace Web.Controllers
{
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class InvoiceController : Controller
    {
        #region Attributes
        private readonly IInvoiceServices _invoiceServices;
        private readonly IConfiguration _config;
        #endregion

        #region builder
        public InvoiceController(IInvoiceServices invoiceServices, IConfiguration configuration)
        {
            _invoiceServices = invoiceServices;
            _config = configuration;
        }
        #endregion

        #region Views
        // GET: InvoiceController
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Invoice()
        {
            string iva = _config.GetSection("ConfigInvoice").GetSection("Iva").Value;
            string condition = _config.GetSection("ConfigInvoice").GetSection("DiscountCondition").Value;
            string rate = _config.GetSection("ConfigInvoice").GetSection("DiscountRate").Value;
            ConfigInvoiceDto configDto = new ConfigInvoiceDto()
            {
                DiscountRate = Convert.ToInt32(rate),
                ConditionalDiscount = Convert.ToInt32(condition),
                Iva = Convert.ToInt32(iva),
            };

            return View(configDto);
        }
        #endregion

        #region Services

        [HttpGet]
        public IActionResult GetAllInvoice()
        {
            List<ConsultInvoiceDto> list = _invoiceServices.GetAllInvoice();
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = list,
                Message = string.Empty
            };

            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetInvoice(int idInvoice)
        {
            ConsultInvoiceDto consult = _invoiceServices.GetInvoice(idInvoice);
            ResponseDto response = new ResponseDto()
            {
                IsSuccess = true,
                Result = consult,
                Message = string.Empty
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> InserInvoice(AddInvoiceDto add)
        {
            ResponseDto response = await _invoiceServices.InsertInvoice(add);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice(int idInvoice)
        {
            bool result = await _invoiceServices.DeleteInvoice(idInvoice);
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
