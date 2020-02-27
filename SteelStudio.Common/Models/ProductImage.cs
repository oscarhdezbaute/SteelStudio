namespace SteelStudio.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    public class ProductImage
    { 
        [Key]
        public int ProductImageId { get; set; }

        [Required]        
        public string ImageUrl { get; set; }

        public virtual Product Product { get; set; }

    }
}
