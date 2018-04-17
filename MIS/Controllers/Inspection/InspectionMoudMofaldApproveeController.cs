using ExceptionHandler;
using MIS.Models.Inspection;
using MIS.Models.Security;
using MIS.Services.Core;
using MIS.Services.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MIS.Controllers.Inspection
{
    public class InspectionMoudMofaldApproveeController : BaseController
    {
        CommonFunction com = new CommonFunction();

        #region MOUD approve list
        public ActionResult InspectionMOUDApproveList()
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
                            ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
                            ViewData["ddl_batch"] = com.GetMOUDApprovedbatch("");
                            ViewData["ddlFinalDecision"] = (List<SelectListItem>)com.getInspectionYesNo("");
                            ViewData["FirstApprovalAll"] = Session["firstApproveAll"];
                            if (ViewData["FirstApprovalAll"].ConvertToString() == "Approved")
                            {
                                ViewData["Message"] = "Approved Successfully";
                            }
                            if (ViewData["FirstApprovalAll"].ConvertToString() == "NotApproved")
                            {
                                ViewData["Message"] = "Records not approved !!!";
                            }
                            Session["firstApproveAll"] = null;
                            ViewData["MoudApproveBatch"] = Session["MoudApproveBatch"].ConvertToString();

                            return View();
                        }

                        [HttpPost]
                        public ActionResult InspectionMOUDApproveList(FormCollection Fc)
                        {
                            InspectionDetailModelClass objInspection = new InspectionDetailModelClass();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService(); 
                            objInspection.district_Cd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                            objInspection.vdc_mun_cd = com.GetCodeFromDataBase(Fc["ddl_Vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                            objInspection.ward_no = Fc["ddl_Wards"].ConvertToString();
                            string Id = Fc["TB1"].ConvertToString() + "-" + Fc["TB2"].ConvertToString() + "-" + Fc["TB3"].ConvertToString() + "-" + Fc["TB4"].ConvertToString() + "-" + Fc["TB5"].ConvertToString();
                            if (Id == "----")
                            {
                                Id = "";
                            }
                            string name = Fc["nameRecp"].ConvertToString();
                            string inspectionLvlCd = Fc["InspectionLevel"].ConvertToString();

                            string Approved = Fc["ApproveYesNo"].ConvertToString();
                            string recommendation = Fc["YesNo"].ConvertToString();

                            string Batch = Fc["Batch"].ConvertToString();
                            string finalDecision = Fc["finalDecision"].ConvertToString();

                            if(Session["MoudApproveBatch"].ConvertToString().Trim()!="")
                            {
                                Batch = Session["MoudApproveBatch"].ConvertToString();
                                Session["MoudApproveBatch"] = null;
                            }

                            DataTable results = objService.GetInspectionMOUDList(objInspection, Id, name, inspectionLvlCd, Approved, Batch, recommendation, finalDecision);

                            ViewData["results"] = results;
                            return PartialView("_InspectionSearchApprove");
                        }
        #endregion 


        #region all engineers approved inspection will be MOUD approved here
                        


                        public ActionResult MOUDApproveAll(string p, string checkedId)
                        {

                            if( checkedId!=null && checkedId !="")
                            {
                                string[] idSplit = checkedId.ConvertToString().Split(',');
                                if (idSplit.Length>1)
                                {
                                   bool result =  MOUDApproveAllByCheckbox(checkedId); 
                                   return RedirectToAction("InspectionMOUDApproveList");
                                }
                                
                            }
                           // string a = fc["parameterss"].ConvertToString();
                            string NraDefCd = "";
                            string InspMstId = "";
                            string paperId = "";
                            string InspectionLevel = "";
                            int j = 0;
                            string batch = "";
                            Users obj;
                            InspectionDesignModelClass ObjDesign = new InspectionDesignModelClass();
                            RouteValueDictionary rvd = new RouteValueDictionary();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                            string strUsername = "";
                            //  string[] ApproveItem = p.Split(','); 
                            string[] ApproveItem = p.Split('+');
                            int lengtOfItem = ApproveItem.Length;

                            try
                            {
                                int i = 0;
                                do
                                {
                                    rvd = QueryStringEncrypt.DecryptString(p);

                                    if (rvd != null)
                                    {

                                        if (rvd["MultiCount"] != null)
                                        {
                                            j = rvd["MultiCount"].ToInt32();
                                            if (j == 0)
                                            {
                                                j = rvd["SingleCount"].ToInt32();
                                            }

                                        }


                                        if (rvd["id" + i] != null)
                                        {
                                            NraDefCd = rvd["id" + i].ToString();

                                        }
                                        if (rvd["mstId" + i] != null)
                                        {
                                            InspMstId = rvd["mstId" + i].ToString();
                                        }
                                        if (rvd["PaperId" + i] != null)
                                        {
                                            paperId = rvd["PaperId" + i].ToString();
                                        }
                                        if (rvd["Level" + i] != null)
                                        {
                                            InspectionLevel = rvd["Level" + i].ToString();
                                        }

                                    }

                                    if (Session[SessionCheck.sessionName] != null)
                                    {
                                        obj = (Users)Session[SessionCheck.sessionName];
                                        strUsername = obj.usrName;
                                    }
                                    ObjDesign.NraDefCode = Convert.ToString(NraDefCd);
                                    ObjDesign.InspectionMstId = Convert.ToString(InspMstId);
                                    ObjDesign.InspectionPaperID = Convert.ToString(paperId);
                                    ObjDesign.InspectionLevel = InspectionLevel;
                                    ObjDesign.Mode = "U";
                                    batch = objService.MOUDApproveAll(ObjDesign, batch);
                                    i++;

                                }

                                while (i <= j);
                            }
                            catch (OracleException oe)
                            {
                                batch = "";
                                ExceptionManager.AppendLog(oe);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.AppendLog(ex);
                                batch = "";
                            }
                            if (batch != "" && batch != null)
                            {
                                Session["firstApproveAll"] = "Approved";
                            }
                            else
                            {
                                Session["firstApproveAll"] = "NotApproved";
                            }
                            Session["MoudApproveBatch"] = batch;
                            return RedirectToAction("InspectionMOUDApproveList");

                        }


                        public bool MOUDApproveAllByCheckbox(string checkedId)
                        {
                            bool result = false;
                            // string a = fc["parameterss"].ConvertToString();
                            string NraDefCd = "";
                            string InspMstId = "";
                            string paperId = "";
                            string InspectionLevel = "";
                            int j = 0;
                            string batch = "";
                            Users obj;
                            InspectionDesignModelClass ObjDesign = new InspectionDesignModelClass();
                            RouteValueDictionary rvd = new RouteValueDictionary();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                            string strUsername = "";
                            //  string[] ApproveItem = p.Split(','); 
                            
                           string [] totalItem  = checkedId.ToString().Split(',');

                            try
                            {
                                int i = 1;
                                do
                                {
                                    
                                   
                                     
                                            
                                            if (j == 0)
                                            {
                                                j = totalItem.Length-1;
                                            }




                                            if (totalItem[i]!=null)
                                            {
                                                string [] EachsplitedData = totalItem[i].Split('-');
                                                NraDefCd =  EachsplitedData[0].ConvertToString() + "-" + EachsplitedData[1].ConvertToString() 
                                                            + "-" + EachsplitedData[2].ConvertToString() + "-" +
                                                            EachsplitedData[3].ConvertToString() + "-" + EachsplitedData[4].ConvertToString();

                                                InspectionLevel = EachsplitedData[5].ConvertToString();
                                                paperId = EachsplitedData[6].ConvertToString();
                                                InspMstId = EachsplitedData[7].ConvertToString();
                                            }
                                        

                                    

                                    if (Session[SessionCheck.sessionName] != null)
                                    {
                                        obj = (Users)Session[SessionCheck.sessionName];
                                        strUsername = obj.usrName;
                                    }
                                    ObjDesign.NraDefCode = Convert.ToString(NraDefCd);
                                    ObjDesign.InspectionMstId = Convert.ToString(InspMstId);
                                    ObjDesign.InspectionPaperID = Convert.ToString(paperId);
                                    ObjDesign.InspectionLevel = InspectionLevel;
                                    ObjDesign.Mode = "U";
                                    batch = objService.MOUDApproveAll(ObjDesign, batch);
                                    i++;

                                }

                                while (i <= j);
                            }
                            catch (OracleException oe)
                            {
                                batch = "";
                                ExceptionManager.AppendLog(oe);
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.AppendLog(ex);
                                batch = "";
                            }
                            if (batch != "" && batch != null)
                            {
                                Session["firstApproveAll"] = "Approved";
                                result = true;
                            }
                            else
                            {
                                Session["firstApproveAll"] = "NotApproved";
                            }
                            Session["MoudApproveBatch"] = batch;
                            return result;

                        }


        #endregion

           


        #region Mofald list
                        public ActionResult InspectionMOFALDList()
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
                            ViewData["ddlYesNo"] = (List<SelectListItem>)com.GetYesNo1("").Data;
                            //ViewData["ddl_Inspection_number"] = com.GetInspectionLevelWithoutDesign("");
                            ViewData["ddl_batch"] = com.GetMOFALDpprovedbatch("");
                            ViewData["SecondApproveAll"] = Session["SecondApproveAll"];
                            if (ViewData["SecondApproveAll"].ConvertToString() == "Approved")
                            {
                                ViewData["Message"] = "Approved Successfully";
                            }
                            if (ViewData["SecondApproveAll"].ConvertToString() == "NotApproved")
                            {
                                ViewData["Message"] = "Records not approved !!!";
                            }
                            Session["SecondApproveAll"] = null;
                            ViewData["MOFALDApproveBatch"] = Session["MOFALDApproveBatch"].ConvertToString();
                            return View();
                        }
                        [HttpPost]
                        public ActionResult InspectionMOFALDList(FormCollection Fc)
                        {

                            InspectionDetailModelClass objInspection = new InspectionDetailModelClass();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();

                           
                            objInspection.district_Cd = com.GetCodeFromDataBase(Fc["ddl_Districts"].ConvertToString(), "MIS_DISTRICT", "DISTRICT_CD");
                            objInspection.vdc_mun_cd = com.GetCodeFromDataBase(Fc["ddl_Vdc"].ConvertToString(), "MIS_VDC_MUNICIPALITY", "VDC_MUN_CD");
                            objInspection.ward_no = Fc["ddl_Wards"].ConvertToString();
                            string Id = Fc["TB1"].ConvertToString() + "-" + Fc["TB2"].ConvertToString() + "-" + Fc["TB3"].ConvertToString() + "-" + Fc["TB4"].ConvertToString() + "-" + Fc["TB5"].ConvertToString();
                            if (Id == "----")
                            {
                                Id = "";
                            }
                            string name = Fc["nameRecp"].ConvertToString();
                            string inspectionLvlCd = Fc["InspectionLevel"].ConvertToString();

                            string MOFALDApproved = Fc["YesNo"].ConvertToString();
                            string DLPIUApproved = Fc["DLPIUApprove"].ConvertToString();

                            string Batch = Fc["Batch"].ConvertToString();
                            DataTable results = new DataTable();

                            if (Session["MOFALDApproveBatch"].ConvertToString().Trim() != "")
                            {
                                Batch = Session["MOFALDApproveBatch"].ConvertToString();
                                Session["MOFALDApproveBatch"] = null;
                            }

                            results = objService.GetInspectionMOFALDList(objInspection, Id, name, inspectionLvlCd,  MOFALDApproved, Batch,DLPIUApproved);




                            ViewData["results"] = results;
                            return PartialView("_InspectionMOFALDApproveList");
                        }
        #endregion

        #region MOFALD approve all
                        public ActionResult InspectionMOFALDApprove(string p, string  checkedId)
                        {
                             
                            string[] idSplit = checkedId.ConvertToString().Split(',');
                            if (idSplit.Length > 1)
                            {
                                bool result = InspectionMOFALDApproveByCheckbox(checkedId);
                                return RedirectToAction("InspectionMOFALDList");
                            }

                            //string a = fc["parameterss"].ConvertToString();
                            string NraDefCd = "";
                            string InspMstId = "";
                            string paperId = "";
                            string HouseOwnerId = "";
                            int j = 0;
                            string batch = "";
                            string InspectionLevel = "";
                            Users obj;
                            InspectionDesignModelClass ObjDesign = new InspectionDesignModelClass();
                            RouteValueDictionary rvd = new RouteValueDictionary();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                            string strUsername = "";

                            try
                            {
                                int i = 0;
                                do
                                {
                                    rvd = QueryStringEncrypt.DecryptString(p);

                                    if (rvd != null)
                                    {

                                        if (rvd["MultiCount"] != null)
                                        {
                                            j = rvd["MultiCount"].ToInt32();
                                            if (j == 0)
                                            {
                                                j = rvd["SingleCount"].ToInt32();
                                            }

                                        }


                                        if (rvd["id" + i] != null)
                                        {
                                            NraDefCd = rvd["id" + i].ToString();

                                        }
                                        if (rvd["mstId" + i] != null)
                                        {
                                            InspMstId = rvd["mstId" + i].ToString();
                                        }
                                        if (rvd["PaperId" + i] != null)
                                        {
                                            paperId = rvd["PaperId" + i].ToString();
                                        }
                                        if (rvd["Level" + i] != null)
                                        {
                                            InspectionLevel = rvd["Level" + i].ToString();
                                        }
                                        if (rvd["HownId" + i] != null)
                                        {
                                            HouseOwnerId = rvd["HownId" + i].ToString();
                                        }
                                        
                                    }

                                    if (Session[SessionCheck.sessionName] != null)
                                    {
                                        obj = (Users)Session[SessionCheck.sessionName];
                                        strUsername = obj.usrName;
                                    }
                                    ObjDesign.NraDefCode = Convert.ToString(NraDefCd);
                                    ObjDesign.InspectionMstId = Convert.ToString(InspMstId);
                                    ObjDesign.InspectionPaperID = Convert.ToString(paperId);
                                    ObjDesign.hOwnerId = Convert.ToString(HouseOwnerId);
                                    ObjDesign.InspectionLevel = InspectionLevel;
                                    ObjDesign.Mode = "U";
                                    batch = objService.InspectionMOFALDApprove(ObjDesign, batch);
                                    i++;

                                }

                                while (i <= j); 
                            }
                            catch (OracleException oe)
                            {
                                ExceptionManager.AppendLog(oe);
                                batch = "";
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.AppendLog(ex);
                                batch = "";
                            }
                            if (batch != "" && batch != null)
                            {
                                Session["SecondApproveAll"] = "Approved";

                            }
                            else
                            {
                                Session["SecondApproveAll"] = "NotApproved";
                            }
                            Session["MOFALDApproveBatch"] = batch;
                            return RedirectToAction("InspectionMOFALDList");
                        }


                        public bool InspectionMOFALDApproveByCheckbox(string checkedId)
                        {
                            bool result = false;
                            //string a = fc["parameterss"].ConvertToString();
                            string NraDefCd = "";
                            string InspMstId = "";
                            string paperId = "";
                            string HouseOwnerId = "";
                            int j = 0;
                            string batch = "";
                            string InspectionLevel = "";
                            Users obj;
                            InspectionDesignModelClass ObjDesign = new InspectionDesignModelClass();
                            RouteValueDictionary rvd = new RouteValueDictionary();
                            InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                            string strUsername = "";
                            string[] totalItem = checkedId.ToString().Split(',');
                            try
                            {
                                int i = 1;
                                do
                                {



                                    if (j == 0)
                                    {
                                        j = totalItem.Length-1;
                                    }




                                    if (totalItem[i] != null)
                                    {
                                        string[] EachsplitedData = totalItem[i].Split('-');
                                        NraDefCd = EachsplitedData[0].ConvertToString() + "-" + EachsplitedData[1].ConvertToString()
                                                    + "-" + EachsplitedData[2].ConvertToString() + "-" +
                                                    EachsplitedData[3].ConvertToString() + "-" + EachsplitedData[4].ConvertToString();

                                        InspectionLevel = EachsplitedData[5].ConvertToString();
                                        paperId = EachsplitedData[6].ConvertToString();
                                        InspMstId = EachsplitedData[7].ConvertToString();
                                    }
                                        

                                    if (Session[SessionCheck.sessionName] != null)
                                    {
                                        obj = (Users)Session[SessionCheck.sessionName];
                                        strUsername = obj.usrName;
                                    }
                                    ObjDesign.NraDefCode = Convert.ToString(NraDefCd);
                                    ObjDesign.InspectionMstId = Convert.ToString(InspMstId);
                                    ObjDesign.InspectionPaperID = Convert.ToString(paperId);
                                    ObjDesign.hOwnerId = Convert.ToString(HouseOwnerId);
                                    ObjDesign.InspectionLevel = InspectionLevel;
                                    ObjDesign.Mode = "U";
                                    batch = objService.InspectionMOFALDApprove(ObjDesign, batch);
                                    i++;

                                }

                                while (i <= j);
                            }
                            catch (OracleException oe)
                            {
                                ExceptionManager.AppendLog(oe);
                                batch = "";
                            }
                            catch (Exception ex)
                            {
                                ExceptionManager.AppendLog(ex);
                                batch = "";
                            }
                            if (batch != "" && batch != null)
                            {
                                Session["SecondApproveAll"] = "Approved";
                                result = true;

                            }
                            else
                            {
                                Session["SecondApproveAll"] = "NotApproved";
                            }
                            Session["MOFALDApproveBatch"] = batch;
                            return result;
                        }

        #endregion


        #region moud vdcwise data

             public ActionResult VdcWiseDataMoud(string distCd, string InspectionLevel)
            {
                DataTable dt = new DataTable();
                InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                string Lang = null;
                if (Session["LanguageSetting"].ToString() == "English")
                {
                    Lang = "E";
                }
                else
                {
                    Lang = "N";
                }
                dt = objService.VdcWiseDataMoudApprove(distCd,Lang,InspectionLevel);

                ViewData["results"] = dt;
                return PartialView("_InspectionMoudApproveVdcWiseList");
            }
        #endregion

            #region mofald vdcwise data

             public ActionResult VdcWiseDataMofald(string distCd, string InspectionLevel)
            {
                DataTable dt = new DataTable();
                InspectionMoudMofaldApproveService objService = new InspectionMoudMofaldApproveService();
                string Lang = null;
                if (Session["LanguageSetting"].ToString() == "English")
                {
                    Lang = "E";
                }
                else
                {
                    Lang = "N";
                }
                dt = objService.VdcWiseDataMofaldApprove(distCd, Lang, InspectionLevel);

                ViewData["results"] = dt;
                return PartialView("_InspectionMofaldApproveVdcWiseList");
            }
            #endregion

    }
}
