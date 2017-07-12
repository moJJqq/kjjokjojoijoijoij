using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Towzin.Models;
namespace Towzin.Controllers
{
    public class sssController : Controller
    {
        // GET: sss
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderSummery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult OrderSummery(string SearchString)
        {
            TempData["SearchString"] = SearchString;
            
            return Report("PDF");
        }
        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "OrderSummery.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<VW_ProductiveDetails_Index> cm = new List<VW_ProductiveDetails_Index>();
            using (TowzinEntities1 dc = new TowzinEntities1())
            {
                var SearchString = TempData["SearchString"];
                if (SearchString != null)
                {
                    string searchString = SearchString.ToString();
                    cm = dc.VW_ProductiveDetails_Index.Where(s => s.OrderCodeID.ToString().Contains(SearchString.ToString())|| s.ProductionLineName.Contains(SearchString.ToString()) || s.PartCode.Contains(searchString)).ToList();
                }
                else
                {
                    cm = dc.VW_ProductiveDetails_Index.ToList();
                }
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }


    }
}