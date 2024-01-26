using H5_Webshop.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Date")]  //this is a columnAttribute from System.CoponentModel.DataAnnotations (defined in enityframework.dll)
        public DateTime OrderDate { get; set; }

        [ForeignKey("AccountId")]
        public Guid AccountId { get; set; }
        public AccountInfo AccountInfo { get; set; }
        public ShippingDetails ShippingDetails { get; set; }

        public List<OrderDetails> OrderDetails { get; set; } = new();

        [ForeignKey("PaymentId")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }

}
