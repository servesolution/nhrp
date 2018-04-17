using EntityFramework;
using ExceptionHandler;
using MIS.Models.Inspection;
using MIS.Services.Core;
using MIS.Services.Inspection;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Inspection
{
    public class InspectionEngineerApproveController : BaseController
    {
        //
        // GET: /InspectionEngineerApprove/
        CommonFunction com = new CommonFunction();
         public ActionResult ManageEngineerApproval()
        {
            
            
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
           ViewData["ddl_Inspection_number"] = com.GetInspectionLevel("");
           ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data; 
           ViewData["EngineerApprovePa"] = Session["EngineerApprovePa"].ConvertToString();

            return View();
        }

        [HttpPost]
        public ActionResult ManageEngineerApproval(FormCollection Fc)
        {
             InspectionEngineersApprovalService objService = new InspectionEngineersApprovalService();
            InspectionDetailModelClass objInspection = new InspectionDetailModelClass();
            DataTable dt = new DataTable();
            objInspection.district_Cd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
            objInspection.vdc_mun_cd = com.GetCodeFromDataBase(Fc["vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
            objInspection.ward_no = Fc["ward"].ConvertToString();
            string name = Fc["nameBenf"].ConvertToString();
            string InspLevel = Fc["ddl_Inspection_number"].ConvertToString();
            string EngineerApprove = Fc["YesNo"].ConvertToString();
            string Id = Fc["TB1"].ConvertToString() + "-" + Fc["TB2"].ConvertToString() + "-" + Fc["TB3"].ConvertToString() + "-" + Fc["TB4"].ConvertToString() + "-" + Fc["TB5"].ConvertToString();
            if (Id == "----")
            {
                Id = "";
            }
            if(Session["EngineerApprovePa"].ConvertToString().Trim()!="")
            {

                objInspection.district_Cd= Session["distEngineer"].ConvertToString();
                objInspection.vdc_mun_cd= Session["vdcEngineer"].ConvertToString();
                objInspection.ward_no=Session["wardEngineer"].ConvertToString();
                name = Session["nameEngineer"].ConvertToString();
                Id= Session["IdEngineer"].ConvertToString(); 
                InspLevel=Session["InspLevelEngineer"].ConvertToString();
                EngineerApprove= Session["EngineerApproveEngineer"].ConvertToString();

                Session["EngineerApprovePa"] = null;
                
                //dt = objService.GetInspectionSearchResults(objInspection, name, Id, InspLevel, EngineerApprove,"100","0");
               // ViewData["results"] = dt;


                return PartialView("_InspectionEngineersApproval", objInspection);
            }
                #region session for engineer approval
                    Session["distEngineer"] = objInspection.district_Cd.ConvertToString();
                    Session["vdcEngineer"] = objInspection.vdc_mun_cd.ConvertToString();
                    Session["wardEngineer"] = objInspection.ward_no.ConvertToString();
                    Session["nameEngineer"] = name;
                    Session["IdEngineer"] = Id;
                    Session["InspLevelEngineer"] = InspLevel;
                    Session["EngineerApproveEngineer"] = EngineerApprove;
                #endregion
                    NameValueCollection paramValues = new NameValueCollection();
                    paramValues.Add("dist", objInspection.district_Cd);
                    paramValues.Add("vdc", objInspection.vdc_mun_cd);
                    paramValues.Add("ward", objInspection.ward_no);
                    paramValues.Add("name", name);
                    paramValues.Add("Id", Id);
                    paramValues.Add("inspectionLevel", InspLevel);
                    paramValues.Add("userDistrict", string.Empty);
                    paramValues.Add("engineerApprove", EngineerApprove);
            
            //dt = objService.GetInspectionSearchResults(objInspection, name, Id, InspLevel, EngineerApprove,"100","0");
            //ViewData["results"] = dt;
            return PartialView("_InspectionEngineersApproval",paramValues);
        }
        [HttpPost]
        public JsonResult GetGetInspectionSearchJsonResults(string dist, string vdc, string ward, string name, string id, string inspectionLevel, string userDistrict, string engineerApprove,  int start, int length, int draw)
        {
            InspectionEngineersApprovalService objService = new InspectionEngineersApprovalService();
            NameValueCollection paramValues = new NameValueCollection();
            if (string.IsNullOrEmpty(dist))
                paramValues.Add("dist", dist);
            if (string.IsNullOrEmpty(vdc))
                paramValues.Add("vdc", vdc);
            if (ward.ConvertToString() != string.Empty)
                paramValues.Add("ward", ward);
            if (string.IsNullOrEmpty(name))
                paramValues.Add("name", name);
            if (string.IsNullOrEmpty(name))
                paramValues.Add("id", id);
            if (string.IsNullOrEmpty(inspectionLevel))
                paramValues.Add("inspectionLevel", inspectionLevel);
            if (string.IsNullOrEmpty(userDistrict))
                paramValues.Add("userDistrict", userDistrict);
            if (string.IsNullOrEmpty(engineerApprove))
                paramValues.Add("engineerApprove", engineerApprove);
            paramValues.Add("pagesize", length);
            paramValues.Add("pageindex", start);



            DataTable dt = new DataTable();
            dt = objService.GetInspectionSearchResults(paramValues);
            var recordCount = 0;
            if (dt != null && dt.Rows.Count > 0)
                recordCount = Convert.ToInt32(dt.Rows[0]["TOTALNUM"]);

            var listDatat = Common.ConvertToList(dt);
            var dataToReturn = Json(new
            {
                data = listDatat,
                recordsFiltered = recordCount,
                recordsTotal = recordCount,
                draw
            }, JsonRequestBehavior.AllowGet);
            dataToReturn.MaxJsonLength = Int32.MaxValue;
            return dataToReturn;
        }

        #region single inspection approve by inspection engineer
        [HttpPost]
        public bool EngineersAgreementSingle(string nraCode, string inspLevel, string checkvalue)
        {
            string NraDefinedCd = nraCode;
            string InspectionLevel = inspLevel;
            bool result = false;
            string final_check = "";




            RouteValueDictionary rvd = new RouteValueDictionary();
            InspectionEngineersApprovalService objService = new InspectionEngineersApprovalService();
            try
            {

                if (checkvalue == "Y")
                {
                    final_check = "";
                }
                if (checkvalue == "N")
                {
                    final_check = "Y";
                }

                result = objService.SingleSecondApprove(NraDefinedCd, InspectionLevel, final_check);

            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            if (result == true)
            {
                System.Web.HttpContext.Current.Session["EngineerApprovePas"] = nraCode.ConvertToString(); 
                //Session["EngineerApprovePas"] = nraCode.ConvertToString();
            }
            //return RedirectToAction("ManageEngineerApproval");

            return result;
        }
        
        #endregion
         
        #region edit by engineers

        public ActionResult EditInspection(string param)
        {

            string id = "";
            string HouseDesign = "";
            string DesignNumber = "";
            string owndesign = "";
            DataTable dtOwn = new DataTable();
            DataTable dtbl = new DataTable();
            CommonFunction com = new CommonFunction();
            InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
            InspectionPaperService objService = new InspectionPaperService();
            RouteValueDictionary rvd = new RouteValueDictionary();

            try
            {

                rvd = QueryStringEncrypt.DecryptString(param);
                if (param != null)
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

                                //objInspectn.DesignNumber = rvd["id6"].ToString();
                                objInspectn = objService.getAllInnfoForInspection1(id, owndesign, objInspectn.DesignNumber);
                                Session["Inspectionlevel"] = rvd["ILevel"].ToString();

                                Session["Insp1Edit"] = (InspectionDesignModelClass)objInspectn;



                                //if (objInspectn.InspectionStatus == "A")
                                //{
                                //    objInspectn.Mode = "V";
                                //}
                                //else
                                //{
                                //    objInspectn.Mode = "U";
                                //}



                                //ViewData["DesignDesc"] = objService.GetHouseDesignDescription(HouseDesign);
                                ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1(objInspectn.OwnDesign.ConvertToString()).Data;
                                ViewData["ddl_PillarType"] = com.GetInspectionPillarMaterial(objInspectn.ConstructMat.ConvertToString(), "");
                                ViewData["ddl_RoofMaterial"] = com.GetInspectionRoofMaterial(objInspectn.Roofmat.ConvertToString(), "");
                                ViewData["ddl_Districts"] = com.GetDistricts(objInspectn.district_Cd.ConvertToString());
                                objInspectn.VdcDefCode = com.GetDefinedCodeFromDataBase(objInspectn.vdc_mun_cd.ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                                ViewData["ddl_Vdc"] = com.GetVDCMunByAllDistrict(objInspectn.VdcDefCode.ConvertToString(), objInspectn.district_Cd.ConvertToString());
                                ViewData["ddl_Wards"] = com.GetWardByVDCMun(objInspectn.ward_no.ConvertToString(), "");
                                //objInspectn.Bank_def_Code = com.GetCodeFromDataBase(objInspectn.Bank_Name.ConvertToString(), "NHRS_BANK", "BANK_CD");
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
                                        //if (DesignNumber == "")
                                        //{
                                        //    HouseDesign = drOwn["HOUSE_MODEL_CD"].ConvertToString();
                                        //}
                                        //else
                                        //{
                                        //    HouseDesign = drOwn["DESIGN_NUMBER"].ConvertToString();
                                        //}




                                        //if (drOwn["INSPECTION_LEVEL1"].ConvertToString() == "1" && rvd["ILevel"].ToString() == "1")
                                        //{
                                        //    objInspectn.Inspection = "1";

                                        //    if (objInspectn.InspectionStatus == "A")
                                        //    {
                                        //        objInspectn.Mode = "V";
                                        //    }
                                        //    else
                                        //    {
                                        //        if (rvd["ILevel"].ToString() == "1")
                                        //        {
                                        //            objInspectn.Mode = "U";
                                        //        }
                                        //        else
                                        //        {
                                        //            objInspectn.Mode = "V";
                                        //        }

                                        //    }
                                        //}

                                        //if (drOwn["INSPECTION_LEVEL2"].ConvertToString() == "2" && rvd["ILevel"].ToString() == "2")
                                        //{
                                        //    objInspectn.Inspection = "2";

                                        //    if (objInspectn.InspectionStatus == "A")
                                        //    {
                                        //        objInspectn.Mode = "V";
                                        //    }
                                        //    else
                                        //    {
                                        //        if (rvd["ILevel"].ToString() == "2")
                                        //        {
                                        //            objInspectn.Mode = "U";
                                        //        }
                                        //        else
                                        //        {
                                        //            objInspectn.Mode = "V";
                                        //        }

                                        //    }
                                        //}
                                        //if (drOwn["INSPECTION_LEVEL3"].ConvertToString() == "3" && rvd["ILevel"].ToString() == "3")
                                        //{
                                        //    objInspectn.Inspection = "3";

                                        //    if (objInspectn.InspectionStatus == "A")
                                        //    {
                                        //        objInspectn.Mode = "V";
                                        //    }
                                        //    else
                                        //    {
                                        //        if (rvd["ILevel"].ToString() == "3")
                                        //        {
                                        //            objInspectn.Mode = "U";
                                        //        }
                                        //        else
                                        //        {
                                        //            objInspectn.Mode = "V";
                                        //        }

                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    if (objInspectn.InspectionStatus == "A")
                                        //    {
                                        //        objInspectn.Mode = "V";
                                        //    }
                                        //    else
                                        //    {
                                        //        objInspectn.Mode = "U";
                                        //    }
                                        //}


                                        //if (drOwn["INSPECTION_LEVEL3"].ConvertToString() == "3" )
                                        //{
                                        //    objInspectn.Inspection = "3";

                                        //    if (objInspectn.final_decision_approve == "Y") 
                                        //    {
                                        //        objInspectn.Mode = "V";
                                        //    }
                                        //    else
                                        //    {
                                        //        if (rvd["ILevel"].ToString() == "3")
                                        //        {
                                        //            objInspectn.Mode = "U";
                                        //        }
                                        //        else
                                        //        {
                                        //            objInspectn.Mode = "V";
                                        //        }

                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    if (drOwn["INSPECTION_LEVEL2"].ConvertToString() == "2")
                                        //    {
                                        //        objInspectn.Inspection = "2";

                                        //        if (objInspectn.final_decision_approve == "Y")
                                        //        {
                                        //            objInspectn.Mode = "V";
                                        //        }
                                        //        else
                                        //        {
                                        //            if (rvd["ILevel"].ToString() == "2")
                                        //            {
                                        //                objInspectn.Mode = "U";
                                        //            }
                                        //            else
                                        //            {
                                        //                objInspectn.Mode = "V";
                                        //            }

                                        //        }
                                        //    }
                                        //    else
                                        //    {
                                        //        if (drOwn["INSPECTION_LEVEL1"].ConvertToString() == "1")
                                        //        {
                                        //            objInspectn.Inspection = "1";

                                        //            if (objInspectn.final_decision_approve == "Y")
                                        //            {
                                        //                objInspectn.Mode = "V";
                                        //            }
                                        //            else
                                        //            {
                                        //                if (rvd["ILevel"].ToString() == "1")
                                        //                {
                                        //                    objInspectn.Mode = "U";
                                        //                }
                                        //                else
                                        //                {
                                        //                    objInspectn.Mode = "V";
                                        //                }

                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            if (objInspectn.final_decision_approve == "Y")
                                        //             {
                                        //                    objInspectn.Mode = "V";
                                        //             }
                                        //            else
                                        //            {
                                        //                objInspectn.Mode = "U";
                                        //            }
                                        //         }
                                        //    }
                                        //}







                                        if (rvd["ILevel"].ToString() == "3")
                                        {
                                            objInspectn.Inspection = "3";
                                            if(objInspectn.INSP_THREE_ENGI_APPROVE=="Y")
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "Y";
                                            }
                                            else
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "N";
                                            }

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


                                            if (objInspectn.INSP_TWO_ENGI_APPROVE == "Y")
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "Y";
                                            }
                                            else
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "N";
                                            }

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

                                            if (objInspectn.INSP_ONE_ENGI_APPROVE == "Y")
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "Y";
                                            }
                                            else
                                            {
                                                objInspectn.INSPECTION_ENGINEER_AGREEMENT = "N";
                                            }

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
            Session["EngineersAgreement"] = "EngineersAgreement";
            Session["EngineerApprovePa"] = id;
            ViewData["EngineerEdit"] = "EngineerEdit";
            return View("InspectionEngineerEdit", objInspectn);
        }
       
        #endregion

        
        public PartialViewResult GetModelInsception(string id, string design, string InspectionLevel, string HierarchyCode)
        {
            InspectionOwnDesign objOwnDesign = new InspectionOwnDesign();
            InspectionDetailModelClass objInspectn = new InspectionDetailModelClass();
            InspectionPaperService objService = new InspectionPaperService();
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
                return PartialView("../InspectionPaper/_OwnHouseDesign", objOwnDesign);
            }

            

            if (HierarchyCode != "")
            {
                objInspection = objService.GetInspectionTreeInspection(HierarchyCode);
                ViewBag.objInsptData = objInspection;
            }
            ViewBag.objInsptData = objInspection;
            Session["InspDetailForPrint1"] = objInspection;


            //for editing inspection1
            InspectionDesignModelClass objEditInsp1 = new InspectionDesignModelClass();
            objEditInsp1 = (InspectionDesignModelClass)Session["Insp1Edit"];
            if (objEditInsp1 != null)
            {
                objInspection.objComplyModelList = objEditInsp1.InspectionEditList;
                Session["Insp1Edit"] = null;
            }



            return PartialView("../InspectionPaper/_InspectionDetail", objInspectn);

        }


        // vdc wise data fro engineers view
        public ActionResult VdcWiseData(string distCd)
        {
            DataTable dt = new DataTable();
            InspectionEngineersApprovalService objService = new InspectionEngineersApprovalService();
            string Lang= null;
            if (Session["LanguageSetting"].ToString() == "English")
            {
                Lang="E";
            }
            else
            {
                Lang = "N";
            }
            dt = objService.VdcWiseData(distCd,Lang);

            ViewData["results"] = dt;
            return PartialView("_InspectionEngineersVdcWiseList");
        }
        #region HTML TO PDF
        public ActionResult GenPDF(FormCollection fc)
        {
            string id = fc["Serial_Num"].ConvertToString();
            string id2 = fc["NraDefCode"].ConvertToString();
            HtmlToPdf converter = new HtmlToPdf();
            InspectionDetailService ins = new InspectionDetailService();
            TextWriter writer = new StringWriter();
            string htmlString = RenderViewToString("../InspectionPaper/InspectionPrintView");

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

            InspectionItem objInspectionItem = (InspectionItem)Session["InspDetailForPrint1"];
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
    }
}
