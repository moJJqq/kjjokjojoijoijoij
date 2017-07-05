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
    public class ProductionLineTechnicalProblemsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: ProductionLineTechnicalProblems
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Index , admin")]
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



            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_ProductionLineTechnicalProblem_Index = from s in db.VW_ProductionLineTechnicalProblem_Index
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.Where(s => s.ProductionLineName.Contains(searchString) || s.TechnicalProblemsName.Contains(searchString) || s.Name.ToString().Contains(searchString) || s.PartCode.ToString().Contains(searchString) || s.OperatorName.ToString().Contains(searchString) || s.DescriptionTechnicalProblem.ToString().Contains(searchString) || s.SolutionTechnicalProblem.ToString().Contains(searchString) || s.Description.ToString().Contains(searchString) || s.State.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.ProductionLineName);
                    break;
                case "T2_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.TechnicalProblemsName);
                    break;
                case "T2":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.TechnicalProblemsName);
                    break;
                case "T3_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.Name);
                    break;
                case "T3":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.Name);
                    break;
                case "T4_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T4":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.PartCode);
                    break;
                case "T5_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.OperatorName);
                    break;
                case "T5":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.OperatorName);
                    break;
                case "T6_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.DescriptionTechnicalProblem);
                    break;
                case "T6":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.DescriptionTechnicalProblem);
                    break;
                case "T7_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.SolutionTechnicalProblem);
                    break;
                case "T7":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.SolutionTechnicalProblem);
                    break;
                case "T8_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.DescriptionTechnicalProblem);
                    break;
                case "T8":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.DescriptionTechnicalProblem);
                    break;
                case "T9_desc":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderByDescending(s => s.State);
                    break;
                case "T9":
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.State);
                    break;

                default:
                    VW_ProductionLineTechnicalProblem_Index = VW_ProductionLineTechnicalProblem_Index.OrderBy(s => s.ProductionLineName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_ProductionLineTechnicalProblem_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProductionLineTechnicalProblems/Details/5
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLineTechnicalProblem productionLineTechnicalProblem = await db.ProductionLineTechnicalProblem.FindAsync(id);
            if (productionLineTechnicalProblem == null)
            {
                return HttpNotFound();
            }
            return View(productionLineTechnicalProblem);
        }

        // GET: ProductionLineTechnicalProblems/Create
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName");
            ViewBag.TechnicalProblemID = new SelectList(db.TechnicalProblem, "ID", "TechnicalProblemsName");
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName");
            return View();
        }

        // POST: ProductionLineTechnicalProblems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,ProductionLineID,TechnicalProblemID,PartID,DescriptionTechnicalProblem,SolutionTechnicalProblem,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductionLineTechnicalProblem productionLineTechnicalProblem)
        {
            productionLineTechnicalProblem.Creator = User.Identity.GetUserId();
            productionLineTechnicalProblem.AddDate = DateTime.Now;
            productionLineTechnicalProblem.LastModificationDate = DateTime.Now;
            productionLineTechnicalProblem.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.ProductionLineTechnicalProblem.Add(productionLineTechnicalProblem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productionLineTechnicalProblem.PartID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", productionLineTechnicalProblem.ProductionLineID);
            ViewBag.TechnicalProblemID = new SelectList(db.TechnicalProblem, "ID", "TechnicalProblemsName", productionLineTechnicalProblem.TechnicalProblemID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productionLineTechnicalProblem.OperatorID);
            return View(productionLineTechnicalProblem);
        }

        // GET: ProductionLineTechnicalProblems/Edit/5
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLineTechnicalProblem productionLineTechnicalProblem = await db.ProductionLineTechnicalProblem.FindAsync(id);
            if (productionLineTechnicalProblem == null)
            {
                return HttpNotFound();
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productionLineTechnicalProblem.PartID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", productionLineTechnicalProblem.ProductionLineID);
            ViewBag.TechnicalProblemID = new SelectList(db.TechnicalProblem, "ID", "TechnicalProblemsName", productionLineTechnicalProblem.TechnicalProblemID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productionLineTechnicalProblem.OperatorID);
            return View(productionLineTechnicalProblem);
        }

        // POST: ProductionLineTechnicalProblems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ProductionLineID,TechnicalProblemID,PartID,DescriptionTechnicalProblem,SolutionTechnicalProblem,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductionLineTechnicalProblem productionLineTechnicalProblem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productionLineTechnicalProblem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productionLineTechnicalProblem.PartID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", productionLineTechnicalProblem.ProductionLineID);
            ViewBag.TechnicalProblemID = new SelectList(db.TechnicalProblem, "ID", "TechnicalProblemsName", productionLineTechnicalProblem.TechnicalProblemID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productionLineTechnicalProblem.OperatorID);
            return View(productionLineTechnicalProblem);
        }

        // GET: ProductionLineTechnicalProblems/Delete/5
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductionLineTechnicalProblem productionLineTechnicalProblem = await db.ProductionLineTechnicalProblem.FindAsync(id);
            if (productionLineTechnicalProblem == null)
            {
                return HttpNotFound();
            }
            return View(productionLineTechnicalProblem);
        }

        // POST: ProductionLineTechnicalProblems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductionLineTechnicalProblem_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProductionLineTechnicalProblem productionLineTechnicalProblem = await db.ProductionLineTechnicalProblem.FindAsync(id);
            db.ProductionLineTechnicalProblem.Remove(productionLineTechnicalProblem);
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
