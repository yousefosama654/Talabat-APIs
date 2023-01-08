namespace Talabat_APIs.Dtos
{
    public class OrderDto
    {
        public string basketId { get; set; }
        public AddressDto shippingaddress { get; set; }
        public int deliveryMethodId { get; set; }
    }
}
