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
using PagedList;

namespace Towzin.Controllers
{
    public class ProductiveStoppagesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: ProductiveStoppages
        
        [Authorize(Roles = "ProductiveStoppages_Index , admin")]
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
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_Productive_Stoppages_Index = from s in db.VW_Productive_Stoppages_Index
                                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.Where(s => s.OrderCodeID.ToString().Contains(searchString) || s.StoppagesName.Contains(searchString) || s.OperatorName.ToString().Contains(searchString) || s.StartTime.ToString().Contains(searchString) || s.EndTime.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.OrderCodeID);
                    break;
                case "T2_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.StoppagesName);
                    break;
                case "T2":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.StoppagesName);
                    break;
                case "T3_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.OperatorName);
                    break;
                case "T3":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.OperatorName);
                    break;
                case "T4_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.StartTime);
                    break;
                case "T4":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.StartTime);
                    break;
                case "T5_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.EndTime);
                    break;
                case "T5":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.EndTime);
                    break;
                case "T6_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.Description);
                    break;
                case "T6":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.Description);
                    break;
                case "T7_desc":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderByDescending(s => s.State);
                    break;
                case "T7":
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.State);
                    break;
                default:
                    VW_Productive_Stoppages_Index = VW_Productive_Stoppages_Index.OrderBy(s => s.OrderCodeID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_Productive_Stoppages_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProductiveStoppages/Details/5
        
        [Authorize(Roles = "ProductiveStoppages_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveStoppages productiveStoppages = await db.ProductiveStoppages.FindAsync(id);
            if (productiveStoppages == null)
            {
                return HttpNotFound();
            }
            return View(productiveStoppages);
        }

        // GET: ProductiveStoppages/Create
        
        [Authorize(Roles = "ProductiveStoppages_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode");
            ViewBag.StoppagesID = new SelectList(db.Stoppages, "ID", "StoppagesName");
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName");
            return View();
        }

        // POST: ProductiveStoppages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveStoppages_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,StoppagesID,OrderCodeID,StartTime,EndTime,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductiveStoppages productiveStoppages)
        {
            if (ModelState.IsValid)
            {
                db.ProductiveStoppages.Add(productiveStoppages);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveStoppages.OrderCodeID);
            ViewBag.StoppagesID = new SelectList(db.Stoppages, "ID", "StoppagesName", productiveStoppages.StoppagesID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveStoppages.OperatorID);
            return View(productiveStoppages);
        }

        // GET: ProductiveStoppages/Edit/5
        
        [Authorize(Roles = "ProductiveStoppages_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveStoppages productiveStoppages = await db.ProductiveStoppages.FindAsync(id);
            if (productiveStoppages == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveStoppages.OrderCodeID);
            ViewBag.StoppagesID = new SelectList(db.Stoppages, "ID", "StoppagesName", productiveStoppages.StoppagesID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveStoppages.OperatorID);
            return View(productiveStoppages);
        }

        // POST: ProductiveStoppages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveStoppages_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StoppagesID,OrderCodeID,StartTime,EndTime,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductiveStoppages productiveStoppages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productiveStoppages).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveStoppages.OrderCodeID);
            ViewBag.StoppagesID = new SelectList(db.Stoppages, "ID", "StoppagesName", productiveStoppages.StoppagesID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveStoppages.OperatorID);
            return View(productiveStoppages);
        }

        // GET: ProductiveStoppages/Delete/5
        
        [Authorize(Roles = "ProductiveStoppages_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveStoppages productiveStoppages = await db.ProductiveStoppages.FindAsync(id);
            if (productiveStoppages == null)
            {
                return HttpNotFound();
            }
            return View(productiveStoppages);
        }

        // POST: ProductiveStoppages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveStoppages_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProductiveStoppages productiveStoppages = await db.ProductiveStoppages.FindAsync(id);
            db.ProductiveStoppages.Remove(productiveStoppages);
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
