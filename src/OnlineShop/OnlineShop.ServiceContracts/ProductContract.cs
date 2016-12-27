using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using OnlineShop.DTO;
using AutoMapper;

namespace OnlineShop.ServiceContracts
{
    public class ProductContract: IProductContract
    {
        IRepository<Product> productRepository;

        public ProductContract(IUnitOfWork unitOfWork)
        {
            this.productRepository = unitOfWork.ProductRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public ProductDto Create(ProductDto item)
        {
            Product convertedProduct = Mapper.Map<ProductDto, Product>(item);
            Product product = productRepository.Create(convertedProduct);
            productRepository.Save();
            ProductDto dto = Mapper.Map<Product, ProductDto>(product);
            return dto;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            productRepository.Delete(id);
            productRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetProduct/{id}")]
        public ProductDto GetItem(string id)
        {
            Product product = productRepository.GetItem(Convert.ToInt32(id));
            ProductDto dto = Mapper.Map<Product, ProductDto>(product);
            return dto;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllProducts")]
        public IEnumerable<ProductDto> GetItemsList()
        {
            var items = productRepository.GetItemsList();
            IEnumerable<ProductDto> dtos = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(items);
            return dtos;
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(ProductDto item)
        {
            Product convertedOrder = Mapper.Map<ProductDto, Product>(item);
            productRepository.Update(convertedOrder);
            productRepository.Save();
        }
    }
}
