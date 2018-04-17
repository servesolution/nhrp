using MIS.Models.GIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using MIS.Services.GIS;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using System.Linq;
using MIS.Services.Core;
using ExceptionHandler;

namespace MIS.Controllers
{
    public class GISController : BaseController
    {
        //
        // GET: /GIS/
        CommonFunction objCommon;
        public GISController()
        {
            objCommon = new CommonFunction();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewData["ddl_SearchCrieria"] = getSearchType("E");
            ViewData["ddDistrict"] = objCommon.GetDistricts("");
            ViewData["ddl_currentWard"] = objCommon.GetWardByVDCMun("", "");
            ViewData["ddl_currentVDCMun"] = objCommon.GetNewVDCMunByDistrict("", "");
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc, string id)
        {
            ViewData["ddl_SearchCrieria"] = getSearchType(id);
            ViewData["ddDistrict"] = objCommon.GetDistricts("");
            ViewData["ddl_currentWard"] = objCommon.GetWardByVDCMun("", "");
            ViewData["ddl_currentVDCMun"] = objCommon.GetNewVDCMunByDistrict("", "");
             
            return View();

        }
        public ActionResult GetMap()
        {
            return View();
        }
        public List<SelectListItem> getSearchType(string selval)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();

            item.Text = Utils.GetLabel("Enrolled");
            item.Value = "T";
            if (selval == "T")
                item.Selected = true;
            listItems.Add(item);
            item = new SelectListItem();
            item.Text = Utils.GetLabel("Payment");
            item.Value = "P";
            if (selval == "P")
                item.Selected = true;
            listItems.Add(item);
            item = new SelectListItem();
            item.Text = Utils.GetLabel("Inspection");
            item.Value = "I";
            if (selval == "I")
                item.Selected = true;
            listItems.Add(item);
           
            item = new SelectListItem();
            item.Text = Utils.GetLabel("Vulnerable");
            item.Value = "V";
            if (selval == "V")
                item.Selected = true;
            listItems.Add(item);
            item = new SelectListItem();
            item.Text = Utils.GetLabel("Reverification");
            item.Value = "R";
            if (selval == "R")
                item.Selected = true;
            listItems.Add(item);

