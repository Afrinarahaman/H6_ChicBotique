using H6_ChicBotique.Database.Entities;
using System.ComponentModel.DataAnnotations;

namespace H5_Webshop.Database.Entities
{
    public class AccountInfo
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? UserId { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public HomeAddress? HomeAddress { get; set; }
        public User User { get; set; }
    }
}