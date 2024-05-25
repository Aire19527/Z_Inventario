using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Invoice
{
    public class InvoiceDto
    {
        [MaxLength(50)]
        [Display(Name ="Nombre Cliente")]
        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "No se permiten caracteres/numeros")]
        public string ClientName { get; set; }
        
        [MaxLength(50)]
        [Display(Name = "Apellido Cliente")]
        [Required(ErrorMessage = "El apellido del cliente es requerido")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,30}$", ErrorMessage = "No se permiten caracteres/numeros")]
        public string ClientLastName { get; set; }

        [MaxLength(15)]
        [Display(Name = "Documento Cliente")]
        [Required(ErrorMessage = "El documento del cliente es requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string ClientDocument { get; set; }

        [MaxLength(100)]
        [Display(Name = "Dirección Cliente")]        
        [Required(ErrorMessage = "La direccióndel cliente es requerido")]
        public string ClientAddress { get; set; }
        
        [MaxLength(12)]
        [Display(Name = "Teléfono Cliente")]
        [Required(ErrorMessage = "El teléfono cliente es requerido")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string ClientPhone { get; set; }
    }
}
