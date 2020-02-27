namespace SteelStudio.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    public class CreateProductModel
    {
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
        public HttpPostedFileBase ImageFile { get; set; }
    }
}
