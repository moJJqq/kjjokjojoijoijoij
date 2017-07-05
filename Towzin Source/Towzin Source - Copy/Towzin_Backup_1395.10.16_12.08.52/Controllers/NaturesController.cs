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
    public class NaturesController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Natures
        
        [Authorize(Roles = "Nature_Index , admin")]
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

            var Nature = from s in db.Nature
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Nature = Nature.Where(s => s.NatureName.Contains(searchString)  || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    Nature = Nature.OrderByDescending(s => s.NatureName);
                    break;
                case "Description_desc":
                    Nature = Nature.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    Nature = Nature.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    Nature = Nature.OrderByDescending(s => s.state);
                    break;
                case "State":
                    Nature = Nature.OrderBy(s => s.state);
                    break;
                default:
                    Nature = Nature.OrderBy(s =>s.NatureName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Nature.ToPagedList(pageNumber, pageSize));
        }


        // GET: Natures/Details/5
        
        [Authorize(Roles = "Nature_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Nature.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // GET: Natures/Create
        
        [Authorize(Roles = "Nature_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Natures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Nature_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,NatureName,Description,state,Creator,AddDate,LastModifier,LastModificationDate,Version")] Nature nature)
        {
            nature.Creator = User.Identity.GetUserId();
            nature.AddDate = DateTime.Now;
            nature.LastModificationDate = DateTime.Now;
            nature.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Nature.Add(nature);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nature);
        }

        // GET: Natures/Edit/5
        
        [Authorize(Roles = "Nature_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Nature.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // POST: Natures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Nature_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,NatureName,Description,state,Creator,AddDate,LastModifier,LastModificationDate,Version")] Nature nature)
        {
            nature.LastModificationDate = DateTime.Now;
            nature.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(nature).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nature);
        }

        // GET: Natures/Delete/5
        
        [Authorize(Roles = "Nature_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nature nature = await db.Nature.FindAsync(id);
            if (nature == null)
            {
                return HttpNotFound();
            }
            return View(nature);
        }

        // POST: Natures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Nature_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Nature nature = await db.Nature.FindAsync(id);
            db.Nature.Remove(nature);
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
