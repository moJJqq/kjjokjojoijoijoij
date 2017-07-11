using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using Towzin.Models;

namespace Towzin.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Simple()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Simple(string OrderCode)
        {
            TempData["OrderCode"] = OrderCode;
            return View();
        }
        public ActionResult FromLoadFileReport()
        {
            
            StiReport report = new StiReport();
            string Path = Server.MapPath("~" + ("//Content//OrderSummery.mrt"));
            report.Load(Path);
           if (TempData["OrderCode"]!= null)
            {
                report.Dictionary.Variables["OrderCode"].Value = TempData["OrderCode"].ToString();
            }
            report.Compile();
            // report["Date"] = "Now Date :" + DateTime.Now.ToShortDateString() + " Design By:barnamenevisan.Org";
            return StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
        }
        public ActionResult PrintReport()
        {

            return StiMvcViewer.PrintReportResult(this.HttpContext);

        }
        public ActionResult ExportReport()
        {
            return StiMvcViewer.ExportReportResult(this.HttpContext);
        }




    }
}
