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
    public class TechnicalProblemsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: TechnicalProblems
        
        [Authorize(Roles = "TechnicalProblem_Index , admin")]
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

            var TechnicalProblem = from s in db.TechnicalProblem
                       select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                TechnicalProblem = TechnicalProblem.Where(s => s.TechnicalProblemsName.Contains(searchString) || s.TechnicalProblemsLatinName.Contains(searchString) || s.Description.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    TechnicalProblem = TechnicalProblem.OrderByDescending(s => s.TechnicalProblemsName);
                    break;
                case "T2_desc":
                    TechnicalProblem = TechnicalProblem.OrderByDescending(s => s.TechnicalProblemsLatinName);
                    break;
                case "T2":
                    TechnicalProblem = TechnicalProblem.OrderBy(s => s.TechnicalProblemsLatinName);
                    break;
                case "T3_desc":
                    TechnicalProblem = TechnicalProblem.OrderByDescending(s => s.Description);
                    break;
                case "T3":
                    TechnicalProblem = TechnicalProblem.OrderBy(s => s.Description);
                    break;
                case "T4_desc":
                    TechnicalProblem = TechnicalProblem.OrderByDescending(s => s.State);
                    break;
                case "T4":
                    TechnicalProblem = TechnicalProblem.OrderBy(s => s.State);
                    break;

                default:
                    TechnicalProblem = TechnicalProblem.OrderBy(s => s.TechnicalProblemsName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(TechnicalProblem.ToPagedList(pageNumber, pageSize));
        }

        // GET: TechnicalProblems/Details/5
        
        [Authorize(Roles = "TechnicalProblem_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalProblem technicalProblem = await db.TechnicalProblem.FindAsync(id);
            if (technicalProblem == null)
            {
                return HttpNotFound();
            }
            return View(technicalProblem);
        }

        // GET: TechnicalProblems/Create
        
        [Authorize(Roles = "TechnicalProblem_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TechnicalProblems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "TechnicalProblem_Create, admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,TechnicalProblemsName,TechnicalProblemsLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] TechnicalProblem technicalProblem)
        {
            technicalProblem.Creator = User.Identity.GetUserId();
            technicalProblem.AddDate = DateTime.Now;
            technicalProblem.LastModificationDate = DateTime.Now;
            technicalProblem.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.TechnicalProblem.Add(technicalProblem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(technicalProblem);
        }

        // GET: TechnicalProblems/Edit/5
        
        [Authorize(Roles = "TechnicalProblem_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalProblem technicalProblem = await db.TechnicalProblem.FindAsync(id);
            if (technicalProblem == null)
            {
                return HttpNotFound();
            }
            return View(technicalProblem);
        }

        // POST: TechnicalProblems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "TechnicalProblem_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,TechnicalProblemsName,TechnicalProblemsLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] TechnicalProblem technicalProblem)
        {
            technicalProblem.LastModificationDate = DateTime.Now;
            technicalProblem.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(technicalProblem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(technicalProblem);
        }

        // GET: TechnicalProblems/Delete/5
        
        [Authorize(Roles = "TechnicalProblem_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TechnicalProblem technicalProblem = await db.TechnicalProblem.FindAsync(id);
            if (technicalProblem == null)
            {
                return HttpNotFound();
            }
            return View(technicalProblem);
        }

        // POST: TechnicalProblems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "TechnicalProblem_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            TechnicalProblem technicalProblem = await db.TechnicalProblem.FindAsync(id);
            db.TechnicalProblem.Remove(technicalProblem);
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
