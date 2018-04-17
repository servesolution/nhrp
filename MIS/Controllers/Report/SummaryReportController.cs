using ExceptionHandler;
using MIS.Services.Core;
using MIS.Services.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc; 

namespace MIS.Controllers.Report
{
    public class SummaryReportController : Controller
    {
       
        public ActionResult SummaryReportDetail()
        {
             string langFlag = "";
            GetSummaryReportService sr = new GetSummaryReportService();
            DataTable dtsm = new DataTable();
            string lan = string.Empty;
            try
            {
                if (Session["LanguageSetting"].ToString() == "English")
                {
                    lan = "E";
                }
                if (Session["LanguageSetting"].ToString() == "Nepali")
                //else
                {
                     lan = "N";
                }
                
                dtsm = sr.GetSummaryReport(lan);
                ViewData["dtsm"] = dtsm;
                ViewBag.ReportTitle = "Summary Report";
                langFlag = Utils.ToggleLanguage("E", "N");

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return PartialView("_SummaryReport");
        }
            
        }

    }

