using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Produdct
{
    public class ProductDto
    {
        [Display(Name = "Nombre")]    
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "No se permiten caracteres/números")]
        [Required(ErrorMessage = "El nombre del Producto es requerido")]
        public string ProductName { get; set; }

        [Display(Name = "Código")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(ErrorMessage = "El código del Producto es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "La el código debe ser mayor a 0")]
        public int Code { get; set; }

        [Display(Name = "Cantidad")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(ErrorMessage = "La cantidad del Producto es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Count { get; set; }

        [Display(Name = "Precio")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        [Required(ErrorMessage = "El precio del Producto es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El precio debe ser mayor a $0")]
        public decimal Price { get; set; }
    }
}
