using Common.Utils.Helper;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Invoice
{
    public class AddInvoiceDto : InvoiceDto
    {
        public AddInvoiceDto()
        {
            InvoiceDetails = new List<InvoiceDetailDto>();
        }

        [Required(ErrorMessage = "Es necesario al menos un producto."), MinLength(1)]
        public List<InvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
