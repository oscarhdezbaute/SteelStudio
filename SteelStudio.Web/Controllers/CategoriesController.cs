using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SteelStudio.Common.Models;
using SteelStudio.Web.Models;

namespace SteelStudio.Web.Controllers
{
    [Authorize (Roles = "administrador")]
    public class CategoriesController : Controller
    {
        private LocalDataContext db = new LocalDataContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
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
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Initials,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                var cat = (from c in db.Categories.AsEnumerable()
                             where c.Description == category.Description                             
                             select c).SingleOrDefault();
                if (cat == null)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Create", new { id = 1 });
                }
                else
                    return RedirectToAction("Create", new { id = 2 });
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           

            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Initials,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");                
            }
            return View(category);
        }        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection result)
        {
            if (result["Id[]"] != null)
            {
                string[] items = result["Id[]"].Split(',');
                foreach (var id in items)
                {
                    Category category = db.Categories.Find(Convert.ToInt32(id));
                    if (category != null)
                    {
                        db.Categories.Remove(category);
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteOne(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
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
