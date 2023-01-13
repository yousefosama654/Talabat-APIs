using System.ComponentModel.DataAnnotations;

namespace Talabat_APIs.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required, Range(1, int.MaxValue, ErrorMessage = "the quantity must be above 0")]
        public int Quantity { get; set; }
        [Required,Range(.1,double.MaxValue,ErrorMessage ="the price must be above .1")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
