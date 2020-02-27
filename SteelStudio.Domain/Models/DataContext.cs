namespace SteelStudio.Domain.Models
{
    using System.Data.Entity;
    using SteelStudio.Common.Models;
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }
        public System.Data.Entity.DbSet<Category> Categories { get; set; }
        public System.Data.Entity.DbSet<Product> Products { get; set; }
        public System.Data.Entity.DbSet<ProductImage> ProductsImages { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ProductImage>().HasRequired(p => p.Product).WithMany().WillCascadeOnDelete(true);
        }*/
    }
}
