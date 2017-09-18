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
    public class FilterExportRayfanCompondController : Controller
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
       
        public ActionResult ExportExcel(string SearchString)
        {
            TempData["SearchString"] = SearchString;

            return Report("EXCEL");
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
            string path = Path.Combine(Server.MapPath("~/Report"), "ExportRayfan.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<VW_ReportExportCompand> cm = new List<VW_ReportExportCompand>();
            using (TowzinEntities1 dc = new TowzinEntities1())
            {
                cm = dc.VW_ReportExportCompand.ToList();
                var SearchString  = TempData["SearchString"];
                if (SearchString.ToString() != "")
                {
                    ////gooogle Search Algoritm
                    string[] Words;
                    Words = new string[20];
                    uint x = 0;
                    string searchString = SearchString.ToString();
                    for (int i = 0; i < searchString.Count(); i++)
                    {
                        string w = searchString.Substring(i, 1);

                        if (w != "+")
                        {
                            Words[x] = Words[x] + w;
                        }
                        else
                        {
                            x = x + 1;
                        }
                    }
                    for (int i = 0; i <= x; i++)
                    {

                        cm = cm.Where(s => s.OrderCodeID.ToString().ToLower().Contains(Words[i].Trim().ToLower()) || s.FormoulaNumber.ToLower().Contains(Words[i].Trim().ToLower()) || s.PartCode.ToLower().Contains(Words[i].Trim().ToLower()) || s.ToOrderCodeID.ToString().ToLower().Contains(Words[i].Trim().ToLower()) || s.Expr1.ToString().ToLower().Contains(Words[i].Trim().ToLower()) ).ToList();
                    }
                    //cm = dc.VW_ProductiveDetails_Index.Where(s => s.OrderCodeID.ToString().Contains(SearchString.ToString())|| s.ProductionLineName.Contains(SearchString.ToString()) || s.PartCode.Contains(searchString) || s.AddDateShamsi.Contains(searchString)).ToList();
                }
                else
                {
                    
                    cm = dc.VW_ReportExportCompand.ToList();
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
            "  <PageWidth>29.7cm</PageWidth>" +
            "  <PageHeight>21cm</PageHeight>" +
            "  <MarginTop>0.5cm</MarginTop>" +
            "  <MarginLeft>1cm</MarginLeft>" +
            "  <MarginRight>1cm</MarginRight>" +
            "  <MarginBottom>0.5cm</MarginBottom>" +
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