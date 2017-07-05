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
    public class OrderPartsController : Controller
    {
        private TowzinEntities1 db = new TowzinEntities1();

        // GET: OrderParts
        
        [Authorize(Roles = "OrderPart_Index , admin")]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.OrderCodeSortParm = String.IsNullOrEmpty(sortOrder) ? "OrderCode_desc" : "";
            ViewBag.CustomerNameSortParm = sortOrder == "CustomerName" ? "CustomerName_desc" : "CustomerName";
            ViewBag.PartCodeeSortParm = sortOrder == "PartCode" ? "PartCode_desc" : "PartCode";
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
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

            var VW_OrderPart_Index = from s in db.VW_OrderPart_Index
                         select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                VW_OrderPart_Index = VW_OrderPart_Index.Where(s =>  s.CustomerName.Contains(searchString) || s.Description.Contains(searchString) || s.Name.Contains(searchString) || s.PartCode.Contains(searchString) || s.OrderCodeID .ToString().Contains(searchString) || s.AmountOfProductive.ToString().Contains(searchString) || s.AmountOfSubUnit.ToString().Contains(searchString) || s.Amount.ToString().Contains(searchString) || s.WasteAmount.ToString().Contains(searchString) || s.RowMaterialAmount.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "OrderCode_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.OrderCodeID);
                    break;
                case "Name_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.Name);
                    break;
                case "CustomerName_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.CustomerName);
                    break;
                case "CustomerName":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.CustomerName);
                    break;
                case "Description_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.Description);
                    break;
                case "Description":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.Description);
                    break;
                case "State_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.State);
                    break;
                case "State":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.State);
                    break;
                case "PartCode_desc":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderByDescending(s => s.PartCode);
                    break;
                case "PartCode":
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.PartCode);
                    break;
                default:
                    VW_OrderPart_Index = VW_OrderPart_Index.OrderBy(s => s.OrderCodeID);
                    break;
            }
            var orderPart = db.OrderPart.Include(o => o.Order).Include(o => o.Part).Include(o => o.Unit);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(VW_OrderPart_Index.ToPagedList(pageNumber, pageSize));

        }

        // GET: OrderParts/Details/5
        
        [Authorize(Roles = "OrderPart_Detail , admin")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPart orderPart = await db.OrderPart.FindAsync(id);
            if (orderPart == null)
            {
                return HttpNotFound();
            }
            return View(orderPart);
        }

        // GET: OrderParts/Create
        
        [Authorize(Roles = "OrderPart_Create , admin")]
        public ActionResult Create()
        {
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode");
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name");
            ViewBag.SubUnitID = new SelectList(db.Unit, "ID", "UnitName");
            return View();
        }

        // POST: OrderParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "OrderPart_Create , admin")]
        public async Task<ActionResult> Create([Bind(Include = "ID,OrderCodeID,PartID,AmountOfProductive,SubUnitID,AmountOfSubUnitID,CustomerName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] OrderPart orderPart)
        {
            orderPart.Creator = User.Identity.GetUserId();
            orderPart.AddDate = DateTime.Now;
            orderPart.LastModificationDate = DateTime.Now;
            orderPart.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.OrderPart.Add(orderPart);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", orderPart.OrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", orderPart.PartID);
            ViewBag.SubUnitID = new SelectList(db.Unit, "ID", "UnitName", orderPart.SubUnitID);
            return View(orderPart);
        }

        // GET: OrderParts/Edit/5
        
        [Authorize(Roles = "OrderPart_Edit , admin")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPart orderPart = await db.OrderPart.FindAsync(id);
            if (orderPart == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", orderPart.OrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", orderPart.PartID);
            ViewBag.SubUnitID = new SelectList(db.Unit, "ID", "UnitName", orderPart.SubUnitID);
            return View(orderPart);
        }

        // POST: OrderParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "OrderPart_Edit , admin")]
        public async Task<ActionResult> Edit([Bind(Include = "ID,OrderCodeID,PartID,AmountOfProductive,SubUnitID,AmountOfSubUnitID,CustomerName,Description,State,Creator,AddDate,LastModifier,LastModificationDate,Version")] OrderPart orderPart)
        {
            orderPart.LastModificationDate = DateTime.Now;
            orderPart.LastModifier = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(orderPart).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderCodeID = new SelectList(db.Order, "OrderCode", "OrderCode", orderPart.OrderCodeID);
            ViewBag.PartID = new SelectList(db.Part, "ID", "Name", orderPart.PartID);
            ViewBag.SubUnitID = new SelectList(db.Unit, "ID", "UnitName", orderPart.SubUnitID);
            return View(orderPart);
        }

        // GET: OrderParts/Delete/5
        
        [Authorize(Roles = "OrderPart_Delete , admin")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPart orderPart = await db.OrderPart.FindAsync(id);
            if (orderPart == null)
            {
                return HttpNotFound();
            }
            return View(orderPart);
        }

        // POST: OrderParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        [Authorize(Roles = "OrderPart_Delete , admin")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            OrderPart orderPart = await db.OrderPart.FindAsync(id);
            db.OrderPart.Remove(orderPart);
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
