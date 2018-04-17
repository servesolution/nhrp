using ExceptionHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MIS.Controllers.CallAPI
{
    public class InspectionAPICallController : Controller
    {
        //
        // GET: /InspectionAPICall/

        public ActionResult Index()
        {
            string data = "";
            string result = "";
            string filepath = Server.MapPath("~/Files/CSV/");
            try
            {
                if (Directory.Exists(filepath))
                {
                    List<string> jsonFiles = Directory.GetDirectories(filepath, "*", System.IO.SearchOption.AllDirectories).Select(path => Path.GetFileName(path)).ToList();
                    foreach (string s in jsonFiles)
                    {
                        data = System.IO.File.ReadAllText(s);
                        Uri uri1 = new Uri("http://localhost:8088/api/inspectionapi?username=admin&password=admin");

                        WebClient client = new WebClient();
                        // var result=client.
                        result += client.UploadString(uri1, data);

                    }
                }
            }
            catch(Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
           
            ViewData["ErrorMessage"] = result;
            return View();
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string data = "";
            //string filename = file.FileName;
            //data = System.IO.File.ReadAllText(filename);
            try
            {
                for (int k = 0; k < Request.Files.Count; k++)
                {
                    file = Request.Files[k];
                    if (file.ContentLength == 0)
                        continue;
                    string FilePath = "~/Files/Inspection/";
                    if (System.IO.File.Exists(Server.MapPath(FilePath) + Path.GetFileName(file.FileName)))
                    {
                        System.IO.File.Delete(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                    }
                    file.SaveAs(Server.MapPath(FilePath) + Path.GetFileName(file.FileName));
                    string pathh = Server.MapPath(FilePath) + Path.GetFileName(file.FileName);
                    data = System.IO.File.ReadAllText(pathh);
                }

                Uri uri1 = new Uri("http://localhost:8088/api/inspectionapi?username=admin&password=admin");

                WebClient client = new WebClient();
                // var result=client.
                var result = client.UploadString(uri1, data);
                ViewData["ErrorMessage"] = result;
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
          
            
            return View();
        }

    }
}
