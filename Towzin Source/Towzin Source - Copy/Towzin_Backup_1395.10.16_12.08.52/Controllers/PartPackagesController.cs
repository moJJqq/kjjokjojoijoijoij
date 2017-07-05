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
    public class PartPackagesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartPackages
        
        [Authorize(Roles = "PartPackage_Index , admin")]
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
            ViewBag.T8SortParm = sortOrder == "T8" ? "T8_desc" : "T8";
   


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_PartPackage_Index = from s in db.VW_PartPackage_Index
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_PartPackage_Index = VW_PartPackage_Index.Where(s => s.PartCode.Contains(searchString) || s.Name.Contains(searchString) || s.PackageName.ToString().Contains(searchString) || s.CountOfDestribution.ToString().Contains(searchString) || s.Value.ToString().Contains(searchString) || s.UnitName.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T2":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.PartCode);
                    break;
                case "T3_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.PackageName);
                    break;
                case "T3":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.PackageName);
                    break;
                case "T4_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.CountOfDestribution);
                    break;
                case "T4":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.CountOfDestribution);
                    break;
                case "T5_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.Value);
                    break;
                case "T5":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.Value);
                    break;
                case "T6_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.UnitName);
                    break;
                case "T6":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.UnitName);
                    break;
                case "T7_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.Description);
                    break;
                case "T7":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.Description);
                    break;
                case "T8_desc":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderByDescending(s => s.State);
                    break;
                case "T8":
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.State);
                    break;

                default:
                    VW_PartPackage_Index = VW_PartPackage_Index.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_PartPackage_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: PartPackages/Details/5
        
        [Authorize(Roles = "PartPackage_Detail , admin")]

        public async Task<ActionResult> Details(long? PartID, long? PackageID,long? UnitID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(UnitID==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PartPackage partPackage = await db.PartPackage.FindAsync(PartID,PackageID,UnitID);
            if (partPackage == null)
            {
                return HttpNotFound();
            }
            return View(partPackage);
        }

        // GET: PartPackages/Create
        
        [Authorize(Roles = "PartPackage_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName");
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName");
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            return View();
        }

        // POST: PartPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackage_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,PackageID,CountOfDestribution,UnitID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartPackage partPackage)
        {
            partPackage.Creator = User.Identity.GetUserId();
            partPackage.AddDate = DateTime.Now;
            partPackage.LastModificationDate = DateTime.Now;
            partPackage.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartPackage.Add(partPackage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackage.UnitID);
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackage.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackage.PartID);
            return View(partPackage);
        }

        // GET: PartPackages/Edit/5
        
        [Authorize(Roles = "PartPackage_Edit , admin")]
        public async Task<ActionResult> Edit(long? PartID, long? PackageID, long? UnitID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (UnitID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartPackage partPackage = await db.PartPackage.FindAsync(PartID, PackageID, UnitID);
            if (partPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackage.UnitID);
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackage.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackage.PartID);
            return View(partPackage);
        }

        // POST: PartPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackage_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,PackageID,CountOfDestribution,UnitID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartPackage partPackage)
        {
            partPackage.LastModificationDate = DateTime.Now;
            partPackage.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partPackage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackage.UnitID);
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackage.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackage.PartID);
            return View(partPackage);
        }

        // GET: PartPackages/Delete/5
        
        [Authorize(Roles = "PartPackage_Delete , admin")]
        public async Task<ActionResult> Delete(long? PartID, long? PackageID, long? UnitID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (UnitID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartPackage partPackage = await db.PartPackage.FindAsync(PartID, PackageID, UnitID);
            if (partPackage == null)
            {
                return HttpNotFound();
            }
            return View(partPackage);
        }

        // POST: PartPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackage_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long? PartID, long? PackageID, long? UnitID)
        {
            PartPackage partPackage = await db.PartPackage.FindAsync(PartID, PackageID, UnitID);
            db.PartPackage.Remove(partPackage);
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
