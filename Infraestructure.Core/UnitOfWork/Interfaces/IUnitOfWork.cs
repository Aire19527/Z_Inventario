using Infraestructure.Core.Repository.Interface;
using Infraestructure.Entity.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infraestructure.Core.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<ProductEntity> ProductRepository { get; }
        IRepository<InvoiceEntity> InvoiceRepository { get; }
        IRepository<InvoiceDetailEntity> InvoiceDetailRepository { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        void Dispose();
        Task<int> Save();
    }
}
