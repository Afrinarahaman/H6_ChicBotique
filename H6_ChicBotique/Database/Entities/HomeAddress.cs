using H5_Webshop.Database.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class HomeAddress
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string TelePhone { get; set; }

        [ForeignKey("AccountInfoId")]
        public Guid AccountInfoId { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }
}
