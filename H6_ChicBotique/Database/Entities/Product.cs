﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H6_ChicBotique.Database.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // Unik identifikator for produktet

        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string Image { get; set; } // Stien til produktbilledet

        [Column(TypeName = "smallint")]
        public int Stock; // Lagerbeholdning af produktet

        //public List<ProductStock> ProductQuantities { get; set; } //  ProductStock.

        public int? CategoryId { get; set; } //Foreign key, der henviser til kategoriens Id

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } // Navigationsegenskab, der forbinder til kategorien
    }

    public class ProductStock
    {
        [Key]
        public int ProductStockId { get; set; } // Unik identifikator for produktstock

        public int ProductId { get; set; }  //Foreign key, der henviser til produktets Id

        public int Quantity { get; set; } // Antal af produktet i stock

        // Navigationsegenskab til at repræsentere relationen mellem ProductStock og Product
        public Product Product { get; set; }
    }
}