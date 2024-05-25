using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infraestructure.Entity.Models
{
    [Table("Product")]
    public class ProductEntity
    {
        public ProductEntity()
        {
            InvoiceDetailEntities=new List<InvoiceDetailEntity>();  
        }

        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }
        public int Code { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public IEnumerable<InvoiceDetailEntity> InvoiceDetailEntities { get; set; }
    }
}
