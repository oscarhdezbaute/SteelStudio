namespace SteelStudio.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using SteelStudio.Common.Models;
    public class UploadImagesProductModel
    {
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Categoria")]
        public string CategoryDescription { get; set; }

        [Display(Name = "Código")]
        public string Code { get; set; }        

        [Display(Name = "Imagen principal")]
        public string ImageFullPath { get; set; }

        public ICollection<ProductImage> ProductImage { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}