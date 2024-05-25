using Common.Utils.Exceptions;
using Common.Utils.Resources;
using Domain.DTO.Invoice;
using Domain.DTO.Produdct;
using Domain.Services.Interfaces;
using Infraestructure.Core.UnitOfWork.Interfaces;
using Infraestructure.Entity.Models;

namespace Domain.Services
{
    public class ProductServices : IProductServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public ProductServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        public List<ConsultProductDto> GetAllProduts()
        {
            IEnumerable<ProductEntity> productEntities = _unitOfWork.ProductRepository.GetAll();

            List<ConsultProductDto> products = productEntities.Select(x => new ConsultProductDto()
            {
                Code = x.Code,
                Count = x.Count,
                Id = x.Id,
                Price = x.Price,
                ProductName = x.ProductName
            }).ToList();

            return products;
        }

        public async Task<bool> Insert(ProductDto add)
        {
            ProductEntity existProduct = GetProductByCode(add.Code);
            if (existProduct != null)
                throw new BusinessException(string.Format(GeneralMessages.ExistProductWithCode, add.Code));

            ProductEntity product = new ProductEntity()
            {
                Code = add.Code,
                Count = add.Count,
                Price = add.Price,
                ProductName = add.ProductName
            };
            _unitOfWork.ProductRepository.Insert(product);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Update(ConsultProductDto update)
        {
            ProductEntity product = GetProductById(update.Id);
            if (product == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);
         
            product.Price = update.Price;
            product.Count = update.Count;
            product.ProductName = update.ProductName;
            _unitOfWork.ProductRepository.Update(product);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            ProductEntity product = GetProductById_Invoice(id);
            if (product == null)
                throw new BusinessException(GeneralMessages.ItemNoFound);

            if (product.InvoiceDetailEntities.Any())
                throw new BusinessException(GeneralMessages.ProductRelation);

            _unitOfWork.ProductRepository.Delete(product);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateStockProduct(List<InvoiceDetailDto> InvoiceDetails)
        {
            foreach (var detail in InvoiceDetails)
            {
                ProductEntity product = GetProductById(detail.IdProduct);
                if (product == null)
                    throw new BusinessException(GeneralMessages.ItemNoFound);

                if (detail.Count > product.Count)
                    throw new BusinessException(string.Format(GeneralMessages.OutOfStock, product.ProductName, product.Count));

                product.Count= product.Count-detail.Count;
                _unitOfWork.ProductRepository.Update(product);
            }

            return await _unitOfWork.Save() > 0;
        }

        #region Privates
        private ProductEntity GetProductByCode(int code) => _unitOfWork.ProductRepository.FirstOrDefault(x => x.Code == code);
        private ProductEntity GetProductById(int id) => _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == id);
        private ProductEntity GetProductById_Invoice(int id)
        {
            return _unitOfWork.ProductRepository.FirstOrDefault(x => x.Id == id, i => i.InvoiceDetailEntities);
        }

        #endregion

        #endregion
    }
}
