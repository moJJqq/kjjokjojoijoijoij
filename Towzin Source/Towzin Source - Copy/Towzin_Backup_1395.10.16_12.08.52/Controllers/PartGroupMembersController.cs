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
using Microsoft.AspNet.Identity;

namespace Towzin.Controllers
{
    public class PartGroupMembersController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartGroupMembers
        
        [Authorize(Roles = "PartGroupMember_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.T1SortParm = String.IsNullOrEmpty(sortOrder) ? "T1" : "";
            ViewBag.T2SortParm = sortOrder == "T2" ? "T2_desc" : "T2";
            ViewBag.T3SortParm = sortOrder == "T3" ? "T3_desc" : "T3";
            ViewBag.T4SortParm = sortOrder == "T4" ? "T4_desc" : "T4";
            ViewBag.T4SortParm = sortOrder == "T5" ? "T5_desc" : "T5";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_PartGroupMember_Index = from s in db.VW_PartGroupMember_Index
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_PartGroupMember_Index = VW_PartGroupMember_Index.Where(s => s.Name.Contains(searchString) || s.PartCode.Contains(searchString) || s.PartGroupName.ToString().Contains(searchString) || s.NatureName.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) );
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderByDescending(s => s.PartGroupName);
                    break;
                case "T2_desc":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderBy(s => s.Name);
                    break;
                case "T3_desc":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T3":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderBy(s => s.PartCode);
                    break;
                case "T4_desc":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderByDescending(s => s.Description);
                    break;
                case "T4":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderBy(s => s.Description);
                    break;
                case "T5_desc":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderByDescending(s => s.State);
                    break;
                case "T5":
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderBy(s =>s.State);
                    break;
                default:
                    VW_PartGroupMember_Index = VW_PartGroupMember_Index.OrderBy(s => s.PartGroupName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_PartGroupMember_Index.ToPagedList(pageNumber, pageSize));
        }


        // GET: PartGroupMembers/Details/5
        
        [Authorize(Roles = "PartGroupMember_Detail , admin")]
        public async Task<ActionResult> Details(long? PartID, long? PartGroupID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PartGroupID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroupMember partGroupMember = await db.PartGroupMember.FindAsync(PartID, PartGroupID);
            if (partGroupMember == null)
            {
                return HttpNotFound();
            }
            return View(partGroupMember);
        }

        // GET: PartGroupMembers/Create
        
        [Authorize(Roles = "PartGroupMember_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.PartGroupID = new SelectList(db.PartGroup, "ID", "PartGroupName");
            return View();
        }

        // POST: PartGroupMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartGroupMember_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,PartGroupID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartGroupMember partGroupMember)
        {
            partGroupMember.Creator = User.Identity.GetUserId();
            partGroupMember.AddDate = DateTime.Now;
            partGroupMember.LastModificationDate = DateTime.Now;
            partGroupMember.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartGroupMember.Add(partGroupMember);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partGroupMember.PartID);
            ViewBag.PartGroupID = new SelectList(db.PartGroup, "ID", "PartGroupName", partGroupMember.PartGroupID);
            return View(partGroupMember);
        }

        // GET: PartGroupMembers/Edit/5
        
        [Authorize(Roles = "PartGroupMember_Edit , admin")]
        public async Task<ActionResult> Edit(long? PartID, long? PartGroupID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PartGroupID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroupMember partGroupMember = await db.PartGroupMember.FindAsync(PartID, PartGroupID);
            if (partGroupMember == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partGroupMember.PartID);
            ViewBag.PartGroupID = new SelectList(db.PartGroup, "ID", "PartGroupName", partGroupMember.PartGroupID);
            return View(partGroupMember);
        }

        // POST: PartGroupMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartGroupMember_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,PartGroupID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartGroupMember partGroupMember)
        {
            partGroupMember.LastModificationDate = DateTime.Now;
            partGroupMember.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partGroupMember).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partGroupMember.PartID);
            ViewBag.PartGroupID = new SelectList(db.PartGroup, "ID", "PartGroupName", partGroupMember.PartGroupID);
            return View(partGroupMember);
        }

        // GET: PartGroupMembers/Delete/5
        
        [Authorize(Roles = "PartGroupMember_Delete , admin")]
        public async Task<ActionResult> Delete(long? PartID, long? PartGroupID)
        {
            if (PartID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (PartGroupID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroupMember partGroupMember = await db.PartGroupMember.FindAsync(PartID, PartGroupID);
            if (partGroupMember == null)
            {
                return HttpNotFound();
            }
            return View(partGroupMember);
        }

        // POST: PartGroupMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartGroupMember_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long? PartID, long? PartGroupID)
        {
            PartGroupMember partGroupMember = await db.PartGroupMember.FindAsync(PartID, PartGroupID);
            db.PartGroupMember.Remove(partGroupMember);
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
