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
    public class OperatorsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Operators
        
        [Authorize(Roles = "Operator_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.LatinNameSortParm = sortOrder == "LatinName" ? "LatinName_desc" : "LatinName";
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

            var Operator = from s in db.Operator
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                Operator = Operator.Where(s => s.OperatorLatinName.Contains(searchString) || s.OperatorName.Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name_desc":
                    Operator = Operator.OrderByDescending(s => s.OperatorName);
                    break;
                case "LatinName_desc":
                    Operator = Operator.OrderByDescending(s => s.OperatorLatinName);
                    break;
                case "LatinName":
                    Operator = Operator.OrderBy(s => s.OperatorLatinName);
                    break;
                case "Description_desc":
                    Operator = Operator.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    Operator = Operator.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    Operator = Operator.OrderByDescending(s => s.State);
                    break;
                case "State":
                    Operator = Operator.OrderBy(s => s.State);
                    break;
                default:
                    Operator = Operator.OrderBy(s => s.OperatorName);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(Operator.ToPagedList(pageNumber, pageSize));
        }

        // GET: Operators/Details/5
        
        [Authorize(Roles = "Operator_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = await db.Operator.FindAsync(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // GET: Operators/Create
        
        [Authorize(Roles = "Operator_Create , admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Operators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Operator_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "OperatorID,OperatorName,OperatorLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Operator @operator)
        {
            @operator.Creator = User.Identity.GetUserId();
            @operator.AddDate = DateTime.Now;
            @operator.LastModificationDate = DateTime.Now;
            @operator.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Operator.Add(@operator);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(@operator);
        }

        // GET: Operators/Edit/5
        
        [Authorize(Roles = "Operator_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = await db.Operator.FindAsync(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // POST: Operators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Operator_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "OperatorID,OperatorName,OperatorLatinName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Operator @operator)
        {
            @operator.LastModificationDate = DateTime.Now;
            @operator.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(@operator).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@operator);
        }

        // GET: Operators/Delete/5
        
        [Authorize(Roles = "Operator_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Operator @operator = await db.Operator.FindAsync(id);
            if (@operator == null)
            {
                return HttpNotFound();
            }
            return View(@operator);
        }

        // POST: Operators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Operator_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Operator @operator = await db.Operator.FindAsync(id);
            db.Operator.Remove(@operator);
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
