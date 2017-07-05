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
    public class PartSpecificationsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartSpecifications
     
        [Authorize(Roles = "PartSpecification_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.T1SortParm = String.IsNullOrEmpty(sortOrder) ? "T1" : "";
            ViewBag.T2SortParm = sortOrder == "T2" ? "T2_desc" : "T2";
            ViewBag.T3SortParm = sortOrder == "T3" ? "T3_desc" : "T3";
            ViewBag.T4SortParm = sortOrder == "T4" ? "T4_desc" : "T4";
            ViewBag.T5SortParm = sortOrder == "T5" ? "T5_desc" : "T5";
            ViewBag.T6SortParm = sortOrder == "T6" ? "T6_desc" : "T6";



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_PartSpecification_Index = from s in db.VW_PartSpecification_Index
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_PartSpecification_Index = VW_PartSpecification_Index.Where(s => s.Name.Contains(searchString) || s.PartCode.Contains(searchString) || s.SpecificationName.ToString().Contains(searchString) || s.Value.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T2":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.PartCode);
                    break;
                case "T3_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.SpecificationName);
                    break;
                case "T3":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.SpecificationName);
                    break;
                case "T4_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.Value);
                    break;
                case "T4":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.Value);
                    break;
                case "T5_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.Description);
                    break;
                case "T5":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.Description);
                    break;
                case "T6_desc":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderByDescending(s => s.State);
                    break;
                case "T6":
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.State);
                    break;
               
                default:
                    VW_PartSpecification_Index = VW_PartSpecification_Index.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_PartSpecification_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: PartSpecifications/Details/5
        
        [Authorize(Roles = "PartSpecification_Detail , admin")]
        public async Task<ActionResult> Details(long? PartID,long? SpecificationID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (SpecificationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSpecification partSpecification = await db.PartSpecification.FindAsync(PartID,SpecificationID);
            if (partSpecification == null)
            {
                return HttpNotFound();
            }
            return View(partSpecification);
        }

        // GET: PartSpecifications/Create
        
        [Authorize(Roles = "PartSpecification_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.SpecificationID = new SelectList(db.Specification, "ID", "SpecificationName");
            return View();
        }

        // POST: PartSpecifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartSpecification_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,SpecificationID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartSpecification partSpecification)
        {
            partSpecification.Creator = User.Identity.GetUserId();
            partSpecification.AddDate = DateTime.Now;
            partSpecification.LastModificationDate = DateTime.Now;
            partSpecification.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartSpecification.Add(partSpecification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partSpecification.PartID);
            ViewBag.SpecificationID = new SelectList(db.Specification, "ID", "SpecificationName", partSpecification.SpecificationID);
            return View(partSpecification);
        }

        // GET: PartSpecifications/Edit/5
        
        [Authorize(Roles = "PartSpecification_Edit , admin")]
        public async Task<ActionResult> Edit(long? PartID, long? SpecificationID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (SpecificationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSpecification partSpecification = await db.PartSpecification.FindAsync(PartID, SpecificationID);
            if (partSpecification == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partSpecification.PartID);
            ViewBag.SpecificationID = new SelectList(db.Specification, "ID", "SpecificationName", partSpecification.SpecificationID);
            return View(partSpecification);
        }

        // POST: PartSpecifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartSpecification_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,SpecificationID,Value,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartSpecification partSpecification)
        {
            partSpecification.LastModificationDate = DateTime.Now;
            partSpecification.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partSpecification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partSpecification.PartID);
            ViewBag.SpecificationID = new SelectList(db.Specification, "ID", "SpecificationName", partSpecification.SpecificationID);
            return View(partSpecification);
        }

        // GET: PartSpecifications/Delete/5
        
        [Authorize(Roles = "PartSpecification_Delete , admin")]
        public async Task<ActionResult> Delete(long? PartID, long? SpecificationID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (SpecificationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartSpecification partSpecification = await db.PartSpecification.FindAsync(PartID, SpecificationID);
            if (partSpecification == null)
            {
                return HttpNotFound();
            }
            return View(partSpecification);
        }

        // POST: PartSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartSpecification_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long? PartID, long? SpecificationID)
        {
            PartSpecification partSpecification = await db.PartSpecification.FindAsync(PartID, SpecificationID);
            db.PartSpecification.Remove(partSpecification);
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
