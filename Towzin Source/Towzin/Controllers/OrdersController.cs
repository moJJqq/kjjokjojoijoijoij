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
    public class OrdersController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: Orders
        
        [Authorize(Roles = "Order_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.OrderStatusSortParm = sortOrder == "OrderStatus" ? "OrderStatus_desc" : "LatinName";
            ViewBag.ProductionLineNameSortParm = sortOrder == "ProductionLineName" ? "ProductionLineName_desc" : "ProductionLineName";
            ViewBag.FrameNameSortParm = sortOrder == "FrameName" ? "FrameName_desc" : "FrameName";
            ViewBag.FrameSerialSortParm = sortOrder == "FrameSerial" ? "FrameSerial_desc" : "FrameSerial";
            ViewBag.StartDateSortParm = sortOrder == "StartDate" ? "StartDate_desc" : "StartDate";
            ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "EndDate_desc" : "EndDate";
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

            var VW_Order_Index = from s in db.VW_Order_Index
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_Order_Index = VW_Order_Index.Where(s => s.OrderCode.ToString().Contains(searchString) || s.StatusTitle.Contains(searchString) || s.ProductionLineName.Contains(searchString) || s.FrameName.Contains(searchString) || s.FrameSerial.Contains(searchString) || s.StartDate.Contains(searchString) || s.EndDate.Contains(searchString) || s.Amount.ToString().Contains(searchString) || s.AmountOfProductive.ToString().Contains(searchString) || s.AmountOfSubUnitID.ToString().Contains(searchString) || s.WasteAmont.ToString().Contains(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "OrderCode_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.OrderCode);
                    break;
                case "OrderStatus_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.StatusTitle);
                    break;
                case "OrderStatus":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.StatusTitle);
                    break;
                case "ProductionLineName_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.ProductionLineName);
                    break;
                case "ProductionLineName":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.ProductionLineName);
                    break;
                case "FrameName_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.FrameName);
                    break;
                case "FrameName":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.FrameName);
                    break;
                case "FrameSerial_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.FrameSerial);
                    break;
                case "FrameSerial":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.FrameSerial);
                    break;
                case "StartDate_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.StartDate);
                    break;
                case "StartDate":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.StartDate);
                    break;
                case "EndDate_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.EndDate);
                    break;
                case "EndDate":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.EndDate);
                    break;
                case "Description_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    VW_Order_Index = VW_Order_Index.OrderByDescending(s => s.State);
                    break;
                case "State":
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.State);
                    break;
                default:
                    VW_Order_Index = VW_Order_Index.OrderBy(s => s.OrderCode);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_Order_Index.ToPagedList(pageNumber, pageSize));
        }

        // GET: Orders/Details/5
        
        [Authorize(Roles = "Order_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Order.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        
        [Authorize(Roles = "Order_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName");
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "StatusTitle");
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Order_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrderCode,OrderStatusID,FrameID,ProductionLineID,StartDate,EndDate,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Order order)
        {

            order.Creator = User.Identity.GetUserId();
            order.AddDate = DateTime.Now;
            order.LastModificationDate = DateTime.Now;
            order.LastModifier = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Order.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", order.FrameID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "StatusTitle", order.OrderStatusID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", order.ProductionLineID);
            return View(order);
        }

        // GET: Orders/Edit/5
        
        [Authorize(Roles = "Order_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Order.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", order.FrameID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "StatusTitle", order.OrderStatusID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", order.ProductionLineID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "Order_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrderCode,OrderStatusID,FrameID,ProductionLineID,StartDate,EndDate,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] Order order)
        {
            order.LastModificationDate = DateTime.Now;
            order.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FrameID = new SelectList(db.Frame, "ID", "FrameName", order.FrameID);
            ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "ID", "StatusTitle", order.OrderStatusID);
            ViewBag.ProductionLineID = new SelectList(db.ProductionLine, "ID", "ProductionLineName", order.ProductionLineID);
            return View(order);
        }

        // GET: Orders/Delete/5
        
        [Authorize(Roles = "Order_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Order.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        
        [Authorize(Roles = "Order_Delete , admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Order order = await db.Order.FindAsync(id);
            db.Order.Remove(order);
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
