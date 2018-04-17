using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MIS.Services.Core;
using ExceptionHandler;
using System.Data.OracleClient;
using MIS.Models.Core;
using MIS.Models.Setup;
using System.IO;
using MIS.Services.Setup;
using ClosedXML.Excel;
using ClosedXML;
using System.Data;
namespace MIS.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        CommonFunction commonfun = null;
        CommonService commonServ = null;
        public CommonController()
        {
            commonfun = new CommonFunction();
            commonServ = new CommonService();
        }
        public ActionResult GetCasteGroup(string id)
        {
            List<SelectListItem> listLayer = new List<SelectListItem>();
            if (id != null)
            {
                listLayer = commonfun.GetCasteGroup("", true);
            }
            ViewBag.layerData = listLayer;
            return PartialView("../Shared/_getLayer");
        }
        public ActionResult getLayer(string id, string districtid, string zoneid, string vdcid)
        {
            CommonFunction common = new CommonFunction();
            ViewBag.LayerType = id;
            ViewBag.layerData = common.getLayer(id, Utils.ConvertToString(districtid), Utils.ConvertToString(zoneid), "");
            CommonVariables.LayerName = Utils.GetLabel("Search " + id);
            return PartialView("../Shared/_getLayer");
        }
        public ActionResult getFilteredDataCasteGroup(string id, string desc)
        {
            CommonService common = new CommonService();
            List<SelectListItem> listLayer = new List<SelectListItem>();
            List<MISCommon> lstCasteGroup = new List<MISCommon>();
            lstCasteGroup = common.GetCasteGroupbyCodeandDesc(id, desc);
            if (lstCasteGroup != null)
            {
                foreach (MISCommon obj in lstCasteGroup)
                {
                    listLayer.Add(new SelectListItem { Value = Convert.ToString(obj.Code), Text = obj.Description });
                }
            }
            ViewBag.FilteredData = listLayer;
            return PartialView("../Shared/_getFilteredData");
        }
        public JsonResult GetHours()
        {
            return commonfun.GetHours("");
        }
        public JsonResult GetMinutes()
        {
            return commonfun.GetMinutes("");
        }
        public JsonResult GetYesNo()
        {
            return commonfun.GetYesNo("");
        }
        public JsonResult GetYesNo1()
        {
            return commonfun.GetYesNo1("");
        }
        public JsonResult GetYesNo2()
        {
            return commonfun.GetYesNo2("");
        }
        public JsonResult FillZone(string isPopup)
        {
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            if (isPopup == "false")
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });

            selLstZone.AddRange(GetData.AllZones(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstZone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterRegion(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterRegion(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterZone(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterZone(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillDistrict(string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });

            selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillColumnName(string columnnamecd, string isPopup)
        {
            List<SelectListItem> selColumnName = new List<SelectListItem>();

            if (isPopup == "true")
                selColumnName.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Column Name") + "---" });
            selColumnName.AddRange(commonfun.GetAllColumnNameOfTable(columnnamecd));
            return new JsonResult { Data = selColumnName, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult FillDistrictbyZone(string zoneDefCd, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            if (zoneDefCd.ConvertToString() != "")
            {
                selLstDistricts.AddRange(GetData.DistrictbyZone(zoneDefCd, Utils.ToggleLanguage("english", "nepali")));
            }
            else
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")));
            }
            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult FillDistrictbyBatchId(string Batchid, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            if (Batchid.ConvertToString() != "")
            {
                selLstDistricts.AddRange(commonfun.GetDistrictByBatchID(Batchid));
            }
            else
            {
                selLstDistricts.AddRange(commonfun.GetDistrictByBatchID(Batchid));
            }
            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillDistrictbyRetroBatchId(string Batchid, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            if (Batchid.ConvertToString() != "")
            {
                selLstDistricts.AddRange(commonfun.GetDistrictByRetroBatchID(Batchid));
            }
            else
            {
                selLstDistricts.AddRange(commonfun.GetDistrictByRetroBatchID(Batchid));
            }
            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillGrievanceBatchIdByDistrict(string DistrictCd, string WardNo, string VDCCd, string isPopup)
        {
            List<SelectListItem> selLstBatch = new List<SelectListItem>();
            //if (isPopup == "false")
            //    selLstBatch.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Batch ID") + "---" });

            CommonFunction common = new CommonFunction();
            string VDCCode = common.GetCodeFromDataBase(VDCCd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            selLstBatch.AddRange(commonfun.GetBatchIDByAddress(VDCCode, WardNo, DistrictCd));

            return new JsonResult { Data = selLstBatch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult FilterDistrict(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterDistrict(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterZonebyRegState(string code, string desc, string RegStateDefCd, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            //Pratik Dai Code
            //if (!String.IsNullOrEmpty(RegStateDefCd))
            //{
            //    filteredResults = GetData.FilterZonebyRegState(code, desc, RegStateDefCd, Utils.ToggleLanguage("english", "nepali"));
            //}

            //My Code
            filteredResults = GetData.FilterZonebyRegState(code, desc, RegStateDefCd, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterDistrictbyZone(string code, string desc, string filterMode, string zoneDefCd)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterDistrictbyZone(code, desc, zoneDefCd, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public JsonResult FillCasteByCasteGroup(string CasteGrpDfdCd, string isPopup)
        {
            List<SelectListItem> selLstCaste = new List<SelectListItem>();
            if (isPopup == "false")
                selLstCaste.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Caste") + "---" });
            if (CasteGrpDfdCd.ConvertToString() != "")
            {
                selLstCaste.AddRange(GetData.CasteByCasteGrp(CasteGrpDfdCd, Utils.ToggleLanguage("english", "nepali")));
            }
            //else
            //{
            //    selLstCaste.AddRange(GetData.AllCastes(Utils.ToggleLanguage("english", "nepali")));
            //}
            return new JsonResult { Data = selLstCaste, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        #region User Specfic
        // User Specfic 

        public ActionResult FilterDistrictbyUser(string code, string desc, string filterMode, string zoneDefCd)
        {
            string DistDefCd = Session["UserDistrictDefCd"].ConvertToString();
            string groupFlag = Session["UserGroupFlag"].ToString();
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterDistrictbyZone(code, desc, zoneDefCd, Utils.ToggleLanguage("english", "nepali")).ToList();
            if (groupFlag == "V" || groupFlag == "D")
            {
                filteredResults = filteredResults.Where(d => d.Value == DistDefCd).ToList();
            }
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterZonebyUser(string code, string desc, string filterMode, string RegionDefCd)
        {
            string groupFlag = Session["UserGroupFlag"].ToString();
            string ZoneDefCd = Session["UserZoneDefCd"].ConvertToString();
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterZonebyRegState(code, desc, RegionDefCd, Utils.ToggleLanguage("english", "nepali")).ToList();
            if (groupFlag == "V" || groupFlag == "D")
            {
                filteredResults = filteredResults.Where(d => d.Value == ZoneDefCd).ToList();
            }
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterVDCMunbyUser(string code, string desc, string filterMode, string districtDefCd)
        {
            string VDCMunDefCd = Session["UserVdcMunDefCd"].ConvertToString();
            string groupFlag = Session["UserGroupFlag"].ToString();
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterVdc(code, desc, districtDefCd, Utils.ToggleLanguage("english", "nepali")).ToList();
            if (groupFlag == "V")
            {
                filteredResults = filteredResults.Where(d => d.Value == VDCMunDefCd).ToList();
            }
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillZoneByUser(string regStateDefCd, string isPopup)
        {
            string groupFlag = Session["UserGroupFlag"].ConvertToString();
            string ZoneDefCd = Session["UserZoneDefCd"].ConvertToString();
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            if (isPopup == "false" && groupFlag == "C")
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Zone") + "---" });
            if (!string.IsNullOrWhiteSpace(regStateDefCd))
                selLstZone.AddRange(GetData.ZonebyRegState(GetData.GetCodeFor(DataType.Region, regStateDefCd), Utils.ToggleLanguage("english", "nepali")));
            else
            {
                selLstZone.AddRange(GetData.AllZones(Utils.ToggleLanguage("english", "nepali")));
            }
            if (groupFlag == "V" || groupFlag == "D")
            {
                selLstZone = selLstZone.Where(z => z.Value == ZoneDefCd).ToList();
            }
            return new JsonResult { Data = selLstZone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillDistrictbyUser(string zoneDefCd, string isPopup)
        {
            string groupFlag = Session["UserGroupFlag"].ConvertToString();
            string DistDefCd = Session["UserDistrictDefCd"].ConvertToString();
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false" && groupFlag == "C")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select District") + "---" });
            if (zoneDefCd.ConvertToString() != "")
            {
                selLstDistricts.AddRange(GetData.DistrictbyZone(zoneDefCd, Utils.ToggleLanguage("english", "nepali")));
            }
            else
            {
                selLstDistricts.AddRange(GetData.AllDistricts(Utils.ToggleLanguage("english", "nepali")));
            }
            if (groupFlag == "V" || groupFlag == "D")
            {
                selLstDistricts = selLstDistricts.Where(d => d.Value == DistDefCd).ToList();

            }
            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillVDCMunbyUser(string districtDefCd, string isPopup)
        {
            string groupFlag = Session["UserGroupFlag"].ConvertToString();
            string VDCMunDefCd = Session["UserVdcMunDefCd"].ConvertToString();
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (isPopup == "false")
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });
            if (!string.IsNullOrWhiteSpace(districtDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.District, districtDefCd), Utils.ToggleLanguage("english", "nepali")));
            if (groupFlag == "V")
            {
                selLstVDCMun = selLstVDCMun.Where(v => v.Value == VDCMunDefCd).ToList();
            }
            return new JsonResult { Data = selLstVDCMun, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        # endregion

        public ActionResult FilterCountry(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterCountry(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterCurrency(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterCurrency(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public JsonResult FillWard(string vdcMunDefCd, string isPopup)
        {
            List<SelectListItem> selLstWard = new List<SelectListItem>();

            if (isPopup == "false")
                selLstWard.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Ward No.") + "---" });
            for (int i = 1; i <= 35; i++)
            {
                SelectListItem li = new SelectListItem();
                li.Value = i.ConvertToString();
                li.Text = Utils.GetLabel(i.ConvertToString());
                selLstWard.Add(li);
            }
            //if (!string.IsNullOrWhiteSpace(vdcMunDefCd))
            //    selLstWard.AddRange(GetData.WardByVdc(GetData.GetCodeFor(DataType.VdcMun, vdcMunDefCd)));
            return new JsonResult { Data = selLstWard, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }



        public JsonResult FillZoneByRegState(string regStateDefCd, string isPopup)
        {
            List<SelectListItem> selLstZone = new List<SelectListItem>();
            if (isPopup == "false")
                selLstZone.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Zone") + "---" });
            if (!string.IsNullOrWhiteSpace(regStateDefCd))
                selLstZone.AddRange(GetData.ZonebyRegState(GetData.GetCodeFor(DataType.Region, regStateDefCd), Utils.ToggleLanguage("english", "nepali")));
            else
            {
                selLstZone.AddRange(GetData.AllZones(Utils.ToggleLanguage("english", "nepali")));
            }
            return new JsonResult { Data = selLstZone, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillVDCMun(string districtDefCd, string isPopup)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (isPopup == "false")
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });
            if (!string.IsNullOrWhiteSpace(districtDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.District, districtDefCd), Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstVDCMun, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillNewVDCMun(string districtDefCd, string isPopup)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (isPopup == "false")
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Rural/Urban Municipality") + "---" });
            if (!string.IsNullOrWhiteSpace(districtDefCd))
                selLstVDCMun.AddRange(GetData.NewVdcByDistrict(GetData.GetCodeFor(DataType.District, districtDefCd), Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstVDCMun, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillAllVDCMun(string districtDefCd, string isPopup)
        {
            List<SelectListItem> selLstVDCMun = new List<SelectListItem>();
            if (isPopup == "false")
                selLstVDCMun.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC/Municipality") + "---" });
            if (!string.IsNullOrWhiteSpace(districtDefCd))
                selLstVDCMun.AddRange(GetData.VdcByDistrict(GetData.GetCodeFor(DataType.AllDistrict, districtDefCd), Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstVDCMun, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterVdc(string code, string desc, string districtDefCd, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterVdc(code, desc, districtDefCd, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillOffice(string isPopup)
        {
            List<SelectListItem> selLstOffice = new List<SelectListItem>();
            if (isPopup == "false")
                selLstOffice.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Office") + "---" });

            selLstOffice.AddRange(GetData.AllOffices(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstOffice, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterOffice(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterOffice(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public ActionResult FilterPosition(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterPosition(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public ActionResult FilterSubClass(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterSubClass(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillDesignation(string posDefCd, string isPopup)
        {
            List<SelectListItem> selLstDesignations = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDesignations.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Designation") + "---" });
            if (posDefCd != "")
            {
                selLstDesignations.AddRange(GetData.AllDesignationByPosition(posDefCd, Utils.ToggleLanguage("english", "nepali")));
            }
            return new JsonResult { Data = selLstDesignations, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillPositionSubClass(string desigDefCd, string isPopup)
        {
            List<SelectListItem> selLstSubClasses = new List<SelectListItem>();
            selLstSubClasses = commonfun.GetPositionSubClassbyDesignation("", desigDefCd);
            return new JsonResult { Data = selLstSubClasses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterPositionSubClass(string code, string desc, string desigDefCd, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonServ.GetPositionSubClassByDesigByCodeAndDesc(code, desc, desigDefCd);
            foreach (MISCommon common in lstCommon)
            {
                filteredResults.Add(new SelectListItem { Value = Convert.ToString(common.DefinedCode), Text = common.Description });
            }
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");
            }
        }
        public JsonResult FillSection(string officeCd, string isPopup)
        {
            List<SelectListItem> selLstSection = new List<SelectListItem>();
            selLstSection = commonfun.GetSection("", officeCd);
            return new JsonResult { Data = selLstSection, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterSection(string code, string desc, string officeCd, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            List<MISCommon> lstCommon = commonServ.GetSectionByCodeAndDesc(code, desc, officeCd);
            foreach (MISCommon common in lstCommon)
            {
                filteredResults.Add(new SelectListItem { Value = Convert.ToString(common.DefinedCode), Text = common.Description });
            }
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");
            }
        }
        public ActionResult FilterDesignation(string code, string desc, string posDefCd, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterDesignation(code, desc, posDefCd, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillCaste(string isPopup)
        {
            List<SelectListItem> selLstCastes = new List<SelectListItem>();
            if (isPopup == "false")
                selLstCastes.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Caste") + "---" });

            selLstCastes.AddRange(GetData.AllCastes(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstCastes, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterCaste(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterCaste(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillEducation(string isPopup)
        {
            List<SelectListItem> selLstEducation = new List<SelectListItem>();
            if (isPopup == "false")
                selLstEducation.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Education") + "---" });

            selLstEducation.AddRange(GetData.AllEducations(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstEducation, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterEducation(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterEducation(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillClassType(string isPopup)
        {
            List<SelectListItem> selLstEducation = new List<SelectListItem>();
            if (isPopup == "false")
                selLstEducation.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Education") + "---" });

            selLstEducation.AddRange(GetData.AllClassTypes(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstEducation, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterClassType(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterClassType(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillReligion(string isPopup)
        {
            List<SelectListItem> selLstReligion = new List<SelectListItem>();
            if (isPopup == "false")
                selLstReligion.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Religion") + "---" });

            selLstReligion.AddRange(GetData.AllReligions(Utils.ToggleLanguage("english", "nepali")));
            return new JsonResult { Data = selLstReligion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public ActionResult FilterReligion(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterReligion(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public ActionResult FilterRelation(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            RelationTypeService relTypeService = new RelationTypeService();
            filteredResults = relTypeService.GetRelationTypeByCodeAndDesc2(code, desc);
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public ActionResult FilterGroup(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = commonfun.GetGroup("", code, desc);
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }

        public ActionResult FilterGender(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = commonfun.GetGenderByCodeAndDesc("", code, desc);
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public ActionResult FilterOccupation(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            OccupationService occService = new OccupationService();
            filteredResults = occService.GetOccupationByCodeAndDesc(code, desc);
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }         
        public ActionResult FilterMaritalStatus(string code, string desc, string filterMode)
        {
            List<SelectListItem> filteredResults = new List<SelectListItem>();
            filteredResults = GetData.FilterMaritalStatus(code, desc, Utils.ToggleLanguage("english", "nepali"));
            if (filterMode == "false")
            {
                ViewData["layerData"] = filteredResults;
                return PartialView("_getLayer");
            }
            else
            {
                ViewData["FilteredData"] = filteredResults;
                return PartialView("_getFilteredData");

            }
        }
        public JsonResult FillBankBranchByBankCode(string VDCCode, string WardNo, string Bankid, string isPopup)
        {
            List<SelectListItem> selLstBankBranch = new List<SelectListItem>();
            if (isPopup == "false")
                selLstBankBranch.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank Branch") + "---" });

            CommonFunction common = new CommonFunction();
            string BankCode = common.GetCodeFromDataBase(Bankid.ConvertToString(), "NHRS_BANK", "BANK_CD");
            if (VDCCode == "undefined")
            {
                VDCCode = "";
            }
            if (WardNo == "undefined")
            {
                WardNo = "";
            }
            string VDCCd = common.GetCodeFromDataBase(VDCCode.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            selLstBankBranch.AddRange(commonfun.GetBankBranchIdByBankID(VDCCd, WardNo, BankCode));

            return new JsonResult { Data = selLstBankBranch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillBankByAddress(string Mode, string DistrictCd, string WardNo, string VDCCd, string isPopup)
        {
            
            List<SelectListItem> selLstBank = new List<SelectListItem>();
            if (isPopup == "false")
                selLstBank.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank") + "---" });

            CommonFunction common = new CommonFunction();
            string VDCCode = common.GetCodeFromDataBase(VDCCd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");

            selLstBank.AddRange(commonfun.GetBankByAddress(Mode,VDCCode, WardNo, DistrictCd));

            return new JsonResult { Data = selLstBank, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillBankBranchByBankId(string Bankid, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Bank Branch") + "---" });

            CommonFunction common = new CommonFunction();
            string bankCD = common.GetCodeFromDataBase(Bankid.ConvertToString(), "NHRS_BANK", "BANK_CD");
            selLstDistricts.AddRange(commonfun.GetBankBranchIdByBankID(bankCD));

            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillDistrictByBankBranchId(string Branchid, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            if (isPopup == "false")
                selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Branch ID") + "---" });

            CommonFunction common = new CommonFunction();
            string districtCD = common.GetCodeFromDataBase(Branchid.ConvertToString(), "NHRS_BANK_MAPPING", "DISTRICT_CD");
            selLstDistricts.AddRange(commonfun.GetDistrictByBankBranchID(districtCD));

            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillOfficeByAddressedOffice(string Officeid, string isPopup)
        {
            List<SelectListItem> selLstOffice = new List<SelectListItem>();
            if (isPopup == "false")
                selLstOffice.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select Office") + "---" });

            CommonFunction common = new CommonFunction();
            string OfficeCD = common.GetCodeFromDataBase(Officeid.ConvertToString(), "MIS_OFFICE", "OFFICE_CD");
            selLstOffice.AddRange(commonfun.GetOfficeByAddressedBank(OfficeCD));

            return new JsonResult { Data = selLstOffice, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult FillVDCSecretaryByEmpCd(string empCode, string isPopup)
        {
            List<SelectListItem> selLstDistricts = new List<SelectListItem>();
            //if (isPopup == "false")
            //    selLstDistricts.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select VDC Secretary Office") + "---" });

            CommonFunction common = new CommonFunction();

            selLstDistricts.AddRange(commonfun.GetVDCSecretaryOffice(empCode, ""));

            return new JsonResult { Data = selLstDistricts, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<ViewDataUploadFilesResult>();
            HttpPostedFileBase hpf = null;
            foreach (string file in Request.Files)
            {
                string directory = Server.MapPath("~/Files/member/temp/");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string filename = Path.GetFileName(hpf.FileName);
                string savedFileName = Path.Combine(directory, Session.SessionID + filename);
                if (hpf.ContentLength <= (1024 * 1024))
                {
                    hpf.SaveAs(savedFileName);

                    r.Add(new ViewDataUploadFilesResult()
                    {
                        Name = Session.SessionID + hpf.FileName,
                        Length = hpf.ContentLength,
                        Type = hpf.ContentType
                    });
                    Session["MemberImageName"] = Session.SessionID + hpf.FileName;
                }
            }
            if (hpf.ContentLength <= (1024 * 1024))
            {
                return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
            }
            else
            {
                return Content("{\"name\":\"" + "name" + "\",\"type\":\"" + "type" + "\",\"size\":\"" + "size" + "\"}", "application/json");
            }
        }
        //public ContentResult UploadFiles()
        //{
        //    var r = new List<ViewDataUploadFilesResult>();
        //    HttpPostedFileBase hpf = null;
        //    foreach (string file in Request.Files)
        //    {
        //        hpf = Request.Files[file] as HttpPostedFileBase;
        //        if (hpf.ContentLength == 0)
        //            continue;
        //        string savedFileName = Path.Combine(Server.MapPath("~/Files/images/member"), Path.GetFileName(hpf.FileName));
        //        if (hpf.ContentLength <= (1024 * 1024))
        //        {
        //            hpf.SaveAs(savedFileName);

        //            r.Add(new ViewDataUploadFilesResult()
        //            {
        //                Name = hpf.FileName,
        //                Length = hpf.ContentLength,
        //                Type = hpf.ContentType
        //            });
        //            Session["MemberImageName"] = hpf.FileName;
        //        }
        //    }
        //    if (hpf.ContentLength <= (1024 * 1024))
        //    {
        //        return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        //    }
        //    else
        //    {
        //        return Content("{\"name\":\"" + "name" + "\",\"type\":\"" + "type" + "\",\"size\":\"" + "size" + "\"}", "application/json");
        //    }
        //}


        public string AgeCalculator(string ToDate, string FromDate)
        {

            string age = string.Empty;
            int Toyyyy = 0;
            int ToMM = 0;
            int ToDD = 0;
            int Frmyyyy = 0;
            int FrmMM = 0;
            int FrmDD = 0;
            int day = 0;
            int month = 0;
            int year = 0;
            if (!String.IsNullOrEmpty(ToDate) && !String.IsNullOrEmpty(FromDate))
            {
                ToDate = ToDate.ToDateTime().ConvertToString("yyyy/MM/dd");
                FromDate = FromDate.ToDateTime().ConvertToString("yyyy/MM/dd");
                string[] ArrtoDate = ToDate.ConvertToString().Split('/', '-');
                Toyyyy = ArrtoDate[0].ToInt32();
                ToMM = ArrtoDate[1].ToInt32();
                ToDD = ArrtoDate[2].ToInt32();
                string[] ArrFromDate = FromDate.ConvertToString().Split('/', '-');
                Frmyyyy = ArrFromDate[0].ToInt32();
                FrmMM = ArrFromDate[1].ToInt32();
                FrmDD = ArrFromDate[2].ToInt32();
                if (Toyyyy > Frmyyyy)
                {

                    if (FrmDD.ToInt32() > ToDD.ToInt32())
                    {
                        ToDD = ToDD + 30;
                        day = ToDD.ToInt32() - FrmDD.ToInt32();
                        ToMM = ToMM - 1;

                    }
                    else
                    {
                        day = ToDD.ToInt32() - FrmDD.ToInt32();
                    }
                    if (FrmMM > ToMM)
                    {
                        ToMM = ToMM + 12;
                        month = ToMM - FrmMM;
                        Toyyyy = Toyyyy - 1;
                    }
                    else
                    {
                        month = ToMM - FrmMM;
                    }
                    year = Toyyyy - Frmyyyy;
                }
                age = Utils.GetLabel(year.ConvertToString()) + " " + Utils.GetLabel("Year") + " -" + Utils.GetLabel(month.ConvertToString()) + " " + Utils.GetLabel("Month") + " -" + Utils.GetLabel(day.ConvertToString()) + " " + Utils.GetLabel("Day");
            }
            return age;

        }
        public ActionResult ExportData(DataTable dt, string Filename)
        {

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                //Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= '" + Filename + "'.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Index", "ExportData");
        }


    }
}
