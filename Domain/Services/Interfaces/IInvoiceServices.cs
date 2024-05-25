using Domain.DTO;
using Domain.DTO.Invoice;

namespace Domain.Services.Interfaces
{
    public interface IInvoiceServices
    {
        ConsultInvoiceDto GetInvoice(int idInvoice);
        Task<ResponseDto> InsertInvoice(AddInvoiceDto add);
        List<ConsultInvoiceDto> GetAllInvoice();
        Task<bool> DeleteInvoice(int idInvoice);

    }
}
