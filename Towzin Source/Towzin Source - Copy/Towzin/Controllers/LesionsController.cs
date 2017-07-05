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
    public class LesionsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Lesions
 
        [Authorize(Roles = "Lesions_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.LatinNameSortParm = sortOrder == "LatinName" ? "LatinName_desc" : "LatinName";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";
            ViewBag.StateSortParm = sortOrder == "State" ? "State_desc" : "State";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var Lesion = from s in db.Lesion
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Lesion = Lesion.Where(s => s.LesionsName.Contains(searchString)
                                       || s.LesionsLatinName.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    Lesion = Lesion.OrderByDescending(s => s.LesionsName);
                    break;
                case "LatinName_desc":
                    Lesion = Lesion.OrderByDescending(s => s.LesionsLatinName);
                    break;
                case "LatinName":
                    Lesion = Lesion.OrderBy(s => s.LesionsLatinName);
                    break;
                case "Description_desc":
                    Lesion = Lesion.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    Lesion = Lesion.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    Lesion = Lesion.OrderByDescending(s => s.State);
                    break;
                case "State":
                    Lesion = Lesion.OrderBy(s => s.State);
                    break;
                default:
                    Lesion = Lesion.OrderBy(s => s.LesionsName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Lesion.ToPagedList(pageNumber, pageSize));
        }

        // GET: Lesions/Details/5
        
        [Authorize(Roles = "Lesions_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesion lesion = await db.Lesion.FindAsync(id);
            if (lesion == null)
            {
                return HttpNotFound();
            }
            return View(lesion);
        }

        // GET: Lesions/Create
        
        [Authorize(Roles = "Lesions_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lesions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Lesions_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,LesionsName,LesionsLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Lesion lesion)
        {
            lesion.Creator = User.Identity.GetUserId();
            lesion.AddDate = DateTime.Now;
            lesion.LastModificationDate = DateTime.Now;
            lesion.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Lesion.Add(lesion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lesion);
        }


        // GET: Lesions/Edit/5
        
        [Authorize(Roles = "Lesions_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesion lesion = await db.Lesion.FindAsync(id);
            if (lesion == null)
            {
                return HttpNotFound();
            }
            return View(lesion);
        }

        // POST: Lesions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Lesions_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,LesionsName,LesionsLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Lesion lesion)
        {
            lesion.LastModificationDate = DateTime.Now;
            lesion.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(lesion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lesion);
        }

        // GET: Lesions/Delete/5
        
        [Authorize(Roles = "Lesions_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesion lesion = await db.Lesion.FindAsync(id);
            if (lesion == null)
            {
                return HttpNotFound();
            }
            return View(lesion);
        }

        // POST: Lesions/Delete/5
        
        [Authorize(Roles = "Lesions_Delete , admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Lesion lesion = await db.Lesion.FindAsync(id);
            db.Lesion.Remove(lesion);
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
