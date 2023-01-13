using AutoMapper;
using StackExchange.Redis;
using System.Collections;
using System.Collections.Generic;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.core.Entities.Order_Aggregate;
using Talabat_APIs.Dtos;

namespace Talabat_APIs.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductType, o => o.MapFrom(c => c.ProductType.Name))
                .ForMember(p => p.ProductBrand, o => o.MapFrom(c => c.ProductBrand.Name))
                .ForMember(p => p.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<Talabat.core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<Talabat.core.Entities.Order_Aggregate.Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<Talabat.core.Entities.Order_Aggregate.Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethodCost, s => s.MapFrom(s => s.OrderDeliveryMethod.Cost))
                .ForMember(d => d.DeliveryMethod, s => s.MapFrom(s => s.OrderDeliveryMethod.ShortName));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, s => s.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, s => s.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.ProductPictureUrl, s => s.MapFrom(s => s.Product.PictureUrl));
        }
    }
}