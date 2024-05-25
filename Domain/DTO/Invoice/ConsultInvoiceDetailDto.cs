using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Invoice
{
    public class ConsultInvoiceDetailDto: InvoiceDetailDto
    {
        public int IdInvoiceDetail { get; set; }
        public string ProductName { get; set; }
        public int Code { get; set; }
    }
}
