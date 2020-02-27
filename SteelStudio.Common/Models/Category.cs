namespace SteelStudio.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(10)]
        [Display(Name = "Sigla")]        
        public string Initials { get; set; }
        
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
