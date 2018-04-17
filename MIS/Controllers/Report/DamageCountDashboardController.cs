using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MIS.Services.Report;
using MIS.Services.Core;
using System.Web.Helpers;

namespace MIS.Controllers.Report
{
    public class DamageCountDashboardController : Controller
    {
        //
        // GET: /DamageCountDashboard/
        public DataTable dtbl = null;
        public string getTotalDamageCount(string cType, string rptType, string isHH, string fName)
        {
            getDamageGdCountService objDamagCountService = new getDamageGdCountService();
            string xmlString = "";
            string searchType = cType;
            string langFlag = "";
            try
            {
                ViewBag.ReportTitle = "DamageGrade Coount";
                langFlag = Utils.ToggleLanguage("E", "N");
                dtbl = objDamagCountService.getDamageCount();

                xmlString = dtbl.GetPieChartXml1();

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (fName != "")
            {


                if (Utils.ToggleLanguage("E", "N") == "N")
                {
                    var utf8WithoutBom = new System.Text.UTF8Encoding(true);
                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString, utf8WithoutBom);

                }
                else
                {

                    System.IO.File.WriteAllText(Server.MapPath("/Files/Xml/") + fName, xmlString);
                }
            }
            else
            {
                return "false";
            }
            return "true";
        }

    }
}
