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
    public class ProductiveDetailsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: ProductiveDetails
     
        [Authorize(Roles = "ProductiveDetail_Index , admin")]
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
            ViewBag.T13SortParm = sortOrder == "T13" ? "T13_desc" : "T13";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var VW_ProductiveDetails_Index = from s in db.VW_ProductiveDetails_Index
                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.Where(s => s.OrderCodeID.ToString().Contains(searchString) || s.PartCode.ToString().Contains(searchString) || s.Name.ToString().Contains(searchString) || s.IO.ToString().Contains(searchString) || s.FromOrderCodeID.ToString().Contains(searchString) || s.ToOrderCodeID.ToString().Contains(searchString) || s.BatchNumber.ToString().Contains(searchString) || s.FormoulaNumber.ToString().Contains(searchString) || s.LesionsName.ToString().Contains(searchString) || s.Amount.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "T1_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.OrderCodeID);
                    break;
                case "T2_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.Name);
                    break;
                case "T2":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.Name);
                    break;
                case "T3_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "T3":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.PartCode);
                    break;
                case "T4_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.OperatorName);
                    break;
                case "T4":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.OperatorName);
                    break;
                case "T5_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.IO);
                    break;
                case "T5":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.IO);
                    break;
                case "T6_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.FromOrderCodeID);
                    break;
                case "T6":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.FromOrderCodeID);
                    break;
                case "T7_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.ToOrderCodeID);
                    break;
                case "T7":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.ToOrderCodeID);
                    break;
                case "T8_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.BatchNumber);
                    break;
                case "T8":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.BatchNumber);
                    break;
                case "T9_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.FormoulaNumber);
                    break;
                case "T9":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.FormoulaNumber);
                    break;
                case "T10_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.Waste);
                    break;
                case "T10":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.Waste);
                    break;
                case "T11_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.LesionsName);
                    break;
                case "T11":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.LesionsName);
                    break;

                case "T12_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.Amount);
                    break;
                case "T12":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.Amount);
                    break;
                case "T13_desc":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderByDescending(s => s.State);
                    break;
                case "T13":
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.State);
                    break;
                default:
                    VW_ProductiveDetails_Index = VW_ProductiveDetails_Index.OrderBy(s => s.OrderCodeID);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_ProductiveDetails_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: ProductiveDetails/Details/5
        
        [Authorize(Roles = "ProductiveDetail_Detail, admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveDetails productiveDetails = await db.ProductiveDetails.FindAsync(id);
            if (productiveDetails == null)
            {
                return HttpNotFound();
            }
            return View(productiveDetails);
        }

        // GET: ProductiveDetails/Create
        
        [Authorize(Roles = "ProductiveDetail_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.LesionID = new SelectList(db.Lesion, "ID", "LesionsName");
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode");
            ViewBag.FromOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode");
            ViewBag.ToOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode");
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName");
            ViewBag.IO = new SelectList(db.ProductiveState, "ID", "Name");
            return View();
        }

        // POST: ProductiveDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveDetail_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrderCodeID,PartID,IO,FromOrderCodeID,ToOrderCodeID,BatchNumber,FormoulaNumber,Waste,LesionID,Amount,ProductiveRowDetails,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductiveDetails productiveDetails)
        {
            productiveDetails.Creator = User.Identity.GetUserId();
            productiveDetails.AddDate = DateTime.Now;
            productiveDetails.LastModificationDate = DateTime.Now;
            productiveDetails.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.ProductiveDetails.Add(productiveDetails);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.LesionID = new SelectList(db.Lesion, "ID", "LesionsName", productiveDetails.LesionID);
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.OrderCodeID);
            ViewBag.FromOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.FromOrderCodeID);
            ViewBag.ToOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.ToOrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productiveDetails.PartID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveDetails.OperatorID);
            ViewBag.IO = new SelectList(db.ProductiveState, "ID", "Name", productiveDetails.IO);
            return View(productiveDetails);
        }

        // GET: ProductiveDetails/Edit/5
        
        [Authorize(Roles = "ProductiveDetail_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveDetails productiveDetails = await db.ProductiveDetails.FindAsync(id);
            if (productiveDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.LesionID = new SelectList(db.Lesion, "ID", "LesionsName", productiveDetails.LesionID);
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.OrderCodeID);
            ViewBag.FromOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.FromOrderCodeID);
            ViewBag.ToOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.ToOrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productiveDetails.PartID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveDetails.OperatorID);
            ViewBag.IO = new SelectList(db.ProductiveState, "ID", "Name", productiveDetails.IO);
            return View(productiveDetails);
        }

        // POST: ProductiveDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveDetail_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrderCodeID,PartID,IO,FromOrderCodeID,ToOrderCodeID,BatchNumber,FormoulaNumber,Waste,LesionID,Amount,ProductiveRowDetails,State,Creator,AddDate,LastModifier,LastModificationDate,Version,OperatorID")] ProductiveDetails productiveDetails)
        {
 
            productiveDetails.LastModificationDate = DateTime.Now;
            productiveDetails.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(productiveDetails).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.LesionID = new SelectList(db.Lesion, "ID", "LesionsName", productiveDetails.LesionID);
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.OrderCodeID);
            ViewBag.FromOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.FromOrderCodeID);
            ViewBag.ToOrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", productiveDetails.ToOrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", productiveDetails.PartID);
            ViewBag.OperatorID = new SelectList(db.Operator, "OperatorID", "OperatorName", productiveDetails.OperatorID);
            ViewBag.IO = new SelectList(db.ProductiveState, "ID", "Name", productiveDetails.IO);
            return View(productiveDetails);
        }

        // GET: ProductiveDetails/Delete/5
        
        [Authorize(Roles = "ProductiveDetail_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductiveDetails productiveDetails = await db.ProductiveDetails.FindAsync(id);
            if (productiveDetails == null)
            {
                return HttpNotFound();
            }
            return View(productiveDetails);
        }

        // POST: ProductiveDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "ProductiveDetail_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProductiveDetails productiveDetails = await db.ProductiveDetails.FindAsync(id);
            db.ProductiveDetails.Remove(productiveDetails);
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
