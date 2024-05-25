using Common.Utils.Exceptions;
using Common.Utils.Resources;
using Domain.DTO;
using Domain.DTO.Invoice;
using Domain.Services.Interfaces;
using Infraestructure.Core.UnitOfWork.Interfaces;
using Infraestructure.Entity.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Domain.Services
{
    public class InvoiceServices : IInvoiceServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IProductServices _productServices;
        #endregion

        #region Builder
        public InvoiceServices(IUnitOfWork unitOfWork, IConfiguration configuration, IProductServices productServices)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
            _productServices = productServices;
        }
        #endregion

        #region Privates

        private decimal GetDiscount(decimal total)
        {
            decimal discount = 0;
            string condition = _config.GetSection("ConfigInvoice").GetSection("DiscountCondition").Value;
            if (total > Convert.ToInt32(condition))
            {
                string rate = _config.GetSection("ConfigInvoice").GetSection("DiscountRate").Value;
                discount = (total * Convert.ToInt32(rate)) / 100;
            }

            return discount;
        }

        private decimal GetIva(decimal total)
        {
            decimal value = 0;
            string iva = _config.GetSection("ConfigInvoice").GetSection("Iva").Value;
            value = (total * Convert.ToInt32(iva)) / 100;

            return value;
        }

        private decimal GetTotalInvoice(decimal subTotal, decimal discount, decimal iva)
        {
            decimal total = (subTotal - discount) + iva;

            return total;
        }
        #endregion

        #region Methods
        public ConsultInvoiceDto GetInvoice(int idInvoice)
        {
            InvoiceEntity invoice = _unitOfWork.InvoiceRepository
                                               .FirstOrDefaultSelect(x => x.IdInvoice == idInvoice,
                                                                     invo => invo.InvoiceDetailEntities,
                                                                     i => i.InvoiceDetailEntities.Select(p => p.ProductEntity));

            ConsultInvoiceDto consult = new ConsultInvoiceDto()
            {
                IdInvoice = invoice.IdInvoice,
                FullName = invoice.FullName,
                ClientDocument = invoice.ClientDocument,
                ClientAddress = invoice.ClientAddress,
                ClientLastName = invoice.ClientLastName,
                ClientName = invoice.ClientName,
                ClientPhone = invoice.ClientPhone,
                Discount = invoice.Discount,
                Iva = invoice.Iva,
                SubTotal = invoice.SubTotal,
                TotalInvoice = invoice.TotalInvoice,
                CreationDate = invoice.CreationDate,
                StrCreationDate = invoice.CreationDate.ToString("yyyy-MM-dd hh:mm tt"),
                InvoiceDetails = invoice.InvoiceDetailEntities.Select(i => new ConsultInvoiceDetailDto()
                {
                    Count = i.Count,
                    IdProduct = i.IdProduct,
                    SalePrice = i.SalePrice,
                    IdInvoiceDetail = i.Id,
                    ProductName = i.ProductEntity.ProductName,
                    Code = i.ProductEntity.Code,
                }).ToList()
            };

            return consult;
        }

        public List<ConsultInvoiceDto> GetAllInvoice()
        {
            IEnumerable<InvoiceEntity> invoiceEntities = _unitOfWork.InvoiceRepository
                                                                    .GetAllSelect(x => x.InvoiceDetailEntities,
                                                                                  i => i.InvoiceDetailEntities.Select(p => p.ProductEntity));

            List<ConsultInvoiceDto> consults = invoiceEntities.Select(x => new ConsultInvoiceDto()
            {
                IdInvoice = x.IdInvoice,
                FullName = x.FullName,
                ClientDocument = x.ClientDocument,
                ClientAddress = x.ClientAddress,
                ClientLastName = x.ClientLastName,
                ClientName = x.ClientName,
                ClientPhone = x.ClientPhone,
                Discount = x.Discount,
                Iva = x.Iva,
                SubTotal = x.SubTotal,
                TotalInvoice = x.TotalInvoice,
                CreationDate = x.CreationDate,
                StrCreationDate = x.CreationDate.ToString("yyyy-MM-dd hh:mm tt"),
                InvoiceDetails = x.InvoiceDetailEntities.Select(i => new ConsultInvoiceDetailDto()
                {
                    Count = i.Count,
                    IdProduct = i.IdProduct,
                    SalePrice = i.SalePrice,
                    IdInvoiceDetail = i.Id,
                    ProductName = i.ProductEntity.ProductName,
                    Code = i.ProductEntity.Code,
                }).ToList()

            }).ToList();

            return consults;
        }

        public async Task<ResponseDto> InsertInvoice(AddInvoiceDto add)
        {
            List<InvoiceDetailEntity> invoiceDetailEntities = add.InvoiceDetails.Select(x => new InvoiceDetailEntity()
            {
                Count = x.Count,
                IdProduct = x.IdProduct,
                SalePrice = x.SalePrice,
                TotalSale = (x.Count * x.SalePrice),
            }).ToList();

            decimal subtotal = invoiceDetailEntities.Sum(x => x.TotalSale);
            decimal discount = GetDiscount(subtotal);
            decimal iva = GetIva(subtotal - discount);

            InvoiceEntity invoice = new InvoiceEntity()
            {
                ClientAddress = add.ClientAddress,
                ClientLastName = add.ClientLastName,
                ClientDocument = add.ClientDocument,
                ClientName = add.ClientName,
                ClientPhone = add.ClientPhone,
                CreationDate = DateTime.Now,
                Discount = discount,
                Iva = iva,
                SubTotal = subtotal,
                TotalInvoice = GetTotalInvoice(subtotal, discount, iva),
                InvoiceDetailEntities = invoiceDetailEntities
            };

            ResponseDto response = new ResponseDto();
            using (var db = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _unitOfWork.InvoiceRepository.Insert(invoice);
                    await _unitOfWork.Save();
                    await _productServices.UpdateStockProduct(add.InvoiceDetails);
                    await db.CommitAsync();

                    response.IsSuccess = true;
                    response.Result = invoice.IdInvoice;
                    response.Message = string.Format(GeneralMessages.InvoiceSave,invoice.IdInvoice);
                }
                catch (BusinessException ex)
                {
                    await db.RollbackAsync();
                    throw ex;
                }
                catch (Exception ex)
                {
                    await db.RollbackAsync();
                    throw new Exception(GeneralMessages.Error500, ex);
                }
            }

            return response;
        }

        public async Task<bool> DeleteInvoice(int idInvoice)
        {
            InvoiceEntity invoice = _unitOfWork.InvoiceRepository.FirstOrDefault(x => x.IdInvoice == idInvoice);
            if (invoice == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            _unitOfWork.InvoiceRepository.Delete(invoice);

            return await _unitOfWork.Save() > 0;
        }

        #endregion
    }
}
