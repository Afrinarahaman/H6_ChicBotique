using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class ShippingDetails
    {
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        public Order Order { get; set; }

    }
}
