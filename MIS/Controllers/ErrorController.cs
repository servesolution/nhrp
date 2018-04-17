using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ExceptionHandler;
using System.Data.OracleClient;
namespace MIS.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return View();
        }
        public ViewResult Unknown()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            ViewData["ErrorMessage"] =  "Application Level Error Found";
            if(Session["ExcelError"] != null)
                ViewData["ErrorMessage"] = "Application Level Error Found <br/> " + Session["ExcelError"].ToString();
            Session["ExcelError"] = null;
            //if (Session["Error_Message"] != null)
            //{
            //    ViewData["ErrorMessage"] = Session["Error_Message"].ToString();// "Unknown Error Found";
            //}
            return View();
        }

        public ViewResult NotFound(string path)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            ViewData["ErrorMessage"] =  "Page Not Found";
            //if (Session["Error_Message"]!=null)
            //{
            //    ViewData["ErrorMessage"] = Session["Error_Message"].ToString();// "Page Not Found";
            //}
            return View();
        }
    }
}
