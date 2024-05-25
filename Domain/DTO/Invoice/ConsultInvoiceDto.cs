using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Invoice
{
    public class ConsultInvoiceDto:InvoiceDto
    {
        public ConsultInvoiceDto()
        {
            InvoiceDetails = new List<ConsultInvoiceDetailDto>();
        }
        public DateTime CreationDate { get; set; }
        public string StrCreationDate { get; set; }
        public int IdInvoice { get; set; }
        public string FullName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Iva { get; set; }
        public decimal TotalInvoice { get; set; }

        public List<ConsultInvoiceDetailDto> InvoiceDetails { get; set; }
    }
}