            return listItems;
        }
        
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Gettargetings()
        {

            GISServices objServices = new GISServices();
            JsonResult targetings = null;
            try
            {
                var targetingGeoJson = new TargetingJson();
                List<TargetingGeometry> targetingFeatureList = new List<TargetingGeometry>();
                targetingFeatureList = objServices.getCoordination();
                targetings = Json(targetingFeatureList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

            }


            return targetings;

        }

        public JsonResult Gettargetings3(string id, string id2, string id3, string PANumber)
        {

            GISServices objServices = new GISServices();
            JsonResult targetings = null;
            try
            {
                var targetingGeoJson = new TargetingJson();
                List<TargetingCor> targetingFeatureList = new List<TargetingCor>();
                targetingFeatureList = objServices.getCoor(id3, id, id2);
                targetings = Json(targetingFeatureList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            } 
            return targetings;

        }
        public JsonResult GetPAReport(  string PANumber)
        {
            GISServices objServices = new GISServices();
            JsonResult targetings = null;
            try
            {
                var targetingGeoJson = new TargetingJson();
                List<TargetingCor> targetingFeatureList = new List<TargetingCor>();
                targetingFeatureList = objServices.getCoor("", "", "","",PANumber);
                targetings = Json(targetingFeatureList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return targetings;

        }
        public JsonResult GetNepal()
        {
            GISServices objServices = new GISServices();
            //get the Json filepath  
            string file = Server.MapPath("~/data/Nepal-districts.txt");
            //deserialize JSON from file  
            string Json1 = System.IO.File.ReadAllText(file);
            IEnumerable<NepalJson> NepalJson = JsonConvert.DeserializeObject<IEnumerable<NepalJson>>(Json1);              
            JsonResult nepals = null;
            try
            {
                nepals = Json(NepalJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return nepals;             
        }
        public JsonResult getVDC(string id)
        {
            GISServices objServices = new GISServices();
            //get the Json filepath  
            string file = Server.MapPath("~/data/vdc.txt");
            //deserialize JSON from file  
            string Json1 = System.IO.File.ReadAllText(file);

            VDCJson VdcFeatures = JsonConvert.DeserializeObject<VDCJson>(Json1);
            IEnumerable<VDCFeature> obj = VdcFeatures.features;

            IEnumerable<VDCFeature> objj = (from item in obj
                                            where item.properties.did == id
                                            select item);
            obj = objj.ToList();

            VdcFeatures.features = obj;
            List<summaryDataJson> summaryData = new List<summaryDataJson>();
            JsonResult targetings = null;
            targetings = Json(VdcFeatures, JsonRequestBehavior.AllowGet);
            try
            {
                summaryData = objServices.getSummaryData(id);
                VdcFeatures.Summary = summaryData;
                targetings = Json(VdcFeatures, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            
            return targetings;
        }
        public JsonResult getSVDC(string id, string vid)
        {
            GISServices objServices = new GISServices();
            //get the Json filepath  
            string file = Server.MapPath("~/data/vdc.txt");
            //deserialize JSON from file  
            string Json1 = System.IO.File.ReadAllText(file);

            VDCJson VdcFeatures = JsonConvert.DeserializeObject<VDCJson>(Json1);
            IEnumerable<VDCFeature> obj = VdcFeatures.features;

            IEnumerable<VDCFeature> objj = (from item in obj
                                            where item.properties.code == vid && item.properties.did == id
                                            select item);
            obj = objj.ToList();

            VdcFeatures.features = obj;
            List<summaryDataJson> summaryData = new List<summaryDataJson>();
            JsonResult targetings = null;
            targetings = Json(VdcFeatures, JsonRequestBehavior.AllowGet);

            try
            {
                summaryData = objServices.getSummaryData(id, vid);
                VdcFeatures.Summary = summaryData;
                targetings = Json(VdcFeatures, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }

            return targetings;
        }

        public JsonResult Road(string id, string vid)
        {
            GISServices objServices = new GISServices();
            //get the Json filepath  
            string file = Server.MapPath("~/data/RoadLine2.txt");
            //deserialize JSON from file  
            string Json1 = System.IO.File.ReadAllText(file);

            IEnumerable<RoadGeometry> RoadJson = JsonConvert.DeserializeObject<IEnumerable<RoadGeometry>>(Json1);
             
            IEnumerable<RoadGeometry> objj = null;
            objj = (from item in RoadJson
                    where item.DCode == id
                    select item);
           


            RoadJson = objj.ToList(); 
            JsonResult roads = null;
            try
            {

                roads = Json(RoadJson, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return roads;
        }
        public JsonResult getSearchReport(string distType, string vdcType, string searchType, string PANumber)
        {
            CommonService obj1 = new CommonService();
            string distGIS = "";
            string vdcGIS = "";
            JsonResult targetings = null;
            try
            {
                if (vdcType != "")
                {
                    distGIS = obj1.GetFieldNameFromCode("MIS_DISTRICT", "DISTRICT_CD", "GISID", distType);
                    vdcGIS = obj1.GetFieldNameFromCode("NHRS_NMUNICIPALITY", "DEFINED_CD", "GIS_VDC_ID", vdcType); 
                    //vdcGIS = obj1.GetGISCodeForVDC(vdcType);
                    targetings = getSVDC(distGIS, vdcGIS);
                }
                else if (distType != "")
                {
                    distGIS = obj1.GetFieldNameFromCode("MIS_DISTRICT", "DISTRICT_CD", "GISID", distType);
                    targetings = getVDC(distGIS);
                }
                //else
                //{
                //    targetings = GetNepal();
                    
                //}
            }
            catch(Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            } 

            return targetings;
        }
        public JsonResult getSearchRoad(string distType, string vdcType)
        {
            CommonService obj1 = new CommonService();
            string distGIS = "";
            string vdcGIS = "";
            JsonResult roads = null;
            try
            {
                distGIS = obj1.GetFieldNameFromCode("MIS_DISTRICT", "DISTRICT_CD", "GISID", distType);
                vdcGIS = obj1.GetGISCodeForVDC(vdcType);
                roads = Road(distGIS, vdcGIS);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }


            return roads;
        }
        public ActionResult GIS()
        {
            return RedirectToAction("", "GIS");
        }

        public JsonResult GetAllSummaryData()
        {
            GISServices objServices = new GISServices();
            List<summaryDataJson> summaryData = new List<summaryDataJson>();
            JsonResult targetings = null;
            VDCJson VdcFeatures = new VDCJson();

            try
            {
                summaryData = objServices.getSummaryData("");
                VdcFeatures.Summary = summaryData;
                targetings = Json(VdcFeatures, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return targetings;
        }

    }
}
