using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using System.Data;
using MIS.Services.Query;

namespace MIS.Controllers.Quey
{
    public class SelectFromServerController : BaseController
    {
        //
        // GET: /SelectFromServer/

        public ActionResult ManageQuery()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManageQuery(FormCollection fc)
        {
            SelectFromServerService objServ = new SelectFromServerService();
            DataTable dt = new DataTable();
            string query = fc["QueryHereId"].ConvertToString();
            dt = objServ.getQuerryResult(query);
            ViewData["result"] = dt;
            return PartialView("_QueryResult");
        }

    }
}
