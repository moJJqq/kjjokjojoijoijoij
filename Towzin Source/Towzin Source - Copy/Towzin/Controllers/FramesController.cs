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
    public class FramesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Frames
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortOrder"></param>
        /// <param name="currentFilter"></param>
        /// <param name="searchString"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        
        [Authorize(Roles ="Frame_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FrameNameSortParm = String.IsNullOrEmpty(sortOrder) ? "frameName_desc" : "";
            ViewBag.FrameLatinNameSortParm = sortOrder == "frameLatinName" ? "frameLatinName_desc" : "frameLatinName";
            ViewBag.FrameSerialSortParm = sortOrder == "frameSerial" ? "frameSerial_desc" : "frameSerial";
            ViewBag.FrameDescSortParm = sortOrder == "frameDescription" ? "frameDescription_desc" : "frameDescription";
            ViewBag.FrameStateSortParm = sortOrder == "frameState" ? "frameState_desc" : "frameState";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var frame = from s in db.Frame
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                frame = frame.Where(s => s.FrameName.Contains(searchString)
                                       || s.FrameLatinName.Contains(searchString) || s.FrameSerial.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "frameName_desc":
                    frame = frame.OrderByDescending(s => s.FrameName);
                    break;
                case "frameLatinName_desc":
                    frame = frame.OrderByDescending(s => s.FrameLatinName);
                    break;
                case "frameLatinName":
                    frame = frame.OrderBy(s => s.FrameLatinName);
                    break;
                case "frameSerial_desc":
                    frame = frame.OrderByDescending(s => s.FrameSerial);
                    break;
                case "frameSerial":
                    frame = frame.OrderBy(s => s.FrameSerial);
                    break;
                case "frameDescription_desc":
                    frame = frame.OrderByDescending(s => s.Description);
                    break;
                case "frameDescription":
                    frame = frame.OrderBy(s => s.Description);
                    break;
                case "frameState_desc":
                    frame = frame.OrderByDescending(s => s.State);
                    break;
                case "frameState":
                    frame = frame.OrderBy(s => s.State);
                    break;
                default:
                    frame = frame.OrderBy(s => s.FrameName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(frame.ToPagedList(pageNumber, pageSize));
           
        }

        // GET: Frames/Details/5
        
        [Authorize(Roles = "Frame_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frame frame = await db.Frame.FindAsync(id);
            if (frame == null)
            {
                return HttpNotFound();
            }
            return View(frame);
        }

        // GET: Frames/Create
        
        [Authorize(Roles = "Frame_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Frames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Frame_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,FrameName,FrameLatinName,FrameSerial,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Frame frame)
        {
            frame.Creator = User.Identity.GetUserId();
            frame.AddDate = DateTime.Now;
            frame.LastModificationDate = DateTime.Now;
            frame.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Frame.Add(frame);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(frame);
        }

        // GET: Frames/Edit/5
        
        [Authorize(Roles = "Frame_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frame frame = await db.Frame.FindAsync(id);
            if (frame == null)
            {
                return HttpNotFound();
            }
            return View(frame);
        }

        // POST: Frames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Frame_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,FrameName,FrameLatinName,FrameSerial,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Frame frame)
        {
            frame.LastModificationDate = DateTime.Now;
            frame.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(frame).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(frame);
        }

        // GET: Frames/Delete/5
        
        [Authorize(Roles = "Frame_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Frame frame = await db.Frame.FindAsync(id);
            if (frame == null)
            {
                return HttpNotFound();
            }
            return View(frame);
        }

        // POST: Frames/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Frame_Delete , admin")]

        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Frame frame = await db.Frame.FindAsync(id);
            db.Frame.Remove(frame);
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
