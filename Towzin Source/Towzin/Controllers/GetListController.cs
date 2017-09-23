using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Towzin.Models;

namespace Towzin.Views.GetList
{
    
    public class GetListController : Controller
    {
        int i = 0;
        private TowzinEntities1 db = new TowzinEntities1();
        // GET: GetList
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string Prefix)
        {
            return View();
        }
        [HttpPost]
        public JsonResult Part(string Prefix)
        {
            
            //Note : you can bind same list from database
            
            //Searching records from list using LINQ query
            List<Part> ObjList = new List<Part>();

            var Part = from s in db.Part
                                select s;
            if (!String.IsNullOrEmpty(Prefix))
            { 
                Part = Part.Where(s => s.Name.Contains(Prefix));
            }
            return Json(Part, JsonRequestBehavior.AllowGet);

        }
    }
}