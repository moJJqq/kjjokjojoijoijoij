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
    public class PartFramesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartFrames
        
        [Authorize(Roles = "PartFrame_Index , admin")]
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

            var VW_PartFrame_Index = from s in db.VW_PartFrame_Index
                                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_PartFrame_Index = VW_PartFrame_Index.Where(s => s.Name.Contains(searchString) || s.PartCode.Contains(searchString) || s.FrameName.ToString().Contains(searchString) || s.FrameSerial.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T2":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.PartCode);
                    break;
                case "T3_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.FrameName);
                    break;
                case "T3":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.FrameName);
                    break;
                case "T4_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.FrameSerial);
                    break;
                case "T4":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.FrameSerial);
                    break;
                case "T5_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.Description);
                    break;
                case "T5":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.Description);
                    break;
                case "T6_desc":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderByDescending(s => s.State);
                    break;
                case "T6":
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.State);
                    break;
                default:
                    VW_PartFrame_Index = VW_PartFrame_Index.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_PartFrame_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: PartFrames/Details/5
        
        [Authorize(Roles = "PartFrame_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartFrame partFrame = await db.PartFrame.FindAsync(id);
            if (partFrame == null)
            {
                return HttpNotFound();
            }
            return View(partFrame);
        }

        // GET: PartFrames/Create
        
        [Authorize(Roles = "PartFrame_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName");
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            return View();
        }

        // POST: PartFrames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartFrame_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,PartID,FrameID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartFrame partFrame)
        {
            partFrame.Creator = User.Identity.GetUserId();
            partFrame.AddDate = DateTime.Now;
            partFrame.LastModificationDate = DateTime.Now;
            partFrame.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartFrame.Add(partFrame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", partFrame.FrameID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partFrame.PartID);
            return View(partFrame);
        }

        // GET: PartFrames/Edit/5
        
        [Authorize(Roles = "PartFrame_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartFrame partFrame = await db.PartFrame.FindAsync(id);
            if (partFrame == null)
            {
                return HttpNotFound();
            }
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", partFrame.FrameID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partFrame.PartID);
            return View(partFrame);
        }

        // POST: PartFrames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartFrame_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartID,FrameID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartFrame partFrame)
        {
            partFrame.LastModificationDate = DateTime.Now;
            partFrame.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partFrame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", partFrame.FrameID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", partFrame.PartID);
            return View(partFrame);
        }

        // GET: PartFrames/Delete/5
        
        [Authorize(Roles = "PartFrame_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartFrame partFrame = await db.PartFrame.FindAsync(id);
            if (partFrame == null)
            {
                return HttpNotFound();
            }
            return View(partFrame);
        }

        // POST: PartFrames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartFrame_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            PartFrame partFrame = await db.PartFrame.FindAsync(id);
            db.PartFrame.Remove(partFrame);
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
