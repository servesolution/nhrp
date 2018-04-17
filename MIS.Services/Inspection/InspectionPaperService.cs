using EntityFramework;
using ExceptionHandler;
using MIS.Models.Inspection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using MIS.Services.Core;
using System.Web.Mvc;
using MIS.Models.Core;
using System.Web;
using MIS.Models.Entity;
using MIS.Models.Setup.Inspection;
using System.Configuration;
using System.Data.OleDb;
using System.Data.Common;


namespace MIS.Services.Inspection
{
    public class InspectionPaperService
    {

        string InspectionUpdateHistoryId = "";
        public DataTable getInspectionDescData()
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            string value = "3";
            
            try
            {
                cmd = "SELECT * FROM NHRS_INSPECTION_DESC_DTL WHERE UPPER_INSPECTION_CODE_ID is null or VALUE_TYPE LIKE '%"
                    + value + "%' ORDER BY INSPECTION_CODE_ID ";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }

        //get count 
        public DataTable getUpperInsCount()
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            int value = 3;
            
            try
            {
                cmd = "SELECT UPPER_INSPECTION_CODE_ID, COUNT(UPPER_INSPECTION_CODE_ID) FROM NHRS_INSPECTION_DESC_DTL where UPPER_INSPECTION_CODE_ID is null or VALUE_TYPE LIKE '%"
                    + value + "%' GROUP BY UPPER_INSPECTION_CODE_ID ";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }

        //hierarchy (child count total)
        public DataTable getHierarchyInsCount(string valueHie)
        {
            DataTable dtbl = new DataTable();
            DataTable dtFinal = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";



            try
            {
                cmd = "SELECT  COUNT(HIERARCHY_CD) FROM NHRS_INSPECTION_DESC_DTL where   HIERARCHY_CD LIKE '%"
                    + "." + valueHie + "." + "%'    ";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }
        public DataTable GetInspectionSearchResults(InspectionDetailModelClass objInspection, string name, string id, string inspectionLevel, string approve1, string approve2, string ApplicationFor, string uploadType,string remarksType)
        {
            DataTable dt = null;
            DataTable dtbl = null;
            DataTable dtFinal = new DataTable();
            string District = objInspection.district_Cd.ConvertToString();
            string VDC = objInspection.vdc_mun_cd.ConvertToString();
            string Ward = objInspection.ward_no.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {
              
                service.Begin();

               
                          dt = service.GetDataTable(true, "GET_INSPECTION_SEARCH",
                          District,
                          VDC,
                          Ward,
                          name,
                          id,
                          inspectionLevel,
                          DBNull.Value,
                          approve1,
                          approve2,
                          ApplicationFor,
                          uploadType,
                          remarksType

                  );
                
               
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("INSPECTION_PAPER_ID");
                dt.Columns.Add("CODE_LOC");
                dt.Columns.Add("DESIGN_ENG");
                dt.Columns.Add("DESIGN_LOC");
                dt.Columns.Add("DESIGN_NUMBER");
                dt.Columns.Add("DESIGN_NUMBER2");
                dt.Columns.Add("DESIGN_NUMBER3");
            }

 
           
            return dt;
        }


        public DataTable InspectionSearchWithPrevGap(InspectionDetailModelClass objInspection, string name, string id, string inspectionLevel, string approve1, string approve2, string ApplicationFor)
        {
            DataTable dt = null;
            DataTable dtbl = null;
            DataTable dtFinal = new DataTable();
            string District = objInspection.district_Cd.ConvertToString();
            string VDC = objInspection.vdc_mun_cd.ConvertToString();
            string Ward = objInspection.ward_no.ConvertToString();
            ServiceFactory service = new ServiceFactory();
            try
            {

                service.Begin();
                dt = service.GetDataTable(true, "GET_INSPECTION_SEARCH_GAP",
                   District,
                   VDC,
                   Ward,
                   name,
                   id,
                   inspectionLevel,
                   DBNull.Value,
                   approve1,
                   approve2,
                   ApplicationFor);
            }
            catch (OracleException oe)
            {
                dt = null;
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                dt = null;
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (service.Transaction != null)
                {
                    service.End();
                }
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("INSPECTION_PAPER_ID");
                dt.Columns.Add("CODE_LOC");
                dt.Columns.Add("DESIGN_ENG");
                dt.Columns.Add("DESIGN_LOC");
                dt.Columns.Add("DESIGN_NUMBER");
                dt.Columns.Add("DESIGN_NUMBER2");
                dt.Columns.Add("DESIGN_NUMBER3");
            }



            return dt;
        }
        
      

       
        public DataTable getOwnDetail(string id)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            

            try
            {
               

                cmd = " SELECT DISTINCT MOU.BENEFICIARY_FULLNAME_ENG,EMS.NRA_DEFINED_CD, EMS.DISTRICT_CD, EMS.VDC_MUN_CD, EMS.WARD_NO, EMS.AREA_ENG, EMS.HOUSEHOLD_ID,HOM.HOUSE_SNO,IDGN.INSPECTION_DESIGN_CD,IDGN.DESIGN_NUMBER,NIMA.HOUSE_MODEL_CD,"
                    + "IDGN.DESIGN_NUMBER,IDGN.CONSTRUCTION_MAT_CD,IDGN.ROOF_MAT_CD,INMS.INSPECTION_MST_ID, INMS.INSPECTION_LEVEL0,INMS.INSPECTION_LEVEL1,INMS.INSPECTION_LEVEL2,INMS.INSPECTION_LEVEL3, INMS.INSPECTION_STATUS,"
              + "IDGN.KITTA_NUMBER,IDGN.DISTRICT_CD AS DESIGN_DISTRICT,IDGN.VDC_MUN_CD AS DESIGN_VDC,IDGN.WARD AS DESIGN_WARD,EMS.HOUSE_OWNER_ID,IDGN.BENF_FULL_NAME DESIGN_BENF,"
              +"IDGN.OWN_DESIGN,"
              + "(SELECT * FROM (SELECT FULL_NAME_ENG FROM NHRS_HOUSE_OWNER_DTL HOD "
              + "WHERE EMS.HOUSE_OWNER_ID = HOD.HOUSE_OWNER_ID ) WHERE ROWNUM=1) HOUSE_OWNER_NAME ,"
              + "(SELECT * FROM (SELECT FULL_NAME_ENG FROM NHRS_HOUSE_OWNER_DTL HOD  WHERE EMS.HOUSE_OWNER_ID = HOD.HOUSE_OWNER_ID ) WHERE ROWNUM=1) BENEFICIARY_NAME "
              + "FROM  NHRS_ENROLLMENT_MST EMS "
              + "JOIN NHRS_HOUSE_OWNER_MST HOM ON EMS.HOUSE_OWNER_ID=HOM.HOUSE_OWNER_ID "
              + " LEFT OUTER JOIN NHRS_INSPECTION_MST INMS ON EMS.NRA_DEFINED_CD=INMS.NRA_DEFINED_CD "
              + " LEFT OUTER JOIN NHRS_INSPECTION_DESIGN IDGN ON EMS.NRA_DEFINED_CD=IDGN.NRA_DEFINED_CD "
              + "LEFT OUTER  JOIN NHRS_INSPECTION_MDL_MAPPING  NIMA ON (IDGN.CONSTRUCTION_MAT_CD=NIMA.CONSTRUCTION_MAT_CD  ) "
              + "LEFT OUTER JOIN NHRS_ENROLLMENT_MOU MOU ON MOU.NRA_DEFINED_CD=EMS.NRA_DEFINED_CD "
              + "WHERE EMS.NRA_DEFINED_CD= '" + id + "'";

                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }

            //getHouseDesign Update

        public DataTable GetAllDesignUpdate(string id)
        {
            DataTable dtbl = new DataTable();
            InspectionDesignModelClass objInspectn = new InspectionDesignModelClass();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            //id = "1-2-3-4-55";

            try
            {


                cmd = " SELECT * FROM NHRS_INSPECTION_DESIGN WHERE NRA_DEFINED_CD= '" + id + "'";

                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
                
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;
        }
        public List<SelectListItem> GetHouseModel(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = GetHouseModelVal(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select House Model") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetInspectionTreeHead(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selLstBatchId = new List<SelectListItem>();
            List<MISCommon> lstCommon = GetTreeHead(code, desc);
            selLstBatchId.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select House Model") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selLstBatchId.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selLstBatchId)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selLstBatchId;
        }
        public List<SelectListItem> GetHouseDesign(string selectedValue, string code = "", string desc = "")
        {
            List<SelectListItem> selHouseDesign = new List<SelectListItem>();
            List<MISCommon> lstCommon = GetHouseDesignVal(code, desc);
            selHouseDesign.Add(new SelectListItem { Value = "", Text = "---" + Utils.GetLabel("Select House Design") + "---" });
            foreach (MISCommon common in lstCommon)
            {

                selHouseDesign.Add(new SelectListItem { Value = common.Code, Text = common.Description });
            }

            foreach (SelectListItem item in selHouseDesign)
            {
                if (item.Value == selectedValue)
                    item.Selected = true;
            }
            return selHouseDesign;
        }
        public List<MISCommon> GetHouseModelVal(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select MODEL_ID, " + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC") + " AS NAME "
                    + "FROM NHRS_HOUSE_MODEL where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["MODEL_ID"].ConvertToString(), Description = drow["NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetTreeHead(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select INSPECTION_CODE_ID, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS NAME "
                    + "FROM NHRS_INSPECTION_DESC_DTL where UPPER_INSPECTION_CODE_ID IS NULL";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["INSPECTION_CODE_ID"].ConvertToString(), Description = drow["NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }
        public List<MISCommon> GetHouseDesignVal(string code, string desc)
        {
            DataTable dtbl = null;
            List<MISCommon> lstCommon = new List<MISCommon>();
            string cmdText = "";
            string sessionLanguage = HttpContext.Current.Session["LanguageSetting"].ConvertToString();
            try
            {
                using (ServiceFactory service = new ServiceFactory())
                {
                    service.Begin();
                    cmdText = "select HOUSE_DESIGN_CD, " + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS NAME "
                    + "FROM NHRS_HOUSE_DESIGN where 1=1";


                    try
                    {
                        dtbl = service.GetDataTable(new
                        {
                            query = cmdText,
                            args = new
                            {

                            }
                        });
                    }
                    catch (Exception)
                    {

                    }
                    finally
                    {
                        if (service.Transaction != null)
                        {
                            service.End();
                        }
                    }
                }
                if (dtbl != null && dtbl.Rows.Count > 0)
                {
                    foreach (DataRow drow in dtbl.Rows)
                    {
                        lstCommon.Add(new MISCommon { Code = drow["HOUSE_DESIGN_CD"].ConvertToString(), Description = drow["NAME"].ConvertToString() });
                    }
                }
            }
            catch (Exception)
            {
                lstCommon = null;
            }
            return lstCommon;
        }

        public InspectionItem GetInspectionTreeInspection(string HouseModelId)
        {
            DataTable dt = null;
            using (ServiceFactory service = new ServiceFactory())
            {


               // string cmdText = "Select INSPECTION_CODE_ID, UPPER_INSPECTION_CODE_ID, Group_FLAG,  VALUE_TYPE," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG  from NHRS_INSPECTION_DESC_DTL "

               //+ "WHERE  1=1 "
               //+ "START with (HOUSE_MODEL_CD LIKE '%"
               //+ HouseModelId + "%' AND  UPPER_INSPECTION_CODE_ID is null)"
               //+ " CONNECT by prior INSPECTION_CODE_ID = UPPER_INSPECTION_CODE_ID ";
                string cmdText = "Select INSPECTION_CODE_ID, UPPER_INSPECTION_CODE_ID, Group_FLAG,  VALUE_TYPE," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG  from NHRS_INSPECTION_DESC_DTL "

                    + "WHERE INSPECTION_CODE_ID is null ";
                if(HouseModelId!="")
                {
                     cmdText = "Select INSPECTION_CODE_ID, UPPER_INSPECTION_CODE_ID, Group_FLAG,  VALUE_TYPE," + Utils.ToggleLanguage("DESC_ENG", "DESC_LOC") + " AS DESC_ENG  from NHRS_INSPECTION_DESC_DTL "

                    + "WHERE  1=1 "
                    + "START with (INSPECTION_CODE_ID LIKE '%"
                    + HouseModelId + "%' AND  UPPER_INSPECTION_CODE_ID is null)"
                    + " CONNECT by prior INSPECTION_CODE_ID = UPPER_INSPECTION_CODE_ID ";
                }
               
                try
                {
                    service.Begin();
                    dt = service.GetDataTable(new
                    {
                        query = cmdText,
                        args = new
                        {

                        }
                    });
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }

            }
            var hInspection = new InspectionItem();
            var Inspection = new HierarchicalInspectionTree();
            if(dt!=null && dt.Rows.Count>0)
            {
                var InspectionItems = new List<InspectionItem>();
                string UpperCode = dt.Rows[0][0].ConvertToString();
                foreach (DataRow dr in dt.Rows)
                {

                    InspectionItems.Add(new InspectionItem()
                    {
                        INSPECTION_CODE_ID = (!dr.IsNull(0) ? dr[0].ToString() : null),
                        UPPER_INSPECTION_CODE_ID = (!dr.IsNull(1) ? dr[1].ToString() : null),
                        Group_FLAG = (!dr.IsNull(2) ? dr[2].ToString() : null),
                        VALUE_TYPE = (!dr.IsNull(3) ? dr[3].ToString() : null),
                        DESC_ENG = (!dr.IsNull(4) ? dr[4].ToString() : null),
                        //DESC_LOC = (!dr.IsNull(5) ? dr[5].ToString() : null),
                    });
                }


                 hInspection = Inspection.CreateHierarchy(InspectionItems, UpperCode);

                var curItem = Inspection.lastCurrentItem;
            }
            
            //if (curItem != null)
            //{
            //    while (curItem.Level != 0)
            //    {
            //        curItem = curItem.Parent;
            //        curItem.IsCurrent = true;
            //    }
            //}
            return hInspection;
        }

        //save inspection info 
        public string saveInspectionInfo(Dictionary<string,MIS.Models.Core.MISCommon.MyValue> lstComply, InspectionDesignModelClass objInspectn)
        {
            string exc =string.Empty;
            bool res                 = false;
            
            string InspectionPaperId = "";
            string InsProcessId      = "";
              string DEFINED_CD        = "";
            string INSPECTION_CODE_ID = "";
            
            string MAP_DESIGN        = "";
            string RC_MATERIAL_CD    = "";
            string FC_MATERIAL_CD    = "";
            
            string HOUSE_MODEL       = "";
            

            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();

                    //if (objInspectn.Inspection == "1")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //}
                    //if (objInspectn.Inspection == "2")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //    objInspectn.InspectionLevel2 ="2";
                    //}
                    //if (objInspectn.Inspection == "3")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //    objInspectn.InspectionLevel2 = "2";
                    //    objInspectn.InspectionLevel3 = "3";
                    //}


                    DataTable InspLevel = new DataTable();

                    string InspLvl = "select * FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' ";
                    InspLevel = sf.GetDataTable(new
                    {
                        query = InspLvl,
                        args = new { }
                    });
                    if (InspLevel != null || InspLevel.Rows.Count > 0)
                    {
                        
                        objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                        objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                        objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();


                        objInspectn.INSP_ONE_FROM_TAB = InspLevel.Rows[0]["INSP_ONE_FROM_TAB"].ConvertToString();
                        objInspectn.INSP_TWO_FROM_TAB = InspLevel.Rows[0]["INSP_TWO_FROM_TAB"].ConvertToString();
                        objInspectn.INSP_THREE_FROM_TAB = InspLevel.Rows[0]["INSP_THREE_FROM_TAB"].ConvertToString();
                    }

                    if (objInspectn.Inspection == "1")
                    {
                        objInspectn.INSP_ONE_FROM_TAB = "N";
                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {

                        objInspectn.InspectionLevel2 = "2";
                        objInspectn.INSP_TWO_FROM_TAB = "N";
                    }
                    if (objInspectn.Inspection == "3")
                    {
                        objInspectn.InspectionLevel3 = "3";
                        objInspectn.INSP_THREE_FROM_TAB = "N";
                    }


                    objInspectn.InspectionLevel0 = "0";

                    //DataTable dtHistory = new DataTable();
                    //string cmdText = "Select INSPECTION_HISTORY_ID ,PPR_INSPECTION_LEVEL ,DEG_ENTERED_DATE FROM NHRS_INSPECTION_UPDATE_HISTORY"
                    //+ " WHERE DEG_NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' order  by DEG_ENTERED_DATE desc";
                    //dtHistory = sf.GetDataTable(cmdText, null);
                    //if (dtHistory != null && dtHistory.Rows.Count > 0 && objInspectn.Inspection == "1")
                    //{
                    //    InspectionUpdateHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    //}
                    //else
                    //{
 
                    DataTable dtHistory = new DataTable();
                        string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                        dtHistory = sf.GetDataTable(new
                        {
                            query = cmdHistoryid,
                            args = new { }
                        });

                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            InspectionUpdateHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                        }

                        string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" +InspectionUpdateHistoryId.ConvertToString() + "')";
                        QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);
                    
                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

                                           SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            DBNull.Value,
                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                            objInspectn.complyFlag.ConvertToString(),
                                            "N"  , // final decision 2
                                            "N"  , // final decision 
                                            InspectionUpdateHistoryId.ToDecimal(),
                                            objInspectn.INSP_ONE_FROM_TAB.ConvertToString(),
                                            objInspectn.INSP_TWO_FROM_TAB.ConvertToString(),
                                            objInspectn.INSP_THREE_FROM_TAB.ConvertToString()
                                            


                                      );
                    if (qrMst.IsSuccess)
                    {
                        string cmdUPdateMst = String.Format("Update nhrs_inspection_mst set "
                            + "INSP_ONE_FROM_TAB='"+objInspectn.INSP_ONE_FROM_TAB.ConvertToString()+"' ,  INSP_TWO_FROM_TAB='"+objInspectn.INSP_TWO_FROM_TAB.ConvertToString()+"' , INSP_THREE_FROM_TAB='"+objInspectn.INSP_THREE_FROM_TAB.ConvertToString()+"'"
                            + " where NRA_DEFINED_CD='"+objInspectn.NraDefCode.ConvertToString()+"'");
 
                            sf.SubmitChanges(cmdUPdateMst, null);
 
                    
                    }
                    //InspectionMstId = objInspectn.InspectionMstId.ConvertToString();
                   
                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                              "I",
                                               DBNull.Value,
                                                //objInspectn.InspectionPaperId.ToDecimal(),
                                                DEFINED_CD.ToDecimal(),

                                                INSPECTION_CODE_ID.ToDecimal(),

                                                objInspectn.NraDefCode,
                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(),
                                               


                                                MAP_DESIGN.ConvertToString(),
                                                objInspectn.DesignNumber.ConvertToString(),//design number
                                                RC_MATERIAL_CD.ToDecimal(),
                                                FC_MATERIAL_CD.ToDecimal(),
                                                objInspectn.TECHNICAL_ASSIST.ConvertToString(),

                                                objInspectn.ORGANIZATION_TYPE.ConvertToString(),
                                                objInspectn.CONSTRUCTOR_TYPE.ConvertToString(),
                                                objInspectn.SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),
                                                objInspectn.PHOTO_CD_1,
                                                objInspectn.PHOTO_CD_2,
                                                objInspectn.PHOTO_CD_3,
                                                objInspectn.PHOTO_CD_4,

                                                objInspectn.PHOTO_1.ConvertToString(),
                                                objInspectn.PHOTO_2.ConvertToString(),
                                                objInspectn.PHOTO_3.ConvertToString(),
                                                objInspectn.PHOTO_4.ConvertToString(),





                                                 "G",
                                                 "Y",
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                DBNull.Value,
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                 DBNull.Value,                                //InspectnInstallment
                                                 DBNull.Value,                                //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),     //Passed_map_no gharko mota moti naksa 
                                                 DBNull.Value,                          //Others
                                                 DBNull.Value,                                //Filebatch

                                                 DBNull.Value,                                            //InspectionType
                                                DateTime.Now,                                   //INSPECTION_DATE
                                                objInspectn.BenfFullNameEng.ConvertToString(),  //BENEFICIARY_NAME
                                                objInspectn.tole.ConvertToString(),             //TOLE
                                                objInspectn.KittaNumber.ConvertToString(),      //LAND_PLOT_NUMBER
                                                objInspectn.ORGANIZATION_OTHERS.ConvertToString(),
                                                objInspectn.PHOTO_CD_5,
                                                objInspectn.PHOTO_5,
                                                objInspectn.PHOTO_CD_6,
                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),//FINAL_DECISION
                                                objInspectn.LATITUDE,
                                                objInspectn.LONGITUDE,
                                                objInspectn.ATTITUDE,
                                                objInspectn.InspectionMstId.ToDecimal(),
                                                objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num,
                                                 objInspectn.Serial_Num.ConvertToString(),
                                                  objInspectn.hOwnerId.ConvertToString(),
                                                  objInspectn.MobileNumber.ConvertToString(),
                                                  objInspectn.form_pad_number.ConvertToString(),        //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),          //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),          //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),            // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),                  //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),           // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),        //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),              //GPS_TAKEN
                                                  objInspectn.final_decision_2.ConvertToString(),          //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N" ,                                    //FINAL DECISION APPROVE 
                                                  "N" ,                                    // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                  objInspectn.Inspection.ConvertToString(),
                                                  InspectionUpdateHistoryId.ToDecimal()
                    );

                    //Main Table 
                    InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (lstComply != null)
                    {
                        //insert inspection element
                        foreach (var item in lstComply)
                        {



                            QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                       "I",

                                        DBNull.Value,
                                        DBNull.Value,
                                        InspectionPaperId.ToDecimal(),
                                        DBNull.Value,
                                        item.Key.ToDecimal(),
                                        item.Value.Value1.ConvertToString(),
                                        item.Value.Value2.ConvertToString(),
                                        objInspectn.DesignNumber.ToDecimal(),
                                         objInspectn.complyFlag,
                                        objInspectn.complyCd.ToDecimal()


                                   );


                        }
                    }

