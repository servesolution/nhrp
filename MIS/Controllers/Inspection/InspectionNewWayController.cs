using ExceptionHandler;
using MIS.Models.Inspection;
using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using MIS.Models.Setup.Inspection;
using CsvHelper;
using MIS.Models.Core;
using System.Data.OracleClient;
using MIS.Models.Security;
using SelectPdf;
 
namespace MIS.Controllers.Inspection
{
    public class InspectionNewWayController : BaseController
    {
            //
            // GET: /InspectionPaper/
            CommonFunction com = new CommonFunction();
            InspectionNewWayService objService = new InspectionNewWayService();
            InspectionDetailModelClass objInspection = new InspectionDetailModelClass();


            public void CheckPermission()
            {
                PermissionParamService objPermissionParamService = new PermissionParamService();
                PermissionParam objPermissionParam = new PermissionParam();
                ViewBag.EnableEdit = "false";
                ViewBag.EnableDelete = "false";
                ViewBag.EnableAdd = "false";
                ViewBag.EnableApprove = "false";
                try
                {
                    objPermissionParam = objPermissionParamService.GetPermissionValue();
                    if (objPermissionParam != null)
                    {
                        if (objPermissionParam.EnableAdd == "true")
                        {
                            ViewBag.EnableAdd = "true";
                        }
                        if (objPermissionParam.EnableEdit == "true")
                        {
                            ViewBag.EnableEdit = "true";
                        }
                        if (objPermissionParam.EnableDelete == "true")
                        {
                            ViewBag.EnableDelete = "true";
                        }
                        if (objPermissionParam.EnableApprove == "true")
                        {
                            ViewBag.EnableApprove = "true";
                        }
                    }
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
            }
            public ActionResult InspectionList(string p)
            {

                CheckPermission();


                if (CommonVariables.GroupCD == "58")
                {
                    string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                    ViewData["ddl_Districts"] = com.GetDistrictsByDistrictCode(Districtcode);
                    ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", Districtcode);
                    ViewData["DistrctCd"] = Districtcode;
                }
                else
                {
                    ViewData["ddl_Districts"] = com.GetDistricts("");
                    ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", "");
                }


                //ViewData["ddl_Districts"] = com.GetAllDistricts("");

                ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
                ViewData["ddl_HouseModel"] = objService.GetHouseModel("", "");
                ViewData["ddl_houseDesign"] = objService.GetHouseDesign("", "");
                ViewData["ddl_Inspection_number"] = com.GetInspectionNo("");
                ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
                ViewData["yesNogap"] = (List<SelectListItem>)com.GetYesNo5("").Data;
                ViewData["ddl_Inspection_Application"] = (List<SelectListItem>)com.GetInspectionApplication("").Data;
                ViewData["ddl_Inspection_Remarks"] = (List<SelectListItem>)com.InspectionRemarksType("").Data;
                ViewData["ddl_Inspection_tab"] = (List<SelectListItem>)com.InspectionTabManual("").Data;
                string nraDefineCd = Session["nraDefCode"].ConvertToString();
                if (nraDefineCd != "")
                {
                    ViewData["NraDefCd"] = nraDefineCd;
                    return View();
                }

                string nraDefineCdInsp1 = Session["nraDefCodeInspection1"].ConvertToString();
                if (nraDefineCdInsp1 != "")
                {
                    ViewData["nraDefCodeInspection1"] = nraDefineCdInsp1;
                    return View();
                }
                if (Session["PaNumSaveUpdate"].ConvertToString() != "" && Session["PaNumSaveUpdate"].ConvertToString() != null)
                {
                    ViewData["PaNumSaveUpdate"] = Session["PaNumSaveUpdate"].ConvertToString();
                    return View();
                }
                //DataTable results               = objService.GetInspectionSearchResults(objInspection, "", "");
                ViewData["NraDefCd"] = "";
                ViewData["nraDefCodeInspection1"] = "";



                return View();
            }

            [HttpPost]
            public ActionResult InspectionList(FormCollection Fc, string id)
            {
                CheckPermission();

                objInspection.district_Cd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                objInspection.vdc_mun_cd = com.GetCodeFromDataBase(Fc["vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                ViewData["ddl_HouseModel"] = objService.GetHouseModel("", "");
                objInspection.ward_no = Fc["ward"].ConvertToString();
                string name = Fc["nameBenf"].ConvertToString();
                string InspLevel = Fc["ddl_Inspection_number"].ConvertToString();
                string uploadType = Fc["uploadedType"].ConvertToString();
                string remarksType = Fc["remarksType"].ConvertToString();
                if (InspLevel == "D")
                {
                    InspLevel = "0";
                }
                string MOUDApprove = Fc["MOUDApprove"].ConvertToString();
                string MOFALDApprove = Fc["MOFALDApprove"].ConvertToString();
                


                string Id = Fc["BenfId"].ConvertToString();

                string nraDefineCd = Session["PaNumSaveUpdate"].ConvertToString();
                if (nraDefineCd != "" && nraDefineCd != null)
                {
                    Id = nraDefineCd;
                    Session["nraDefCode"] = null;
                    Session["PaNumSaveUpdate"] = null;
                }
                string nraDefineCdInsp1 = Session["nraDefCodeInspection1"].ConvertToString();
                if (nraDefineCdInsp1 != "" && nraDefineCdInsp1 != null)
                {
                    Id = nraDefineCdInsp1;
                    Session["nraDefCodeInspection1"] = null;
                }
               
                    Session["GapInspection"] = null;
                    Session["GapInspectionOwnDesign"] = null;
                    DataTable results = objService.InspectionSearchWithPrevGap(objInspection, name, Id, InspLevel, MOUDApprove, MOFALDApprove);
                    ViewData["results"] = results; 
                    ViewData["InsertMessage"] = Session["InsertMessage"].ConvertToString();
                    Session["InsertMessage"] = null;

                    return PartialView("_InspectionSearchResultsGap");
            
                

            }

            public PartialViewResult GetModelInsception(string id, string design, string InspectionLevel, string HierarchyCode)
            {
                InspectionOwnDesign objOwnDesign = new InspectionOwnDesign();
                InspectionDetailModelClass objInspectn = new InspectionDetailModelClass();
                InspectionNewWayService objService = new InspectionNewWayService();
                DataTable dtbl = new DataTable();
                InspectionItem objInspection = new InspectionItem();


                // for gha vargako ghar
                if (design == "Own Design" || design == "आफ्नै डिजाइन")
                {
                    InspectionLevel = Session["Inspectionlevel"].ConvertToString();
                    Session["Inspectionlevel"] = null;
                    InspectionDesignModelClass obj = new InspectionDesignModelClass();
                    objOwnDesign = objService.getOwnDesignDetail(id, InspectionLevel);
                   ViewData["ddl_constructStatus"] = (List<SelectListItem>)com.InspectionConstructionStatus(objOwnDesign.ConstructionStatus.ConvertToString()).Data;
                    Session["OwnDesignEdit"] = null;
                    return PartialView("_OwnHouseDesign", objOwnDesign);
                }

               

                if (HierarchyCode != "")
                {
                    objInspection = objService.GetInspectionTreeInspection(HierarchyCode);
                    ViewBag.objInsptData = objInspection;
                }
                ViewBag.objInsptData = objInspection;
                Session["InspDetailForPrint"] = objInspection;

                //for editing inspection1
                InspectionDesignModelClass objEditInsp1 = new InspectionDesignModelClass();
                objEditInsp1 = (InspectionDesignModelClass)Session["Insp1Edit"];
                if (objEditInsp1 != null)
                {
                    objInspection.objComplyModelList = objEditInsp1.InspectionEditList;
                    Session["Insp1Edit"] = null;
                }



                return PartialView("_InspectionDetail", objInspectn);

            }

            public ActionResult ManageInspectionDesign(string p)
            {

                string id = "";
                string InspectionMode = "";
                DataTable dtOwn = new DataTable();
                DataTable dtbl = new DataTable();

                InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
                InspectionNewWayService objService = new InspectionNewWayService();
                RouteValueDictionary rvd = new RouteValueDictionary();

                try
                {
                    //construction material list
                    List<SelectListItem> constructList = com.GetInspectionPillarMaterial("", "");
                    if (constructList != null)
                    {
                        List<InspectionDesignModelClass> ListConstruct = new List<InspectionDesignModelClass>();
                        foreach (var lstItem in constructList)
                        {
                            if (lstItem.Value != "")
                            {
                                InspectionDesignModelClass Obj = new InspectionDesignModelClass();
                                Obj.ConstructMat = lstItem.Value.ConvertToString();
                                Obj.ConstMaterialDesc = lstItem.Text.ConvertToString();

                                ListConstruct.Add(Obj);
                            }

                        }

                        objInspectn.ConstructMaterialList = ListConstruct;

                    }
                    //roof material list

                    List<SelectListItem> roofList = com.GetInspectionRoofMaterial("", "");
                    if (roofList != null)
                    {
                        List<InspectionDesignModelClass> ListRoof = new List<InspectionDesignModelClass>();
                        foreach (var lstItem in roofList)
                        {
                            if (lstItem.Value != "")
                            {
                                InspectionDesignModelClass Obj = new InspectionDesignModelClass();
                                Obj.Roofmat = lstItem.Value.ConvertToString();
                                Obj.RoofMaterialDesc = lstItem.Text.ConvertToString();

                                ListRoof.Add(Obj);
                            }
                        }

                        objInspectn.RoofMaterialList = ListRoof;

                    }


                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (p != null)
                    {


                        if (rvd != null)
                        {
                            if (rvd["id2"] != null)
                            {

                                id = rvd["id2"].ToString();      //pa number

                                dtOwn = objService.getOwnDetail(id);  // get owner detail by pa number
                                if (dtOwn != null && dtOwn.Rows.Count > 0)
                                {
                                    List<InspectionOwnerDetailModelClass> objOwnList = new List<InspectionOwnerDetailModelClass>();
                                    foreach (DataRow drOwn in dtOwn.Rows)
                                    {
                                        InspectionOwnerDetailModelClass objOwn = new InspectionOwnerDetailModelClass();
                                        objOwn.OwnerFNameE = drOwn["HOUSE_OWNER_NAME"].ConvertToString();
                                        //objOwn.ReceipentName = drOwn["BENEFICIARY_NAME"].ConvertToString();
                                        objOwn.OwnerDistCd = com.GetDescriptionFromCode(drOwn["DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                                        objOwn.OwnVdcCd = com.GetDescriptionFromCode(drOwn["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                        objOwn.OwnWaard = drOwn["WARD_NO"].ConvertToString();
                                        objOwn.ReceipentName = drOwn["BENEFICIARY_FULLNAME_ENG"].ConvertToString();
                                        objOwn.paNumber = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                        //objOwn.kittaNum = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                        objOwn.toleE = drOwn["AREA_ENG"].ConvertToString();
                                        objOwn.toleL = drOwn["AREA_ENG"].ConvertToString();

                                        //objOwn.HouseOwnerId = drOwn["HOUSE_OWNER_ID"].ConvertToString();
                                        objOwn.HouseHoldId = drOwn["HOUSEHOLD_ID"].ConvertToString();
                                        objOwn.BuldingStrNo = objService.GetBuildingStructure(objOwn);
                                        objOwn.HouseSno = drOwn["HOUSE_SNO"].ConvertToString();

                                        //for inspection design part
                                        objInspectn.KittaNumber = drOwn["KITTA_NUMBER"].ConvertToString();
                                        objInspectn.InspectionLevel0 = drOwn["INSPECTION_LEVEL0"].ConvertToString();
                                        objInspectn.InspectionStatus = drOwn["INSPECTION_STATUS"].ConvertToString();
                                        objInspectn.NraDefCode = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                        ViewData["ddl_Districts"] = com.GetDistricts(drOwn["DISTRICT_CD"].ConvertToString());
                                        objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(drOwn["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                        ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), drOwn["DISTRICT_CD"].ConvertToString());
                                        ViewData["ddl_Wards"] = com.GetWardByVDCMun(drOwn["WARD_NO"].ConvertToString(), "");
                                        objInspectn.ward_no = drOwn["WARD_NO"].ConvertToString();
                                        objInspectn.district_Cd = drOwn["DISTRICT_CD"].ConvertToString();
                                        //objInspectn.district_Cd = drOwn["DISTRICT_CD"].ConvertToString();
                                        if (drOwn["INSPECTION_DESIGN_CD"].ConvertToString() == null || drOwn["INSPECTION_DESIGN_CD"].ConvertToString() == "")
                                        {
                                            InspectionMode = "I";
                                        }
                                        else
                                        {


                                            if (drOwn["INSPECTION_LEVEL1"].ToDecimal() > 0 || drOwn["INSPECTION_LEVEL2"].ToDecimal() > 0 || drOwn["INSPECTION_LEVEL3"].ToDecimal() > 0)
                                            {
                                                InspectionMode = "V";
                                            }
                                            else if (drOwn["INSPECTION_LEVEL0"].ConvertToString() == "0")
                                            {
                                                if (drOwn["INSPECTION_STATUS"].ConvertToString() == "A")
                                                {
                                                    InspectionMode = "V";
                                                }
                                                else
                                                {
                                                    InspectionMode = "U";
                                                }

                                            }
                                        }
                                        objOwnList.Add(objOwn);

                                    }



                                    objInspectn.ListOwner = objOwnList;
                                    objInspectn.Mode = InspectionMode;

                                    if (objInspectn.Mode == "V" || objInspectn.Mode == "U")
                                    {

                                        // update inspection design
                                        dtbl = objService.GetAllDesignUpdate(id);

                                        string[] Name = null;
                                        if (dtbl != null && dtbl.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr in dtbl.Rows)
                                            {
                                                objInspectn.BenfFullNameEng = dr["BENF_FULL_NAME"].ConvertToString();
                                                string Fullname = objInspectn.BenfFullNameEng.ConvertToString();
                                                if (Fullname != null)
                                                {
                                                    Name = Fullname.Split(null);
                                                }
                                                if (Name.Length == 3)
                                                {
                                                    objInspectn.BenfFirstName = Name[0].ConvertToString();
                                                    objInspectn.BenfMiddleName = Name[1].ConvertToString();
                                                    objInspectn.BenfLastName = Name[2].ConvertToString();
                                                }
                                                if (Name.Length == 2)
                                                {
                                                    objInspectn.BenfFirstName = Name[0].ConvertToString();
                                                    objInspectn.BenfMiddleName = null;
                                                    objInspectn.BenfLastName = Name[1].ConvertToString();
                                                }
                                                objInspectn.DesignCd = dr["INSPECTION_DESIGN_CD"].ConvertToString();
                                                objInspectn.district_Cd = dr["DISTRICT_CD"].ConvertToString();
                                                objInspectn.vdc_mun_cd = dr["VDC_MUN_CD"].ConvertToString();
                                                objInspectn.ward_no = dr["WARD"].ConvertToString();
                                                objInspectn.tole = dr["TOLE"].ConvertToString();
                                                objInspectn.OwnDesign = dr["OWN_DESIGN"].ConvertToString();
                                                objInspectn.DesignNumber = dr["DESIGN_NUMBER"].ConvertToString();
                                                objInspectn.ConstructMat = dr["CONSTRUCTION_MAT_CD"].ConvertToString();
                                                objInspectn.Roofmat = dr["ROOF_MAT_CD"].ConvertToString();
                                                objInspectn.ConstOther = dr["OTHER_CONSTRUCTION"].ConvertToString();
                                                objInspectn.RoofOther = dr["OTHER_ROOF"].ConvertToString();

                                            }
                                        }


                                        ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1(objInspectn.OwnDesign.ConvertToString()).Data;
                                        ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial(objInspectn.ConstructMat.ConvertToString(), "");
                                        ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial(objInspectn.Roofmat.ConvertToString(), "");
                                        ViewData["ddl_Districts"] = com.GetDistricts(objInspectn.district_Cd.ConvertToString());
                                        objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                        ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), objInspectn.district_Cd.ConvertToString());
                                        ViewData["ddl_Wards"] = com.GetWardByVDCMun(objInspectn.ward_no.ConvertToString(), "");
                                        ViewData["ddl_design"] = com.GetallHouseModelForInspectionOne(objInspectn.DesignNumber.ConvertToString());




                                        return View(objInspectn);
                                    }
                                    if (objInspectn.BenfFirstName.ConvertToString() == "" || objInspectn.BenfFirstName.ConvertToString() == null)
                                    {
                                        objInspectn.BenfFullNameEng = dtOwn.Rows[0]["HOUSE_OWNER_NAME"].ConvertToString();
                                        string FullnameFromMst = objInspectn.BenfFullNameEng.ConvertToString();
                                        string[] Namemst = null;
                                        if (FullnameFromMst != null)
                                        {
                                            Namemst = FullnameFromMst.Split(null);
                                        }
                                        if (Namemst.Length == 3)
                                        {
                                            objInspectn.BenfFirstName = Namemst[0].ConvertToString();
                                            objInspectn.BenfMiddleName = Namemst[1].ConvertToString();
                                            objInspectn.BenfLastName = Namemst[2].ConvertToString();
                                        }
                                        if (Namemst.Length == 2)
                                        {
                                            objInspectn.BenfFirstName = Namemst[0].ConvertToString();
                                            objInspectn.BenfMiddleName = null;
                                            objInspectn.BenfLastName = Namemst[1].ConvertToString();
                                        }
                                    }

                                }
                            }
                        }
                    }


                    ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;


                    ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial("", "");
                    ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial("", "");
                    //ViewData["ddl_Districts"] = com.GetDistricts("");
                    //ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
                    //ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
                    ViewData["ddl_design"] = com.GetallHouseModelForInspectionOne("");






                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                return View(objInspectn);
            }

            [HttpPost]
            public ActionResult ManageInspectionDesign(FormCollection fc, InspectionDesignModelClass ObjDesign)
            {


                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                InspectionGapService objInspectionGapService = new InspectionGapService();
                if (ObjDesign.BenfMiddleName.ConvertToString() == "" || ObjDesign.BenfMiddleName.ConvertToString() == null)
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();
                }
                else
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfMiddleName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();

                }
                ObjDesign.DesignNumber = fc["DesigneNumber"].ConvertToString();
                ObjDesign.ConstructMat = fc["ConstructionMaterial"].ConvertToString();
                ObjDesign.Roofmat = fc["RoofMaterial"].ConvertToString();

                ObjDesign.vdc_mun_cd = com.GetCodeFromDataBase(ObjDesign.VdcDefCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                ObjDesign.InspectionLevel0 = "0";
                ObjDesign.InspectionStatus = "R";


                ObjDesign.OwnDesign = fc["HouseDesign"].ConvertToString();
                if (ObjDesign.OwnDesign == "Yes")
                {
                    ObjDesign.OwnDesign = "Y";
                }
                if (ObjDesign.OwnDesign == "No")
                {
                    ObjDesign.OwnDesign = "N";
                }
                if (fc["btn_Submit"].ConvertToString() == "Submit" || fc["btn_Submit"].ConvertToString() == "पेश गर्नुहोस्")
                {
                    ObjDesign.Mode = "I";
                }
                else
                {
                    ObjDesign.Mode = "U";
                }

                bool result = ObjServDesign.saveInspectionDesign(ObjDesign);
                if (!result)
                {
                    Session["InsertMessage"] = "Data insert failed.";
                }
                // Session["nraDefCode"] = ObjDesign.NraDefCode.ConvertToString();
                Session["PaNumSaveUpdate"] = ObjDesign.NraDefCode.ConvertToString();

                return RedirectToAction("InspectionList");
            }

            //inspectio1
            public ActionResult ManageInapection1(string p)
            {

                string id = "";
                string HouseDesign = "";
                string DesignNumber = "";
                string InspectionLevelstatus = "";
                DataTable dtOwn = new DataTable();
                DataTable dtbl = new DataTable();
                CommonFunction com = new CommonFunction();
                InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
                InspectionNewWayService objService = new InspectionNewWayService();
                InspectionPaperService objPaperService = new InspectionPaperService();
                RouteValueDictionary rvd = new RouteValueDictionary();

                try
                {


                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (p != null)
                    {


                        if (rvd != null)
                        {
                            if (rvd["id2"] != null)
                            {
                                id = rvd["id2"].ToString();


                                dtOwn = objService.getOwnDetail(id);
                                if (dtOwn != null && dtOwn.Rows.Count > 0)
                                {
                                    List<InspectionOwnerDetailModelClass> objOwnList = new List<InspectionOwnerDetailModelClass>();
                                    foreach (DataRow drOwn in dtOwn.Rows)
                                    {
                                        InspectionOwnerDetailModelClass objOwn = new InspectionOwnerDetailModelClass();
                                        objOwn.OwnerFNameE = drOwn["HOUSE_OWNER_NAME"].ConvertToString();
                                        objOwn.OwnerDistCd = com.GetDescriptionFromCode(drOwn["DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                                        objOwn.OwnVdcCd = com.GetDescriptionFromCode(drOwn["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                        objOwn.OwnWaard = drOwn["WARD_NO"].ConvertToString();
                                        objOwn.paNumber = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                        //objOwn.kittaNum    = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                        objOwn.toleE = drOwn["AREA_ENG"].ConvertToString();
                                        objOwn.toleL = drOwn["AREA_ENG"].ConvertToString();
                                        objInspectn.hOwnerId = drOwn["HOUSE_OWNER_ID"].ConvertToString();
                                        objOwn.HouseHoldId = drOwn["HOUSEHOLD_ID"].ConvertToString();
                                        objOwn.BuldingStrNo = objService.GetBuildingStructure(objOwn);
                                        objOwn.HouseSno = drOwn["HOUSE_SNO"].ConvertToString();

                                        objInspectn.KittaNumber = drOwn["KITTA_NUMBER"].ConvertToString();
                                        objInspectn.InspectionMstId = drOwn["INSPECTION_MST_ID"].ConvertToString();
                                        objInspectn.district_Cd = drOwn["DESIGN_DISTRICT"].ConvertToString();
                                        objInspectn.vdc_mun_cd = drOwn["DESIGN_VDC"].ConvertToString();
                                        objInspectn.ward_no = drOwn["DESIGN_WARD"].ConvertToString();
                                        objInspectn.BenfFullNameEng = drOwn["DESIGN_BENF"].ConvertToString();
                                        objInspectn.Bank_ACC_Num = drOwn["ACCOUNT_NUMBER"].ConvertToString();
                                        ViewData["ddl_bankname"] = com.GetBankName(com.GetDefinedCodeFromDataBase(drOwn["BANK_CD"].ConvertToString(), "NHRS_BANK", "BANK_CD"));
                                        objInspectn.SuprtntfFullnameEng = SessionCheck.getSessionUsername().ConvertToString();
                                        if (objInspectn.BenfFullNameEng.ConvertToString() != "" && objInspectn.BenfFullNameEng.ConvertToString() != null)
                                        {
                                            string[] fullNameBenificiary = objInspectn.BenfFullNameEng.ConvertToString().Split(' ');
                                            if (fullNameBenificiary.Length == 3)
                                            {
                                                objInspectn.BenfFirstName = fullNameBenificiary[0];
                                                objInspectn.BenfMiddleName = fullNameBenificiary[1];
                                                objInspectn.BenfLastName = fullNameBenificiary[2];
                                            }
                                            if (fullNameBenificiary.Length == 2)
                                            {
                                                objInspectn.BenfFirstName = fullNameBenificiary[0];
                                                objInspectn.BenfLastName = fullNameBenificiary[1];
                                            }
                                            else
                                            {
                                                objInspectn.BenfFirstName = fullNameBenificiary[0];

                                            }
                                        }
                                       // DesignNumber = drOwn["DESIGN_NUMBER"].ConvertToString(); // if design is from available template

                                        if (drOwn["CONSTRUCTION_MAT_CD"].ConvertToString() == "" && drOwn["ROOF_MAT_CD"].ConvertToString() == "")
                                        {
                                            DesignNumber = drOwn["DESIGN_NUMBER"].ConvertToString();
                                        }
                                        else
                                        {
                                            string constructionMaterialId = drOwn["CONSTRUCTION_MAT_CD"].ConvertToString();
                                            string RoofMaterialId = drOwn["ROOF_MAT_CD"].ConvertToString();

                                            DesignNumber = objPaperService.getDesignByMaterialID(constructionMaterialId, RoofMaterialId);
                                        }
                                        //if (DesignNumber == "")
                                        //{
                                        //    HouseDesign = drOwn["HOUSE_MODEL_CD"].ConvertToString(); // design number access by joining for own design radio button
                                        //}
                                        //else
                                        //{
                                        //    HouseDesign = drOwn["DESIGN_NUMBER"].ConvertToString();
                                        //}

                                        objOwnList.Add(objOwn);

                                        objInspectn.OwnDesign = drOwn["OWN_DESIGN"].ConvertToString();
                                        if (objInspectn.OwnDesign == "Y")
                                        {
                                            objInspectn.OwnDesign = "Yes";
                                        }
                                        objInspectn.ConstMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["CONSTRUCTION_MAT_CD"].ConvertToString(), "NHRS_CONSTRUCTION_MATERIAL", "CONSTRUCTION_MAT_CD");
                                        objInspectn.RoofMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["ROOF_MAT_CD"].ConvertToString(), "NHRS_RC_MATERIAL", "RC_MAT_CD");
                                        objInspectn.ConstructMat = drOwn["CONSTRUCTION_MAT_CD"].ConvertToString();
                                        objInspectn.Roofmat = drOwn["ROOF_MAT_CD"].ConvertToString();
                                    }
                                    objInspectn.ListOwner = objOwnList;
                                }
                                objInspectn.Mode = "I";
                                //InspectionLevelstatus      = rvd["id2"].ToString();
                                objInspectn.NraDefCode = id;
                                ViewData["Design"] = com.GetDescriptionFromCode(DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");

                                objInspectn.DesignNumber = DesignNumber;
                                ViewData["DesignDesc"] = objService.GetHouseDesignDescription(DesignNumber);
                                ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1(objInspectn.OwnDesign.ConvertToString()).Data;
                                ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial(objInspectn.ConstructMat.ConvertToString(), "");
                                ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial(objInspectn.Roofmat.ConvertToString(), "");
                                ViewData["ddl_Districts"] = com.GetDistricts(objInspectn.district_Cd.ConvertToString());
                                objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), objInspectn.district_Cd.ConvertToString());
                                ViewData["ddl_Wards"] = com.GetWardByVDCMun(objInspectn.ward_no.ConvertToString(), "");
                                //ViewData["ddl_bankname"] = com.GetBankName("");
                                ViewData["ddl_Relation"] = com.GetRelation(objInspectn.BenfRelation.ConvertToString());


                                // get the design related hierarchy item from the getHierarchyCd method from NHRS_HOUSE_MODEL
                                //for inspection One
                                if (rvd["ILevel"].ToString() == "1")
                                {
                                    if (ViewData["Design"].ConvertToString() == "आफ्नै डिजाइन" || ViewData["Design"].ConvertToString() == "Own Design")
                                    {
                                        objInspectn.Inspection = "1";
                                        objInspectn.OwnDesign = "Yes";
                                    }
                                    else
                                    {


                                        objInspectn.Inspection = "1";
                                        objInspectn.Hierarchy_cd = objService.getHierarchyCd(DesignNumber);
                                        if (objInspectn.Hierarchy_cd.ConvertToString() == "")   //hierarchy code for given design not found in house model
                                        {
                                            ViewData["Design"] = Utils.ToggleLanguage("Inspection one not found", "सम्बन्धित पहिलो निरीक्षण भेटिएन");
                                        }
                                    }



                                }
                                //for inspection Two
                                if (rvd["ILevel"].ToString() == "2")
                                {
                                    objInspectn.Inspection = "2";
                                     
                                    string[] NextDesign = objService.getNextInspectionDesignId(HouseDesign);
                                    if (NextDesign != null && NextDesign.Length > 0)
                                    {
                                        string houseDesignId = NextDesign[0];
                                        objInspectn.DesignNumber = houseDesignId;
                                        objInspectn.Hierarchy_cd = NextDesign[1];
                                        ViewData["Design"] = com.GetDescriptionFromCode(houseDesignId, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(houseDesignId);
                                    }
                                    else
                                    {
                                        objInspectn.Hierarchy_cd = "";
                                        objInspectn.DesignNumber = HouseDesign;
                                        ViewData["Design"] = Utils.ToggleLanguage("Related second inspection not found", "सम्बन्धित दोश्रो निरीक्षण भेटिएन");
                                        ViewData["DesignDesc"] = Utils.ToggleLanguage("not found", "भेटिएन");
                                    }



                                }
                                //for inspection Three
                                if (rvd["ILevel"].ToString() == "3")
                                {
                                    objInspectn.Inspection = "3";
                                    //FOR INSPECTIO TWO
                                    string houseDesignId = "";
                                    string[] NextDesign = objService.getNextInspectionDesignId(HouseDesign);
                                    if (NextDesign != null && NextDesign.Length > 0)
                                    {
                                        houseDesignId = NextDesign[0];
                                        objInspectn.Hierarchy_cd = NextDesign[1];
                                        objInspectn.DesignNumber = houseDesignId;
                                    }



                                    //FOR INSPECTION 3
                                    string[] DesignThree = objService.getNextInspectionDesignId(houseDesignId);
                                    
                                    string DesignID = "";
                                    if (DesignThree != null)
                                    {
                                        DesignID = DesignThree[0];
                                        objInspectn.Hierarchy_cd = DesignThree[1];
                                        objInspectn.DesignNumber = DesignID;
                                        ViewData["Design"] = com.GetDescriptionFromCode(DesignID, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(DesignID);
                                    }
                                    else
                                    {
                                        objInspectn.Hierarchy_cd = "";
                                        objInspectn.DesignNumber = DesignID;
                                        ViewData["Design"] = Utils.ToggleLanguage("Related third inspection not found", "सम्बन्धित तेश्रो निरीक्षण भेटिएन");
                                        ViewData["DesignDesc"] = Utils.ToggleLanguage("not found", "भेटिएन");
                                    }



                                }

                                return View(objInspectn);

                            }
                        }
                    }


                    ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
                    ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial("", "");
                    ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial("", "");
                    ViewData["ddl_Districts"] = com.GetDistricts("");
                    ViewData["ddl_Vdc"] = com.GetVDCMunByDistrict("", "");
                    ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
                    ViewData["ddl_Relation"] = com.GetRelation("");
                    Session["Design"] = ViewData["Design"];
                    Session["DesignDesc"] = ViewData["DesignDesc"];





                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                return View(objInspectn);
            }



            [HttpPost]
            public ActionResult ManageInapection1(FormCollection fc, InspectionDesignModelClass ObjDesign, HttpPostedFileBase file)
            {
                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                string ownDesignHouse = com.GetDescriptionFromCode(ObjDesign.DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");

                if (ownDesignHouse == "Own Design" || ownDesignHouse == "आफ्नै डिजाइन")
                {
                    //calling insert for gha vargako ghar
                    bool SaveOwnDesign = saveInspectionOwnDesign(fc, ObjDesign, file);
                    Session["nraDefCodeInspection1"] = ObjDesign.NraDefCode.ConvertToString();
                    return RedirectToAction("InspectionList");
                }

                if (ObjDesign.Final_Remarks_eng.ConvertToString() != "")
                {
                    ObjDesign.Final_Remarks = ObjDesign.Final_Remarks_eng.ConvertToString();
                }

                if (ObjDesign.BenfMiddleName.ConvertToString() == "" || ObjDesign.BenfMiddleName.ConvertToString() == null)
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();
                }
                else
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfMiddleName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();
                }
                string finalDecision = fc["Approval"].ConvertToString();
                if (finalDecision == "Approve")
                {
                    ObjDesign.finalDecision = "Y";
                }
                else
                {
                    ObjDesign.finalDecision = "N";
                }
                string finalDecision2 = fc["InspPassName"].ConvertToString();
                if (finalDecision2 == "1")
                {
                    ObjDesign.final_decision_2 = "Y";
                }
                else
                {
                    ObjDesign.final_decision_2 = "N";
                }
                ObjDesign.vdc_mun_cd = com.GetCodeFromDataBase(ObjDesign.VdcDefCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                ObjDesign.InspectionLevel1 = "1";

                ObjDesign.InspectionStatus = "R";
                ObjDesign.TECHNICAL_ASSIST = fc["TechAssistradio"].ConvertToString();
                ObjDesign.ORGANIZATION_TYPE = fc["orgradio"].ConvertToString();
                ObjDesign.CONSTRUCTOR_TYPE = fc["Constructorradio"].ConvertToString();
                ObjDesign.SOIL_TYPE = fc["SoilRadio"].ConvertToString();

                 MIS.Models.Core.MISCommon.MyValue objvalue = new MIS.Models.Core.MISCommon.MyValue();

                Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply = new Dictionary<string, MIS.Models.Core.MISCommon.MyValue>();
                dynamic keyComply = fc.AllKeys.Where(s => s.IndexOf("checkBox") >= 0);
                Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply1 = new Dictionary<string, MIS.Models.Core.MISCommon.MyValue>();
                foreach (var item in keyComply)
                {
                    MIS.Models.Core.MISCommon.MyValue val = new MIS.Models.Core.MISCommon.MyValue();
                    val.Value1 = item.ToString().Split('_')[1];
                    val.Value2 = fc["Remarks_" + fc[item]];
                    lstComply1.Add(fc[item], val);
                }
                lstComply = lstComply1;
                //collect image file
                if (ObjDesign.MultiFile != null)
                {
                    int photCounter = 1;
                    foreach(HttpPostedFileBase upFile in  ObjDesign.MultiFile)
                    {                       
                        string filePath = "~/Files/images/InspectionDesign/";
                        if (upFile != null)
                        {
                            var phPath = ObjDesign.NraDefCode + "_" + photCounter+".jpg";//front
                            if (System.IO.File.Exists(Server.MapPath(filePath + phPath)))
                            {
                                System.IO.File.Delete(Server.MapPath(filePath + phPath));
                            }
                            upFile.SaveAs(Server.MapPath(filePath + phPath));
                        }
                    }
                }
                else
                {
                    ObjDesign.PHOTO_1 = ObjDesign.NraDefCode + "_" + "1.jpg";//fc["PHOTO_1"]!=null ? fc["PHOTO_1"].ToString():string.Empty;
                    ObjDesign.PHOTO_2 = ObjDesign.NraDefCode + "_" + "2.jpg";//fc["PHOTO_2"]!=null ? fc["PHOTO_2"].ToString():string.Empty;
                    ObjDesign.PHOTO_3 = ObjDesign.NraDefCode + "_" + "3.jpg";//fc["PHOTO_3"]!=null ? fc["PHOTO_3"].ToString():string.Empty;
                    ObjDesign.PHOTO_4 = ObjDesign.NraDefCode + "_" + "4.jpg"; //fc["PHOTO_4"]!=null ? fc["PHOTO_4"].ToString():string.Empty;
                    ObjDesign.PHOTO_5 = ObjDesign.NraDefCode + "_" + "5.jpg";//fc["PHOTO_5"]!=null ? fc["PHOTO_5"].ToString():string.Empty;
                    ObjDesign.PHOTO_6 = ObjDesign.NraDefCode + "_" + "6.jpg";//fc["PHOTO_6"]!=null ? fc["PHOTO_6"].ToString():string.Empty;
                    ObjDesign.PHOTO_7 = ObjDesign.NraDefCode + "_" + "7.jpg";//fc["PHOTO_7"]!=null ? fc["PHOTO_7"].ToString():string.Empty;
                    ObjDesign.PHOTO_8 = ObjDesign.NraDefCode + "_" + "8.jpg";//fc["PHOTO_8"]!=null ? fc["PHOTO_8"].ToString():string.Empty;

                    //Saving All Files
                    string filePath = "~/Files/images/InspectionDesign/";
                    if (ObjDesign.FrontFile != null)
                    {
                        var frontPath = ObjDesign.NraDefCode + "_" + "1.jpg";//front
                        if (System.IO.File.Exists(Server.MapPath(filePath + frontPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + frontPath));
                        }
                        ObjDesign.FrontFile.SaveAs(Server.MapPath(filePath + frontPath));
                    }
                    if (ObjDesign.BackFile != null)
                    {
                        var backPath = ObjDesign.NraDefCode + "_" + "2.jpg";//back
                        if (System.IO.File.Exists(Server.MapPath(filePath + backPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + backPath));
                        }
                        ObjDesign.BackFile.SaveAs(Server.MapPath(filePath + backPath));
                    }
                    if (ObjDesign.TopFile != null)
                    {
                        var topPath = ObjDesign.NraDefCode + "_" + "3.jpg";//top
                        if (System.IO.File.Exists(Server.MapPath(filePath + topPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + topPath));
                        }
                        ObjDesign.TopFile.SaveAs(Server.MapPath(filePath + topPath));
                    }
                    if (ObjDesign.ButtomtFile != null)
                    {
                        var buttomPath = ObjDesign.NraDefCode + "_" + "4.jpg";//buttom
                        if (System.IO.File.Exists(Server.MapPath(filePath + buttomPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + buttomPath));
                        }
                        ObjDesign.ButtomtFile.SaveAs(Server.MapPath(filePath + buttomPath));
                    }
                    if (ObjDesign.LeftFile != null)
                    {
                        var leftPath = ObjDesign.NraDefCode + "_" + "5.jpg";//left
                        if (System.IO.File.Exists(Server.MapPath(filePath + leftPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + leftPath));
                        }
                        ObjDesign.LeftFile.SaveAs(Server.MapPath(filePath + leftPath));
                    }
                    if (ObjDesign.RightFile != null)
                    {
                        var rightPath = ObjDesign.NraDefCode + "_" + "6.jpg";//right
                        if (System.IO.File.Exists(Server.MapPath(filePath + rightPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + rightPath));
                        }
                        ObjDesign.RightFile.SaveAs(Server.MapPath(filePath + rightPath));
                    }
                    if (ObjDesign.MapFile != null)
                    {
                        var mapPath = ObjDesign.NraDefCode + "_" + "7.jpg";//map
                        if (System.IO.File.Exists(Server.MapPath(filePath + mapPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + mapPath));
                        }
                        ObjDesign.MapFile.SaveAs(Server.MapPath(filePath + mapPath));
                    }
                    if (ObjDesign.FormFile != null)
                    {
                        var formPath = ObjDesign.NraDefCode + "_" + "8.jpg";//form
                        if (System.IO.File.Exists(Server.MapPath(filePath + formPath)))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath + formPath));
                        }
                        ObjDesign.FormFile.SaveAs(Server.MapPath(filePath + formPath));
                    }
                }




                if (fc["btn_Submit1"].ConvertToString() == "Update and Agree" || fc["btn_Submit1"].ConvertToString() == "Update and Agree")
                {
                    InspectionEngineerApproveController objEngineerCotroller = new InspectionEngineerApproveController();
                    string InsPprId = ObjServDesign.UpdateInspectionInfo(lstComply, ObjDesign);
                    bool engineerAgreement = objEngineerCotroller.EngineersAgreementSingle(ObjDesign.NraDefCode, ObjDesign.Inspection, "N");
                    if (InsPprId == "false")
                    {
                        Session["InsertMessage"] = "Data upload failed !!!";
                    }
                }


                if (fc["btn_Submit"].ConvertToString() == "Submit" || fc["btn_Submit"].ConvertToString() == "पेश गर्नुहोस्")
                {

                    string InsPprId = ObjServDesign.saveInspectionInfo(lstComply, ObjDesign);
                    if (InsPprId == "false")
                    {
                        Session["InsertMessage"] = "Data upload failed !!!";
                    }
                }
                if (fc["btn_Submit"].ConvertToString() == "Update" || fc["btn_Submit"].ConvertToString() == "अपडेट")
                {
                    string InsPprId = ObjServDesign.UpdateInspectionInfo(lstComply, ObjDesign);
                }
                Session["PaNumSaveUpdate"] = ObjDesign.NraDefCode.ConvertToString();

 
                #region data updated by focal engineers redirects to their list
                if (Session["EngineersAgreement"].ConvertToString() == "EngineersAgreement")
                {

                    return RedirectToAction("../InspectionEngineerApprove/ManageEngineerApproval");
                }
                #endregion

                return RedirectToAction("InspectionList");

            }

            //saving gha bargako ghar
            public bool saveInspectionOwnDesign(FormCollection fc, InspectionDesignModelClass ObjDesign, HttpPostedFileBase file)
            {
                bool result = false;
                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                InspectionOwnDesign objOwnDesign = new InspectionOwnDesign();

                //paper info
                if (ObjDesign.BenfMiddleName.ConvertToString() == "" || ObjDesign.BenfMiddleName.ConvertToString() == null)
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();
                }
                else
                {
                    ObjDesign.BenfFullNameEng = ObjDesign.BenfFirstName.ConvertToString() + " " + ObjDesign.BenfMiddleName.ConvertToString() + " " + ObjDesign.BenfLastName.ConvertToString();
                }
                string finalDecision = fc["Approval"].ConvertToString();
                if (finalDecision == "Approve")
                {
                    ObjDesign.finalDecision = "Y";
                }
                else
                {
                    ObjDesign.finalDecision = "N";
                }
                if (ObjDesign.Final_Remarks_eng.ConvertToString() != "")
                {
                    ObjDesign.Final_Remarks = ObjDesign.Final_Remarks_eng.ConvertToString();
                }

                string finalDecision2 = fc["InspPassName"].ConvertToString();
                if (finalDecision2 == "1")
                {
                    ObjDesign.final_decision_2 = "Y";
                }
                else
                {
                    ObjDesign.final_decision_2 = "N";
                }
                ObjDesign.vdc_mun_cd = com.GetCodeFromDataBase(ObjDesign.VdcDefCode, "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                ObjDesign.InspectionLevel1 = "1";
                ObjDesign.InspectionStatus = "R"; 
                objOwnDesign.ConstructionStatus = fc["ddl_constructStatus"].ConvertToString();

                objOwnDesign.FloorCount = fc["FloorCount"].ConvertToString();

                objOwnDesign.BaseConstructMaterial = fc["BaseConstructMaterial"].ConvertToString();
                objOwnDesign.BaseDepthBelowGrnd = fc["BaseDepthBelowGrnd"].ConvertToString();
                objOwnDesign.BaseAvgWidth = fc["BaseAvgWidth"].ConvertToString();
                objOwnDesign.BseHeightAbvGrnd = fc["BseHeightAbvGrnd"].ConvertToString();

                objOwnDesign.gndFloMat = fc["gndFloMat"].ConvertToString();
                objOwnDesign.GrndFlorPrncpl = fc["GrndFlorPrncpl"].ConvertToString();
                objOwnDesign.WallStructDescpt = fc["WallStructDescpt"].ConvertToString();

                objOwnDesign.FloorRoofDescrpt = fc["FloorRoofDescrpt"].ConvertToString();
                objOwnDesign.FloorRoofMat = fc["FloorRoofMat"].ConvertToString();
                objOwnDesign.FloorRoofPrncpl = fc["FloorRoofPrncpl"].ConvertToString();
                objOwnDesign.FloorRoofDetl = fc["FloorRoofDetl"].ConvertToString();

                objOwnDesign.FirstFlorMat = fc["FirstFlorMat"].ConvertToString();
                objOwnDesign.FirstFLorPrncpl = fc["FirstFLorPrncpl"].ConvertToString();
                objOwnDesign.FirstFlorDtl = fc["FirstFlorDtl"].ConvertToString();

                objOwnDesign.Roofmat = fc["Roofmat"].ConvertToString();
                objOwnDesign.RoofPrncpl = fc["RoofPrncpl"].ConvertToString();
                objOwnDesign.RoofDetal = fc["RoofDetal"].ConvertToString();

                //collect image file
                int countt = Request.Files.Count;
                for (int k = 0; k < Request.Files.Count; k++)
                {
                    HttpPostedFileBase filee = Request.Files[k];
                    if (filee != null && filee.ContentLength > 0)
                    {
                        try
                        {
                            string filePath = "~/Content/images/Inspection/";
                            if (System.IO.File.Exists(Server.MapPath(filePath) + Path.GetFileName(filee.FileName)))
                            {
                                System.IO.File.Delete(Server.MapPath(filePath) + Path.GetFileName(filee.FileName));
                            }
                            filee.SaveAs(Server.MapPath(filePath) + Path.GetFileName(filee.FileName));
                            if (k == 0)
                            {
                                ObjDesign.PHOTO_1 = filee.FileName; //front
                            }
                            if (k == 1)
                            {
                                ObjDesign.PHOTO_2 = filee.FileName;//back
                            }
                            if (k == 2)
                            {
                                ObjDesign.PHOTO_3 = filee.FileName;//top
                            }
                            if (k == 3)
                            {
                                ObjDesign.PHOTO_4=filee.FileName;//bottom
                            }
                            if (k == 4)
                            {
                                ObjDesign.PHOTO_5 = filee.FileName;//right
                            } 
                            if (k == 5)
                            {
                                ObjDesign.PHOTO_6 = filee.FileName; //left
                            }
                            if (k == 6)
                            {
                                ObjDesign.PHOTO_7 = filee.FileName; //map 
                            }
                            if (k == 7)
                            {
                                ObjDesign.PHOTO_8 = filee.FileName; //form 
                            }
                        }
                        catch (Exception ex)
                        {
                            ExceptionManager.AppendLog(ex);
                        }
                    }
                }

                
                if (fc["btn_Submit"].ConvertToString() == "Submit" || fc["btn_Submit"].ConvertToString() == "पेश गर्नुहोस्")
                {
                    ObjDesign.Mode = "I";
                    result = ObjServDesign.saveOwnInspectionDesign(ObjDesign, objOwnDesign);
                }
                else
                {
                    ObjDesign.Mode = "U";
                    result = ObjServDesign.saveOwnInspectionDesign(ObjDesign, objOwnDesign);
                }
                return result;
            }

            //check inspection with complied inspection  
            public string InspectionComplyModelClass(Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply, InspectionDesignModelClass ObjDesign)
            {
                string ComplyFlag = "N";
                InspectionNewWayService ObjServDesign = new InspectionNewWayService();


                foreach (var item in lstComply)
                {
                    if ((item.Value.ConvertToString() == "Y"))
                    {
                        ComplyFlag = "Y";
                        continue;

                    }
                    else
                    {
                        ComplyFlag = "N";
                        goto NotComply;
                    }
                }
            NotComply:

                return ComplyFlag;
            }

            //approve InspectionDesign

            //edit Inspection 
            public ActionResult EditInspection(string p)
            {

                string id = "";
                string HouseDesign = "";
                string DesignNumber = "";
                string owndesign = "";
                DataTable dtOwn = new DataTable();
                DataTable dtbl = new DataTable();
                CommonFunction com = new CommonFunction();
                InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
                InspectionNewWayService objService = new InspectionNewWayService();
                RouteValueDictionary rvd = new RouteValueDictionary();

                try
                {


                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (p != null)
                    {


                        if (rvd != null)
                        {
                            if (rvd["id2"] != null)
                            {
                                id = rvd["id2"].ToString();
                                if (rvd["id7"].ToString() == "R" || rvd["id7"].ToString() == "A") //Inspection status 
                                {



                                    //get design number by nra code
                                    DataTable designId = new DataTable();
                                    designId = objService.GetAllDesignUpdate(id);
                                    if (designId != null && designId.Rows.Count > 0)
                                    {
                                        if (designId.Rows[0]["CONSTRUCTION_MAT_CD"].ConvertToString() == "" && designId.Rows[0]["ROOF_MAT_CD"].ConvertToString() == "")
                                        {
                                            DesignNumber = designId.Rows[0]["DESIGN_NUMBER"].ConvertToString();


                                            objInspectn.DesignNumber = DesignNumber;
                                        }
                                        else
                                        {
                                            string constructionMaterialId = designId.Rows[0]["CONSTRUCTION_MAT_CD"].ConvertToString();
                                            string RoofMaterialId = designId.Rows[0]["ROOF_MAT_CD"].ConvertToString();

                                            DesignNumber = objService.getDesignByMaterialID(constructionMaterialId, RoofMaterialId);
                                            objInspectn.DesignNumber = DesignNumber;
                                        }

                                    }

                                    //for inspection One
                                    if (rvd["ILevel"].ToString() == "1")
                                    {
                                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(objInspectn.DesignNumber);
                                        ViewData["Design"] = com.GetDescriptionFromCode(objInspectn.DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        owndesign = com.GetDescriptionFromCode(objInspectn.DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        if (owndesign == "Own Design" || owndesign == "आफ्नै डिजाइन")
                                        {
                                            owndesign = "Y";

                                        }
                                        if (owndesign != "Y")
                                        {
                                            objInspectn.Hierarchy_cd = objService.getHierarchyCd(objInspectn.DesignNumber);
                                            if (objInspectn.Hierarchy_cd.ConvertToString() == "")
                                            {
                                                ViewData["Design"] = Utils.ToggleLanguage("Inspection one not found", "सम्बन्धित पहिलो निरीक्षण भेटिएन");
                                            }
                                        }

                                        objInspectn.Inspection = "1";

                                    }
                                    //for inspection Two
                                    if (rvd["ILevel"].ToString() == "2")
                                    {
                                        objInspectn.Inspection = "2";
                                        string[] NextDesign = objService.getNextInspectionDesignId(objInspectn.DesignNumber);
                                        string houseDesignId = NextDesign[0];
                                        objInspectn.DesignNumber = houseDesignId;
                                        objInspectn.Hierarchy_cd = NextDesign[1];

                                        ViewData["Design"] = com.GetDescriptionFromCode(houseDesignId, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(houseDesignId);
                                        if (ViewData["Design"].ConvertToString() == "Own Design" || ViewData["Design"].ConvertToString() == "आफ्नै डिजाइन")
                                        {
                                            owndesign = "Y";

                                        }

                                    }
                                    //for inspection Three
                                    if (rvd["ILevel"].ToString() == "3")
                                    {
                                        objInspectn.Inspection = "3";
                                        //FOR INSPECTIO TWO
                                        string houseDesignId = "";
                                        string[] NextDesign = objService.getNextInspectionDesignId(objInspectn.DesignNumber);
                                        if (NextDesign.Length > 0)
                                        {
                                            houseDesignId = NextDesign[0];
                                            objInspectn.Hierarchy_cd = NextDesign[1];
                                            objInspectn.DesignNumber = houseDesignId;
                                        }



                                        //FOR INSPECTION 3
                                        string[] DesignThree = objService.getNextInspectionDesignId(houseDesignId);
                                        string DesignID = "";
                                        if (DesignThree != null)
                                        {
                                            DesignID = DesignThree[0];
                                            objInspectn.Hierarchy_cd = DesignThree[1];
                                            objInspectn.DesignNumber = DesignID;
                                        }
                                        else
                                        {
                                            objInspectn.Hierarchy_cd = "";
                                            objInspectn.DesignNumber = DesignID;
                                        }

                                        ViewData["Design"] = com.GetDescriptionFromCode(DesignID, "NHRS_HOUSE_MODEL", "MODEL_ID");
                                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(DesignID);
                                        if (ViewData["Design"].ConvertToString() == "Own Design" || ViewData["Design"].ConvertToString() == "आफ्नै डिजाइन")
                                        {
                                            owndesign = "Y";

                                        }
                                    }

                                     objInspectn = objService.getAllInnfoForInspection1(id, owndesign, objInspectn.DesignNumber);
                                    Session["Inspectionlevel"] = rvd["ILevel"].ToString(); 
                                    Session["Insp1Edit"] = (InspectionDesignModelClass)objInspectn; 
 
                                    ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1(objInspectn.OwnDesign.ConvertToString()).Data;
                                    ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial(objInspectn.ConstructMat.ConvertToString(), "");
                                    ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial(objInspectn.Roofmat.ConvertToString(), "");
                                    ViewData["ddl_Districts"] = com.GetDistricts(objInspectn.district_Cd.ConvertToString());
                                    objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                    ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), objInspectn.district_Cd.ConvertToString());
                                    ViewData["ddl_Wards"] = com.GetWardByVDCMun(objInspectn.ward_no.ConvertToString(), "");
                                    ViewData["ddl_Relation"] = com.GetRelation(objInspectn.BenfRelation.ConvertToString());

                                    ViewData["ddl_bankname"] = com.GetBankName(objInspectn.Bank_Name.ConvertToString(), "", "");

                                    dtOwn = objService.getOwnDetail(id);
                                    if (dtOwn != null && dtOwn.Rows.Count > 0)
                                    {
                                        List<InspectionOwnerDetailModelClass> objOwnList = new List<InspectionOwnerDetailModelClass>();
                                        foreach (DataRow drOwn in dtOwn.Rows)
                                        {
                                            InspectionOwnerDetailModelClass objOwn = new InspectionOwnerDetailModelClass();
                                            objOwn.OwnerFNameE = drOwn["HOUSE_OWNER_NAME"].ConvertToString();

                                            objOwn.OwnerDistCd = com.GetDescriptionFromCode(drOwn["DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                                            objOwn.OwnVdcCd = com.GetDescriptionFromCode(drOwn["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                            objOwn.OwnWaard = drOwn["WARD_NO"].ConvertToString();

                                            objOwn.paNumber = drOwn["NRA_DEFINED_CD"].ConvertToString();
                                             objOwn.toleE = drOwn["AREA_ENG"].ConvertToString();
                                            objOwn.toleL = drOwn["AREA_ENG"].ConvertToString();

                                            objOwn.HouseHoldId = drOwn["HOUSEHOLD_ID"].ConvertToString();
                                            objOwn.BuldingStrNo = objService.GetBuildingStructure(objOwn);
                                            objOwn.HouseSno = drOwn["HOUSE_SNO"].ConvertToString();
                                            objInspectn.InspectionMstId = drOwn["INSPECTION_MST_ID"].ConvertToString();

                                            objInspectn.KittaNumber = drOwn["KITTA_NUMBER"].ConvertToString();
                                            objInspectn.hOwnerId = drOwn["HOUSE_OWNER_ID"].ConvertToString();
                                             if (objInspectn.BenfFullNameEng.ConvertToString() == "" || objInspectn.BenfFullNameEng.ConvertToString() == null)
                                            {
                                                objInspectn.BenfFullNameEng = drOwn["DESIGN_BENF"].ConvertToString();
                                            }

                                            objInspectn.ConstMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["CONSTRUCTION_MAT_CD"].ConvertToString(), "NHRS_CONSTRUCTION_MATERIAL", "CONSTRUCTION_MAT_CD");
                                            objInspectn.RoofMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["ROOF_MAT_CD"].ConvertToString(), "NHRS_RC_MATERIAL", "RC_MAT_CD");

                                            if (objInspectn.BenfFullNameEng.ConvertToString() != "" && objInspectn.BenfFullNameEng.ConvertToString() != null)
                                            {
                                                string[] fullNameBenificiary = objInspectn.BenfFullNameEng.ConvertToString().Split(' ');
                                                if (fullNameBenificiary.Length == 3)
                                                {
                                                    objInspectn.BenfFirstName = fullNameBenificiary[0];
                                                    objInspectn.BenfMiddleName = fullNameBenificiary[1];
                                                    objInspectn.BenfLastName = fullNameBenificiary[2];
                                                }
                                                if (fullNameBenificiary.Length == 2)
                                                {
                                                    objInspectn.BenfFirstName = fullNameBenificiary[0];
                                                    objInspectn.BenfLastName = fullNameBenificiary[1];
                                                }
                                                else
                                                {
                                                    objInspectn.BenfFirstName = fullNameBenificiary[0];

                                                }
                                            }
                                             


                                            if (rvd["ILevel"].ToString() == "3")
                                            {
                                                objInspectn.Inspection = "3";
                                                if (objInspectn.INSP_THREE_MOUD_APPROVE == "Y")
                                                {
                                                    objInspectn.Mode = "V";
                                                }
                                                else
                                                {
                                                    objInspectn.Mode = "U";
                                                }
                                            }
                                            else if (rvd["ILevel"].ToString() == "2")
                                            {
                                                objInspectn.Inspection = "2";
                                                if (objInspectn.INSP_TWO_MOUD_APPROVE == "Y")
                                                {
                                                    objInspectn.Mode = "V";
                                                }
                                                else
                                                {
                                                    objInspectn.Mode = "U";
                                                }
                                            }
                                            else if (rvd["ILevel"].ToString() == "1")
                                            {
                                                objInspectn.Inspection = "1";
                                                if (objInspectn.INSP_ONE_MOUD_APPROVE == "Y")
                                                {
                                                    objInspectn.Mode = "V";
                                                }
                                                else
                                                {
                                                    objInspectn.Mode = "U";
                                                }
                                            }

                                             

                                            objOwnList.Add(objOwn);
                                        }

                                        objInspectn.ListOwner = objOwnList;
                                        objInspectn.photo8Url = "/Content/images/Inspection/" + objInspectn.PHOTO_8.ConvertToString(); 
                                        objInspectn.photo7Url = "/Content/images/Inspection/" + objInspectn.PHOTO_7.ConvertToString();
                                        objInspectn.photo6Url = "/Content/images/Inspection/" + objInspectn.PHOTO_6.ConvertToString();
                                        objInspectn.photo5Url = "/Content/images/Inspection/" + objInspectn.PHOTO_5.ConvertToString();
                                        objInspectn.photo4Url = "/Content/images/Inspection/" + objInspectn.PHOTO_4.ConvertToString();
                                        objInspectn.photo3Url = "/Content/images/Inspection/" + objInspectn.PHOTO_3.ConvertToString();
                                        objInspectn.photo2Url = "/Content/images/Inspection/" + objInspectn.PHOTO_2.ConvertToString();
                                        objInspectn.photo1Url = "/Content/images/Inspection/" + objInspectn.PHOTO_1.ConvertToString();

                                        if (objInspectn.PHOTO_8.ConvertToString() == "")
                                        {
                                            objInspectn.photo8Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_7.ConvertToString() == "")
                                        {
                                            objInspectn.photo7Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_6.ConvertToString() == "")
                                        {
                                            objInspectn.photo6Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_5.ConvertToString() == "")
                                        {
                                            objInspectn.photo5Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_4.ConvertToString() == "")
                                        {
                                            objInspectn.photo4Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_3.ConvertToString() == "")
                                        {
                                            objInspectn.photo3Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_2.ConvertToString() == "")
                                        {
                                            objInspectn.photo2Url = "/Content/images/Inspection/NoImage.jpg";
                                        }
                                        if (objInspectn.PHOTO_1.ConvertToString() == "")
                                        {
                                            objInspectn.photo1Url = "/Content/images/Inspection/NoImage.jpg";
                                        } 
                                    }



                                }
                            }
                        }
                    }
                }






                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                Session["Design"] = ViewData["Design"];
                Session["DesignDesc"] = ViewData["DesignDesc"];
                Session["InspectionObject"] = objInspectn;


                return View("ManageInapection1", objInspectn);


            }


            //delete inspection
            public ActionResult deleteInspection(string data, string p)
            {
                bool deleteResult = false;
                 string id = "";
                RouteValueDictionary rvd = new RouteValueDictionary();
                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                InspectionDesignModelClass ObjDesign = new InspectionDesignModelClass();

                try
                {
                    rvd = QueryStringEncrypt.DecryptString(p);
                    if (p != null)
                    {


                        if (rvd != null)
                        {
                            if (rvd["id"] != null)
                            {
                                id = rvd["id"].ToString();
                                ObjDesign.NraDefCode = id;
                            }
                            if (rvd["id1"] != null)
                            {
                                string MstId = rvd["id1"].ToString();
                                ObjDesign.InspectionMstId = MstId;
                            }
                            if (rvd["id2"] != null)
                            {
                                string PaperId = rvd["id2"].ToString();
                                ObjDesign.InspectionPaperID = PaperId;
                            }
                            if (rvd["id3"] != null)
                            {
                                string InspectionLevel = rvd["id3"].ToString();
                                ObjDesign.InspectionLevel = InspectionLevel;
                            }
                            if (rvd["id5"] != null)
                            {
                                string houseOwnerId = rvd["id5"].ToString();
                                ObjDesign.hOwnerId = houseOwnerId;
                            }
                            //if (rvd["id5"] != null)
                            //{
                            //     scondDelete = rvd["id5"].ToString();

                            //}
                            deleteResult = ObjServDesign.deleteInspection(ObjDesign);

                        }
                    }
                }
                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                if (rvd["id5"].ToString() == "SecondDelete")
                {
                    Session["firstApproveAll"] = "Approved";
                    return RedirectToAction("InspectionSecondApprove");
                }
                if (rvd["id5"].ToString() == "FirstDelete")
                {
                    Session["firstApproveAll"] = "Approved";
                    return RedirectToAction("InspectionSearchApprove");
                }

                return RedirectToAction("InspectionSearchApprove");
            }

            //getImagename 
            public string GetImageName(string id)
            {
                string imagename = "";

                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                imagename = ObjServDesign.getImageNameFromDesign(id);
                return imagename;

            }

            //get comply or not
            public string GetComplyList(string id)
            {
                string listExist = "";
                InspectionNewWayService ObjServDesign = new InspectionNewWayService();
                listExist = ObjServDesign.getIfComplyExist(id);
                return listExist;
            }

            // CheckDuplicateSerialNumber
            public bool CheckDuplicateSerialNumber(string PANumber, string FormNumber)
            {
                bool result = false;
                InspectionNewWayService objServ = new InspectionNewWayService();
                result = objServ.getSerialNumber(PANumber, FormNumber);
                return result;
            }


            //get view of inspected data in approval process
            //edit Inspection 
            [HttpPost]
            public ActionResult GetInspectionDataForApprovalView(string Pa_number, string level)
            {

                string id = "";
                string HouseDesign = "";
                string DesignNumber = "";
                string owndesign = "";
                DataTable dtOwn = new DataTable();
                DataTable dtbl = new DataTable();
                CommonFunction com = new CommonFunction();
                InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
                InspectionNewWayService objService = new InspectionNewWayService();

                try
                {





                    id = Pa_number.ToString();
                    //get design number by nra code
                    DataTable designId = new DataTable();
                    designId = objService.GetAllDesignUpdate(id);
                    if (designId != null && designId.Rows.Count > 0)
                    {
                        if (designId.Rows[0]["CONSTRUCTION_MAT_CD"].ConvertToString() == "" && designId.Rows[0]["ROOF_MAT_CD"].ConvertToString() == "")
                        {
                            DesignNumber = designId.Rows[0]["DESIGN_NUMBER"].ConvertToString();


                            objInspectn.DesignNumber = DesignNumber;
                        }
                        else
                        {
                            string constructionMaterialId = designId.Rows[0]["CONSTRUCTION_MAT_CD"].ConvertToString();
                            string RoofMaterialId = designId.Rows[0]["ROOF_MAT_CD"].ConvertToString();

                            DesignNumber = objService.getDesignByMaterialID(constructionMaterialId, RoofMaterialId);
                            objInspectn.DesignNumber = DesignNumber;
                        }

                    }

                    //for inspection One
                    if (level.ToString() == "1")
                    {
                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(objInspectn.DesignNumber);
                        ViewData["Design"] = com.GetDescriptionFromCode(objInspectn.DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");
                        owndesign = com.GetDescriptionFromCode(objInspectn.DesignNumber, "NHRS_HOUSE_MODEL", "MODEL_ID");
                        if (owndesign == "Own Design" || owndesign == "आफ्नै डिजाइन")
                        {
                            owndesign = "Y";

                        }
                        if (owndesign != "Y")
                        {
                            objInspectn.Hierarchy_cd = objService.getHierarchyCd(objInspectn.DesignNumber);
                            if (objInspectn.Hierarchy_cd.ConvertToString() == "")
                            {
                                ViewData["Design"] = Utils.ToggleLanguage("Inspection one not found", "सम्बन्धित पहिलो निरीक्षण भेटिएन");
                            }
                        }

                        objInspectn.Inspection = "1";

                    }
                    //for inspection Two
                    if (level.ToString() == "2")
                    {
                        objInspectn.Inspection = "2";
                        string[] NextDesign = objService.getNextInspectionDesignId(objInspectn.DesignNumber);
                        string houseDesignId = NextDesign[0];
                        objInspectn.DesignNumber = houseDesignId;
                        objInspectn.Hierarchy_cd = NextDesign[1];

                        ViewData["Design"] = com.GetDescriptionFromCode(houseDesignId, "NHRS_HOUSE_MODEL", "MODEL_ID");
                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(houseDesignId);
                        if (ViewData["Design"].ConvertToString() == "Own Design" || ViewData["Design"].ConvertToString() == "आफ्नै डिजाइन")
                        {
                            owndesign = "Y";

                        }

                    }
                    //for inspection Three
                    if (level.ToString() == "3")
                    {
                        objInspectn.Inspection = "3";
                        //FOR INSPECTIO TWO
                        string houseDesignId = "";
                        string[] NextDesign = objService.getNextInspectionDesignId(objInspectn.DesignNumber);
                        if (NextDesign.Length > 0)
                        {
                            houseDesignId = NextDesign[0];
                            objInspectn.Hierarchy_cd = NextDesign[1];
                            objInspectn.DesignNumber = houseDesignId;
                        }



                        //FOR INSPECTION 3
                        string[] DesignThree = objService.getNextInspectionDesignId(houseDesignId);
                        string DesignID = "";
                        if (DesignThree != null)
                        {
                            DesignID = DesignThree[0];
                            objInspectn.Hierarchy_cd = DesignThree[1];
                            objInspectn.DesignNumber = DesignID;
                        }
                        else
                        {
                            objInspectn.Hierarchy_cd = "";
                            objInspectn.DesignNumber = DesignID;
                        }

                        ViewData["Design"] = com.GetDescriptionFromCode(DesignID, "NHRS_HOUSE_MODEL", "MODEL_ID");
                        ViewData["DesignDesc"] = objService.GetHouseDesignDescription(DesignID);
                        if (ViewData["Design"].ConvertToString() == "Own Design" || ViewData["Design"].ConvertToString() == "आफ्नै डिजाइन")
                        {
                            owndesign = "Y";

                        }
                    }

                    //objInspectn.DesignNumber = rvd["id6"].ToString();
                    objInspectn = objService.getAllInnfoForInspection1(id, owndesign, objInspectn.DesignNumber);
                    objInspectn.InspectionLevel = level;
                    Session["Insp1Edit"] = (InspectionDesignModelClass)objInspectn;






                    objInspectn.Bank_Name = com.GetDescriptionFromCode(objInspectn.Bank_Name.ConvertToString(), "NHRS_BANK", "BANK_CD");
                    objInspectn.district_Cd = com.GetDescriptionFromCode(objInspectn.district_Cd.ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                    objInspectn.BenfRelation = com.GetDescriptionFromCode(objInspectn.BenfRelation.ConvertToString(), "MIS_RELATION_TYPE", "RELATION_TYPE_CD");
                    objInspectn.VdcDefCode = com.GetDescriptionFromCode(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");


                    dtOwn = objService.getOwnDetail(id);
                    if (dtOwn != null && dtOwn.Rows.Count > 0)
                    {
                        List<InspectionOwnerDetailModelClass> objOwnList = new List<InspectionOwnerDetailModelClass>();
                        foreach (DataRow drOwn in dtOwn.Rows)
                        {
                            InspectionOwnerDetailModelClass objOwn = new InspectionOwnerDetailModelClass();
                            objOwn.OwnerFNameE = drOwn["HOUSE_OWNER_NAME"].ConvertToString();

                            objOwn.OwnerDistCd = com.GetDescriptionFromCode(drOwn["DISTRICT_CD"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                            objOwn.OwnVdcCd = com.GetDescriptionFromCode(drOwn["VDC_MUN_CD"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                            objOwn.OwnWaard = drOwn["WARD_NO"].ConvertToString();

                            objOwn.paNumber = drOwn["NRA_DEFINED_CD"].ConvertToString();
                            //objOwn.kittaNum = drOwn["NRA_DEFINED_CD"].ConvertToString();
                            objOwn.toleE = drOwn["AREA_ENG"].ConvertToString();
                            objOwn.toleL = drOwn["AREA_ENG"].ConvertToString();

                            objOwn.HouseHoldId = drOwn["HOUSEHOLD_ID"].ConvertToString();
                            objOwn.BuldingStrNo = objService.GetBuildingStructure(objOwn);
                            objOwn.HouseSno = drOwn["HOUSE_SNO"].ConvertToString();
                            objInspectn.InspectionMstId = drOwn["INSPECTION_MST_ID"].ConvertToString();

                            objInspectn.KittaNumber = drOwn["KITTA_NUMBER"].ConvertToString();
                            objInspectn.hOwnerId = drOwn["HOUSE_OWNER_ID"].ConvertToString();
                            //objInspectn.DesignNumber = drOwn["DESIGN_NUMBER"].ConvertToString();
                            objInspectn.BenfFullNameEng = drOwn["DESIGN_BENF"].ConvertToString();
                            objInspectn.ConstMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["CONSTRUCTION_MAT_CD"].ConvertToString(), "NHRS_CONSTRUCTION_MATERIAL", "CONSTRUCTION_MAT_CD");
                            objInspectn.RoofMaterialDesc = com.GetDescriptionFromCodeByName(drOwn["ROOF_MAT_CD"].ConvertToString(), "NHRS_RC_MATERIAL", "RC_MAT_CD");

                            if (objInspectn.BenfFullNameEng.ConvertToString() != "" && objInspectn.BenfFullNameEng.ConvertToString() != null)
                            {
                                string[] fullNameBenificiary = objInspectn.BenfFullNameEng.ConvertToString().Split(' ');
                                if (fullNameBenificiary.Length == 3)
                                {
                                    objInspectn.BenfFirstName = fullNameBenificiary[0];
                                    objInspectn.BenfMiddleName = fullNameBenificiary[1];
                                    objInspectn.BenfLastName = fullNameBenificiary[2];
                                }
                                if (fullNameBenificiary.Length == 2)
                                {
                                    objInspectn.BenfFirstName = fullNameBenificiary[0];
                                    objInspectn.BenfLastName = fullNameBenificiary[1];
                                }
                                else
                                {
                                    objInspectn.BenfFirstName = fullNameBenificiary[0];

                                }
                            }




                            objOwnList.Add(objOwn);
                        }

                        objInspectn.ListOwner = objOwnList;
                        objInspectn.GharkoNaksaUrl = "/Content/images/Inspection/" + objInspectn.PHOTO_HOUSE.ConvertToString();
                        objInspectn.photo6Url = "/Content/images/Inspection/" + objInspectn.PHOTO_6.ConvertToString();
                        objInspectn.photo5Url = "/Content/images/Inspection/" + objInspectn.PHOTO_5.ConvertToString();
                        objInspectn.photo4Url = "/Content/images/Inspection/" + objInspectn.PHOTO_4.ConvertToString();
                        objInspectn.photo3Url = "/Content/images/Inspection/" + objInspectn.PHOTO_3.ConvertToString();
                        objInspectn.photo2Url = "/Content/images/Inspection/" + objInspectn.PHOTO_2.ConvertToString();
                        objInspectn.photo1Url = "/Content/images/Inspection/" + objInspectn.PHOTO_1.ConvertToString();

                        if (objInspectn.PHOTO_HOUSE.ConvertToString() == "")
                        {
                            objInspectn.GharkoNaksaUrl = "/Content/images/Inspection/NoPhoto.png";
                        }
                        if (objInspectn.PHOTO_5.ConvertToString() == "")
                        {
                            objInspectn.photo5Url = "/Content/images/Inspection/NoPhoto.png";
                        }
                        if (objInspectn.PHOTO_4.ConvertToString() == "")
                        {
                            objInspectn.photo4Url = "/Content/images/Inspection/NoPhoto.png";
                        }
                        if (objInspectn.PHOTO_3.ConvertToString() == "")
                        {
                            objInspectn.photo3Url = "/Content/images/Inspection/NoPhoto.png";
                        }
                        if (objInspectn.PHOTO_2.ConvertToString() == "")
                        {
                            objInspectn.photo2Url = "/Content/images/Inspection/NoPhoto.png";
                        }
                        if (objInspectn.PHOTO_1.ConvertToString() == "")
                        {
                            objInspectn.photo1Url = "/Content/images/Inspection/NoPhoto.png";
                        }

                    }






                }






                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                Session["Inspectionlevel"] = level;
                return PartialView("_InspectionView", objInspectn);
            }



          
          

            #region HTML TO PDF
            public ActionResult GenPDF(FormCollection fc)
            {
                string id = fc["Serial_Num"].ConvertToString();
                string id2 = fc["NraDefCode"].ConvertToString();
                HtmlToPdf converter = new HtmlToPdf();
                InspectionDetailService ins = new InspectionDetailService();
                TextWriter writer = new StringWriter();
                string htmlString = RenderViewToString("InspectionPrintView");

                string baseUrl = ins.GetBaseUrl();

                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

                byte[] pdf = doc.Save();
                doc.Close();
                FileResult fileResult = new FileContentResult(pdf, "application/pdf");
                fileResult.FileDownloadName = "InspectionForm_'" + id2 + "_" + id + "'.pdf";
                return fileResult;
            }

            protected string RenderViewToString(string viewName)
            {
                ViewData.Model = Session["InspectionObject"];
                InspectionDesignModelClass objInspectn = (InspectionDesignModelClass)Session["InspectionObject"];

                InspectionItem objInspectionItem = (InspectionItem)Session["InspDetailForPrint"];
                ViewBag.objInsptData = objInspectionItem;

                ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1(objInspectn.OwnDesign.ConvertToString()).Data;
                ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial(objInspectn.ConstructMat.ConvertToString(), "");
                ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial(objInspectn.Roofmat.ConvertToString(), "");
                ViewData["ddl_Districts"] = com.GetDistricts(objInspectn.district_Cd.ConvertToString());
                objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), objInspectn.district_Cd.ConvertToString());
                ViewData["ddl_Wards"] = com.GetWardByVDCMun(objInspectn.ward_no.ConvertToString(), "");
                //objInspectn.Bank_def_Code = com.GetCodeFromDataBase(objInspectn.Bank_Name.ConvertToString(), "NHRS_BANK", "BANK_CD");
                ViewData["ddl_Relation"] = com.GetRelation(objInspectn.BenfRelation.ConvertToString());
                ViewData["Design"] = Session["Design"];
                ViewData["DesignDesc"] = Session["DesignDesc"];
                ViewData["ddl_bankname"] = com.GetBankName(objInspectn.Bank_Name.ConvertToString(), "", "");

                if (string.IsNullOrEmpty(viewName))
                    viewName = ControllerContext.RouteData.GetRequiredString("action");

                using (StringWriter sw = new StringWriter())
                {
                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, "");
                    ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    return sw.GetStringBuilder().ToString();
                }

            }

            #endregion HTML TO PDF


        public string GetPhoto(string PANumber, string PhotoType)
        
        {

            string PhotoExist ="";
            PhotoExist = "NoImage.jpg";
            return PhotoExist;
        }

        public ActionResult PhotoUpload()
        {
            CheckPermission();


            if (CommonVariables.GroupCD == "58")
            {
                string Districtcode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode);
                ViewData["ddl_Districts"] = com.GetDistrictsByDistrictCode(Districtcode);
                ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", Districtcode);
                ViewData["DistrctCd"] = Districtcode;
            }
            else
            {
                ViewData["ddl_Districts"] = com.GetDistricts("");
                ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict("", "");
            }



            ViewData["ddl_Wards"] = com.GetWardByVDCMun("", "");
            ViewData["ddl_HouseModel"] = objService.GetHouseModel("", "");
            ViewData["ddl_houseDesign"] = objService.GetHouseDesign("", "");
            ViewData["ddl_Inspection_number"] = com.GetInspectionNo("");
            ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
            ViewData["yesNogap"] = (List<SelectListItem>)com.GetYesNo5("").Data;
            ViewData["ddl_Inspection_Application"] = (List<SelectListItem>)com.GetInspectionApplication("").Data;
            ViewData["ddl_Inspection_Remarks"] = (List<SelectListItem>)com.InspectionRemarksType("").Data;
            ViewData["ddl_Inspection_tab"] = (List<SelectListItem>)com.InspectionTabManual("").Data;
            string nraDefineCd = Session["nraDefCode"].ConvertToString();
            if (nraDefineCd != "")
            {
                ViewData["NraDefCd"] = nraDefineCd;
                return View();
            }

            string nraDefineCdInsp1 = Session["nraDefCodeInspection1"].ConvertToString();
            if (nraDefineCdInsp1 != "")
            {
                ViewData["nraDefCodeInspection1"] = nraDefineCdInsp1;
                return View();
            }
            if (Session["PaNumSaveUpdate"].ConvertToString() != "" && Session["PaNumSaveUpdate"].ConvertToString() != null)
            {
                ViewData["PaNumSaveUpdate"] = Session["PaNumSaveUpdate"].ConvertToString();
                return View();
            }
            ViewData["NraDefCd"] = "";
            ViewData["nraDefCodeInspection1"] = "";
            return View();
        }
        [HttpPost]
        public ActionResult PhotoUpload(FormCollection Fc)
        {

            CheckPermission();
            string distCode = null;
            if (CommonVariables.GroupCD == "58")
            {
                distCode = CommonFunction.GetDistrictByEmployeeCode(CommonVariables.EmpCode); 
            }
             
                
                string Id = Fc["BenfId"].ConvertToString();
                DataTable results = objService.GetBeneficiary(distCode, Id);
                ViewData["results"] = results;
              
                return PartialView("_InspectionPhotoUpload");
             

           
        }

        public ActionResult SavePhoto(InspectionDesignModelClass objModel, FormCollection fc)
        {
            try
            {
                objModel.NraDefCode = fc["NRA_DEFINED_CD"].ConvertToString();
                if (objModel.PHOTO_1  != null)
                {
                     objModel.PHOTO_1 = objModel.PHOTO_1.ConvertToString() + ".jpg"; 
                }
                if (objModel.PHOTO_2  != null)
                {
                     objModel.PHOTO_2 = objModel.PHOTO_2.ConvertToString() + ".jpg";
                }
                if (objModel.PHOTO_3  != null)
                {
                     objModel.PHOTO_3 = objModel.PHOTO_3.ConvertToString() + ".jpg";
                }
                if (objModel.PHOTO_4  != null)
                {
                     objModel.PHOTO_4 = objModel.PHOTO_4.ConvertToString() + ".jpg";
                }
                if (  objModel.PHOTO_5 !=null )
                {
                     objModel.PHOTO_5 = objModel.PHOTO_5.ConvertToString() + ".jpg";
                }
                if (objModel.PHOTO_6  != null)
                {
                     objModel.PHOTO_6 = objModel.PHOTO_6.ConvertToString() + ".jpg";
                }
                if (objModel.PHOTO_7  != null)
                {
                     objModel.PHOTO_7 = objModel.PHOTO_7.ConvertToString() + ".jpg";
                }
                if (objModel.PHOTO_8  != null)
                {
                     objModel.PHOTO_8 = objModel.PHOTO_8.ConvertToString() + ".jpg";
                }

                if (fc["btn_Submit"].ConvertToString() == "Submit" || fc["btn_Submit"].ConvertToString() == "पेश गर्नुहोस्")
                {
                    objModel.Mode = "I";
                }
                else
                {
                    //objModel.Mode = "U";
                    objModel.Mode = "I";
                }
                bool result = objService.saveInspectionPhotoOnly(objModel);
            }
            catch(OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
           catch(Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return View();
        }

        public string CheckIfMoudApproved(string PANumber, string InspLevel)
        {
            string result = null;
            try
            {
                result = objService.CheckIfMoudApprove(PANumber, InspLevel);

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            return result;
        }
    }
}
