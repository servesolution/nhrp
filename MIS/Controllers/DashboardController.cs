using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Reflection;
using MIS.Authentication;
using System.Net; 
using EntityFramework;
using MIS.Services.Report;

namespace MIS.Controllers
{
    public class DashboardController : BaseController
    {
        
        public DashboardController()
        {          
            List<SelectListItem> Districts = GetData.AllDistricts(Utils.ToggleLanguage("english","nepali"));
        }

        [CustomAuthorizeAttribute(PermCd = PermissionCode.List)]
        [HttpGet]
        public ActionResult Dashboard()
        {
            DataImportReportService objService = new DataImportReportService();
            if (CommonVariables.MembershipID != null)
            {
                ViewData["MemberID"] = CommonVariables.MembershipID;
            } 
            return View();
        }    



    }
}
