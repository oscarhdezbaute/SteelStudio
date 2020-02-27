using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SteelStudio.Common.Models;
using SteelStudio.Web.Helpers;
using SteelStudio.Web.Models;

namespace SteelStudio.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await db.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create(int id = 0)
        {
            if (id == 1)
                ViewBag.Creado = "Y";
            else
            {
                if (id == 2)
                    ViewBag.Creado = "E";
                else
                    ViewBag.Creado = "N";
            }

            List<Category> categories_list = new List<Category>();            
            categories_list.AddRange(db.Categories.ToList());            
            ViewBag.categories = categories_list;
            CreateProductModel model = new CreateProductModel();

            return View(model);            
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var prod = (from p in db.Products.AsEnumerable()
                           where p.Code == productModel.Code
                           select p).SingleOrDefault();
                if (prod == null)
                {
                    var product = this.ToProduct(productModel);
                    if (productModel.CategoryId != 0)
                        product.Category = db.Categories.Find(productModel.CategoryId);

                    var pic = string.Empty;
                    var folder = "~/Content/Images/Products";

                    //verifico que se haya seleccionado una foto como foto principal
                    if (productModel.ImageFile != null) 
                    {
                        pic = FileHelper.UploadPhoto(productModel.ImageFile, folder);
                        pic = $"{folder}/{pic}";

                        List<ProductImage> list = new List<ProductImage>();
                        ProductImage productImage = new ProductImage()
                        {
                            ImageUrl = pic,        
                            //Product = product,
                        };
                        list.Add(productImage);

                        product.ProductImage = list;
                    }
                    
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Create", new { id = 1 });
                }
                else
                    return RedirectToAction("Create", new { id = 2 });
            }

            return View(productModel);
        }

        private Product ToProduct(CreateProductModel view)
        {
            return new Product
            {
                Code = view.Code,
                Notes = view.Notes,
                UserName = User.Identity.GetUserName(),
            };
        }

        private Product ToProduct(EditProductModel view)
        {
            return new Product
            {
                ProductId = view.ProductId,
                Code = view.Code,
                Notes = view.Notes,
                Category = db.Categories.Find(view.CategoryId),
                UserName = view.UserName,                
            };
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            EditProductModel model = new EditProductModel();
            model.ProductId = product.ProductId;
            model.Code = product.Code;
            model.CategoryId = product.Category.CategoryId;
            model.UserName = product.UserName;
            model.Notes = product.Notes;
            model.ProductImage = product.ProductImage;
            model.ImageFullPath = product.ImageFullPath;

            List<Category> categories_list = new List<Category>();
            categories_list.AddRange(db.Categories.ToList());
            ViewBag.categories = categories_list;

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(model.ProductId);

                //var product = this.ToProduct(model);            

                product.Code = model.Code;
                product.Notes = model.Notes;
                product.Category = db.Categories.Find(model.CategoryId);

                //db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", model.ProductId);
        }

        // GET: Products/UploadImages/5
        public async Task<ActionResult> UploadImages(int? id, int error = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (error == 1)
                ViewBag.Creado = "Y";
            else
            {
                if (error == 2)
                    ViewBag.Creado = "E";
                else
                    ViewBag.Creado = "N";
            }

            UploadImagesProductModel model = new UploadImagesProductModel();
            model.ProductId = product.ProductId;
            model.Code = product.Code;
            model.CategoryDescription = product.Category.Description;
            model.ProductImage = product.ProductImage;
            model.ImageFullPath = product.ImageFullPath;

            return View(model);
        }

        // POST: Products/UploadImages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UploadImages(UploadImagesProductModel model)
        {
            if (ModelState.IsValid)
            {
                var product = db.Products.Find(model.ProductId);

                var pic = string.Empty;
                var folder = "~/Content/Images/Products";

                //verifico que se haya seleccionado una foto como foto principal
                if (model.ImageFile != null)
                {
                    pic = FileHelper.UploadPhoto(model.ImageFile, folder);
                    pic = $"{folder}/{pic}";
                    
                    ProductImage productImage = new ProductImage()
                    {
                        ImageUrl = pic,
                        //Product = product,
                    };
                   
                    product.ProductImage.Add(productImage);
                }

                await db.SaveChangesAsync();
                return RedirectToAction("UploadImages", new { id = model.ProductId, error = 1 });
            }
            return RedirectToAction("UploadImages", new { id = model.ProductId, error = 2 });
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);
            //var list_images = product.ProductImage;
            ProductImage[] list_images = new ProductImage[product.ProductImage.Count];
            product.ProductImage.CopyTo(list_images, 0);
            db.Products.Remove(product);
            await db.SaveChangesAsync();

            //-----------trato de eliminar los ficheros de las imagenes de ese producto-----------//
            try
            {
                foreach (var item in list_images)
                {
                    var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(item.ImageUrl));
                    System.IO.File.Delete(path);
                }                
            }
            catch
            {

            }

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteImageFromEditForm(int id)
        {            
            ProductImage image = await db.ProductsImages.FindAsync(id);
            var imagePath = image.ImageUrl;
            var productid = image.Product.ProductId;
            db.ProductsImages.Remove(image);
            await db.SaveChangesAsync();

            //-----trato de eliminar el fichero de imagen-----//
            try
            {
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagePath));                
                System.IO.File.Delete(path);
            }
            catch
            {

            }

            return RedirectToAction("Edit",new {id = productid});
        }

        public async Task<ActionResult> DeleteImageFromUpload(int id)
        {
            ProductImage image = await db.ProductsImages.FindAsync(id);
            var imagePath = image.ImageUrl;
            var productid = image.Product.ProductId;
            db.ProductsImages.Remove(image);
            await db.SaveChangesAsync();
            //-----trato de eliminar el fichero de imagen-----//
            try
            {
                var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(imagePath));
                System.IO.File.Delete(path);
            }
            catch
            {

            }

            return RedirectToAction("UploadImages", new { id = productid, error = 0});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
