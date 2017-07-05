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
    public class PartsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Parts
        
        [Authorize(Roles = "Part_Index , admin")]
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
            ViewBag.T9SortParm = sortOrder == "T9" ? "T9_desc" : "T9";
            ViewBag.T10SortParm = sortOrder == "T10" ? "T10_desc" : "T10";
            ViewBag.T11SortParm = sortOrder == "T11" ? "T11_desc" : "T11";
            ViewBag.T12SortParm = sortOrder == "T12" ? "T12_desc" : "T12";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_Part_Index = from s in db.VW_Part_Index
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_Part_Index = VW_Part_Index.Where(s => s.Name.Contains(searchString) || s.LatinName.Contains(searchString) || s.PartCode.ToString().Contains(searchString) || s.WasteName.ToString().Contains(searchString) || s.WastePartCode.ToString().Contains(searchString) || s.NatureName.ToString().Contains(searchString) || s.Length.ToString().Contains(searchString) || s.Width.ToString().Contains(searchString) || s.Height.ToString().Contains(searchString) || s.Weight.ToString().Contains(searchString) || s.UnitName.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.LatinName);
                    break;
                case "T2":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.LatinName);
                    break;
                case "T3_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T3":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.PartCode);
                    break;
                case "T4_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.WasteName);
                    break;
                case "T4":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.WasteName);
                    break;
                case "T5_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.WastePartCode);
                    break;
                case "T5":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.WastePartCode);
                    break;
                case "T6_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.NatureName);
                    break;
                case "T6":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.NatureName);
                    break;
                case "T7_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Length);
                    break;
                case "T7":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Length);
                    break;
                case "T8_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Width);
                    break;
                case "T8":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Width);
                    break;
                case "T9_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Height);
                    break;
                case "T9":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Height);
                    break;
                case "T10_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Weight);
                    break;
                case "T10":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Weight);
                    break;
                case "T11_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.Description);
                    break;
                case "T11":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Description);
                    break;

                case "T12_desc":
                    VW_Part_Index = VW_Part_Index.OrderByDescending(s => s.State);
                    break;
                case "T12":
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.State);
                    break;
                default:
                    VW_Part_Index = VW_Part_Index.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_Part_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: Parts/Details/5
        
        [Authorize(Roles = "Part_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await db.Part.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // GET: Parts/Create
        
        [Authorize(Roles = "Part_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.NatureID = new SelectList(db.Nature, "ID", "NatureName");
            ViewBag.PartWesteID = new SelectList(db.Part, "ID", "Name");
            ViewBag.MajorUntiID = new SelectList(db.Unit, "ID", "UnitName");
            return View();
        }

        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Part_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,LatinName,PartCode,PartWesteID,NatureID,Length,Width,Height,Weight,MajorUntiID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Part part)
        {
            part.Creator = User.Identity.GetUserId();
            part.AddDate = DateTime.Now;
            part.LastModificationDate = DateTime.Now;
            part.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Part.Add(part);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.NatureID = new SelectList(db.Nature, "ID", "NatureName", part.NatureID);
            ViewBag.PartWesteID = new SelectList(db.Part, "ID", "Name", part.PartWesteID);
            ViewBag.MajorUntiID = new SelectList(db.Unit, "ID", "UnitName", part.MajorUntiID);
            return View(part);
        }

        // GET: Parts/Edit/5
        
        [Authorize(Roles = "Part_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await db.Part.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            ViewBag.NatureID = new SelectList(db.Nature, "ID", "NatureName", part.NatureID);
            ViewBag.PartWesteID = new SelectList(db.Part, "ID", "Name", part.PartWesteID);
            ViewBag.MajorUntiID = new SelectList(db.Unit, "ID", "UnitName", part.MajorUntiID);
            return View(part);
        }

        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Part_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,LatinName,PartCode,PartWesteID,NatureID,Length,Width,Height,Weight,MajorUntiID,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Part part)
        {
            part.LastModificationDate = DateTime.Now;
            part.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.NatureID = new SelectList(db.Nature, "ID", "NatureName", part.NatureID);
            ViewBag.PartWesteID = new SelectList(db.Part, "ID", "Name", part.PartWesteID);
            ViewBag.MajorUntiID = new SelectList(db.Unit, "ID", "UnitName", part.MajorUntiID);
            return View(part);
        }

        // GET: Parts/Delete/5
        
        [Authorize(Roles = "Part_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part part = await db.Part.FindAsync(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Part_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Part part = await db.Part.FindAsync(id);
            db.Part.Remove(part);
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
