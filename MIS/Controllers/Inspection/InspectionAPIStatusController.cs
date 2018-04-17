using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.Inspection
{
    public class InspectionAPIStatusController : BaseController
    {
        //
        // GET: /InspectionAPIStatus/
        InspectionImportJsonService objInspectionJsonService = new InspectionImportJsonService();
        public ActionResult ShowRecord()
        {
            DataTable dt = new DataTable();
            //dt=objInspectionJsonService.GetInspectionAPICount();
            ViewData["Record"] = dt;
            return View();
        }

    }
}