                    QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                       "I",

                                        DBNull.Value,
                                        DBNull.Value,
                                        objInspectn.BeneficiaryFullName.ConvertToString(),
                                        objInspectn.BenfRelation.ConvertToString(),

                                        objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                         objInspectn.SupertntDesignation.ConvertToString(),

                                        objInspectn.EngineerFullname.ConvertToString(),
                                        objInspectn.EngineerDesignation.ConvertToString(),

                                        objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        "Registered",

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        DBNull.Value,
                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),
                                        InspectionPaperId.ToDecimal() ,
                                        InspectionUpdateHistoryId.ToDecimal()
                                         
                                       


                                   );
                    InsProcessId = qre["V_INSPECTION_PROCESS_ID"].ConvertToString();

                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;



                }
                catch (OracleException oe)
                {
                    if ( objInspectn.InspectionMstId.ConvertToString()!= "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                      

                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                             + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                             
                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";
                            objInspectn.InspectionLevel3 = "";
                            
                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                     

                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                        cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        +"AND INSPECTION_LEVEL='"+objInspectn.Inspection.ConvertToString()+"'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";
                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "";
                            objInspectn.InspectionLevel3 = "";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";
                            objInspectn.InspectionLevel3 = "";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                         + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                         + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);

                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }

                if (res)
                {
                    res = false;
                    InspectionPaperId = "false";
                }

            }
            return (InspectionPaperId);
        }

        //save own design (gha vargako ghar) info 
        public bool saveOwnInspectionDesign(InspectionDesignModelClass objInspectn, InspectionOwnDesign objOwnDesign)
        {
            string exc = string.Empty;
            bool res = false;
            

            bool result = false;
            QueryResult qre = null;
            string InspectionPaperId = "";
            string InsProcessId = "";
            string InspectionMstId = "";

            string DEFINED_CD = "";
            string INSPECTION_CODE_ID = "";
            string MAP_DESIGN = "";
            string RC_MATERIAL_CD = "";
            string FC_MATERIAL_CD = "";
            string TECHNICAL_ASSIST = "";
            string ORGANIZATION_TYPE = "";
            string CONSTRUCTOR_TYPE = "";
            string SOIL_TYPE = "";
            string HOUSE_MODEL = "";


            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();

                    if (objInspectn.Inspection == "1")
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = "1";
                        objInspectn.InspectionLevel2 = "2";
                    }
                    if (objInspectn.Inspection == "3")
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = "1";
                        objInspectn.InspectionLevel2 = "2";
                        objInspectn.InspectionLevel3 = "3";
                    }
                  
                

                    DataTable dtHistory = new DataTable();
                    string inspectionHIstoryId = "";
                    //string ppr_nra_defined_cd = "";
                    //string cmdText = "Select INSPECTION_HISTORY_ID,PPR_NRA_DEFINED_CD,DEG_ENTERED_DATE FROM NHRS_INSPECTION_UPDATE_HISTORY  WHERE DEG_NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "'order by DEG_ENTERED_DATE desc";
                    //dtHistory = sf.GetDataTable(cmdText, null);
                    //if (dtHistory != null && dtHistory.Rows.Count > 0)
                    //{
                    //    inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    //    ppr_nra_defined_cd = dtHistory.Rows[0]["PPR_NRA_DEFINED_CD"].ConvertToString();
                    //}

                    //if ((ppr_nra_defined_cd != "" && ppr_nra_defined_cd != null) || (dtHistory == null || dtHistory.Rows.Count == 0))
                    //{

                        string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                        dtHistory = sf.GetDataTable(new
                        {
                            query = cmdHistoryid,
                            args = new { }
                        });

                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                        }

                        string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + inspectionHIstoryId + "')";
                        QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);
                    

                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

                                           SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            DBNull.Value,
                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                           objInspectn.complyFlag.ConvertToString(),
                                           "N"  , // final decision 2
                                           "N" ,// final decision 
                                           inspectionHIstoryId.ToDecimal()


                                      );
                    //InspectionMstId = objInspectn.InspectionMstId.ConvertToString();

                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",
                                              objInspectn.Mode,
                                               DBNull.Value,
                        //objInspectn.InspectionPaperId.ToDecimal(),
                                                DEFINED_CD.ToDecimal(),

                                                INSPECTION_CODE_ID.ToDecimal(),

                                                objInspectn.NraDefCode,
                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(),



                                                MAP_DESIGN.ConvertToString(),
                                                objInspectn.DesignNumber.ConvertToString(),//design number
                                                RC_MATERIAL_CD.ToDecimal(),
                                                FC_MATERIAL_CD.ToDecimal(),
                                                TECHNICAL_ASSIST.ConvertToString(),

                                                ORGANIZATION_TYPE.ConvertToString(),
                                                CONSTRUCTOR_TYPE.ConvertToString(),
                                                SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),
                                                objInspectn.PHOTO_CD_1.ConvertToString(),
                                                objInspectn.PHOTO_CD_2.ConvertToString(),
                                                objInspectn.PHOTO_CD_3.ConvertToString(),
                                                objInspectn.PHOTO_CD_4.ConvertToString(),

                                                objInspectn.PHOTO_1,
                                                objInspectn.PHOTO_2,
                                                objInspectn.PHOTO_3,
                                                objInspectn.PHOTO_4,





                                                 "G",
                                                 "Y",
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                DBNull.Value,
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                 DBNull.Value,                                //InspectnInstallment
                                                 DBNull.Value,                                //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),     //Passed_map_no gharko mota moti naksa
                                                 DBNull.Value,                          //Others
                                                 DBNull.Value,                                //Filebatch

                                                DBNull.Value,                                            //InspectionType
                                                DateTime.Now,                                   //INSPECTION_DATE
                                                objInspectn.BenfFullNameEng.ConvertToString(),  //BENEFICIARY_NAME
                                                objInspectn.tole.ConvertToString(),             //TOLE
                                                objInspectn.KittaNumber.ConvertToString(),      //LAND_PLOT_NUMBER
                                                objInspectn.ORGANIZATION_OTHERS.ConvertToString(),//ORGANIZATION_OTHERS
                                                objInspectn.PHOTO_CD_5,
                                                objInspectn.PHOTO_5.ConvertToString(),
                                                objInspectn.PHOTO_CD_6,
                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),//FINAL_DECISION
                                                objInspectn.LATITUDE.ConvertToString(),
                                                objInspectn.LONGITUDE.ConvertToString(),
                                                objInspectn.ATTITUDE.ConvertToString(),
                                                objInspectn.InspectionMstId.ToDecimal(),
                                                objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num.ConvertToString(),
                                                objInspectn.Serial_Num.ConvertToString(),
                                                objInspectn.hOwnerId.ConvertToString(),
                                                objInspectn.MobileNumber.ConvertToString(),
                                                objInspectn.form_pad_number.ConvertToString(),    //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),   //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),    //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),    // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),         //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),     // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),    //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),   //GPS_TAKEN
                                                  objInspectn.finalDecision2.ConvertToString(),    //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N" ,                            //FINAL DECISION APPROVE 
                                                  "N" ,                            // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                  objInspectn.Inspection.ConvertToString(),
                                                   inspectionHIstoryId.ToDecimal()

                    );
                  
                    //Main Table 
                    InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (objInspectn.Mode == "U")
                    {
                        InspectionPaperId = objInspectn.InspectionPaperID.ConvertToString();
                    }

                            QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_OWN_DESIGN",
                                      objInspectn.Mode,

                                        DBNull.Value,
                                        DBNull.Value,
                                        
                                        InspectionPaperId.ToDecimal(),
                                        objInspectn.DesignNumber.ConvertToString(),

                                        objOwnDesign.PillarConstructing.ConvertToString(),
                                        objOwnDesign.PillerConstructed.ConvertToString(),
                                        objOwnDesign.GroundRoofCompleted.ConvertToString(),
                                        objOwnDesign.GroundFloorCompleted.ConvertToString(),

                                        objOwnDesign.FloorCount.ConvertToString(),

                                        objOwnDesign.BaseConstructMaterial.ConvertToString(),
                                        objOwnDesign.BaseDepthBelowGrnd.ConvertToString(),
                                        objOwnDesign.BaseAvgWidth.ConvertToString(),
                                        objOwnDesign.BseHeightAbvGrnd.ConvertToString(),

                                        objOwnDesign.gndFloMat.ConvertToString(),
                                        objOwnDesign.GrndFlorPrncpl.ConvertToString(),
                                        objOwnDesign.WallStructDescpt.ConvertToString(),

                                        objOwnDesign.FloorRoofDescrpt.ConvertToString(),
                                        objOwnDesign.FloorRoofMat.ConvertToString(),
                                        objOwnDesign.FloorRoofPrncpl.ConvertToString(),
                                        objOwnDesign.FloorRoofDetl.ConvertToString(),

                                        objOwnDesign.FirstFlorMat.ConvertToString(),
                                        objOwnDesign.FirstFLorPrncpl.ConvertToString(),
                                        objOwnDesign.FirstFlorDtl.ConvertToString(),

                                        objOwnDesign.Roofmat.ConvertToString(),
                                        objOwnDesign.RoofPrncpl.ConvertToString(),
                                        objOwnDesign.RoofDetal.ConvertToString(),

                                        DBNull.Value,
                                        DBNull.Value,
                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        DBNull.Value,
                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),
                                        objOwnDesign.ConstructionStatus.ConvertToString(),
                                        inspectionHIstoryId.ToDecimal()

                                   );


                      

                     qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                       objInspectn.Mode,

                                        DBNull.Value,
                                        DBNull.Value,
                                        objInspectn.BeneficiaryFullName.ConvertToString(),
                                        objInspectn.BenfRelation.ConvertToString(),

                                        objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                         objInspectn.SupertntDesignation.ConvertToString(),

                                        objInspectn.EngineerFullname.ConvertToString(),
                                        objInspectn.EngineerDesignation.ConvertToString(),

                                        objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        "Registered",

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        DBNull.Value,
                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),
                                        InspectionPaperId.ToDecimal(),
                                         inspectionHIstoryId.ToDecimal()


                                   );
                    if( objInspectn.Mode=="U")
                    {
                        QueryResult qreDesign = sf.SubmitChanges("PR_DESIGN_HISTORY_UPDATE",
                                                              "U",
                                                               objInspectn.NraDefCode,
                                                              inspectionHIstoryId.ToDecimal()
                                                                           );
                    }
                  

                    InsProcessId = qre["V_INSPECTION_PROCESS_ID"].ConvertToString();

                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;



                }
                catch (OracleException oe)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);
                        string cmdText = "";
                        if(objInspectn.Mode.ConvertToString()=="I")
                        {
                             cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (objInspectn.Mode.ConvertToString() == "U")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }




                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                        + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";

                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);
 


                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);
                        string cmdText = "";
                        if (objInspectn.Mode.ConvertToString() == "I")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + InspectionPaperId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (objInspectn.Mode.ConvertToString() == "U")
                        {
                            cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);

                            cmdText = String.Format("delete from NHRS_INSPECTION_OWN_DESIGN where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }



                        cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "' AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                        string a = cmdText;
                        SaveErrorMessgae(cmdText);


                        if (objInspectn.Inspection == "1")
                        {
                            objInspectn.InspectionLevel0 = "0";

                        }
                        if (objInspectn.Inspection == "2")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";

                        }
                        if (objInspectn.Inspection == "3")
                        {
                            objInspectn.InspectionLevel0 = "0";
                            objInspectn.InspectionLevel1 = "1";
                            objInspectn.InspectionLevel2 = "2";

                        }
                        cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                        + "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                        + ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                        SaveErrorMessgae(cmdText);



                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (qre != null)
                {
                    result = qre.IsSuccess;
                }



            }
            return (result);
        }


        //update Inspection Info
        public string UpdateInspectionInfo(Dictionary<string, MIS.Models.Core.MISCommon.MyValue> lstComply, InspectionDesignModelClass objInspectn)
        {
            string exc = string.Empty;
            bool res = false;
            

            string InspectionPaperId = "";
            string InsProcessId = ""; 
            string DEFINED_CD = ""; 
            string HOUSE_MODEL = "";


            using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();

                    //if (objInspectn.Inspection == "1")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //}
                    //if (objInspectn.Inspection == "2")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //    objInspectn.InspectionLevel2 = "2";
                    //}
                    //if (objInspectn.Inspection == "3")
                    //{
                    //    objInspectn.InspectionLevel0 = "0";
                    //    objInspectn.InspectionLevel1 = "1";
                    //    objInspectn.InspectionLevel2 = "2";
                    //    objInspectn.InspectionLevel3 = "3";
                    //}


                    DataTable InspLevel = new DataTable();

                    string InspLvl = "select * FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "' ";
                    InspLevel = sf.GetDataTable(new
                    {
                        query = InspLvl,
                        args = new { }
                    });
                    if (InspLevel != null || InspLevel.Rows.Count > 0)
                    {
                        objInspectn.InspectionLevel0 = "0";
                        objInspectn.InspectionLevel1 = InspLevel.Rows[0]["INSPECTION_LEVEL1"].ConvertToString();
                        objInspectn.InspectionLevel2 = InspLevel.Rows[0]["INSPECTION_LEVEL2"].ConvertToString();
                        objInspectn.InspectionLevel3 = InspLevel.Rows[0]["INSPECTION_LEVEL3"].ConvertToString();
                    }
                    if (objInspectn.Inspection == "1")
                    {

                        objInspectn.InspectionLevel1 = "1";
                    }
                    if (objInspectn.Inspection == "2")
                    {

                        objInspectn.InspectionLevel2 = "2";
                    }
                    if (objInspectn.Inspection == "3")
                    {
                        objInspectn.InspectionLevel3 = "3";
                    }

                    DataTable dtHistory = new DataTable();
                    string inspectionHIstoryId = "";
                    //string ppr_nra_defined_cd ="";
                    //string cmdText = "Select INSPECTION_HISTORY_ID,PPR_NRA_DEFINED_CD,DEG_ENTERED_DATE FROM NHRS_INSPECTION_UPDATE_HISTORY  WHERE DEG_NRA_DEFINED_CD='" + objInspectn.NraDefCode.ConvertToString() + "'order by DEG_ENTERED_DATE desc";
                    //dtHistory = sf.GetDataTable(cmdText, null);
                    //if(dtHistory!=null && dtHistory.Rows.Count>0)
                    //{
                    //    inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                    //    ppr_nra_defined_cd = dtHistory.Rows[0]["PPR_NRA_DEFINED_CD"].ConvertToString();
                    //}

                    //if ((ppr_nra_defined_cd != "" && ppr_nra_defined_cd != null) || (dtHistory == null || dtHistory.Rows.Count == 0))
                    //{
                       
                        string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                        dtHistory = sf.GetDataTable(new
                        {
                            query = cmdHistoryid,
                            args = new { }
                        });

                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            inspectionHIstoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                        }

                        string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + inspectionHIstoryId + "')";
                        QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd, null);
                   

                    QueryResult qrMst = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                          "U",

                                           objInspectn.InspectionMstId.ToDecimal(),
                                           DEFINED_CD.ConvertToString(),
                                           objInspectn.InspectionLevel0.ToDecimal(),
                                           DBNull.Value,   //inspectionDate
                                           objInspectn.InspectionStatus.ConvertToString(),

                                           SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            DBNull.Value,
                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),

                                            SessionCheck.getSessionUsername(),
                                            DateTime.Now,
                                            System.DateTime.Now.ConvertToString(),
                                            objInspectn.NraDefCode.ConvertToString(),
                                            objInspectn.InspectionLevel1.ToDecimal(),
                                            objInspectn.InspectionLevel2.ToDecimal(),
                                            objInspectn.InspectionLevel3.ToDecimal(),
                                            objInspectn.complyFlag.ConvertToString(),
                                            "N" , // final decision 2
                                            "N" , // final decision 
                                            inspectionHIstoryId.ToDecimal(),
                                             DBNull.Value,
                                           DBNull.Value,
                                           DBNull.Value


                                      );
                   
                    //updated by pa number and design number
                    QueryResult qr = sf.SubmitChanges("PR_NHRS_INSPECTION_PAPER_DTL",

                                                objInspectn.Mode,
                                                DBNull.Value,
                                                DBNull.Value,//DEFINED_CD
                                                DBNull.Value,//INSPECTION_CODE_ID
                                                objInspectn.NraDefCode,

                                                objInspectn.district_Cd.ToDecimal(),
                                                objInspectn.vdc_mun_cd.ToDecimal(),
                                                objInspectn.ward_no.ToDecimal(),

                                                DBNull.Value,   //MAP_DESIGN
                                                objInspectn.DesignNumber.ConvertToString(),
                                                objInspectn.Roofmat.ToDecimal(),
                                                objInspectn.ConstructMat.ToDecimal(),
                                                objInspectn.TECHNICAL_ASSIST.ConvertToString(),

                                                objInspectn.ORGANIZATION_TYPE.ConvertToString(),
                                                objInspectn.CONSTRUCTOR_TYPE.ConvertToString(),
                                                objInspectn.SOIL_TYPE.ConvertToString(),
                                                HOUSE_MODEL.ToDecimal(),         //HOUSE_MODEL

                                                objInspectn.PHOTO_CD_1.ConvertToString(),
                                                objInspectn.PHOTO_CD_2.ConvertToString(),
                                                objInspectn.PHOTO_CD_3.ConvertToString(),
                                                objInspectn.PHOTO_CD_4.ConvertToString(),

                                                objInspectn.PHOTO_1.ConvertToString(),
                                                objInspectn.PHOTO_2.ConvertToString(),
                                                objInspectn.PHOTO_3.ConvertToString(),
                                                objInspectn.PHOTO_4.ConvertToString(),

                                                 "N",//status
                                                 DBNull.Value,//active
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                DBNull.Value,
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),
                                                SessionCheck.getSessionUsername(),
                                                DateTime.Now,
                                                System.DateTime.Now.ConvertToString(),

                                                 DBNull.Value,      //InspectnInstallment
                                                 DBNull.Value,            //MAP_DESIGN_CD
                                                 objInspectn.PHOTO_HOUSE.ConvertToString(),//Passed_map_no GHARKO MOTAMOTI NAKSA
                                                 DBNull.Value,               //Others
                                                 DBNull.Value,                    //Filebatch

                                                 DBNull.Value,                                //InspectionType
                                                 objInspectn.Inspection_date_loc.ConvertToString(), //INSPECTION_DATE
                                                 objInspectn.BenfFullNameEng.ConvertToString(), //BENEFICIARY_NAME
                                                 objInspectn.tole.ConvertToString(),            //TOLE
                                                 objInspectn.KittaNumber.ConvertToString(),     //LAND_PLOT_NUMBER
                                                 objInspectn.ORGANIZATION_OTHERS.ConvertToString(),
                                                 objInspectn.PHOTO_CD_5,
                                                 objInspectn.PHOTO_5,
                                                 objInspectn.PHOTO_CD_6,

                                                objInspectn.PHOTO_6,
                                                objInspectn.finalDecision.ConvertToString(),
                                                objInspectn.LATITUDE,
                                                objInspectn.LONGITUDE,
                                                objInspectn.ATTITUDE,
                                                objInspectn.InspectionMstId.ToDecimal(),//  inspection mst id
                                               objInspectn.Bank_Name.ToDecimal(),
                                                objInspectn.Bank_ACC_Num,
                                                 objInspectn.Serial_Num.ConvertToString(),
                                                  objInspectn.hOwnerId.ConvertToString(),
                                                    objInspectn.MobileNumber.ConvertToString(),
                                                    objInspectn.form_pad_number.ConvertToString(),    //FORM_PAD_NUMBER
                                                  objInspectn.design_detail.ConvertToString(),   //DESIGN_DETAILS
                                                  objInspectn.edit_required.ConvertToString(),    //EDIT_REQUIRED
                                                  objInspectn.edit_required_detail.ConvertToString(),    //EDIT_REQUIRED_DETAILS
                                                  objInspectn.bank_select.ConvertToString(),    // BANK_SELECT
                                                  objInspectn.bank_branch.ToDecimal(),          //BANK_BRANCH
                                                  objInspectn.Final_Remarks.ConvertToString(),     // FINAL_REMARKS
                                                  objInspectn.accept_the_entry.ConvertToString(),    //ACCEPT_THE_ENTRY
                                                  objInspectn.gps_taken.ConvertToString(),   //GPS_TAKEN
                                                  objInspectn.final_decision_2.ConvertToString(),    //FINAL_DECISION_2
                                                  objInspectn.bank_not_available_remarks.ConvertToString(),  //BANK_NOT_AVAILABLE_REMARKS
                                                  "N" ,                            //FINAL DECISION APPROVE 
                                                  "N" ,                            // FIANL DECISION APPROVE 2
                                                  objInspectn.approve_batch.ToDecimal(),
                                                  objInspectn.approve_batch_2.ToDecimal(),
                                                   objInspectn.Inspection.ConvertToString(),
                                                   inspectionHIstoryId.ToDecimal()


                    );

                    //Main Table 
                    //InspectionPaperId = qr["v_INSPECTION_PAPER_ID"].ConvertToString();

                    if (lstComply != null)
                    {


                        //insert inspection detail history
                        DataTable inspectionDetail = new DataTable();
                        string cmdSelDetl = "select * from NHRS_INSPECTION_DETAIL WHERE INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'";

                        inspectionDetail = sf.GetDataTable(new
                        {
                            query = cmdSelDetl,
                            args = new { }
                        });
                        if (inspectionDetail != null && inspectionDetail.Rows.Count > 0)
                        {
                            foreach (DataRow dr in inspectionDetail.Rows)
                            {
                                QueryResult qrr = sf.SubmitChanges("PR_INSPECTION_DTL_HISTORY",


                                               DBNull.Value,
                                               inspectionHIstoryId.ToDecimal(),
                                               dr["INSPECTION_DETAIL_ID"].ToDecimal(),
                                               dr["DEFINED_CD"].ToDecimal(),
                                               dr["INSPECTION_PAPER_ID"].ToDecimal(),
                                               dr["INSPECTION_ID"].ToDecimal(),
                                               dr["INSPECTION_ELEMENT_ID"].ConvertToString(),
                                               dr["VALUE_TYPE"].ConvertToString(),
                                               dr["REMARKS"].ConvertToString(),             //  Inspection Type/house model
                                               dr["HOUSE_MODEL"].ToDecimal(),
                                               dr["COMPLY_FLAG"].ConvertToString(),
                                               dr["COMPLY_ID"].ToDecimal()
                                               );
                            }
                        }



                        //string cmdTextDel = "DELETE FROM NHRS_INSPECTION_DETAIL WHERE (HOUSE_MODEL=" + objInspectn.DesignNumber + " AND INSPECTION_PAPER_ID=" + objInspectn.InspectionPaperID + ")";
                        string cmdTextDel = "DELETE FROM NHRS_INSPECTION_DETAIL WHERE ( INSPECTION_PAPER_ID=" + objInspectn.InspectionPaperID + ")";

                        sf.Begin();
                        QueryResult qrdel = sf.SubmitChanges(cmdTextDel, null);
                        //insert inspection element
                        foreach (var item in lstComply)
                        {



                            QueryResult qrr = sf.SubmitChanges("PR_NHRS_INSPECTION_DETAIL",
                                       "I",

                                        DBNull.Value,//INSPECTION_DETAIL_ID
                                        DBNull.Value,//DEFINED_CD
                                        objInspectn.InspectionPaperID.ToDecimal(),
                                        DBNull.Value,//INSPECTION_ID
                                        item.Key.ToDecimal(),//INSPECTION_ELEMENT_ID
                                        item.Value.Value1,//VALUE_TYPE
                                        item.Value.Value2,//REMARKS
                                        objInspectn.DesignNumber.ToDecimal(),//DESIGN/MODEL
                                        objInspectn.complyFlag,
                                        objInspectn.complyCd.ToDecimal()

                                   );


                        }
                    }

                    QueryResult qre = sf.SubmitChanges("PR_NHRS_INSPECTION_PROCESS_DTL",
                                       "U",

                                        DBNull.Value,
                                        DBNull.Value,
                                        objInspectn.BeneficiaryFullName.ConvertToString(),
                                        objInspectn.BenfRelation.ConvertToString(),

                                        objInspectn.SuprtntfFullnameEng.ConvertToString(),
                                         objInspectn.SupertntDesignation.ConvertToString(),

                                        objInspectn.EngineerFullname.ConvertToString(),
                                        objInspectn.EngineerDesignation.ConvertToString(),

                                        objInspectn.BenfSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.SupertntSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        objInspectn.EngineerSignDateLoc.ConvertToString("YYYY-MM-DD"),
                                        "N",//STATUS

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        DBNull.Value,
                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),

                                        SessionCheck.getSessionUsername(),
                                        DateTime.Now,
                                        System.DateTime.Now.ConvertToString(),
                                        objInspectn.InspectionPaperID.ToDecimal(),
                                        inspectionHIstoryId.ToDecimal()
                                         


                                   );
                   

                    InspectionPaperId = InspectionPaperId + ',' + InsProcessId;


                    QueryResult qrDesign = sf.SubmitChanges("PR_DESIGN_HISTORY_UPDATE",
                                        "U",

                                           objInspectn.NraDefCode, 
                                          inspectionHIstoryId.ToDecimal()


                                    );


                }
                catch (OracleException oe)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);

                       // string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);

                       // cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);



                       // cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                       // + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);


                       // if (objInspectn.Inspection == "1")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "";
                       //     objInspectn.InspectionLevel2 = "";
                       //     objInspectn.InspectionLevel3 = "";
                       // }
                       // if (objInspectn.Inspection == "2")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "1";
                       //     objInspectn.InspectionLevel2 = "";
                       //     objInspectn.InspectionLevel3 = "";

                       // }
                       // if (objInspectn.Inspection == "3")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "1";
                       //     objInspectn.InspectionLevel2 = "2";
                       //     objInspectn.InspectionLevel3 = "";

                       // }
                       // cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                       //+ "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                       //+ ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);

                        InspectionPaperId = "Failed";

                    }
                }
                catch (Exception ex)
                {
                    if (objInspectn.InspectionMstId.ConvertToString() != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        InspectionPaperId = "Failed";

                       // string cmdText = String.Format("delete from NHRS_INSPECTION_PROCESS_DTL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);

                       // cmdText = String.Format("delete from NHRS_INSPECTION_DETAIL where INSPECTION_PAPER_ID='" + objInspectn.InspectionPaperID.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);



                       // cmdText = String.Format("delete from NHRS_INSPECTION_PAPER_DTL  where INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'"
                       // + "AND INSPECTION_LEVEL='" + objInspectn.Inspection.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);


                       // if (objInspectn.Inspection == "1")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "";
                       //     objInspectn.InspectionLevel2 = "";
                       //     objInspectn.InspectionLevel3 = "";
                       // }
                       // if (objInspectn.Inspection == "2")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "1";
                       //     objInspectn.InspectionLevel2 = "";
                       //     objInspectn.InspectionLevel3 = "";

                       // }
                       // if (objInspectn.Inspection == "3")
                       // {
                       //     objInspectn.InspectionLevel0 = "0";
                       //     objInspectn.InspectionLevel1 = "1";
                       //     objInspectn.InspectionLevel2 = "2";
                       //     objInspectn.InspectionLevel3 = "";

                       // }
                       // cmdText = String.Format("Update  nhrs_inspection_mst set INSPECTION_LEVEL1='" + objInspectn.InspectionLevel1.ConvertToString() + "',"
                       //+ "INSPECTION_LEVEL2='" + objInspectn.InspectionLevel2.ConvertToString() + "'"
                       //+ ", INSPECTION_LEVEL3='" + objInspectn.InspectionLevel3.ConvertToString() + "'WHERE INSPECTION_MST_ID='" + objInspectn.InspectionMstId.ConvertToString() + "'");
                       // SaveErrorMessgae(cmdText);



                    }
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }



            }
            return (InspectionPaperId);
        }
        //get inspctn ppr dtl getInspctPprDtl
        public DataTable getInspctPprDtl(string id)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";


            try
            {
                cmd = "SELECT IPD.INSPECTION_PAPER_ID,IPD.NRA_DEFINED_CD,IPD.MAP_DESIGN,DESIGN_NUMBER,IPD.RC_MATERIAL_CD, "
                + "IPD.FC_MATERIAL_CD,IPD.TECHNICAL_ASSIST,IPD.ORGANIZATION_TYPE,IPD.CONSTRUCTOR_TYPE,IPD.HOUSE_MODEL, "
                + " INSD.INSPECTION_ELEMENT_ID,IPSD.INSPECTION_PROCESS_ID,IPSD.BENF_FULL_NAME,IPSD.RELATN_TO_BENF,IPSD.REGISTRATION_DATE,IPSD.STATUS, "
                + " SOIL_TYPE, HOUSE_MODEL,PHOTO_CD_1,PHOTO_1 FROM NHRS_INSPECTION_PAPER_DTL IPD "
                + "JOIN NHRS_INSPECTION_DETAIL INSD ON IPD.INSPECTION_PAPER_ID= INSD.INSPECTION_PAPER_ID "
                + "JOIN NHRS_INSPECTION_PROCESS_DTL IPSD ON IPD.INSPECTION_PAPER_ID= IPSD.INSPECTION_PAPER_ID "
                + " WHERE IPD.INSPECTION_PAPER_ID='" + id + "'";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }
            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            return dtbl;

        }

        
         public string GetBuildingStructure( InspectionOwnerDetailModelClass objOwn   )
        {
            string buildingStrCount = null;
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string id = objOwn.HouseOwnerId.ConvertToString();
            string cmd = "";
           
            try
            {
                cmd = "SELECT BUILDING_STRUCTURE_NO FROM VW_HOUSE_BUILDING_DTL WHERE HOUSE_OWNER_ID = '" + id + "'"; 
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
             if (dtbl!=null)
             {
                 foreach(DataRow dr in dtbl.Rows)
                 {
                     buildingStrCount = dr["BUILDING_STRUCTURE_NO"].ConvertToString();
                 }
             }
            return buildingStrCount;
        }


        //get hierarchy cd 
         public string getHierarchyCd(string id)
         {
             string hierarchyCd = "";
              DataTable dtbl = new DataTable();
             ServiceFactory sf = new ServiceFactory();
              string cmd = "";

             try
             {
                 cmd = "SELECT HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE  MODEL_ID= '" + id + "'";
                 sf.Begin();
                 dtbl = sf.GetDataTable(new
                 {
                     query = cmd,
                     args = new { }
                 });
             }

             catch (OracleException oe)
             {
                 ExceptionManager.AppendLog(oe);
             }
             catch (Exception ex)
             {
                 ExceptionManager.AppendLog(ex);
             }
             finally
             {
                 if (sf.Transaction != null)
                     sf.End();
             }
             if (dtbl != null)
             {
                 foreach (DataRow dr in dtbl.Rows)
                 {
                     hierarchyCd = dr["HIERARCHY_PARENT_ID"].ConvertToString();
                 }
             }
             return hierarchyCd;
         }

        //get  Next Inspection Design for already defined Lower Level

         public string[] getNextInspectionDesignId(string id)
         {
             string [] values = null;
             DataTable dtbl = new DataTable();
             ServiceFactory sf = new ServiceFactory();
             string cmd = "";

             try
             {
                 cmd = "SELECT MODEL_ID,HIERARCHY_PARENT_ID FROM NHRS_HOUSE_MODEL WHERE PREVIOUS_DESIGN_CD='"+id+"'";
                 sf.Begin();
                 dtbl = sf.GetDataTable(new
                 {
                     query = cmd,
                     args = new { }
                 });
             }

             catch (OracleException oe)
             {
                 ExceptionManager.AppendLog(oe);
             }
             catch (Exception ex)
             {
                 ExceptionManager.AppendLog(ex);
             }
             finally
             {
                 if (sf.Transaction != null)
                     sf.End();
             }
             if (dtbl != null && dtbl.Rows.Count>0)
             {
                 foreach (DataRow dr in dtbl.Rows)
                 {
                     string hierarchy = dr["HIERARCHY_PARENT_ID"].ConvertToString();
                     string design = dr["MODEL_ID"].ConvertToString();

                    string[] a=  { design,hierarchy};
                    values = a;
                 }
             }
             return values;
         }
         //get  get bank acc and bannk name by design and pa number

         public string[] getBankInfo (string id,string design)
         {
             string[] values = null;
             DataTable dtbl = new DataTable();
             ServiceFactory sf = new ServiceFactory();
             string cmd = "";

             try
             {
                 cmd = "SELECT BANK_CD,BANK_ACC_NUM FROM NHRS_INSPECTION_PAPER_DTL WHERE NRA_DEFINED_CD='" + id + "' ";
                 sf.Begin();
                 dtbl = sf.GetDataTable(new
                 {
                     query = cmd,
                     args = new { }
                 });
             }

             catch (OracleException oe)
             {
                 ExceptionManager.AppendLog(oe);
             }
             catch (Exception ex)
             {
                 ExceptionManager.AppendLog(ex);
             }
             finally
             {
                 if (sf.Transaction != null)
                     sf.End();
             }
             if (dtbl != null)
             {
                 foreach (DataRow dr in dtbl.Rows)
                 {
                     string bankId = dr["BANK_CD"].ConvertToString();
                     string AccNo = dr["BANK_ACC_NUM"].ConvertToString();

                     string[] a = { bankId, AccNo };
                     values = a;
                 }
             }
             return values;
         }

        //GET IMPORTED FILE STATUS 
         public System.Data.DataTable GetDataImportRecordByDistrict(string district, string vdc)
         {
             DataRow row;
             DataTable dtbl = new DataTable();

             using (ServiceFactory service = new ServiceFactory())
             {
                 service.Begin();
                 try
                 {
                     service.PackageName = "PKG_NHRS_INSPECTION";
                     dtbl = service.GetDataTable(true, "PR_NHRS_INSPECTION_FILE_BATCH",
                          district.ToDecimal(),//DistrictID
                         vdc.ToDecimal(),//VDC
                         //DBNull.Value,//WARD
                         DBNull.Value);
                     // dtbl = service.GetDataTable(cmdTxt, null);

                 }
                 catch (Exception ex)
                 {
                     dtbl = null;
                     ExceptionHandler.ExceptionManager.AppendLog(ex);
                 }
                 finally
                 {
                     if (service.Transaction != null)
                     {
                         service.End();
                     }
                 }
             }

             return dtbl;
         }

        

        

       

        
         

         //save Inspection Design

         public Boolean saveInspectionDesign(InspectionDesignModelClass ObjDesign)
         {
             string exc = string.Empty;
             bool res = false;
             QueryResult qre = null;
             QueryResult qr = null;
             string InspectionMstId = "";



             string DEFINED_CD = "";
             string INSPECTION_LEVEL = "";
             string INSPECTION_DATE = "";
             string INSPECTION_STATUS = "";
             string NRA_DEFINED_CD = "";
             string InspectionHistoryId = "";

             using (ServiceFactory sf = new ServiceFactory())
            {

                try
                {
                    sf.PackageName = "NHRS.PKG_NHRS_INSPECTION";
                    sf.Begin();
                    if( ObjDesign.Mode=="I")
                    {
                        //save to inspection MST
                        qr = sf.SubmitChanges("PR_NHRS_INSPECTION_MST",
                                         ObjDesign.Mode.ConvertToString(),

                                          ObjDesign.InspectionMstId.ToDecimal(),
                                          DEFINED_CD.ConvertToString(),
                                          ObjDesign.InspectionLevel0.ToDecimal(),
                                          INSPECTION_DATE.ToDateTime(),
                                          ObjDesign.InspectionStatus.ConvertToString(),

                                          SessionCheck.getSessionUsername(),
                                           DateTime.Now,
                                           System.DateTime.Now.ConvertToString(),

                                           DBNull.Value,
                                           SessionCheck.getSessionUsername(),
                                           DateTime.Now,
                                           System.DateTime.Now.ConvertToString(),

                                           SessionCheck.getSessionUsername(),
                                           DateTime.Now,
                                           System.DateTime.Now.ConvertToString(),
                                           ObjDesign.NraDefCode.ConvertToString(),
                                           ObjDesign.InspectionLevel1.ToDecimal(),
                                           ObjDesign.InspectionLevel2.ToDecimal(),
                                           ObjDesign.InspectionLevel3.ToDecimal(),
                                           ObjDesign.complyFlag.ConvertToString(),
                                           "N"  ,// final decision 2 APPROVE
                                           "N" ,  // final decision APPROVE
                                           InspectionUpdateHistoryId.ToDecimal(),
                                           DBNull.Value,
                                           DBNull.Value,
                                           DBNull.Value


                                     );
                        InspectionMstId = qr["v_INSPECTION_MST_ID"].ConvertToString();

                       QueryResult qrRegister = sf.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                               ObjDesign.Mode.ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               ObjDesign.district_Cd.ToDecimal(),

                                                  ObjDesign.vdc_mun_cd.ToDecimal(),
                                                  ObjDesign.ward_no.ToDecimal(),
                                                ObjDesign.NraDefCode.ConvertToString(),
                                               ObjDesign.BenfFullNameEng.ConvertToString(),
                                               0,
                                               DBNull.Value,
                                               DBNull.Value, //remarks


                                               DBNull.Value,
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),


                                               DBNull.Value, //reg no
                                                DateTime.Now.ConvertToString(), //reg date

                                               DBNull.Value, //schedule
                                               DBNull.Value,
                                               1,
                                               DBNull.Value,
                                               DBNull.Value



                       );
                   
                        if (InspectionMstId != null)
                        {
                            if (ObjDesign.InspectionLevel0 == "0")
                            {
                                    qr = sf.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                                                 ObjDesign.Mode.ConvertToString(),

                                                  ObjDesign.DesignCd.ToDecimal(),
                                                  "".ToString(),
                                                  ObjDesign.NraDefCode.ConvertToString(),
                                                  ObjDesign.BenfFullNameEng.ConvertToString(),
                                                  ObjDesign.district_Cd.ToDecimal(),

                                                  ObjDesign.vdc_mun_cd.ToDecimal(),
                                                  ObjDesign.ward_no.ToDecimal(),
                                                  ObjDesign.tole.ConvertToString(),
                                                  ObjDesign.OwnDesign.ConvertToString(),

                                                  ObjDesign.DesignNumber.ConvertToString(),
                                                  ObjDesign.ConstructMat.ToDecimal(),
                                                  ObjDesign.Roofmat.ToDecimal(),

                                                  ObjDesign.ConstOther.ConvertToString(),
                                                  ObjDesign.RoofOther.ConvertToString(),

                                                   SessionCheck.getSessionUsername(),
                                                   DateTime.Now,
                                                   System.DateTime.Now.ConvertToString(),

                                                   DBNull.Value,
                                                   SessionCheck.getSessionUsername(),
                                                   DateTime.Now,
                                                   System.DateTime.Now.ConvertToString(),

                                                   SessionCheck.getSessionUsername(),
                                                   DateTime.Now,
                                                   System.DateTime.Now.ConvertToString(),

                                                  InspectionMstId.ToDecimal(),
                                                  ObjDesign.InspectionStatus.ConvertToString(),
                                                  ObjDesign.KittaNumber.ConvertToString(),
                                                  InspectionHistoryId.ToDecimal()
                                                  



                                             );
                        }
                    }
                    }

                    if(ObjDesign.Mode=="U")
                    {

                        DataTable dtHistory = new DataTable();
                        
                        string cmdHistoryid = " SELECT (NVL(MAX(INSPECTION_HISTORY_ID), 0) + 1) AS  INSPECTION_HISTORY_ID  FROM NHRS_INSPECTION_UPDATE_HISTORY";
                        dtHistory = sf.GetDataTable(new
                        {
                            query = cmdHistoryid,
                            args = new { }
                        });

                        if (dtHistory != null && dtHistory.Rows.Count > 0)
                        {
                            InspectionHistoryId = dtHistory.Rows[0]["INSPECTION_HISTORY_ID"].ConvertToString();
                        }

                        string insertCmd = "INSERT INTO NHRS_INSPECTION_UPDATE_HISTORY (INSPECTION_HISTORY_ID) VALUES ('" + InspectionHistoryId + "')";
                        QueryResult InsertHistoryId = sf.SubmitChanges(insertCmd,null);

                        qr = sf.SubmitChanges("PR_NHRS_INSPECTION_DESIGN",
                                               ObjDesign.Mode.ConvertToString(),

                                                ObjDesign.DesignCd.ToDecimal(),
                                                DBNull.Value,
                                                ObjDesign.NraDefCode.ConvertToString(),
                                                ObjDesign.BenfFullNameEng.ConvertToString(),
                                                ObjDesign.district_Cd.ToDecimal(),

                                                ObjDesign.vdc_mun_cd.ToDecimal(),
                                                ObjDesign.ward_no.ToDecimal(),
                                                ObjDesign.tole.ConvertToString(),
                                                ObjDesign.OwnDesign.ConvertToString(),

                                                ObjDesign.DesignNumber.ConvertToString(),
                                                ObjDesign.ConstructMat.ToDecimal(),
                                                ObjDesign.Roofmat.ToDecimal(),

                                                ObjDesign.ConstOther.ConvertToString(),
                                                ObjDesign.RoofOther.ConvertToString(),

                                                 SessionCheck.getSessionUsername(),
                                                 DateTime.Now,
                                                 System.DateTime.Now.ConvertToString(),

                                                DBNull.Value,
                                                 SessionCheck.getSessionUsername(),
                                                 DateTime.Now,
                                                 System.DateTime.Now.ConvertToString(),

                                                 SessionCheck.getSessionUsername(),
                                                 DateTime.Now,
                                                 System.DateTime.Now.ConvertToString(),

                                                InspectionMstId.ToDecimal(),
                                                ObjDesign.InspectionStatus.ConvertToString(),
                                                 ObjDesign.KittaNumber.ConvertToString(),
                                                 InspectionHistoryId.ToDecimal()
                                                );

                        QueryResult qrApplicant = sf.SubmitChanges("PR_NHRS_INSPECTION_APPLICATION",
                                                  ObjDesign.Mode.ConvertToString(),
                                               DBNull.Value,
                                               DBNull.Value,
                                               ObjDesign.district_Cd.ToDecimal(),

                                                  ObjDesign.vdc_mun_cd.ToDecimal(),
                                                  ObjDesign.ward_no.ToDecimal(),
                                                ObjDesign.NraDefCode.ConvertToString(),
                                               ObjDesign.BenfFullNameEng.ConvertToString(),
                                               0,
                                               DBNull.Value,
                                               DBNull.Value, //remarks


                                               DBNull.Value,
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),
                                               SessionCheck.getSessionUsername(),
                                               DateTime.Now,
                                               System.DateTime.Now.ConvertToString(),


                                               DBNull.Value, //reg no
                                               DateTime.Now.ConvertToString(), //reg date

                                               DBNull.Value, //schedule
                                               DBNull.Value,
                                               1,
                                               DBNull.Value,
                                               DBNull.Value





                                             );


                        //updating other table in history

                        QueryResult QrMst = sf.SubmitChanges("PR_MST_HISTORY_UPDATE",
                                                  "U",
                                                  ObjDesign.NraDefCode.ConvertToString(),
                                                  InspectionHistoryId 
                                             );

                        


                    }

                    



                }
                catch (OracleException oe)
                {
                    if (InspectionMstId != "")
                    {
                        res = true;
                        exc += oe.Message.ToString();
                        ExceptionManager.AppendLog(oe);
                        if(ObjDesign.Mode.ConvertToString()=="I")
                        {
                            string cmdText = String.Format("delete from NHRS_INSPECTION_MST where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_DESIGN where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD ='" + ObjDesign.NraDefCode.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (ObjDesign.Mode.ConvertToString() == "U")
                        {
                            string cmdText = String.Format("delete from NHRS_INSPECTION_MST where INSPECTION_MST_ID='" + ObjDesign.InspectionMstId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_DESIGN where INSPECTION_MST_ID='" + ObjDesign.InspectionMstId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD ='" + ObjDesign.NraDefCode.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                    }
              
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    if (InspectionMstId != "")
                    {
                        res = true;
                        exc += ex.Message.ToString();
                        ExceptionManager.AppendLog(ex);

                        if (ObjDesign.Mode.ConvertToString() == "I")
                        {
                            string cmdText = String.Format("delete from NHRS_INSPECTION_MST where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_DESIGN where INSPECTION_MST_ID='" + InspectionMstId + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD ='" + ObjDesign.NraDefCode.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }
                        if (ObjDesign.Mode.ConvertToString() == "U")
                        {
                            string cmdText = String.Format("delete from NHRS_INSPECTION_MST where INSPECTION_MST_ID='" + ObjDesign.InspectionMstId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_DESIGN where INSPECTION_MST_ID='" + ObjDesign.InspectionMstId.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                            cmdText = String.Format("delete from NHRS_INSPECTION_APPLICATION where NRA_DEFINED_CD ='" + ObjDesign.NraDefCode.ConvertToString() + "'");
                            SaveErrorMessgae(cmdText);
                        }

                    }
                    
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                    {
                        sf.End();
                    }
                }
                if (res)
                {
                    res = false;
                }
                else
                {
                    if (qr != null)
                    {
                        res = qr.IsSuccess;
                    }
                }

                return res;
             }

         }

        //approve inspection
         public bool ApproveInspectionLevel(InspectionDesignModelClass ObjDesign)
         {
             bool res = false;
             DataTable dt = new DataTable();
             DataTable dTt = new DataTable();
             
             using (ServiceFactory service = new ServiceFactory())
             {
                 service.Begin();
                 string CmdText = "SELECT INSPECTION_STATUS FROM NHRS_INSPECTION_MST WHERE NRA_DEFINED_CD ='" + ObjDesign.NraDefCode + "'";
                 string CommandText = ("UPDATE  NHRS_INSPECTION_MST SET INSPECTION_STATUS='" + ObjDesign.InspectionStatus + "'  WHERE INSPECTION_MST_ID='" + ObjDesign.InspectionMstId + "' AND NRA_DEFINED_CD='" + ObjDesign.NraDefCode + "'");
                 //string CommandText = ("UPDATE  NHRS_INSPECTION_MST SET INSPECTION_STATUS='" + ObjDesign.InspectionStatus + "' AND COMPLY_CD='" + ObjDesign.complyCd + "' WHERE INSPECTION_MST_ID='" + ObjDesign.InspectionMstId + "' AND NRA_DEFINED_CD='" + ObjDesign.NraDefCode + "'");

                 try
                 {
                     QueryResult qrapprove = service.SubmitChanges(CommandText);
                     if(qrapprove!=null)
                     {
                         string CdText = "UPDATE  NHRS_PAYROLL_DTL SET INSPECTION_FLAG = 'Y' ,INSPECTION_CD='"+ObjDesign.InspectionLevel+"' WHERE HOUSE_OWNER_ID='" + ObjDesign.hOwnerId + "' ";
                         QueryResult payrollApprove = service.SubmitChanges(CdText);
                     }
                    

                 }
                 catch (Exception ex)
                 {
                     dt = null;
                     ExceptionManager.AppendLog(ex);
                 }
                 finally
                 {
                     if (service.Transaction != null)
                     {
                         service.End();
                     }
                 }
             }
             if (dt.Rows.Count > 0)
             {
                 res = true;

             }

             return res;
         }

        

       
        

      
        // get Inspection design Description

         public string GetHouseDesignDescription(string id)
         {

             DataTable dtbl = new DataTable();
             ServiceFactory sf = new ServiceFactory();
             string cmd = "";
             string DsignDesc = "";


             try
             {
                 cmd = "SELECT " + Utils.ToggleLanguage("NAME_ENG", "NAME_LOC ") + " as HOUSE_DESIGN FROM NHRS_HOUSE_MODEL WHERE MODEL_ID = '" + id + "'";
                 sf.Begin();
                 dtbl = sf.GetDataTable(new
                 {
                     query = cmd,
                     args = new { }
                 });
             }

             catch (OracleException oe)
             {
                 ExceptionManager.AppendLog(oe);
             }
             catch (Exception ex)
             {
                 ExceptionManager.AppendLog(ex);
             }
             finally
             {
                 if (sf.Transaction != null)
                     sf.End();
             }
             if (dtbl != null)
             {
                 foreach (DataRow dr in dtbl.Rows)
                 {
                     DsignDesc = dr["HOUSE_DESIGN"].ConvertToString();
                 }
             }
             return DsignDesc;
         }
          public DataTable GetHouseInspectionValue(string InspectionPaperID,string DesignNumber)
         {
             
             DataTable dtbl = new DataTable();
             ServiceFactory sf = new ServiceFactory();
             string cmd = "";
             

             try
             {
                 cmd = "SELECT * from NHRS_INSPECTION_DETAIL WHERE inspection_paper_id = '" + InspectionPaperID + "' and house_model='"+DesignNumber+"' order by INSPECTION_ELEMENT_ID";
                 sf.Begin();
                 dtbl = sf.GetDataTable(new
                 {
                     query = cmd,
                     args = new { }
                 });
             }

             catch (OracleException oe)
             {
                 ExceptionManager.AppendLog(oe);
             }
             catch (Exception ex)
             {
                 ExceptionManager.AppendLog(ex);
             }
             finally
             {
                 if (sf.Transaction != null)
                     sf.End();
             }
             return dtbl;
         }

          public DataTable GetInspectionComplyValue(string id)
          {

              DataTable dtbl = new DataTable();
              ServiceFactory sf = new ServiceFactory();
              string cmd = "";


              try
              {
                  cmd = "SELECT * from NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN = '" + id + "' order by INSPECTION_ELEMENT_ID";
                  sf.Begin();
                  dtbl = sf.GetDataTable(new
                  {
                      query = cmd,
                      args = new { }
                  });
              }

              catch (OracleException oe)
              {
                  ExceptionManager.AppendLog(oe);
              }
              catch (Exception ex)
              {
                  ExceptionManager.AppendLog(ex);
              }
              finally
              {
                  if (sf.Transaction != null)
                      sf.End();
              }
              return dtbl;
          }

          //get all inspection information for edit 

          public InspectionDesignModelClass getAllInnfoForInspection1(string nraDefCode, string owndesign,string DesignId)
          {
              DataTable dt = new DataTable();
              string cmdtext="";
              InspectionDesignModelClass objInspDesign = new InspectionDesignModelClass();
              ServiceFactory sf = new ServiceFactory();

                try
                {
                    if (owndesign=="Y")
                    {
                        cmdtext = "SELECT NIM.INSPECTION_MST_ID, NIM.INSPECTION_STATUS,NIM.INSPECTION_LEVEL0, NIM.INSPECTION_LEVEL1, NIM.INSPECTION_LEVEL2, NIM.INSPECTION_LEVEL3,"
                           + "NIPD.BENEFICIARY_NAME,NIPD.INSPECTION_PAPER_ID,NIPD.DISTRICT_CD, NIPD.VDC_MUN_CD, NIPD.WARD_CD,NIPD.TOLE, NIPD.DESIGN_NUMBER, NIPD.PHOTO_CD_1, NIPD.PHOTO_CD_2,NIPD.NRA_DEFINED_CD,"
                           + "NIPD.PHOTO_CD_3, NIPD.PHOTO_CD_4,NIPD.PHOTO_CD_5, NIPD.PHOTO_CD_6, NIPD.PHOTO_1,NIPD.PHOTO_2, NIPD.PHOTO_3, NIPD.PHOTO_4, NIPD.PHOTO_5,NIPD.PHOTO_6,"
                           + "NIPD.PASSED_NAKSA_NO,NIPD.LATITUDE,NIPD.LONGITUDE,NIPD.ALTITUDE,NIPD.FINAL_DECISION,NIPR.INSPECTION_PROCESS_ID,NIPD.SERIAL_NUMBER,NIPD.BANK_CD,NIPD.BANK_ACC_NUM,NIPD.ORGANIZATION_OTHERS,NIPD.TECHNICAL_ASSIST,NIPD.ORGANIZATION_TYPE,NIPD.CONSTRUCTOR_TYPE,NIPD.SOIL_TYPE,"
                           + "NIPD.INSPECTION_DATE,NIPD.MOBILE_NUMBER,NIPD.FINAL_DECISION_2_APPROVE  ,NIPD.FINAL_REMARKS,NIPD.FINAL_DECISION_2,NIM.FINAL_DECISION_APPROVE,"
                           + "NIPR.BENF_FULL_NAME,NIPR.RELATN_TO_BENF,NIPR.EXAMINAR_FULL_NAME,NIPR.EXAMINAR_DESIGNATION,NIPR.ENGINEER_FULL_NAME,NIPR.ENGINEER_DESIGNATION,NIPR.REGISTRATION_DATE,"

                            + " NIM.INSP_ONE_MOUD_APPROVE,NIM.INSP_TWO_MOUD_APPROVE,NIM.INSP_THREE_MOUD_APPROVE,NIM.INSP_ONE_MOFALD_APPROVE "
                            + ",NIM.INSP_TWO_MOFALD_APPROVE ,NIM.INSP_THREE_MOFALD_APPROVE,NIM.INSP_ONE_ENGI_APPROVE,NIM.INSP_TWO_ENGI_APPROVE,NIM.INSP_THREE_ENGI_APPROVE, "
              
                           
                           + "NIPR.PROCEED_DATE,NIPR.APPROVAL_DATE, NHM.HIERARCHY_PARENT_ID "
                           + "FROM NHRS_INSPECTION_MST NIM "
                           + "JOIN NHRS_INSPECTION_PAPER_DTL NIPD ON NIM.INSPECTION_MST_ID=NIPD.INSPECTION_MST_ID "
                           + "JOIN NHRS_INSPECTION_PROCESS_DTL NIPR ON NIPD.INSPECTION_PAPER_ID=NIPR.INSPECTION_PAPER_ID "
                           + "LEFT OUTER JOIN NHRS_HOUSE_MODEL NHM ON NIPD.DESIGN_NUMBER=NHM.MODEL_ID "

                           + "WHERE NIM.NRA_DEFINED_CD='" + nraDefCode + "' AND NIPD.DESIGN_NUMBER='" + DesignId + "'";
                    }
                    else
                    {
                        cmdtext = "SELECT NIM.INSPECTION_MST_ID, NIM.INSPECTION_STATUS,NIM.INSPECTION_LEVEL0, NIM.INSPECTION_LEVEL1, NIM.INSPECTION_LEVEL2, NIM.INSPECTION_LEVEL3,"
                            + "NIPD.BENEFICIARY_NAME,NIPD.INSPECTION_PAPER_ID,NIPD.DISTRICT_CD, NIPD.VDC_MUN_CD, NIPD.WARD_CD,NIPD.TOLE, NIPD.DESIGN_NUMBER, NIPD.PHOTO_CD_1, NIPD.PHOTO_CD_2,NIPD.NRA_DEFINED_CD,"
                             + "NIPD.PHOTO_CD_3, NIPD.PHOTO_CD_4,NIPD.PHOTO_CD_5, NIPD.PHOTO_CD_6, NIPD.PHOTO_1,NIPD.PHOTO_2, NIPD.PHOTO_3, NIPD.PHOTO_4, NIPD.PHOTO_5,NIPD.PHOTO_6,"
                            + "NIPD.PASSED_NAKSA_NO,NIPD.LATITUDE,NIPD.LONGITUDE,NIPD.ALTITUDE,NIPD.FINAL_DECISION,NID.INSPECTION_ELEMENT_ID,NID.VALUE_TYPE,NIPR.INSPECTION_PROCESS_ID,NIPD.SERIAL_NUMBER,NIPD.BANK_CD,NIPD.BANK_ACC_NUM,NIPD.ORGANIZATION_OTHERS,NIPD.TECHNICAL_ASSIST,NIPD.ORGANIZATION_TYPE,NIPD.CONSTRUCTOR_TYPE,NIPD.SOIL_TYPE,"
                            + "NIPD.INSPECTION_DATE,NIPD.MOBILE_NUMBER,NIPD.FINAL_DECISION_2_APPROVE ,NIPD.FINAL_REMARKS ,NIPD.FINAL_DECISION_2,NIM.FINAL_DECISION_APPROVE,"

                            + "NIPR.BENF_FULL_NAME,NIPR.RELATN_TO_BENF,NIPR.EXAMINAR_FULL_NAME,NIPR.EXAMINAR_DESIGNATION,NIPR.ENGINEER_FULL_NAME,NIPR.ENGINEER_DESIGNATION,NIPR.REGISTRATION_DATE,NID.REMARKS,"


                            + " NIM.INSP_ONE_MOUD_APPROVE,NIM.INSP_TWO_MOUD_APPROVE,NIM.INSP_THREE_MOUD_APPROVE,NIM.INSP_ONE_MOFALD_APPROVE "
                            + ",NIM.INSP_TWO_MOFALD_APPROVE ,NIM.INSP_THREE_MOFALD_APPROVE,NIM.INSP_ONE_ENGI_APPROVE,NIM.INSP_TWO_ENGI_APPROVE,NIM.INSP_THREE_ENGI_APPROVE,  "
              
                            
                            
                            + "NIPR.PROCEED_DATE,NIPR.APPROVAL_DATE,NHM.HIERARCHY_PARENT_ID "
                            + "FROM NHRS_INSPECTION_MST NIM "
                            + "JOIN NHRS_INSPECTION_PAPER_DTL NIPD ON NIM.INSPECTION_MST_ID=NIPD.INSPECTION_MST_ID "
                            + "JOIN NHRS_INSPECTION_DETAIL NID ON NIPD.INSPECTION_PAPER_ID=NID.INSPECTION_PAPER_ID  "
                            + "JOIN NHRS_INSPECTION_PROCESS_DTL NIPR ON NIPD.INSPECTION_PAPER_ID=NIPR.INSPECTION_PAPER_ID "
                            + "LEFT OUTER JOIN NHRS_HOUSE_MODEL NHM ON NIPD.DESIGN_NUMBER=NHM.MODEL_ID "
                            + "WHERE NIM.NRA_DEFINED_CD='" + nraDefCode + "'  AND NIPD.DESIGN_NUMBER='" + DesignId + "'";
                    }
                     
                    sf.Begin();
                    dt = sf.GetDataTable(new
                    {
                        query = cmdtext,
                        args = new { }
                    });
                }

                catch (OracleException oe)
                {
                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {
                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (sf.Transaction != null)
                        sf.End();
                }
                List<InspectionComplyModelClass> objInspection1List = new List<InspectionComplyModelClass>();
              if(dt!=null&&dt.Rows.Count>0)
              {
                  foreach(DataRow dr in dt.Rows)
                  {
                              objInspDesign.NraDefCode       = dr["NRA_DEFINED_CD"].ConvertToString();
                              objInspDesign.InspectionMstId  = dr["INSPECTION_MST_ID"].ConvertToString();
                              objInspDesign.InspectionStatus = dr["INSPECTION_STATUS"].ConvertToString();
                              objInspDesign.InspectionLevel0 = dr["INSPECTION_LEVEL0"].ConvertToString();
                              objInspDesign.InspectionLevel1 = dr["INSPECTION_LEVEL1"].ConvertToString();
                              objInspDesign.InspectionLevel2 = dr["INSPECTION_LEVEL2"].ConvertToString();

                              objInspDesign.InspectionLevel3  = dr["INSPECTION_LEVEL3"].ConvertToString();
                              objInspDesign.BenfFullNameEng   = dr["BENEFICIARY_NAME"].ConvertToString();
                              objInspDesign.InspectionPaperID = dr["INSPECTION_PAPER_ID"].ConvertToString();
                              objInspDesign.district_Cd       = dr["DISTRICT_CD"].ConvertToString();
                              objInspDesign.vdc_mun_cd        = dr["VDC_MUN_CD"].ConvertToString();

                              objInspDesign.ward_no      = dr["WARD_CD"].ConvertToString();
                              objInspDesign.DesignNumber = dr["DESIGN_NUMBER"].ConvertToString();
                              objInspDesign.PHOTO_CD_1   = dr["PHOTO_CD_1"].ConvertToString();
                              objInspDesign.PHOTO_CD_2   = dr["PHOTO_CD_2"].ConvertToString();
                              objInspDesign.PHOTO_CD_3   = dr["PHOTO_CD_3"].ConvertToString();

                              objInspDesign.PHOTO_CD_4 = dr["PHOTO_CD_4"].ConvertToString();
                              objInspDesign.PHOTO_CD_5 = dr["PHOTO_CD_5"].ConvertToString();
                              objInspDesign.PHOTO_CD_6 = dr["PHOTO_CD_6"].ConvertToString();
                              objInspDesign.PHOTO_1    = dr["PHOTO_1"].ConvertToString();
                              objInspDesign.PHOTO_2    = dr["PHOTO_2"].ConvertToString();

                              objInspDesign.PHOTO_3 = dr["PHOTO_3"].ConvertToString();
                              objInspDesign.PHOTO_4 = dr["PHOTO_4"].ConvertToString();
                              objInspDesign.PHOTO_5 = dr["PHOTO_5"].ConvertToString();
                              objInspDesign.PHOTO_6 = dr["PHOTO_6"].ConvertToString();
                              objInspDesign.PHOTO_HOUSE = dr["PASSED_NAKSA_NO"].ConvertToString();

                              objInspDesign.LATITUDE            = dr["LATITUDE"].ConvertToString();
                              objInspDesign.LONGITUDE           = dr["LONGITUDE"].ConvertToString();
                              objInspDesign.ATTITUDE            = dr["ALTITUDE"].ConvertToString();
                              objInspDesign.finalDecision = dr["FINAL_DECISION"].ConvertToString();
                              objInspDesign.BeneficiaryFullName = dr["BENF_FULL_NAME"].ConvertToString();// for approvel section 

                              objInspDesign.BenfRelation        = dr["RELATN_TO_BENF"].ConvertToString();
                              objInspDesign.SuprtntfFullnameEng = dr["EXAMINAR_FULL_NAME"].ConvertToString();
                              objInspDesign.SupertntDesignation = dr["EXAMINAR_DESIGNATION"].ConvertToString();
                              objInspDesign.EngineerFullname    = dr["ENGINEER_FULL_NAME"].ConvertToString();
                              objInspDesign.EngineerDesignation = dr["ENGINEER_DESIGNATION"].ConvertToString();


                              objInspDesign.INSP_ONE_ENGI_APPROVE   = dr["INSP_ONE_ENGI_APPROVE"].ConvertToString();
                              objInspDesign.INSP_ONE_MOUD_APPROVE   = dr["INSP_ONE_MOUD_APPROVE"].ConvertToString(); 
                              objInspDesign.INSP_ONE_MOFALD_APPROVE = dr["INSP_ONE_MOFALD_APPROVE"].ConvertToString();

                              objInspDesign.INSP_TWO_ENGI_APPROVE   = dr["INSP_TWO_ENGI_APPROVE"].ConvertToString();
                              objInspDesign.INSP_TWO_MOUD_APPROVE   = dr["INSP_TWO_MOUD_APPROVE"].ConvertToString();
                              objInspDesign.INSP_TWO_MOFALD_APPROVE = dr["INSP_TWO_MOFALD_APPROVE"].ConvertToString();

                              objInspDesign.INSP_THREE_ENGI_APPROVE     = dr["INSP_THREE_ENGI_APPROVE"].ConvertToString();
                              objInspDesign.INSP_THREE_MOUD_APPROVE     = dr["INSP_THREE_MOUD_APPROVE"].ConvertToString();
                              objInspDesign.INSP_THREE_MOFALD_APPROVE   = dr["INSP_THREE_MOFALD_APPROVE"].ConvertToString();

                              objInspDesign.BenfSignDateLoc     = dr["REGISTRATION_DATE"].ConvertToString();

                              if (dr["PROCEED_DATE"].ConvertToString()!="")
                              {
                                  objInspDesign.SupertntSignDateLoc = Convert.ToDateTime(dr["PROCEED_DATE"].ConvertToString()).ToString("yyyy-MM-dd");
                              }
                              if (dr["APPROVAL_DATE"].ConvertToString() != "")
                              {
                                  objInspDesign.EngineerSignDateLoc = Convert.ToDateTime(dr["APPROVAL_DATE"].ConvertToString()).ToString("yyyy-MM-dd");
                              }
                             
                              objInspDesign.tole                = dr["TOLE"].ConvertToString();
                              objInspDesign.Serial_Num          = dr["SERIAL_NUMBER"].ConvertToString();
                              objInspDesign.Bank_Name = dr["BANK_CD"].ConvertToString();
                              objInspDesign.Bank_ACC_Num        = dr["BANK_ACC_NUM"].ConvertToString();
                              objInspDesign.ORGANIZATION_OTHERS = dr["ORGANIZATION_OTHERS"].ConvertToString();
                              objInspDesign.TECHNICAL_ASSIST    = dr["TECHNICAL_ASSIST"].ConvertToString();
                              objInspDesign.ORGANIZATION_TYPE   = dr["ORGANIZATION_TYPE"].ConvertToString();
                              objInspDesign.CONSTRUCTOR_TYPE    = dr["CONSTRUCTOR_TYPE"].ConvertToString();
                              objInspDesign.SOIL_TYPE           = dr["SOIL_TYPE"].ConvertToString();
                              if (dr["INSPECTION_DATE"].ConvertToString() != "")
                              {
                                  objInspDesign.Inspection_date_loc = Convert.ToDateTime(dr["INSPECTION_DATE"].ConvertToString()).ToString("yyyy-MM-dd");
                              }
                              objInspDesign.MobileNumber        = dr["MOBILE_NUMBER"].ConvertToString();
                              objInspDesign.Serial_Num          = dr["SERIAL_NUMBER"].ConvertToString();
                              objInspDesign.Hierarchy_cd        = dr["HIERARCHY_PARENT_ID"].ConvertToString();
                              objInspDesign.final_decision_2_approve = dr["FINAL_DECISION_2_APPROVE"].ConvertToString();
                              objInspDesign.final_decision_approve = dr["FINAL_DECISION_APPROVE"].ConvertToString();
                              objInspDesign.Final_Remarks       = dr["FINAL_REMARKS"].ConvertToString();
                              objInspDesign.final_decision_2 = dr["FINAL_DECISION_2"].ConvertToString();
                      
                              if(objInspDesign.BenfFullNameEng!="")
                              {
                                  string[] fullName = objInspDesign.BenfFullNameEng.Split(' ');
                                  if(fullName.Length==3)
                                  {
                                      objInspDesign.BenfFirstName = fullName[0].ConvertToString();
                                      objInspDesign.BenfMiddleName = fullName[1].ConvertToString();
                                      objInspDesign.BenfLastName = fullName[2].ConvertToString();

                                  }
                                  if (fullName.Length == 2)
                                  {
                                      objInspDesign.BenfFirstName = fullName[0].ConvertToString();
                                      objInspDesign.BenfLastName = fullName[1].ConvertToString();

                                  }
                                  if (fullName.Length == 1)
                                  {
                                      objInspDesign.BenfFirstName = fullName[0].ConvertToString();

                                  }
                              }
                              break;

                     
                     
                   }

                  // inspection design table portion 
                  if (owndesign != "Y")
                  {
                      foreach (DataRow dr in dt.Rows)
                      {
                          InspectionComplyModelClass inspectionEditList = new InspectionComplyModelClass();
                          inspectionEditList.InspectionElementId = dr["INSPECTION_ELEMENT_ID"].ConvertToString();
                          inspectionEditList.InspectionComplyFlag = dr["VALUE_TYPE"].ConvertToString();
                          inspectionEditList.InspectionDesign = dr["DESIGN_NUMBER"].ConvertToString();
                          inspectionEditList.InspectionRemarks = dr["REMARKS"].ConvertToString();
                          objInspection1List.Add(inspectionEditList);
                      }
                      objInspDesign.InspectionEditList = objInspection1List;
                  }
                 
                            

                  }


              
                return objInspDesign;
            
            
        }
        //get own house design data (edit)
          public InspectionOwnDesign getOwnDesignDetail(string nraDefCode ,string level )
          {
              DataTable dt = new DataTable();
              string cmdtext = "";
              InspectionOwnDesign objOwnDesign = new InspectionOwnDesign();

              ServiceFactory sf = new ServiceFactory();

              try
              {

                  cmdtext = "SELECT NIM.INSPECTION_MST_ID, NIM.INSPECTION_STATUS,NIM.INSPECTION_LEVEL0, NIM.INSPECTION_LEVEL1, NIM.INSPECTION_LEVEL2, NIM.INSPECTION_LEVEL3,"
                        + "NIPD.BENEFICIARY_NAME,NIPD.INSPECTION_PAPER_ID,NIPD.DISTRICT_CD, NIPD.VDC_MUN_CD, NIPD.WARD_CD,NIPD.TOLE, NIPD.DESIGN_NUMBER, NIPD.PHOTO_CD_1, NIPD.PHOTO_CD_2,NIPD.NRA_DEFINED_CD,"
                        + "NIPD.PHOTO_CD_3, NIPD.PHOTO_CD_4,NIPD.PHOTO_CD_5, NIPD.PHOTO_CD_6, NIPD.PHOTO_1,NIPD.PHOTO_2, NIPD.PHOTO_3, NIPD.PHOTO_4, NIPD.PHOTO_5,NIPD.PHOTO_6,"
                        + "NIPD.PASSED_NAKSA_NO,NIPD.LATITUDE,NIPD.LONGITUDE,NIPD.ALTITUDE,NIPD.FINAL_DECISION,NIPR.INSPECTION_PROCESS_ID,"
                        + "NIPR.BENF_FULL_NAME,NIPR.RELATN_TO_BENF,NIPR.EXAMINAR_FULL_NAME,NIPR.EXAMINAR_DESIGNATION,NIPR.ENGINEER_FULL_NAME,NIPR.ENGINEER_DESIGNATION,NIPR.REGISTRATION_DATE,"
                        + "NIOD.BASE_CONSTRUCTIONG,NIOD.BASE_CONSTRUCTED,NIOD.GROUND_ROOF_FINISHED,NIOD.GROUND_FLOOR_FINISHED,NIOD.STOREY_COUNT,NIOD.BASE_MATERIAL,"
                        + "NIOD.BASE_DEPTH,NIOD.BASE_WIDTH,NIOD.BASE_HEIGHT,NIOD.GROUND_FLOOR_MAT,NIOD.GROUND_FLOOR_PRINCIPAL,NIOD.WALL_DETAIL,NIOD.FLOOR_ROOF_DESC,"
                        + "NIOD.FLOOR_ROOF_MAT,NIOD.FLOOR_ROOF_PRINCIPAL,NIOD.FLOOR_ROOF_DTL,NIOD.FIRST_FLOOR_MAT,NIOD.FIRST_FLOOR_PRINCIPAL,NIOD.FIRST_FLOOR_DTL,"
                        + "NIOD.ROOF_MAT,NIOD.ROOF_PRINCIPAL,NIOD.ROOF_DTL,NIOD.CONSTRUCTION_STATUS,"

                        + "NIPR.PROCEED_DATE,NIPR.APPROVAL_DATE "
                        + "FROM NHRS_INSPECTION_MST NIM "
                        + "JOIN NHRS_INSPECTION_PAPER_DTL NIPD ON NIM.INSPECTION_MST_ID=NIPD.INSPECTION_MST_ID "
                        + "JOIN NHRS_INSPECTION_OWN_DESIGN NIOD ON NIPD.INSPECTION_PAPER_ID=NIOD.INSPECTION_PAPER_ID  "
                        + "JOIN NHRS_INSPECTION_PROCESS_DTL NIPR ON NIPD.INSPECTION_PAPER_ID=NIPR.INSPECTION_PAPER_ID "
                        + "WHERE NIM.NRA_DEFINED_CD='" + nraDefCode + "' AND NIPD.INSPECTION_LEVEL='"+level+"'";
                 

                  sf.Begin();
                  dt = sf.GetDataTable(new
                  {
                      query = cmdtext,
                      args = new { }
                  });
              }

              catch (OracleException oe)
              {
                  ExceptionManager.AppendLog(oe);
              }
              catch (Exception ex)
              {
                  ExceptionManager.AppendLog(ex);
              }
              finally
              {
                  if (sf.Transaction != null)
                      sf.End();
              }
            
              if (dt != null && dt.Rows.Count > 0)
              {
                 foreach(DataRow dr in dt.Rows)
                 {
                     objOwnDesign.PillarConstructing        = dr["BASE_CONSTRUCTIONG"].ConvertToString();
                          objOwnDesign.PillerConstructed    = dr["BASE_CONSTRUCTED"].ConvertToString();
                          objOwnDesign.GroundRoofCompleted  = dr["GROUND_ROOF_FINISHED"].ConvertToString();
                          objOwnDesign.GroundFloorCompleted = dr["GROUND_FLOOR_FINISHED"].ConvertToString();

                          objOwnDesign.FloorCount           = dr["STOREY_COUNT"].ConvertToString();

                          objOwnDesign.BaseConstructMaterial = dr["BASE_MATERIAL"].ConvertToString();
                          objOwnDesign.BaseDepthBelowGrnd   = dr["BASE_DEPTH"].ConvertToString();
                          objOwnDesign.BaseAvgWidth         = dr["BASE_WIDTH"].ConvertToString();
                          objOwnDesign.BseHeightAbvGrnd     = dr["BASE_HEIGHT"].ConvertToString();

                          objOwnDesign.gndFloMat            = dr["GROUND_FLOOR_MAT"].ConvertToString();
                          objOwnDesign.GrndFlorPrncpl       = dr["GROUND_FLOOR_PRINCIPAL"].ConvertToString();
                          objOwnDesign.WallStructDescpt     = dr["WALL_DETAIL"].ConvertToString();

                          objOwnDesign.FloorRoofDescrpt     = dr["FLOOR_ROOF_DESC"].ConvertToString();
                          objOwnDesign.FloorRoofMat         = dr["FLOOR_ROOF_MAT"].ConvertToString();
                          objOwnDesign.FloorRoofPrncpl      = dr["FLOOR_ROOF_PRINCIPAL"].ConvertToString();
                          objOwnDesign.FloorRoofDetl        = dr["FLOOR_ROOF_DTL"].ConvertToString();

                          objOwnDesign.FirstFlorMat         = dr["FIRST_FLOOR_MAT"].ConvertToString();
                          objOwnDesign.FirstFLorPrncpl      = dr["FIRST_FLOOR_PRINCIPAL"].ConvertToString();
                          objOwnDesign.FirstFlorDtl         = dr["FIRST_FLOOR_DTL"].ConvertToString();

                          objOwnDesign.Roofmat              = dr["ROOF_MAT"].ConvertToString();
                          objOwnDesign.RoofPrncpl           = dr["ROOF_PRINCIPAL"].ConvertToString();
                          objOwnDesign.RoofDetal            = dr["ROOF_DTL"].ConvertToString();
                          objOwnDesign.ConstructionStatus   = dr["CONSTRUCTION_STATUS"].ConvertToString();
                 }

                          
               }
                      


               


          



              return objOwnDesign;


          }

        //delete inspection 
          public bool deleteInspection(InspectionDesignModelClass objDesign)
          {
              bool result = false;
              string cmdtext        = "";
              QueryResult qrDelete  = null;
              string deleteMst      = "";
              string deleteDesign   = "";
              string deletepaper    = "";
              string deleteOwnDesign= "";
              string deleteDetail   = "";
              string deleteProcess  = "";
              using (ServiceFactory sf = new ServiceFactory())
              {
                  if(objDesign.InspectionLevel=="2" ||objDesign.InspectionLevel=="3" )
                  {


                        deletepaper      = "delete from nhrs_inspection_paper_dtl    where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString() + "'";
                        deleteOwnDesign  = "delete from nhrs_inspection_own_design   where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString() + "'";

                        deleteDetail     = "delete from nhrs_inspection_detail       where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString() + "'";
                        deleteProcess    = "delete from nhrs_inspection_process_dtl  where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString() + "'";

                  }
                  if (objDesign.InspectionLevel == "1" )
                  {
                      deleteMst         = "delete from nhrs_inspection_mst          where NRA_DEFINED_CD='"      + objDesign.NraDefCode.ConvertToString()             + "'";
                      deleteDesign      = "delete from nhrs_inspection_design       where INSPECTION_MST_ID='"   + objDesign.InspectionMstId.ConvertToString()        + "'";
                      deletepaper       = "delete from nhrs_inspection_paper_dtl    where INSPECTION_MST_ID='"   + objDesign.InspectionMstId.ConvertToString()        + "'";
                      deleteOwnDesign   = "delete from nhrs_inspection_own_design   where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString()      + "'";

                      deleteDetail      = "delete from nhrs_inspection_detail       where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString()      + "'";
                      deleteProcess     = "delete from nhrs_inspection_process_dtl  where INSPECTION_PAPER_ID='" + objDesign.InspectionPaperID.ConvertToString()      + "'";
                      cmdtext           = "UPDATE  NHRS_HOUSE_OWNER_MST SET INSPECTION_FLAG   = 'N'   WHERE HOUSE_OWNER_ID='" + objDesign.hOwnerId.ConvertToString()  + "' ";  // while delete from second approval 

                      
                  }
                  
                  try
                  {
                      sf.Begin();
                      if (objDesign.InspectionLevel == "2" || objDesign.InspectionLevel == "3")
                      {


                          qrDelete = sf.SubmitChanges(deleteProcess);
                          qrDelete = sf.SubmitChanges(deleteDetail);
                          qrDelete = sf.SubmitChanges(deleteOwnDesign);
                          qrDelete = sf.SubmitChanges(deletepaper);

                           

                          //string update = "UPDATE  NHRS_PAYROLL_DTL SET INSPECTION_FLAG = 'N' ,INSPECTION_CD='" + null + "' WHERE HOUSE_OWNER_ID='" + objDesign.hOwnerId + "' ";
                          //qrDelete = sf.SubmitChanges(update);

                      }
                      if (objDesign.InspectionLevel == "1")
                      {
                          qrDelete = sf.SubmitChanges(deleteProcess);
                          qrDelete = sf.SubmitChanges(deleteDetail);
                          qrDelete = sf.SubmitChanges(deleteOwnDesign);
                          qrDelete = sf.SubmitChanges(deletepaper);
                          qrDelete = sf.SubmitChanges(deleteDesign);
                          qrDelete = sf.SubmitChanges(deleteMst);
                          QueryResult updateHouseMst = sf.SubmitChanges(cmdtext);
                          //string update = "UPDATE  NHRS_PAYROLL_DTL SET INSPECTION_FLAG = 'N' ,INSPECTION_CD='" + null + "' WHERE HOUSE_OWNER_ID='" + objDesign.hOwnerId + "' ";
                          //qrDelete = sf.SubmitChanges(update);

                      }
                    
                  }
                  catch (OracleException oe)
                  {
                      result = false;
                      sf.RollBack();
                      //exc = oe.Code.ConvertToString();
                      ExceptionManager.AppendLog(oe);
                  }
                  catch (Exception ex)
                  {
                      sf.RollBack();
                      result = false;
                      //exc = ex.ToString();
                      ExceptionManager.AppendLog(ex);
                  }
                  finally
                  {
                      if (sf.Transaction != null)
                      {
                          sf.End();
                      }
                  }
                  if (qrDelete != null)
                  {
                      result = qrDelete.IsSuccess;
                  }

                  return result;
              }
          }

        //getImageName
        public string  getImageNameFromDesign(string designId)
          {
              DataTable dtbl = new DataTable();
              ServiceFactory sf = new ServiceFactory();
              string cmd = "";
              string ImageName = "";


              try
              {
                  cmd = "SELECT IMAGE_NAME from NHRS_HOUSE_MODEL WHERE MODEL_ID = " + designId + " ";
                  sf.Begin();
                  dtbl = sf.GetDataTable(new
                  {
                      query = cmd,
                      args = new { }
                  });
              }

              catch (OracleException oe)
              {
                  ExceptionManager.AppendLog(oe);
              }
              catch (Exception ex)
              {
                  ExceptionManager.AppendLog(ex);
              }
              finally
              {
                  if (sf.Transaction != null)
                      sf.End();
              }
            if(dtbl!=null && dtbl.Rows.Count>0)
            {
                ImageName = dtbl.Rows[0][0].ConvertToString();
            }
            return ImageName;

          }

        // get if comply exist
        public string getIfComplyExist(string designId)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            string complyExist = "";


            try
            {
                cmd = "SELECT INSPECTION_DESIGN from NHRS_INSPECTION_COMPLY WHERE INSPECTION_DESIGN = " + designId + " ";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                complyExist = dtbl.Rows[0][0].ConvertToString();
            }
            return complyExist;

        }

        //get design number from roof and construction material
        public string getDesignByMaterialID(string constructMatID, string roofMatID)
        {
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";
            string DesignId = "";


            try
            {
                cmd = "";
                if (constructMatID != "")
                {
                    cmd = "SELECT HOUSE_MODEL_CD from NHRS_INSPECTION_MDL_MAPPING WHERE CONSTRUCTION_MAT_CD = '" + constructMatID + "' " + "AND ROOF_MAT_CD='" + roofMatID + "'";
                }
                //if (roofMatID != "")
                //{
                //    cmd = "SELECT HOUSE_MODEL_CD from NHRS_INSPECTION_MDL_MAPPING WHERE ROOF_MAT_CD='" + roofMatID + "'";
                //}
                //if (roofMatID != "" && constructMatID != "")
                //{
                //    cmd = "SELECT HOUSE_MODEL_CD from NHRS_INSPECTION_MDL_MAPPING WHERE ROOF_MAT_CD='" + roofMatID + "'AND CONSTRUCTION_MAT_CD = '" + constructMatID + "'";
                //}
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            if (dtbl != null && dtbl.Rows.Count > 0)
            {
                DesignId = dtbl.Rows[0][0].ConvertToString();
            }
            return DesignId;

        }

        //save error message 
        public bool SaveErrorMessgae(string errorMsg)
        {
            bool res = false;
            QueryResult qr = null;
            using (ServiceFactory service = new ServiceFactory())
            {

                try
                {
                    service.Begin();
                    service.SubmitChanges(errorMsg, null);

                }
                catch (OracleException oe)
                {

                    ExceptionManager.AppendLog(oe);
                }
                catch (Exception ex)
                {

                    ExceptionManager.AppendLog(ex);
                }
                finally
                {
                    if (service.Transaction != null)
                    {
                        service.End();
                    }
                }
            }
            if (qr != null && qr.IsSuccess)
            {
                res = qr.IsSuccess;
            }
            return res;

        }


        //get sserial number
        public bool getSerialNumber( string paNum,string serialNo)
        {
           bool result= false;
            DataTable dtbl = new DataTable();
            ServiceFactory sf = new ServiceFactory();
            string cmd = "";

             try{
                cmd = "SELECT SERIAL_NUMBER, NRA_DEFINED_CD FROM NHRS_INSPECTION_PAPER_DTL WHERE SERIAL_NUMBER='"+serialNo+"' AND NRA_DEFINED_CD='"+paNum+"'";
                sf.Begin();
                dtbl = sf.GetDataTable(new
                {
                    query = cmd,
                    args = new { }
                });
            }

            catch (OracleException oe)
            {
                ExceptionManager.AppendLog(oe);
            }
            catch (Exception ex)
            {
                ExceptionManager.AppendLog(ex);
            }
            finally
            {
                if (sf.Transaction != null)
                    sf.End();
            }
            if(dtbl!=null && dtbl.Rows.Count>0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }


    }

    public class HierarchicalInspectionTree
    {
        public InspectionItem lastCurrentItem = null;
        int LastLevel;
        /// <summary>
        /// constructor that create Hierarchy Inspection
        /// </summary>
        /// <param name="InspectionItems">Inspection items</param>
        /// <param name="InspectionUrl">Url</param>
        /// <returns>Inspection item</returns>
        public InspectionItem CreateHierarchy(List<InspectionItem> InspectionItems, string UpperCode)
        {
            // var parentItems = InspectionItems.Where(p => p.UPPER_INSPECTION_CODE_ID == null).OrderBy(k => k.InspectionOrder).ToList();
            var parentItems = InspectionItems.Where(p => p.UPPER_INSPECTION_CODE_ID == UpperCode).ToList();
            var root = new InspectionItem();
            foreach (var InspectionItem in parentItems)
            {
                InspectionItem.Level = 1;
                InspectionItem.Parent = root;
                LastLevel = 1;
                BuildTree(InspectionItem, p => p.Children = InspectionItems.Where(child => p.INSPECTION_CODE_ID == child.UPPER_INSPECTION_CODE_ID).ToList(), 2);

                InspectionItem.Parent.LastLevel = InspectionItem.Level;
            }
            root.Children = parentItems;
            root.LastLevel = LastLevel;
            return root;
        }

        /// <summary>
        /// It build tree structure Inspection as per parent
        /// </summary>
        /// <param name="parent">perent Inspection</param>
        /// <param name="InspectionUrl">URL</param>
        /// <param name="func">class</param>
        /// <param name="level">Inspection level</param>
        void BuildTree(InspectionItem parent, Func<InspectionItem, List<InspectionItem>> func, int level)
        {

            foreach (var child in func(parent))
            {

                child.Level = level;
                child.Parent = parent;
                BuildTree(child, func, level + 1);

            }


        }





    }




}
