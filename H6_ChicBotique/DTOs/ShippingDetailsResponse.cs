using H6_ChicBotique.Database.Entities;

namespace H6_ChicBotique.DTOs
{
    public class ShippingDetailsResponse
    {
        public int OrderId { get; set; }
        public int Id { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public Order Order { get; set; }
    }
}
