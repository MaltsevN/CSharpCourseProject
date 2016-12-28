using AutoMapper;
using DomainModel;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.ServiceContracts
{
    public class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(MapperConfig);
        }

        private static void MapperConfig(IMapperConfigurationExpression config)
        {
            ProductConfigurator(config);
            OrderItemConfigurator(config);
            UserChildConfigurator(config);
            OrderConfigurator(config);
            UserConfigurator(config);
            RankConfigurator(config);
            StatusConfigurator(config);
            AuthenticationTokenConfigurator(config);
        }

        private static void AuthenticationTokenConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<AuthenticationToken, AuthenticationTokenDto>();
            config.CreateMap<AuthenticationTokenDto, AuthenticationToken>();
        }

        private static void StatusConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<Status, StatusDto>();
            config.CreateMap<StatusDto, Status>();
        }

        private static void RankConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<Rank, RankDto>();
            config.CreateMap<RankDto, Rank>();
        }

        private static void UserConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserDto>();
            config.CreateMap<UserDto, User>();
        }

        private static void OrderConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.OrderItems, dto => dto.MapFrom(order => order.Items));
            config.CreateMap<OrderDto, Order>()
                .ForMember(order => order.Items, order => order.MapFrom(dto => dto.OrderItems)); 
        }

        private static void UserChildConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<User, UserChildDto>();
            config.CreateMap<UserChildDto, User>();
        }

        private static void OrderItemConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<OrderItem, OrderItemDto>();
            config.CreateMap<OrderItemDto, OrderItem>()
                .ForMember(item => item.ProductId, item => item.MapFrom(dto => dto.Product.Id));
        }

        private static void ProductConfigurator(IMapperConfigurationExpression config)
        {
            config.CreateMap<Product, ProductDto>();
            config.CreateMap<ProductDto, Product>();

            config.CreateMap<ProductPrice, ProductPriceDto>();
            config.CreateMap<ProductPriceDto, ProductPrice>();
        }
    }
}
