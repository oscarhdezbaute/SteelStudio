namespace SteelStudio.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using SteelStudio.Common.Models;
    public class EditProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Categoria")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Display(Name = "Imagen principal")]
        public string ImageFullPath { get; set; }

        [Required]
        public string UserName { get; set; }

        public ICollection<ProductImage> ProductImage { get; set; }
    }
}