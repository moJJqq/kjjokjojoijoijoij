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
    public class PartPackageDetailsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartPackageDetails
        
        [Authorize(Roles = "PartPackageDetail_Index , admin")]
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

            var VW_PartPackageDetail = from s in db.VW_PartPackageDetail
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_PartPackageDetail = VW_PartPackageDetail.Where(s => s.PackageName.Contains(searchString) || s.Name.Contains(searchString) || s.PartCode.ToString().Contains(searchString) || s.UnitName.ToString().Contains(searchString) || s.Value.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.PackageName);
                    break;
                case "T2_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.Name);
                    break;
                case "T2":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.Name);
                    break;
                case "T3_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.PartCode);
                    break;
                case "T3":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.PartCode);
                    break;
                case "T4_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.UnitName);
                    break;
                case "T4":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.UnitName);
                    break;
                case "T5_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.Value);
                    break;
                case "T5":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.Value);
                    break;
                case "T6_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.Description);
                    break;
                case "T6":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.Description);
                    break;
                case "T7_desc":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderByDescending(s => s.State);
                    break;
                case "T7":
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.State);
                    break;
              
                default:
                    VW_PartPackageDetail = VW_PartPackageDetail.OrderBy(s => s.PackageName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_PartPackageDetail.ToPagedList(pageNumber, pageSize));
        }

        // GET: PartPackageDetails/Details/5
        
        [Authorize(Roles = "PartPackageDetail_Detail , admin")]
        public async Task<ActionResult> Details(long? PartID, long? PackageID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartPackageDetail partPackageDetail = await db.PartPackageDetail.FindAsync(PartID, PackageID);
            if (partPackageDetail == null)
            {
                return HttpNotFound();
            }
            return View(partPackageDetail);
        }

        // GET: PartPackageDetails/Create
        
        [Authorize(Roles = "PartPackageDetail_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName");
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName");
            return View();
        }

        // POST: PartPackageDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackageDetail_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,PackageID,UnitID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartPackageDetail partPackageDetail)
        {
            partPackageDetail.Creator = User.Identity.GetUserId();
            partPackageDetail.AddDate = DateTime.Now;
            partPackageDetail.LastModificationDate = DateTime.Now;
            partPackageDetail.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartPackageDetail.Add(partPackageDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackageDetail.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackageDetail.PartID);
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackageDetail.UnitID);
            return View(partPackageDetail);
        }

        // GET: PartPackageDetails/Edit/5
        
        [Authorize(Roles = "PartPackageDetail_Edit , admin")]
        public async Task<ActionResult> Edit(long? PartID, long? PackageID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartPackageDetail partPackageDetail = await db.PartPackageDetail.FindAsync(PartID, PackageID);
            if (partPackageDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackageDetail.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackageDetail.PartID);
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackageDetail.UnitID);
            return View(partPackageDetail);
        }

        // POST: PartPackageDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackageDetail_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,PackageID,UnitID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartPackageDetail partPackageDetail)
        {
            partPackageDetail.LastModificationDate = DateTime.Now;
            partPackageDetail.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partPackageDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PackageID = new SelectList(db.Package, "ID", "PackageName", partPackageDetail.PackageID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partPackageDetail.PartID);
            ViewBag.UnitID = new SelectList(db.Unit, "ID", "UnitName", partPackageDetail.UnitID);
            return View(partPackageDetail);
        }

        // GET: PartPackageDetails/Delete/5
        
        [Authorize(Roles = "PartPackageDetail_Delete , admin")]
        public async Task<ActionResult> Delete(long? PartID, long? PackageID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PackageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartPackageDetail partPackageDetail = await db.PartPackageDetail.FindAsync(PartID, PackageID);
            if (partPackageDetail == null)
            {
                return HttpNotFound();
            }
            return View(partPackageDetail);
        }

        // POST: PartPackageDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartPackageDetail_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long? PartID, long? PackageID)
        {
            PartPackageDetail partPackageDetail = await db.PartPackageDetail.FindAsync(PartID, PackageID);
            db.PartPackageDetail.Remove(partPackageDetail);
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
