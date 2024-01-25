using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class Category
    {
            // Unik identifikator(Primary key) for kategorien
            [Key]
            public int Id { get; set; }

            // Navn på kategorien med en maksimal længde på 20 tegn
            [Column(TypeName = "nvarchar(20)")]
            public string? CategoryName { get; set; }

            // Liste af produkter, der hører til denne kategori. Initialiseres som en tom liste.
            public List<Product> Products { get; set; } = new();
        }
    }
