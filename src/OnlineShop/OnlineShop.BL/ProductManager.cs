using AutoMapper;
using DAL;
using DomainModel;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public class ProductManager : IProductManager
    {
        IRepository<Product> productRepository;

        static ProductManager()
        {
            AutoMapperConfigurator.Configure();
        }

        public ProductManager(IUnitOfWork unitOfWork)
        {
            this.productRepository = unitOfWork.ProductRepository;
        }
        
        public ProductDto Create(ProductDto item)
        {
            Product convertedProduct = Mapper.Map<ProductDto, Product>(item);
            Product product = productRepository.Create(convertedProduct);
            productRepository.Save();
            ProductDto dto = Mapper.Map<Product, ProductDto>(product);
            return dto;
        }
        
        public void Delete(int id)
        {
            productRepository.Delete(id);
            productRepository.Save();
        }
        
        public ProductDto GetProduct(int id)
        {
            Product product = productRepository.GetItem(id);
            ProductDto dto = Mapper.Map<Product, ProductDto>(product);
            return dto;
        }
        
        public IEnumerable<ProductDto> GetAllProducts()
        {
            var items = productRepository.GetItemsList();
            IEnumerable<ProductDto> dtos = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(items);
            return dtos;
        }
        
        public void Update(ProductDto item)
        {
            Product convertedOrder = Mapper.Map<ProductDto, Product>(item);
            productRepository.Update(convertedOrder);
            productRepository.Save();
        }
    }
}
