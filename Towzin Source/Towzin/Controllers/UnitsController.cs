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
    public class UnitsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Units
        
        [Authorize(Roles = "Unit_Index , admin")]
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

            var Unit = from s in db.Unit
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Unit = Unit.Where(s => s.UnitName.Contains(searchString) || s.sign.Contains(searchString) || s.Description.ToString().Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    Unit = Unit.OrderByDescending(s => s.UnitName);
                    break;
                case "T2_desc":
                    Unit = Unit.OrderByDescending(s => s.sign);
                    break;
                case "T2":
                    Unit = Unit.OrderBy(s => s.sign);
                    break;
                case "T3_desc":
                    Unit = Unit.OrderByDescending(s => s.Description);
                    break;
                case "T3":
                    Unit = Unit.OrderBy(s => s.Description);
                    break;
                case "T4_desc":
                    Unit = Unit.OrderByDescending(s => s.State);
                    break;
                case "T4":
                    Unit = Unit.OrderBy(s => s.State);
                    break;

                default:
                    Unit = Unit.OrderBy(s => s.UnitName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Unit.ToPagedList(pageNumber, pageSize));
        }


        // GET: Units/Details/5
        
        [Authorize(Roles = "Unit_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // GET: Units/Create
        
        [Authorize(Roles = "Unit_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Unit_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,UnitName,sign,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Unit unit)
        {
            unit.Creator = User.Identity.GetUserId();
            unit.AddDate = DateTime.Now;
            unit.LastModificationDate = DateTime.Now;
            unit.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Unit.Add(unit);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unit);
        }

        // GET: Units/Edit/5
        
        [Authorize(Roles = "Unit_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Unit_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UnitName,sign,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Unit unit)
        {
            unit.LastModificationDate = DateTime.Now;
            unit.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(unit).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unit);
        }

        // GET: Units/Delete/5
        
        [Authorize(Roles = "Unit_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit unit = await db.Unit.FindAsync(id);
            if (unit == null)
            {
                return HttpNotFound();
            }
            return View(unit);
        }

        // POST: Units/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Unit_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Unit unit = await db.Unit.FindAsync(id);
            db.Unit.Remove(unit);
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
