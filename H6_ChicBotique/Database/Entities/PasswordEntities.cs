using H5_Webshop.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace H6_ChicBotique.Database.Entities
{
    public class PasswordEntity

    {
        [Key]
        public int PasswordId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(64)")]
        public string Salt { get; set; }
        [Column(TypeName = "date")]
        public DateTime LastUpdated { get; set; }

        public User User { get; set; } //navigational object
    }
}
