using Domain.DTO.Invoice;
using Domain.DTO.Produdct;

namespace Domain.Services.Interfaces
{
    public interface IProductServices
    {
        List<ConsultProductDto> GetAllProduts();
        Task<bool> Insert(ProductDto add);
        Task<bool> Update(ConsultProductDto update);
        Task<bool> Delete(int id);
        Task<bool> UpdateStockProduct(List<InvoiceDetailDto> InvoiceDetails);
    }
}
