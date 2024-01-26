using System.ComponentModel.DataAnnotations;

namespace H6_ChicBotique.DTOs
{
    public class CategoryRequest
    {
        [Required] // Specifies that the property is required
        [StringLength(32, ErrorMessage = "Category Name cannot be more than 32 chars")]
        public string CategoryName { get; set; }
    }
}
