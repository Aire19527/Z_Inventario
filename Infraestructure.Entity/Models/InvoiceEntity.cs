using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Models
{
    [Table("Invoice")]
    public class InvoiceEntity
    {
        [Key]
        public int IdInvoice { get; set; }
        public DateTime CreationDate { get; set; }
        [MaxLength(50)]
        public string ClientName { get; set; }
        [MaxLength(50)]
        public string ClientLastName { get; set; }
        [MaxLength(10)]
        public string ClientAddress { get; set; }
        [MaxLength(12)]
        public string ClientPhone { get; set; }
        
        [MaxLength(15)]
        public string ClientDocument{ get; set; }

        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Iva { get; set; }
        public decimal TotalInvoice { get; set; }

        [NotMapped]
        public string FullName { get { return $"{this.ClientName} {this.ClientLastName}"; } }
        public IEnumerable<InvoiceDetailEntity> InvoiceDetailEntities { get; set; }
    }
}
