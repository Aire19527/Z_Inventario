using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Invoice
{
    public class InvoiceDetailDto
    {
        [Required(ErrorMessage = "El Producto es requerido")]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Count { get; set; }
        [Required(ErrorMessage = "La valor debe ser mayor a $0")]
        public decimal SalePrice { get; set; }
    }
}
