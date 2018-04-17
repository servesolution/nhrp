using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using MIS.Services.Inspection;

namespace MIS.Controllers.Inspection
{
    public class InspectionConflictedFileController : BaseController
    {
        //
        // GET: /InspectionConflictedFile/

        public ActionResult DuplicateInspection()
        {
            InspectionConflictedFileService objService = new InspectionConflictedFileService();
            DataTable dt = new DataTable();
            string dist="";
            string vdc="";
            string fileName="";
            dt = objService.GetConflictedInspection(  dist,   vdc,   fileName);
            ViewData["Result"] = dt;
            return View();
        }
        public ActionResult InspectionApplicationList()
        {
            InspectionConflictedFileService objService = new InspectionConflictedFileService();
            DataTable dt = new DataTable();
            string message = "";
            dt = objService.GetInspectionApplicationList(message);
            ViewData["Result"] = dt;
            return View();
        }

        

    }
}
