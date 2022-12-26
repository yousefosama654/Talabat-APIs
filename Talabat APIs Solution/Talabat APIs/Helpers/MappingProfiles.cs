using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using Talabat.core.Entities;
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

  
        }
    }
}
