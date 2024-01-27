using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class Category
    {

        // Category Model Class
        // Primary key
        [Key]
        public int Id { get; set; }
        // CategoryName
        [Column(TypeName = "nvarchar(20)")]
        public string? CategoryName { get; set; }
        // Products
        public List<Product> Products { get; set; } = new();

       }
    }
