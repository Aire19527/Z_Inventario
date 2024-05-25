namespace Domain.DTO.Invoice
{
    public class ConfigInvoiceDto
    {
        public AddInvoiceDto Invoice { get; set; }
        public int Iva { get; set; }
        public int DiscountRate { get; set; }
        public int ConditionalDiscount { get; set; }
    }
}
