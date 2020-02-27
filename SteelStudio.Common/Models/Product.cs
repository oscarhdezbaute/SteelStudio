namespace SteelStudio.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(50)]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Display(Name = "Observaciones")]
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Сreado por")]
        public string UserName { get; set; }

        [Display(Name = "Categoria")]
        public virtual Category Category { get; set; }        

        public virtual ICollection<ProductImage> ProductImage { get; set; }

        [NotMapped]
        public string ImageFullPath
        {
            get
            {
                if (this.ProductImage.Count == 0)
                {
                    return "noproduct";
                }

                ProductImage[] array = new Models.ProductImage[this.ProductImage.Count];
                this.ProductImage.CopyTo(array, 0);
                //return $"http://localhost:53797/{array[0].ImageUrl.Substring(1)}";
                return array[0].ImageUrl;
            }
        }
    }
}
