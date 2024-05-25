using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Models
{
    [Table("InvoiceDetail")]
    public class InvoiceDetailEntity
    {
        [Key]
        public int Id { get; set; }
      
        [ForeignKey("ProductEntity")]
        public int IdProduct { get; set; }
        public ProductEntity ProductEntity { get; set; }

        [ForeignKey("InvoiceEntity")]
        public int IdInvoice { get; set; }
        public InvoiceEntity InvoiceEntity { get; set; }

        public int Count { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalSale { get; set; }
    }
}
