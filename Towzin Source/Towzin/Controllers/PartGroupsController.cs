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
    public class PartGroupsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: PartGroups
 
        [Authorize(Roles = "PartGroup_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";
            ViewBag.StateSortParm = sortOrder == "State" ? "State_desc" : "State";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var PartGroup = from s in db.PartGroup
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                PartGroup = PartGroup.Where(s => s.PartGroupName.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    PartGroup = PartGroup.OrderByDescending(s => s.PartGroupName);
                    break;
                case "Description_desc":
                    PartGroup = PartGroup.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    PartGroup = PartGroup.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    PartGroup = PartGroup.OrderByDescending(s => s.State);
                    break;
                case "State":
                    PartGroup = PartGroup.OrderBy(s => s.State);
                    break;
                default:
                    PartGroup = PartGroup.OrderBy(s => s.PartGroupName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(PartGroup.ToPagedList(pageNumber, pageSize));
        }

        // GET: PartGroups/Details/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PartGroup_Detail")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroup partGroup = await db.PartGroup.FindAsync(id);
            if (partGroup == null)
            {
                return HttpNotFound();
            }
            return View(partGroup);
        }

        // GET: PartGroups/Create
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "PartGroup_Create")]

        public ActionResult Create()
        {
            return View();
        }

        // POST: PartGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        
        [Authorize(Roles = "PartGroup_Create , admin")]

        public async Task<ActionResult> Create([Bind(Include = "ID,PartGroupName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartGroup partGroup)
        {
            partGroup.Creator = User.Identity.GetUserId();
            partGroup.AddDate = DateTime.Now;
            partGroup.LastModificationDate = DateTime.Now;
            partGroup.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.PartGroup.Add(partGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(partGroup);
        }

        // GET: PartGroups/Edit/5
        
        [Authorize(Roles = "PartGroup_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroup partGroup = await db.PartGroup.FindAsync(id);
            if (partGroup == null)
            {
                return HttpNotFound();
            }
            return View(partGroup);
        }

        // POST: PartGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "PartGroup_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,PartGroupName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] PartGroup partGroup)
        {
            partGroup.LastModificationDate = DateTime.Now;
            partGroup.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(partGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(partGroup);
        }

        // GET: PartGroups/Delete/5
        
        [Authorize(Roles = "PartGroup_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartGroup partGroup = await db.PartGroup.FindAsync(id);
            if (partGroup == null)
            {
                return HttpNotFound();
            }
            return View(partGroup);
        }

        // POST: PartGroups/Delete/5
        
        [Authorize(Roles = "PartGroup_Delete , admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            PartGroup partGroup = await db.PartGroup.FindAsync(id);
            db.PartGroup.Remove(partGroup);
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
