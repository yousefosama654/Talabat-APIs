using System.Collections.Generic;
using Talabat.core.Entities;

namespace Talabat_APIs.Dtos
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
