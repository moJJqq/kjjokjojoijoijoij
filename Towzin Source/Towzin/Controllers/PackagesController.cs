using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Towzin.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Towzin.Controllers
{
    public class PackagesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Packages
        
        [Authorize(Roles = "Package_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.T1SortParm = String.IsNullOrEmpty(sortOrder) ? "T1" : "";
            ViewBag.T2SortParm = sortOrder == "T2" ? "T2_desc" : "T2";
            ViewBag.T3SortParm = sortOrder == "T3" ? "T3_desc" : "T3";
            ViewBag.T4SortParm = sortOrder == "T4" ? "T4_desc" : "T4";
            ViewBag.T5SortParm = sortOrder == "T5" ? "T5_desc" : "T5";
            ViewBag.T6SortParm = sortOrder == "T6" ? "T6_desc" : "T6";
            ViewBag.T7SortParm = sortOrder == "T7" ? "T7_desc" : "T7";
            ViewBag.T7SortParm = sortOrder == "T8" ? "T8_desc" : "T8";
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Package = from s in db.Package
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Package = Package.Where(s => s.LatinPackageName.Contains(searchString) || s.PackageName.Contains(searchString) || s.Length.ToString().Contains(searchString) || s.Width.ToString().Contains(searchString) || s.Weight.ToString().Contains(searchString) || s.Height.ToString().Contains(searchString) || s.Description.Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    Package = Package.OrderByDescending(s => s.PackageName);
                    break;
                case "T2_desc":
                    Package = Package.OrderByDescending(s => s.LatinPackageName);
                    break;
                case "T2":
                    Package = Package.OrderBy(s => s.LatinPackageName);
                    break;
                case "T3_desc":
                    Package = Package.OrderByDescending(s => s.Length);
                    break;
                case "T3":
                    Package = Package.OrderBy(s => s.Length);
                    break;
                case "T4_desc":
                    Package = Package.OrderByDescending(s => s.Width);
                    break;
                case "T4":
                    Package = Package.OrderBy(s => s.Width);
                    break;
                case "T5_desc":
                    Package = Package.OrderByDescending(s => s.Height);
                    break;
                case "T5":
                    Package = Package.OrderBy(s => s.Height);
                    break;
                case "T6_desc":
                    Package = Package.OrderByDescending(s => s.Weight);
                    break;
                case "T6":
                    Package = Package.OrderBy(s => s.Weight);
                    break;
                case "T7_desc":
                    Package = Package.OrderByDescending(s => s.Description);
                    break;
                case "T7":
                    Package = Package.OrderBy(s => s.Description);
                    break;

                case "T8_desc":
                    Package = Package.OrderByDescending(s => s.State);
                    break;
                case "T8":
                    Package = Package.OrderBy(s => s.State);
                    break;
                default:
                    Package = Package.OrderBy(s => s.PackageName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Package.ToPagedList(pageNumber, pageSize));
        }


        // GET: Packages/Details/5
        
        [Authorize(Roles = "Package_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Package.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // GET: Packages/Create
        
        [Authorize(Roles = "Package_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Packages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Package_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PackageName,LatinPackageName,Length,Width,Height,Weight,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Package package)
        {
            package.Creator = User.Identity.GetUserId();
            package.AddDate = DateTime.Now;
            package.LastModificationDate = DateTime.Now;
            package.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Package.Add(package);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(package);
        }

        // GET: Packages/Edit/5
        
        [Authorize(Roles = "Package_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Package.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Package_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PackageName,LatinPackageName,Length,Width,Height,Weight,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Package package)
        {
            package.LastModificationDate = DateTime.Now;
            package.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(package).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(package);
        }

        // GET: Packages/Delete/5
        
        [Authorize(Roles = "Package_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Package package = await db.Package.FindAsync(id);
            if (package == null)
            {
                return HttpNotFound();
            }
            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Package_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Package package = await db.Package.FindAsync(id);
            db.Package.Remove(package);
            await db.SaveChangesAsync();
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
