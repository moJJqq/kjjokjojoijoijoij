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
    public class ProductionLinesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: ProductionLines

        [Authorize(Roles = "ProductionLine_Index , admin")]
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

            var ProductionLine = from s in db.ProductionLine
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                ProductionLine = ProductionLine.Where(s => s.ProductionLineName.Contains(searchString) || s.ProductionLineLatinName.Contains(searchString) || s.Description .ToString().Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    ProductionLine = ProductionLine.OrderByDescending(s => s.ProductionLineName);
                    break;
                case "T2_desc":
                    ProductionLine = ProductionLine.OrderByDescending(s => s.ProductionLineLatinName);
                    break;
                case "T2":
                    ProductionLine = ProductionLine.OrderBy(s => s.ProductionLineLatinName);
                    break;
                case "T3_desc":
                    ProductionLine = ProductionLine.OrderByDescending(s => s.Description);
                    break;
                case "T3":
                    ProductionLine = ProductionLine.OrderBy(s => s.Description);
                    break;
                case "T4_desc":
                    ProductionLine = ProductionLine.OrderByDescending(s => s.State);
                    break;
                case "T4":
                    ProductionLine = ProductionLine.OrderBy(s => s.State);
                    break;
                
                default:
                    ProductionLine = ProductionLine.OrderBy(s => s.ProductionLineName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(ProductionLine.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProductionLines/Details/5
        
        [Authorize(Roles = "ProductionLine_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLine productionLine = await db.ProductionLine.FindAsync(id);
            if (productionLine == null)
            {
                return HttpNotFound();
            }
            return View(productionLine);
        }

        // GET: ProductionLines/Create
        
        [Authorize(Roles = "ProductionLine_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductionLines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLine_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProductionLineName,ProductionLineLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] ProductionLine productionLine)
        {
            productionLine.Creator = User.Identity.GetUserId();
            productionLine.AddDate = DateTime.Now;
            productionLine.LastModificationDate = DateTime.Now;
            productionLine.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.ProductionLine.Add(productionLine);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(productionLine);
        }

        // GET: ProductionLines/Edit/5
        
        [Authorize(Roles = "ProductionLine_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLine productionLine = await db.ProductionLine.FindAsync(id);
            if (productionLine == null)
            {
                return HttpNotFound();
            }
            return View(productionLine);
        }

        // POST: ProductionLines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLine_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProductionLineName,ProductionLineLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] ProductionLine productionLine)
        {
            productionLine.LastModificationDate = DateTime.Now;
            productionLine.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(productionLine).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(productionLine);
        }

        // GET: ProductionLines/Delete/5
         
        [Authorize(Roles = "ProductionLine_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLine productionLine = await db.ProductionLine.FindAsync(id);
            if (productionLine == null)
            {
                return HttpNotFound();
            }
            return View(productionLine);
        }

        // POST: ProductionLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLine_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProductionLine productionLine = await db.ProductionLine.FindAsync(id);
            db.ProductionLine.Remove(productionLine);
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
