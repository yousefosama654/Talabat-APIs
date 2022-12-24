using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using System;
using Talabat.core.Entities;
using Talabat_APIs.Dtos;

namespace Talabat_APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, String>
    {
        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!String.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{Configuration["BaseApiUrl"]}/{source.PictureUrl}";
            }
            else
                return null;
        }
    }
}
