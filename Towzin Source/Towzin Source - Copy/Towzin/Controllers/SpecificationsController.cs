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
    public class SpecificationsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();
        
        [Authorize(Roles = "Specification_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.T1SortParm = String.IsNullOrEmpty(sortOrder) ? "T1" : "";
            ViewBag.T2SortParm = sortOrder == "T2" ? "T2_desc" : "T2";
            ViewBag.T3SortParm = sortOrder == "T3" ? "T3_desc" : "T3";
            ViewBag.T4SortParm = sortOrder == "T4" ? "T4_desc" : "T4";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Specification = from s in db.Specification
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Specification = Specification.Where(s => s.SpecificationName.Contains(searchString) || s.SpecificationLatinName.Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    Specification = Specification.OrderByDescending(s => s.SpecificationName);
                    break;
                case "T2_desc":
                    Specification = Specification.OrderByDescending(s => s.SpecificationLatinName);
                    break;
                case "T2":
                    Specification = Specification.OrderBy(s => s.SpecificationLatinName);
                    break;
                case "T3_desc":
                    Specification = Specification.OrderByDescending(s => s.Description);
                    break;
                case "T3":
                    Specification = Specification.OrderBy(s => s.Description);
                    break;
                case "T4_desc":
                    Specification = Specification.OrderByDescending(s => s.State);
                    break;
                case "T4":
                    Specification = Specification.OrderBy(s => s.State);
                    break;

                default:
                    Specification = Specification.OrderBy(s => s.SpecificationName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Specification.ToPagedList(pageNumber, pageSize));
        }
        // GET: Specifications

        // GET: Specifications/Details/5
        
        [Authorize(Roles = "Specification_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification Specification = await db.Specification.FindAsync(id);
            if (Specification == null)
            {
                return HttpNotFound();
            }
            return View(Specification);
        }

        // GET: Specifications/Create
       
        [Authorize(Roles = "Specification_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Specifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Specification_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,SpecificationName,SpecificationLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Specification Specification)
        {
            Specification.Creator = User.Identity.GetUserId();
            Specification.AddDate = DateTime.Now;
            Specification.LastModificationDate = DateTime.Now;
            Specification.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Specification.Add(Specification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Specification);
        }

        // GET: Specifications/Edit/5
        
        [Authorize(Roles = "Specification_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification Specification = await db.Specification.FindAsync(id);
            if (Specification == null)
            {
                return HttpNotFound();
            }
            return View(Specification);
        }

        // POST: Specifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Specification_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,SpecificationName,SpecificationLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Specification Specification)
        {
            Specification.LastModificationDate = DateTime.Now;
            Specification.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(Specification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Specification);
        }

        // GET: Specifications/Delete/5
        
        [Authorize(Roles = "Specification_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specification Specification = await db.Specification.FindAsync(id);
            if (Specification == null)
            {
                return HttpNotFound();
            }
            return View(Specification);
        }

        // POST: Specifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Specification_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Specification Specification = await db.Specification.FindAsync(id);
            db.Specification.Remove(Specification);
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
