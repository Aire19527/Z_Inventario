using Infraestructure.Core.Repository.Interface;
using Infraestructure.Core.Repository;
using Infraestructure.Core.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.Entity.Models;
using Infraestructure.Core.Data;

namespace Infraestructure.Core.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        #region Attributes
        private readonly DataContext _context;
        private bool disposed = false;
        #endregion Attributes

        #region builder
        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }
        #endregion builder

        #region Properties
        private IRepository<ProductEntity> productRepository;
        private IRepository<InvoiceEntity> invoiceRepository;
        private IRepository<InvoiceDetailEntity> invoiceDetailRepository;
        #endregion


        #region Members
        public IRepository<ProductEntity> ProductRepository
        {
            get
            {
                if (this.productRepository == null)
                    this.productRepository = new Repository<ProductEntity>(_context);

                return productRepository;
            }
        } 
        
        public IRepository<InvoiceEntity> InvoiceRepository
        {
            get
            {
                if (this.invoiceRepository == null)
                    this.invoiceRepository = new Repository<InvoiceEntity>(_context);

                return invoiceRepository;
            }
        } 
        public IRepository<InvoiceDetailEntity> InvoiceDetailRepository
        {
            get
            {
                if (this.invoiceDetailRepository == null)
                    this.invoiceDetailRepository = new Repository<InvoiceDetailEntity>(_context);

                return invoiceDetailRepository;
            }
        }
        #endregion

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save() => await _context.SaveChangesAsync();
    }
}
