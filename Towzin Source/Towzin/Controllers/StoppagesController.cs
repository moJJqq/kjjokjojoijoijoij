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
    public class StoppagesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Stoppages
        
        [Authorize(Roles = "Stoppages_Index , admin")]
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

            var Stoppages = from s in db.Stoppages
                                   select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Stoppages = Stoppages.Where(s => s.StoppagesName.Contains(searchString) || s.StoppagesLatinName.Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    Stoppages = Stoppages.OrderByDescending(s => s.StoppagesName);
                    break;
                case "T2_desc":
                    Stoppages = Stoppages.OrderByDescending(s => s.StoppagesLatinName);
                    break;
                case "T2":
                    Stoppages = Stoppages.OrderBy(s => s.StoppagesLatinName);
                    break;
                case "T3_desc":
                    Stoppages = Stoppages.OrderByDescending(s => s.Description);
                    break;
                case "T3":
                    Stoppages = Stoppages.OrderBy(s => s.Description);
                    break;
                case "T4_desc":
                    Stoppages = Stoppages.OrderByDescending(s => s.State);
                    break;
                case "T4":
                    Stoppages = Stoppages.OrderBy(s => s.State);
                    break;

                default:
                    Stoppages = Stoppages.OrderBy(s => s.StoppagesName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Stoppages.ToPagedList(pageNumber, pageSize));
        }
        // GET: Stoppages/Details/5
        
        [Authorize(Roles = "Stoppages_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stoppages stoppages = await db.Stoppages.FindAsync(id);
            if (stoppages == null)
            {
                return HttpNotFound();
            }
            return View(stoppages);
        }

        // GET: Stoppages/Create
        
        [Authorize(Roles = "Stoppages_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stoppages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Stoppages_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,StoppagesLatinName,StoppagesName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Stoppages stoppages)
        {
            stoppages.Creator = User.Identity.GetUserId();
            stoppages.AddDate = DateTime.Now;
            stoppages.LastModificationDate = DateTime.Now;
            stoppages.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Stoppages.Add(stoppages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(stoppages);
        }

        // GET: Stoppages/Edit/5
        
        [Authorize(Roles = "Stoppages_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stoppages stoppages = await db.Stoppages.FindAsync(id);
            if (stoppages == null)
            {
                return HttpNotFound();
            }
            return View(stoppages);
        }

        // POST: Stoppages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Stoppages_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StoppagesLatinName,StoppagesName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Stoppages stoppages)
        {
            stoppages.LastModificationDate = DateTime.Now;
            stoppages.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(stoppages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(stoppages);
        }

        // GET: Stoppages/Delete/5
        
        [Authorize(Roles = "Stoppages_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stoppages stoppages = await db.Stoppages.FindAsync(id);
            if (stoppages == null)
            {
                return HttpNotFound();
            }
            return View(stoppages);
        }

        // POST: Stoppages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Stoppages_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Stoppages stoppages = await db.Stoppages.FindAsync(id);
            db.Stoppages.Remove(stoppages);
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
